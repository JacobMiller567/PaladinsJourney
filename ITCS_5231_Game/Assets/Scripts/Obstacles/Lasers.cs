using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour, Damageable
{
    private float laser_damage;
    private bool canDamage;

    private void Start()
    {
        laser_damage = 10f;
        canDamage = true;
    }

    public void Damage(float damage)
    {

    }


    private void OnTriggerEnter(Collider other) // maybe change to always fireing event on collision
    {
        if ((other.gameObject.tag == "Player") && canDamage)
        {
            Damageable damagable = other.gameObject.transform.GetComponent<Damageable>();
            damagable?.Damage(laser_damage);
            StartCoroutine(CoolDown());
        }
    }

    IEnumerator CoolDown()
    {
        canDamage = false;
        yield return new WaitForSeconds(0.5f); // laser damages every 0.5 seconds
        canDamage = true;
    }





    
}
