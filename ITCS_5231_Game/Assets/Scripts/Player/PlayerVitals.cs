using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVitals : MonoBehaviour, Damageable
{
 [SerializeField] public float playerHealth;
  public HealthBar healthBar;
  public float maxHealth;
  private Animator animate;
  private AudioSource playerHurt;
//  Vector3 respawn;
//  Vector3 holdLocation;

  //public Checkpoints respawnPoint;

     void Start()
    {
        maxHealth = playerHealth;
        animate = GetComponentInChildren<Animator>(); // Get Paladins animator controller
        playerHurt = GetComponent<AudioSource>();
    //    holdLocation = transform.position;

    }



    public void Damage(float damage) // damage taken
    {
      playerHealth -= damage;

      if (damage != 0) 
      {
        playerHurt.Play();
      }
    }


    void Update()
    {
        healthBar.DisplayHealth(playerHealth);


        if (playerHealth <= 0)
        {
          //Vector3 position
         // Debug.Log("PLAYER IS DEAD");
          animate.SetBool("isDead", true);
          // Go to Game Over screen
          StartCoroutine(GameOver());
          /*
          if (transform.position.y > 1f)
          {
            transform.position.y -= 0.2f;

          }
          */
        }   
/*
      if (transform.position.y < -20f) // if player falls off the map
      {
  //      Debug.Log("Fell off the map!");
  //      respawn = new Vector3(0f, 10f, 0f);
        playerHealth -= 20f; // lose 20hp each time player falls
        yield return new WaitForSeconds(1.5f);
*/
      }

    IEnumerator GameOver() // wait a few seconds before going to gameover screen
    {
      yield return new WaitForSeconds(5f);
      Cursor.lockState = CursorLockMode.None; // Test?
      SceneManager.LoadScene("GameOver");
    }
  

}
