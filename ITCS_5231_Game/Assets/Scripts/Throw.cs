using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  // Reference: https://www.youtube.com/watch?v=za2Ja52bGK0
  //[SerializeField] private GameObject player;
  //[SerializeField] private GameObject knife;
  private GameObject player; // player
  private GameObject sword; // player weapon
  private bool throwing; // control boomarang direction
  private Transform itemRotate; // rotation of weapon
  private Vector3 inFrontOfPlayer; // location from player to be thrown
  private float damage = 5f;
  private float returnDamage = 10f;
  private bool canDamage = true;

  private void Awake()
  {
    damage = 0f;
    returnDamage = 0f;
    StartCoroutine(AllowDamage());
  }

  private void Start()
    {
      player = GameObject.Find("Player");
      sword = GameObject.Find("Sword");

      sword.GetComponent<MeshRenderer>().enabled = false; // turn main weapon invisible
      itemRotate = gameObject.transform.GetChild(0); // find child of weapon holder

      inFrontOfPlayer = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z) + player.transform.forward * 10f;  // Throws boomerang in front of player 

      StartCoroutine(Boomarang());
    }

    IEnumerator Boomarang()
    {
      throwing = true;
      yield return new WaitForSeconds(1.5f);
      throwing = false;
    }

    private void Update()
    {
      //itemRotate.transform.Rotate(0, Time.deltaTime * 500, 0); // rotate the object similar to a drill
      itemRotate.transform.Rotate(Time.deltaTime * 500, Time.deltaTime * 500, Time.deltaTime * 500); // rotate the object in circular rotation

      if (throwing)
      {
        transform.position = Vector3.MoveTowards(transform.position, inFrontOfPlayer, Time.deltaTime * 40); // move object in front of the player
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo)) // Raycast out the info of the hit enemy
        {
          if (canDamage)
          {
           // Damageable damagable = hitInfo.transform.GetComponent<Damageable>();
          //  damagable?.Damage(damage); // Damage function to damage the enemy
            if (hitInfo.collider.CompareTag("Enemy")) // ? Compare tag might not be needed
            {
              Damageable damagable = hitInfo.transform.GetComponent<Damageable>();
              damagable?.Damage(damage); // Damage function to damage the enemy
              Debug.Log("HIT");
              StartCoroutine(SecondHit());
            }
          }

          if (hitInfo.collider.CompareTag("Bullseye"))
          {
              Debug.Log("BULLSEYE!!!");
          }
        }

      }
      if (!throwing)
      {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y+1, player.transform.position.z), Time.deltaTime * 40); // return object to the player
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
           if (hit.collider.CompareTag("Enemy")) // see if weapon collides with enemy on the way back
            {
              Damageable damagable = hit.transform.GetComponent<Damageable>();
              damagable?.Damage(returnDamage); // damage enemy
              Debug.Log("HIT 2");
            }

            if (hit.collider.CompareTag("Bullseye"))
            {
              Debug.Log("BULLSEYE!!!");
            }

          }
      }
      if (!throwing && Vector3.Distance(player.transform.position, transform.position) < 1.5) // if weapon is close to the player
      {
          sword.GetComponent<MeshRenderer>().enabled = true; // turn main weapon visible
          Destroy(this.gameObject); // destroy copy of weapon
      }

    }

    IEnumerator SecondHit() // allows weapon to hit twice
    {
      canDamage = false;
      yield return new WaitForSeconds(0.5f); // wait half a second
      canDamage = true;
      //Debug.Log("Can Attack");

    }

    IEnumerator AllowDamage() // stop bug where enemy would take damage from boomarang at start of game
    {
      yield return new WaitForSeconds(1.5f);
      damage = 5f;
      returnDamage = 10f;
    }



}
