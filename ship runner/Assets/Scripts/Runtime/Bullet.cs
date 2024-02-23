using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    private GameObject owner;

    public float Damage
    {
        set
        {
            if (damage != 0) return;
            damage = value;
        }
    }

    public GameObject Owner
    {
        set
        {
            if (owner != null) return;
            owner = value;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water") || other.transform.gameObject == owner) return;
        if (other.collider.GetComponent<Boat>())
        {
            other.collider.GetComponent<Boat>().TakeDamage(damage, gameObject);
        }

        Destroy(gameObject);
    }
}