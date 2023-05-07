using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private bool level_complete;
    private float distance = 1.5f;
    private SphereCollider doorCollider;
    private AudioSource sound;
    // Start is called before the first frame update
   private void Start()
    {
        level_complete = false;
        doorCollider = GetComponent<SphereCollider>();
        doorCollider.isTrigger = true;
        doorCollider.radius = distance;
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
  {
    if (other.tag.Equals("Player")) 
    {
        level_complete = true;

        if (level_complete == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // move to next level
            sound.Play();
            level_complete = false;
            Destroy(this.gameObject); 
        }
    }
  }


}
