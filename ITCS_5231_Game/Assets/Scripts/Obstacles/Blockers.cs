using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockers : MonoBehaviour
{
  //  public Transform[] blockers;
    public GameObject[] blockers;
    private int randomSpawn = 0;
    private bool nextRandom;
    private int spawnAmount = 0;
    // Start is called before the first frame update
    private void Start()
    {
        nextRandom = true;
    }

    // Update is called once per frame
    public void Update()
    {
       // StartCoroutine(NextBlocker());


       // Transform check_point = checkpoint[currentCheckpoint];

        // Overview: Spawn 7 random blockers out of fixed amount
            // After certain amount of time switch blocker to next random 7
            // Repeat process till player makes it to next level

        if (nextRandom == true) // if blockers can spawn
        {
            if (spawnAmount <= 6) // if blocker amount is <= 7
            {
                randomSpawn = Random.Range(0,25); // choose a random blocker

            //    Transform _blockers = blockers[randomSpawn];
                GameObject _blockers = blockers[randomSpawn];
                spawnAmount += 1; // increase spawn amount by 1
                Debug.Log("Spawn: " + randomSpawn);
                blockers[randomSpawn].gameObject.SetActive(true);
            }
            else 
            {
               StartCoroutine(NextBlocker());
            }
        }
        
    }


    IEnumerator NextBlocker()
    {
        nextRandom = false; // stop spawning
        yield return new WaitForSeconds(10f); // wait 10 seconds for next random spawn
        // Deactivate all blockers
        for (int i = 0; i < 25; i++)
        {
            blockers[i].gameObject.SetActive(false);
        }
        nextRandom = true; // allow spawning
        spawnAmount = 0; // reset spawn amount
    }
}
