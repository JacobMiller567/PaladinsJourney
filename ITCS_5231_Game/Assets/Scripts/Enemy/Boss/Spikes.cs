using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour, Damageable
{

    private float spikeDamage = 8f;
    private bool canDamage = true;

    public void Damage(float damage) { }
  
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
}
