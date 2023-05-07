using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour, Damageable
{
    // Either damage enemy when sword collides with them
    // Or damage enemy if within attack range
    [SerializeField] private Transform swordBlade;
    [SerializeField] private Transform swordBase;
    [SerializeField] private Transform weaponMiddle;
    [SerializeField] private Animator animate;
    [SerializeField] public Transform player;
    private AudioSource bossAttack;

    public bool inRange;
    private bool canAttack;
    private bool canDamage;
  //  public Animator animate;
   // public const string isAttacking = "isAttacking";
    private float attackRange = 2.25f;
  //  [SerializeField] private float attackDamage;
    private float attackDamage = 15f;

    private void Start()
    {
        canAttack = true;
        canDamage = false;
        inRange = false;
        bossAttack = GetComponent<AudioSource>();

     //   animate = GetComponent<Animator>();     
    }

    private void Update()
    {
        if (animate.GetBool("isDead") == true)
        {
            canAttack = false;
        }
        
        if (animate.GetBool("canAttack") == true && canAttack) 
        {
       // if (inRange) {
            PlayerAttack();
        }
       // }
    }
    public void Damage(float damage) {}




    /*
    public void PlayerCollision() // Check collision from sword in animation event
    {
        Debug.Log("Hit??");
    }
*/

    

    private void PlayerAttack()
    {
        //animate.SetLayerWeight(animate.GetLayerIndex("Attack Layer"), 1);
        //animate.SetTrigger("Slash"); // play attack animation

        animate.SetTrigger("EnemyAttack");
      //  Debug.Log("Boss Can Attack!!!!!!!!!!!");
        canDamage = true;
        StartCoroutine(SwingCoolDown());

        
    }
        
    IEnumerator SwingCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        bossAttack.Play();
    //    canDamage = false;
        yield return new WaitForSeconds(2.5f);
      //  animate.SetLayerWeight(animate.GetLayerIndex("Attack Layer"), 0);
      //  animate.SetBool(isAttacking, false);
        canAttack = true;
        animate.ResetTrigger("EnemyAttack");
  //      animate.ResetTrigger("Slash"); 
    }

    void OnTriggerEnter(Collider other) 
    {
        if ((other.gameObject.tag == "Player") && canDamage == true) 
        {
            //Damageable damagable = other.gameObject.transform.GetComponent<Damageable>();
           // damagable?.Damage(attackDamage);
           // Debug.Log("MONKEY KING!!!!");
            canDamage = false;
           // StartCoroutine(CanDamage());
        }
    }  

}
