using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform player;
 
    [SerializeField] private float spawnDelay;
    //private List<enemyList>
    public GameObject[] spawnPoints;
    public BossDoor door;
    public bool unlockDoor;

    private bool completeWave;
    private bool canSpawn;
    private int randomSpawn = 0;
    private float detectionRange = 80f;

    public float timeRemaining = 60;
    public bool timerIsRunning;
    public Text timeText;

   private void Start()
    {
       
        completeWave = false;
        canSpawn = true;
        unlockDoor = true;
        timerIsRunning = false;
    }

   private void Update()
    {
        if ((transform.position - player.position).magnitude <= detectionRange)
        {
            
            StartCoroutine(WaveComplete());
            timerIsRunning = true;
            if (canSpawn && !completeWave)
            {
                StartCoroutine(SpawnEnemy());
            }
        }

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    private IEnumerator SpawnEnemy()
    {
        canSpawn = false;
        randomSpawn = Random.Range(0,3);
        GameObject enemyPosition = spawnPoints[randomSpawn];
        if (enemyPosition != null) { // stops null error
            GameObject newEnemy = Instantiate(enemy, enemyPosition.transform.position, Quaternion.identity);
        }
        //GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(5, 10));
        canSpawn = true;
    }

    private IEnumerator WaveComplete()
    {
        yield return new WaitForSeconds(60f); // survive for 1 minute
        Debug.Log("------- WAVE COMPLETE -------");
        completeWave = true;
        door.UnlockDoor(unlockDoor); 
        gameObject.SetActive(false);
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
