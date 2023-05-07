using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    //[SerializeField] public GameObject weapon;
    
    public GameObject weapon;
    public bool canThrow;
   // public bool objectThrown;
//    private float damage = 5f;
 //  [SerializeField] private Animator animate;
    private Animator animate;
    public const string isThrowing = "isThrowing";
    private bool animationWait;

    void Start()
    {
      StartCoroutine(CoolDown()); // Wait 1.5 seconds to attack
      animate = GetComponentInChildren<Animator>();
      animationWait = true;

    }

    void Update()
    {
      if (animate.GetBool("isDead") == true)
      {
        canThrow = false;
      }
      if (Input.GetMouseButtonDown(1) && canThrow) // if player left clicks the mouse
      {
        canThrow = false;
       // GameObject clone;
        animate.SetLayerWeight(animate.GetLayerIndex("Attack Layer"), 1);
        animate.SetTrigger("Throw");
  //      animate.SetBool(isThrowing, true);
        StartCoroutine(AnimationWait());

       //   if (!animationWait) 
     //    {
     //     clone = Instantiate(weapon, new Vector3(transform.position.x, transform.position.y+1, transform.position.z), transform.rotation) as GameObject;
  
    //      animationWait = true;
     //     StartCoroutine(CoolDown());
      }


      if (!animationWait) 
      {
        animationWait = true;
        GameObject clone;
        clone = Instantiate(weapon, new Vector3(transform.position.x, transform.position.y+1, transform.position.z), transform.rotation) as GameObject;
        // Bug: Player telports to the left a small amount when throwing weapon
       
        StartCoroutine(CoolDown());

      }
      

    }

    IEnumerator CoolDown()
    {
//      animate.SetBool(isThrowing, false);
      canThrow = false;
      yield return new WaitForSeconds(1.5f);
      animate.SetLayerWeight(animate.GetLayerIndex("Attack Layer"), 0);
 //     animate.SetBool(isThrowing, false);
//      objectThrown = false;
      canThrow = true;
    }



    IEnumerator AnimationWait()
    {
      yield return new WaitForSeconds(0.8f);
      animationWait = false;
    }
    

}
