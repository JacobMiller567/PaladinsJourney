using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.tag == "Block")
        {
      //      Transform block = collide.GetComponent<Transform>();

      //      if (Vector3.Distance(block.position, transform.position) < 0.5f)
   //         {
               Debug.Log("Activate");
               // block.GetComponent<Rigidbody>().isKinematic = true;
               collide.GetComponent<Rigidbody>().isKinematic = true;
      //          block.GetComponent<Renderer>().material.color = Color.green;
       //         Destroy(block.gameObject, 2f);
   //         }
        }
    }
}
