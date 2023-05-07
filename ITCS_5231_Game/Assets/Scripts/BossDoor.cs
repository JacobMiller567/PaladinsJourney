using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public bool unlockDoor;
   // private AudioSource bossMusic;
    void Start()
    {
        unlockDoor = false;
       // bossMusic = GetComponent<AudioSource>();
        
    }
    void Update()
    {
        
        
    }

    public void UnlockDoor(bool unlock)
    {

        if (unlock == true)
        {
        //    bossMusic.Play();
            gameObject.SetActive(false);
        }

    }
}
