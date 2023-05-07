using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWall : MonoBehaviour
{
  [SerializeField] private Transform itemToRotate; // item that will rotate
  [SerializeField] private Transform playerLocation; // player 
  [SerializeField] private Rigidbody playerBody; // player rigidbody
  [SerializeField] private Rigidbody itemRotatingBody; // item to rotates rigidbody
  [SerializeField] private float spinRange; // range needed for wall to spin
  [SerializeField] private float spinSpeed; // speed of object rotation
  [SerializeField] private float pushForce; // force the wall passes onto player

    private void Start()
    {

    }

    private void Update()
    {
      if ((itemToRotate.position - playerLocation.position).magnitude <= spinRange) // if player is within range then spin the obstacle
      {
        itemToRotate.transform.Rotate(0, Time.deltaTime * spinSpeed, 0); // spins the obstacle
        // ADD: if wall collides with player
        // push force on player in direction it was hit
        // causing player to get launched in certain direction
      }
      else 
      {
        itemRotatingBody.isKinematic = false; 
      }

    }

    void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.tag == "Player")
      {
        itemRotatingBody.isKinematic = true; 
        Rigidbody rb = collision.collider.attachedRigidbody;

      //  if (rb != null && !rb.isKinematic)
        if (rb != null)
        {
          Debug.Log("Wall Collision: " + collision.GetContact(0));
       //   rb.isKinematic = true; 
         // rb.velocity = collision.GetContact(0) * pushForce;
        //  rb.AddForceAtPosition()
      //    rb.AddForce(collision.GetContact(0).normal);
        Vector3 direction = rb.transform.position - transform.position;
        rb.AddForceAtPosition(direction.normalized, transform.position);
        }
      }
        
    }



}
