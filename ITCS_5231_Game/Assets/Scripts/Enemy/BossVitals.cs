using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossVitals : MonoBehaviour, Damageable
{
  [SerializeField] private float enemyHealth; 
  private float maxHealth;
  public Slider healthBar; 
  private Animator animate;
  [SerializeField] private AudioSource bossHurt;
  [SerializeField] private AudioSource bossDead;
  private bool playDeath = true;
 // public Canvas canvas;
  //[SerializeField] private HealthBar healthBar;
  //[SerializeField] private Canvas healthBarCanvas;
  //[SerializeField] private Camera cameras;

  private void Start()
    {
      animate = GetComponent<Animator>();
      maxHealth = enemyHealth;
      healthBar.maxValue = maxHealth;
    //  bossHurt = GetComponent<AudioSource>();
    }

  private void Update()
  {
      healthBar.value = enemyHealth;   
  }

  public void Damage(float damage) // damage enemy
  {
    float randomNumber = Random.Range(0, 5);

    if (randomNumber > 3) // 33% chance to block
    {
        Debug.Log("---BLOCKED---");
        //animate.SetTrigger("EnemyBlock");
       // damage = 0; // set damage to 0
        return;
    }
   // animate.ResetTrigger("EnemyBlock");
    enemyHealth -= damage;
    animate.SetTrigger("EnemyHit"); // trigger enemy hit
    bossHurt.Play();


    if (enemyHealth <= 0)
    {
   //   healthBar.enabled = false; // TEST???
      animate.SetTrigger("Dead"); // trigger death animation
      animate.SetBool("isDead", true);
      //animate.SetInt("isDead", 1);
      StartCoroutine(DestroyEnemy());
    }
  }

  public float CheckHealth() // check boss health
  {
    return enemyHealth;
  }

  IEnumerator DestroyEnemy()
  {
    if (playDeath) 
    {
      bossDead.Play();
      playDeath = false;
    }
    yield return new WaitForSeconds(3.5f);
    animate.SetBool("isDead", false);
    Destroy(gameObject); // Destroy enemy

    Cursor.lockState = CursorLockMode.None; // Test?
    SceneManager.LoadScene("WinGame");


  }

}
