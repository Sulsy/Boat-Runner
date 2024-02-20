using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  private float damage;
  private GameObject boatf;
  public float Damage
  {
    get => damage;
    set
    {
      if (damage!=0) return;
      damage = value;
    }
  }
  public GameObject Boatf
  {
    get => boatf;
    set
    {
      if (boatf!=null) return;
      boatf = value;
    }
  }

  private void OnCollisionEnter(Collision other)
  {
    if (other.collider.GetComponent<Boat>()&& !other.transform.gameObject.CompareTag(gameObject.tag))
    {
      other.collider.GetComponent<Boat>().TakeDamage(damage,gameObject);
      Destroy(gameObject);
    }
  }
}
