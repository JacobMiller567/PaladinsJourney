using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{
  //  public GameObject[] spawnLoc;
    [SerializeField] private float spawnSpeed;
    [SerializeField] private bool randomLocation;
    [SerializeField] private Transform playerLocation; // player 
    [SerializeField] private float range;

    public GameObject ball;
    public bool spawn;
    private int randomSpawn = 0;
    private bool inRange;
    private bool forceStop;
  //  Vector3 spawnLocation;

    void Start()
    {
       spawn = true;
       forceStop = false;
    }

    void Update()
    {
      if (forceStop != true) 
      {
        if ((transform.position - playerLocation.position).magnitude <= range)
        {
            inRange = true;

            if ((transform.position - playerLocation.position).magnitude <= 15f)
            {
              forceStop = true;
              spawn = false;
            //  Debug.Log("Stopped Spawning");

            }
        }
        else 
        {
          inRange = false;
        }

          if (spawn && (ball != null) && inRange) 
          {

            if (randomLocation) // if you want objects to spawn differently on x position
            {
              randomSpawn = Random.Range(-2,2);

              GameObject clone;
              clone = Instantiate(ball, new Vector3(transform.position.x, transform.position.y+1, transform.position.z + randomSpawn), transform.rotation) as GameObject;
          //  clone = Instantiate(ball, new Vector3(spawnLocation.position.x, spawnLocation.position.y+1, spawnLocation.position.z), spawnLocation.rotation) as GameObject;
              StartCoroutine(SpawnObject());
            }

            else 
            {
              GameObject clone;
              clone = Instantiate(ball, new Vector3(transform.position.x, transform.position.y+1, transform.position.z), transform.rotation) as GameObject;
          //  clone = Instantiate(ball, new Vector3(spawnLocation.position.x, spawnLocation.position.y+1, spawnLocation.position.z), spawnLocation.rotation) as GameObject;
              StartCoroutine(SpawnObject());
            }
  /*
            GameObject clone;
            clone = Instantiate(ball, new Vector3(transform.position.x, transform.position.y+1, transform.position.z), transform.rotation) as GameObject;
        //  clone = Instantiate(ball, new Vector3(spawnLocation.position.x, spawnLocation.position.y+1, spawnLocation.position.z), spawnLocation.rotation) as GameObject;
            StartCoroutine(SpawnObject());
  */
        }
      }
    }

     IEnumerator SpawnObject()
    {
      spawn = false;
      yield return new WaitForSeconds(spawnSpeed); //8f
//      objectThrown = false;
      spawn = true;
    }

}
