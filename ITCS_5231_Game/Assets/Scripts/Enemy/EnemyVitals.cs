using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyVitals : MonoBehaviour, Damageable
{
  [SerializeField] private float enemyHealth; 
  private float maxHealth;
  public Slider healthBar; 
  private Animator animate;
  private AudioSource enemyDead;
 // public Canvas canvas;
  //[SerializeField] private HealthBar healthBar;
  //[SerializeField] private Canvas healthBarCanvas;
  //[SerializeField] private Camera cameras;

  private void Start()
    {
      animate = GetComponent<Animator>();
      maxHealth = enemyHealth;
      healthBar.maxValue = maxHealth;
      enemyDead = GetComponent<AudioSource>();
    }

  private void Update()
  {
      healthBar.value = enemyHealth;   
  }

  public void Damage(float damage) // damage enemy
  {
    enemyHealth -= damage;
    animate.SetTrigger("EnemyHit"); // trigger enemy hit

    if (enemyHealth <= 0)
    {
   //   healthBar.enabled = false; // TEST???
      animate.SetTrigger("Dead"); // trigger death animation
      animate.SetBool("isDead", true);
      //animate.SetInt("isDead", 1);
      StartCoroutine(DestroyEnemy());
    }
  }

  IEnumerator DestroyEnemy()
  {
    yield return new WaitForSeconds(0.2f);
    enemyDead.Play();
    yield return new WaitForSeconds(1f);
    animate.SetBool("isDead", false);
    Destroy(gameObject); // Destroy enemy
  }

}
