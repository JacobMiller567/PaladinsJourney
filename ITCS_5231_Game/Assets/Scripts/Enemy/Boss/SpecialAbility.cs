using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbility : MonoBehaviour, Damageable
{
    public GameObject[] spikes;
    private int randomSpawn = 0;
    private bool nextRandom;
    private int spawnAmount = 0;
    private BossVitals bossHP;
    [SerializeField] private AudioSource spikeAppear;
   // private float spikeDamage = 8f;
   // private bool canDamage;

    private void Start()
    {
        bossHP = GetComponent<BossVitals>();
        nextRandom = true;
        //spikeAppear = GetComponent<AudioSource>();
       // canDamage = true;
    }

    public void Damage(float damage) {}


    public void Update()
    {
        if (bossHP.CheckHealth() <= 75f) // Boss is at 1/2 hp
        {

            // Overview: Spawn spikes out of fixed amount
                // After certain amount of time switch spikes to next random
                // Repeat process till boss is dead

            if (nextRandom == true) 
            {
                if (spawnAmount <= 15) 
                {
                    randomSpawn = Random.Range(0,30); // choose a random spike from array

                    GameObject _spikes = spikes[randomSpawn];
                    spawnAmount += 1; // increase spawn amount by 1
                    spikes[randomSpawn].gameObject.SetActive(true);
                    spikeAppear.Play();
                }
                else 
                {
                StartCoroutine(NextSpike());
                }
            } 
        }
    }

    IEnumerator NextSpike()
    {
        nextRandom = false; // stop spawning
        yield return new WaitForSeconds(3f); // wait 3 seconds for next random spawn
        // Deactivate all blockers
        for (int i = 0; i < 30; i++)
        {
            spikes[i].gameObject.SetActive(false);
        }
        nextRandom = true; // allow spawning
        spawnAmount = 0; // reset spawn amount
    }

/*
    void OnTriggerEnter(Collider other) 
    {
        if ((other.gameObject.tag == "Player") && canDamage == true) 
        {
            Damageable damagable = other.gameObject.transform.GetComponent<Damageable>();
            damagable?.Damage(spikeDamage);
            canDamage = false;
            StartCoroutine(SpikeDamage());
        }
    } 


    IEnumerator SpikeDamage()
    {
        yield return new WaitForSeconds(1f);
        canDamage = true;

    }
    */

}

