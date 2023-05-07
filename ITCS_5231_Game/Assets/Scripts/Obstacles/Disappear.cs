using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
  [SerializeField] private Transform ground;
  //[SerializeField] private Transform player; // player location
  [SerializeField] private Transform groundCheck;
  [SerializeField] private float disappearTime;

    private void Start()
    {

    }

    private void Update()
    {
      if ((groundCheck.position - ground.position).magnitude <= 1f) // if player is standing on platform
      {
        StartCoroutine(HidePlatform());
        }
    }

    IEnumerator HidePlatform()
    {
      yield return new WaitForSeconds(disappearTime); // wait certain amount of time
      Destroy(this.gameObject); // destroy platform
    }
}
