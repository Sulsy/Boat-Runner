using UnityEngine;

public class Boat : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cannonPlace;
    public float price;
    public int type;
    [SerializeField] 
    internal float Hp;

    public void TakeDamage(float dmg,GameObject bullet)
    {
        Hp -= dmg;
        Debug.Log(gameObject.name+" "+ Hp);
        if (Hp<=0)
        {
            Destroy(gameObject,0.5f);
            if (gameObject.transform.CompareTag("Player"))
            {
                GameController.instance.Lose();
            }
        }
    }

    public bool IsBoatAlive()
    {
        return !(Hp<=0);
    }
}
