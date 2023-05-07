using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))] // Must have SphereCollider
public class HealPlayer : MonoBehaviour
{
  [SerializeField] public float healthBoost;
  public float PickupRadius = 1f;
  //public InventoryItemData ItemData;

  private SphereCollider myCollider;

  public PlayerVitals currentHealth;


  private void Awake()
  {
      myCollider = GetComponent<SphereCollider>();
      myCollider.isTrigger = true;
      myCollider.radius = PickupRadius;
  }

  private void OnTriggerEnter(Collider other)
  {
    currentHealth.playerHealth += healthBoost;
    Destroy(this.gameObject);

  }
}

