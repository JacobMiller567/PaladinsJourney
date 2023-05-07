using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour  ////  FIXXX: Remove character controller or rigidbody!!!!  ////
{

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask Ground;
    [SerializeField] public Transform playerTransform;
  //  [SerializeField] public Animator animate; 
    private Animator animate;
    [SerializeField] private float rotateSpeed; //player rotation speed
    private CharacterController controller;
    [SerializeField] private float pushForce;
    [SerializeField] private float speed;
    private Rigidbody rb;

  
    //public PlayerAttack playerAttack;
    private Vector3 velocity;
    //private float speed = 6f;
    private float gravity = -9.81f;  // default gravity
  //  private float groundDistance = 0.51f; // 
    private float jumpHeight = 5f;
    private float sprint = 9f;
    private float holdSpeed = 6f;
    //private float forceMultiplier = 20f; // TEST??


    private bool isRunning;
    private bool onGround;
    private bool doubleJump;
    private bool hasThrown;
    private bool flipping;
    private bool canAttack;
    private bool jumpCooldown;
    //private bool hasStamina = true;

    // Used for player animation
    //private const string isMoving = "isMoving";
    private const string isJump = "isJump";
    private const string isFalling = "isFalling";
    private const string canDouble = "canDouble";
    private const string isThrowing = "isThrowing";
    private const string isAttacking = "isAttacking";


    private void Start()
    {
        animate = GetComponentInChildren<Animator>(); // Get Paladins animator controller
        controller = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // locks cursor in place
        canAttack = true;

      //  rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
      if (animate.GetBool("isDead") != true)
      {
      //  speed = 0f;
      //  sprint = 0f;
      //  jumpHeight = 0f;
      //  holdSpeed = 0f;
      

       onGround = controller.isGrounded;
       if (onGround && velocity.y < 0)
        {
        //    velocity.y = 0f;    // Stops player from being able to jump while moving on a downwards slope
            animate.SetBool(isFalling, false); // TEST?
            animate.SetTrigger("OnGround");
        }

       float horizontal = Input.GetAxis("Horizontal");
       float vertical = Input.GetAxis("Vertical");


        Vector3 movePlayer = (playerTransform.right * horizontal + playerTransform.forward * vertical).normalized;
       // Vector3 movePlayer = (playerTransform.forward * vertical).normalized;
       // Vector3 turnPlayer = (playerTransform.right * horizontal).normalized;

        //controller.Move(movePlayer * speed * Time.deltaTime);
        if (movePlayer != Vector3.zero)
        {
          controller.Move(movePlayer * speed * Time.deltaTime);
 //         animate.SetBool(isMoving, true);    
          animate.SetFloat("BlendSpeed", 0.5f, 0.1f, Time.deltaTime); // Walk Animation

          Quaternion toRotate = Quaternion.LookRotation(movePlayer, Vector3.up );//, Vector3.up);
       
          transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotateSpeed * Time.deltaTime);
          //transform.rotation = Quaternion.Lerp(transform.rotation, toRotate, rotateSpeed * Time.deltaTime);

        }
        else
        {
          animate.SetFloat("BlendSpeed", 0f, 0.1f, Time.deltaTime); // Idle Animation
 //         animate.SetBool(isMoving, false);
        }


        if (Input.GetKey(KeyCode.LeftShift)) // While player holds shift player can sprint
        {
            if(!isRunning) // if player is not already running
            {
                speed = sprint; // set speed to sprint value
                isRunning = true;
                // StartCoroutine(Stamina());
                //animate.SetFloat("BlendSpeed", 1f, 0.1f, Time.deltaTime); // Sprinting
            }

            if (isRunning)
            {
              animate.SetFloat("BlendSpeed", 1f, 0.1f, Time.deltaTime); // Sprinting
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) // When player releases shift
        {
            speed = holdSpeed; // set speed back to normal
            isRunning = false;
            // StartCoroutine(Stamina());

        }

        if (onGround && jumpCooldown == false) // player is on the ground
        {
          if (Input.GetKeyDown(KeyCode.Space)) /// FIX: Allow player to jump if they walk off platform ///
          {
            animate.SetTrigger("Jump");
//            animate.SetFloat("BlendJump", 0f, 0.1f, Time.deltaTime); // Jump Animation
            //animate.SetTrigger("isJump");
            animate.SetBool(isJump, true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // player jump
            animate.SetBool(isFalling, true);
            doubleJump = true;
            jumpCooldown = true;
            StartCoroutine(JumpCoolDown());
          }
        }

        // For Downward slope: if groundCheck is < .5f from ground and haven't recently jumped


      else // player is in the air
       {
         if (Input.GetKeyDown(KeyCode.Space))
         {
          if (doubleJump) // player can double jump
          {
          //   animate.SetTrigger("canDouble");
//            animate.SetFloat("BlendJump", 0.66f, 0.1f, Time.deltaTime); // Flip Animation
            animate.SetBool(canDouble, true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // player jump
            doubleJump = false;
          //  jumpCooldown = true;
           // secondJump = true;
            StartCoroutine(FallingIdle());
          }
          else
          {
            animate.SetBool(canDouble, false);
            animate.SetBool(isJump, false);
          }
        }
      }

  //  if (Input.GetKeyUp(KeyCode.Space) && !doubleJump)
    if (flipping)
      {
        animate.SetBool(canDouble, false);
        flipping = false;
      }
      


      if (Input.GetMouseButtonDown(0) && hasThrown)
      {
           animate.SetBool(isThrowing, true);
           StartCoroutine(Refresh());
           //animate.SetBool(isThrowing, false);
      }


/*
        if (!onGround && velocity.y < 0)
        {
          animate.SetBool(isFalling, true);
        }
  */

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
      }

      else // Player is dead
      {
        velocity.y += -3f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
      }

    }


    IEnumerator Refresh()
    {
      hasThrown = false;
      animate.SetBool(isThrowing, false);
      yield return new WaitForSeconds(1.5f);
      //animate.SetBool(isThrowing, false);
      hasThrown = true;
    }

    IEnumerator FallingIdle()
    {
      yield return new WaitForSeconds(.25f);
      flipping = true;
    }


    // REMOVE??
    private void OnControllerColliderHit(ControllerColliderHit hit)  // Allow player to push blocks
    {

      if (hit.gameObject.tag == "Block")
      {
        Rigidbody rb = hit.collider.attachedRigidbody;

        if (rb != null && !rb.isKinematic)
        {
          rb.velocity = hit.moveDirection * pushForce;
        }
      }

      if (hit.gameObject.tag == "Ball")
      {
      //  Rigidbody rb = hit.collider.attachedRigidbody;
      //hit.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * ForceMultiplier);
      //  rb.AddForce(Vector3.forward * forceMultiplier);
        Debug.Log("Slam!");
      //  rb = GetComponent<Rigidbody>();
      //  rb.AddForce(0, 0, pushForce, ForceMode.Impulse);
      }
    }


/*
public void SwordHit()
{
  Debug.Log("Hit??");
  print("HIT");
}
*/



      IEnumerator JumpCoolDown()
    {
     //   jumpCooldown = true;
        yield return new WaitForSeconds(3f);  // Remove once blend tree implemented?!
        jumpCooldown = false;
    }

}
