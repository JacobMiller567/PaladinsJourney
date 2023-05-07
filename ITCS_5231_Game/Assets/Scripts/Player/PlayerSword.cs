using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour, Damageable
{
    // Either damage enemy when sword collides with them
    // Or damage enemy if within attack range
    [SerializeField] private Transform swordBlade;
    [SerializeField] private Transform swordBase;
    [SerializeField] private Animator animate;
    [SerializeField] private float attackDamage;
    private AudioSource swordSlash;
    private bool canAttack;
    private bool canDamage;
  //  public Animator animate;
    public const string isAttacking = "isAttacking";
    private float attackRange = 1.5f;
  //  [SerializeField] private float attackDamage;

    private void Start()
    {
        canAttack = true;
        canDamage = false;
        swordSlash = GetComponent<AudioSource>();

     //   animate = GetComponent<Animator>();     
    }

    private void Update()
    {
        if (animate.GetBool("isDead") == true)
        {
            canAttack = false;
        }
        
        if (Input.GetMouseButtonDown(0) && canAttack) // if player right clicks the mouse
        {
            PlayerAttack();
        }
    }
    public void Damage(float damage) {}


/*

    public void SwordHit() // Check collision from sword in animation event
    {
        Debug.Log("Hit??");
        print("HIT");
    }
*/

    

    private void PlayerAttack()
    {
        animate.SetLayerWeight(animate.GetLayerIndex("Attack Layer"), 1);
        animate.SetTrigger("Slash"); // play attack animation
        //swordSlash.Play();
        canDamage = true;

        StartCoroutine(SwingCoolDown());
        //canAttack = false; // just attacked so set canAttack to false
        
        /*
        if (Physics.Linecast(swordBase.position, swordBlade.position, out RaycastHit hit)) // line between base of sword and tip of sword
        {
            Debug.DrawLine(swordBase.position, swordBlade.position, Color.red);
        }

        */
        /*
        if (canAttack)
        {
            //Output the name of the Collider your Box hit
            Debug.Log("Hit : " + hitInfo.collider.name);
        }
        */
    }
                // Vector3 endPosition
            // Debug.DrawRay(swordLocation.position, swordLocation.TransformDirection(Vector3.forward) * hitInfo.distance, Color.yellow);
        /*    if (hit.collider.CompareTag("Enemy") && canDamage) // if sword collides with an enemy
            {
                Damageable damagable = hit.transform.GetComponent<Damageable>();
                damagable?.Damage(attackDamage); // Damage function to damage the enemy
                Debug.Log("Sword Collision");

                StartCoroutine(CanDamage());
            }
            
                //break;

            //StartCoroutine(SwingCoolDown());
            Debug.Log("How many times does this print?");
        }

     //   StartCoroutine(SwingCoolDown());
}
    */


    IEnumerator SwingCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.15f);
        swordSlash.Play();
        //canDamage = false
        yield return new WaitForSeconds(1.25f);
        animate.SetLayerWeight(animate.GetLayerIndex("Attack Layer"), 0);
        animate.SetBool(isAttacking, false);
        canAttack = true;
        animate.ResetTrigger("Slash"); 
    }

/*
    IEnumerator CanDamage()
    {
        canDamage = false;
        yield return new WaitForSeconds(.75f);
      //  animate.SetBool(isAttacking, false);
        //canDamage = true;
    }
*/


    void OnTriggerEnter(Collider other) 
    {
        if ((other.gameObject.tag == "Enemy") && canDamage == true) 
        {
            Damageable damagable = other.gameObject.transform.GetComponent<Damageable>();
            damagable?.Damage(attackDamage);
           // Debug.Log("BANG!!!!");
            canDamage = false;
           // StartCoroutine(CanDamage());
        }
    }  

/*

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Within Radius");
        }
    }
*/

}
