using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using Pathfinding;

public class EnemyMovement : MonoBehaviour, Damageable
{
  [SerializeField] private Transform player;
  [SerializeField] private Rigidbody rb;
  [SerializeField] private float detectionRange;
  [SerializeField] private float speed;
  [SerializeField] private float damage;
  //private GameObject player;
  private Animator animate;
  private EnemyVitals enemyHP;
 // [SerializeField] private NavMeshAgent enemyMeshAgent;
  //Vector3 enemyOffset = new Vector3(5f, 0f, 5f);
  private float rotationSpeed;
  private float smoothTime = 0.4f;
  private bool detected;
  private bool canAttack;
  private float radiusOfSatisfaction = 0.5f;
  private float wanderRadius = 10f;

  Vector3 velocity;
  Vector3 enemyMove;
  Vector3 holdLocation;
  Vector3 respawn;
  //IAstarAI ai;

    private void Start()
    {
     // player = GameObject.Find("Player");
      animate = GetComponent<Animator>();
      enemyHP = GetComponent<EnemyVitals>();
   //   ai = GetComponent<IAstarAI>();
      rotationSpeed = 8f;
      canAttack = true;
      holdLocation = transform.position; // set location to enemy spawnpoint
    }

    private void Update()
    {
      /*

      if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath) && !detected) {
            ai.destination = PickRandomPoint();
            ai.SearchPath();
        }
        */

     if (animate.GetBool("isDead") == true) // make enemy not move or do damage while playing death animation
      {
        damage = 0;
        speed = 0;
      }
    

        FacePlayer(); // used for enemy to face the player
        ChasePlayer(); // used for enemy to chase the player
        FellOffMap(); // used for when enemy falls off the map

        if (canAttack && (player.position - transform.position).magnitude <= 2f) // if enemy is close to player
        {
          animate.SetTrigger("EnemyAttack");
          Debug.Log("ATTACK");
          Damageable damagable = player.transform.GetComponent<Damageable>();
          damagable?.Damage(damage); // damage player
          StartCoroutine(CollisonDamage()); // wait to attack again
        }

    }

    public void Damage(float damage) {}

    private void FacePlayer() // rotate enemy to face the player
    {
      //Vector3 lookAtPlayer = GameManager.instance.player.playerTransform.position - transform.position;
      Vector3 lookAtPlayer = player.position - transform.position; // get distance of player from enemyOffset

      if (lookAtPlayer.magnitude < detectionRange) // if player is within given range
      {
        Quaternion targetRotation = Quaternion.LookRotation(lookAtPlayer);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // rotate the enemy
        detected = true;
        //detected = false;
      }
      else
      {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotationSpeed * Time.deltaTime); // rotate back to position
        detected = false;
        animate.SetFloat("EnemySpeed", 0f, 0.1f, Time.deltaTime); // Idle Animation
      }
    }

    IEnumerator CollisonDamage()
    {
      canAttack = false;
      yield return new WaitForSeconds(1f);
      canAttack = true;
    }

    private void ChasePlayer() // move the enemy towards the player
    {
      if (detected == true )//&& (transform.position - player.position) > radiusOfSatisfaction)
      {
        animate.SetFloat("EnemySpeed", 0.5f, 0.1f, Time.deltaTime); // Walk Animation
        enemyMove = new Vector3(player.position.x, 1f, player.position.z); // prevents enemy from floating
        transform.position = Vector3.SmoothDamp(transform.position, enemyMove, ref velocity, smoothTime, speed);
  //      enemyMeshAgent.SetDestination(player.transform.position);
  //      enemyMeshAgent.SetDestination(AgentSpawner.ChooseRandomPointOnNavMesh(Triangulation));

      }
    }

    private void Wander() // simple enemy movement when player is not detected
    {
      if (!detected)
      {
       
  //      Vector3 randomPoints = new Vector3(0, 0, 0);
      }

    }

     Vector3 PickRandomPoint() 
     {
        var point = Random.insideUnitSphere * wanderRadius;
        point.y = 0;
       // point += ai.position;
        return point;
        }

    private void FellOffMap()
    {
      if (transform.position.y < -20f)
      {
         respawn = new Vector3(0f, 3f, 0f); // drops enemy from height
         transform.position = holdLocation + respawn; // set location to spawn location
      }
    }


}
