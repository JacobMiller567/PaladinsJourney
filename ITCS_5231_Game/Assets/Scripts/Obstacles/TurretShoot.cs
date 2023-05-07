using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour, Damageable
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform barrelTransform;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private float detectionRange;
    [SerializeField] private float damage;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private bool useGravity;
   // public GameObject projectile;
    public Rigidbody projectile;
    private AudioSource cannonShot;
    
    private bool canShoot;
    private bool detected;
    private float rotationSpeed;

    Vector3 velocity;


    
    private void Start()
    {
        canShoot = true;
        rotationSpeed = 8f;
        cannonShot = GetComponent<AudioSource>();
    }


    private void Update()
    {
        FacePlayer();
        ShootPlayer();
        //GameObject clone;
        //clone = Instantiate(weapon, new Vector3(transform.position.x, transform.position.y+1, transform.position.z), transform.rotation) as GameObject;  
    }

    public void Damage(float damage) {}

    private void FacePlayer() // rotate enemy to face the player
    {
      Vector3 lookAtPlayer = playerTransform.position - transform.position; // get distance of player from enemyOffset
    //  Vector3 lookAtPlayer = (barrelTransform.position - playerTransform.position);
      if (lookAtPlayer.magnitude <= detectionRange) // if player is within given range
      {
          
        Quaternion targetRotation = Quaternion.LookRotation(lookAtPlayer);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // rotate the enemy
        
        detected = true;
        
      }
      else
      {

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotationSpeed * Time.deltaTime); // rotate back to position
        detected = false;
      }
    }

    private void ShootPlayer()  // spawn projectile prefab and shoot at player
    {
        if ((detected == true) && canShoot)
        {
        //GameObject clone;
        // clone = Instantiate(weapon, new Vector3(transform.position.x, transform.position.y+1, transform.position.z), transform.rotation) as GameObject;
     //   var getTransform = transform.position = Vector3.SmoothDamp(transform.position, playerTransform.position, ref velocity, .5f, projectileSpeed);
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo)) // Raycast out the info of the hit enemy
            {

                if (hitInfo.collider.CompareTag("Player")) 
                {
                    Rigidbody bullet = Instantiate(projectile, barrelTransform.position, transform.rotation);
                    cannonShot.Play();
                    bullet.velocity = transform.forward * projectileSpeed;
                    StartCoroutine(Reload());
                }
            }
        }
        
    }


    IEnumerator Reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(reloadSpeed);
        canShoot = true;

    }

}
