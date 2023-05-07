using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour, Damageable
{
    [SerializeField] private float projectileDamage;

    private void Start()
    {
     //   StartCoroutine(MissedShot());
    //  ParticleSystem system = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        StartCoroutine(MissedShot());
    }

    public void Damage(float damage){}

    void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.tag == "Player")
      {
       // ps.Play();
        Damageable damagable = collision.gameObject.transform.GetComponent<Damageable>();
        damagable?.Damage(projectileDamage);

        Destroy(gameObject);  
      }
    }


    IEnumerator MissedShot() // if projectile does not collide with player
    {
         yield return new WaitForSeconds(3f); // after 3 seconds of being shot
         Destroy(gameObject); // destroy the game object
    }
}
