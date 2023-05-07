using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour, Damageable
{
  public Transform[] checkpoint;
  private int currentCheckpoint = 0;
  public ProgressBar progressbar;
  private bool proceed = false;
  private bool canDamage = true;
  Vector3 holdLocation;
  Vector3 respawn;
 // Vector3 getPosition;
//  [SerializeField] private Transform end;

  public void Start()
  {
  //  holdLocation = transform.position; // get player starting location
  }

  public void Damage(float damage){}

  public void Update()
  {
      Transform check_point = checkpoint[currentCheckpoint];
      progressbar.DisplayProgress(currentCheckpoint); // display checkpoint progress
        if (Vector3.Distance(transform.position, check_point.position) < 8f) // if player is within 8f of waypoint
        {
            Debug.Log("Checkpoint " + currentCheckpoint);
            holdLocation = check_point.position; // set location to waypoint location
            currentCheckpoint = (currentCheckpoint + 1) % checkpoint.Length; // Change to next checkpoint
        }

        if (transform.position.y < -20.0f) // if player falls off the map
        {
          canDamage = false;
          Damageable damagable = gameObject.transform.GetComponent<Damageable>();
          damagable?.Damage(1f);
          Debug.Log("Fell off the map!");
          respawn = new Vector3(0f, 16f, 0f);
          transform.position = (holdLocation + respawn);  // spawn player back on surface at checkpoint
         // StartCoroutine(WaitTime());
         // transform.position = check_point.position + respawn; // spawn player on checkpoint
        }

    } 

    /*

    IEnumerator WaitTime()
    {
      yield return new WaitForSeconds(0.5f);
      canDamage = true;
    }
    */
}
