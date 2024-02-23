using UnityEngine;

public class Boat : MonoBehaviour, IUpgradable
{
    public Transform cannonPlace;
    public float moveSpeed = 5f;
    public float price;
    public int type;
    [SerializeField] 
    internal float Hp;

    public float GetPrice()
    {
        return price;
    }

    public void TakeDamage(float dmg, GameObject bullet)
    {
        Hp -= dmg;
        if (!(Hp <= 0)) return;
        if (gameObject.transform.CompareTag("Player"))
        {
            GameController.instance.Lose();
        }
        else if (gameObject.transform.CompareTag("Boss"))
        {
            GameController.instance.Win();
        }

        Destroy(gameObject);
    }
}