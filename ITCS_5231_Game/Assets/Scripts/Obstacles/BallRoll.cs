using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRoll : MonoBehaviour, Damageable
{
 // [SerializeField] private Transform playerLocation; // player 
 // [SerializeField] private Rigidbody playerBody; // player rigidbody
  [SerializeField] private Rigidbody rigidbody; 
  //[SerializeField] private float range; 
  [SerializeField] private float pushForce; 
  [SerializeField] private float rollDamage;
  [SerializeField] private float despawnTime;

  private bool canDamage;
  private bool kinematic = true;
  [SerializeField] private float forceMultiply;

    private void Start()
    { 
        canDamage = true;
     //   rigidbody.AddRelativeForce(Vector3.forward * forceMultiply); // test??
        rigidbody.AddForce(Vector3.forward * forceMultiply); // test??
    }
   

    private void Update()
    {
      StartCoroutine(Missed());
      /*
      if ((rigidbody.position - playerLocation.position).magnitude <= range) // if player is within range of the obstacle
      {
          rigidbody.useGravity = true;
          rigidbody.isKinematic = false;
      }
      */

      if (transform.position.y < -20f)
      {
        Destroy(gameObject);
      }
    }


    public void Damage(float damage){}
    

     void OnCollisionEnter(Collision collision)
    {
      if ((collision.gameObject.tag == "Player") && canDamage)
      {
          Damageable damagable = collision.gameObject.transform.GetComponent<Damageable>();
        //  collision.gameObject.transform.localScale.y = 0.1; // Player gets squished
          damagable?.Damage(rollDamage);
          StartCoroutine(Despawn());
      }
    }


          /*
        Rigidbody rb = collision.collider.attachedRigidbody;
       
        //rb.AddForce(rb.transform.up * pushForce);
        if (rb!= null) 
        {
       // rb.velocity = collision.moveDirection * pushForce;
            rb.addForce(playerLocation.forward * pushingForce);
        }
      }
    }
    */


    IEnumerator Despawn()
    {
        canDamage = false;
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }

    IEnumerator Missed() // if ball does not collide with player
    {
       yield return new WaitForSeconds(despawnTime); // after given time if ball doesn't hit player or fall off the map
       Destroy(gameObject); // destroy the game object
    }



}
