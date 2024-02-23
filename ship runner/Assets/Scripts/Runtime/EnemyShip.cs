using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShip : MonoBehaviour, IBoatController
{
    public float moveDistance = 3f;
    public Boat currentBoat { get; set; }
    public Cannon currentCannon { get; set; }

    [SerializeField] 
    private bool isBoss;

    private Rigidbody rb;
    private float nextMoveTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentBoat = GetComponent<Boat>();
        currentCannon = GetComponentInChildren<Cannon>();
        currentCannon.Owner = gameObject;
        nextMoveTime = Time.time + 1f;
    }

    private void Update()
    {
        if (GameController.instance.isLevelRun)
        {
            if (currentBoat.IsDestroyed() && isBoss)
            {
                GameController.instance.player.AddCoin(Mathf.RoundToInt(Mathf.Abs(currentBoat.price / 6f)));
                if (isBoss)
                {
                    GameController.instance.Win();
                }
            }

            if (Time.time >= nextMoveTime)
            {
                MoveOnXAxis();
                nextMoveTime = Time.time + Random.Range(1, 5);
            }

            if (Time.time >= currentCannon.nextFireTime)
            {
                currentCannon.nextFireTime = Time.time + currentCannon.fireRate;
                currentCannon.Shoot();
            }
        }
    }

    private void MoveOnXAxis()
    {
        int direction = Random.Range(0, 2) * 2 - 1;
        float targetX = direction * moveDistance;
        var velocity = rb.velocity;
        velocity = new Vector3(currentBoat.moveSpeed * targetX, velocity.y, velocity.z);
        rb.velocity = velocity;
    }

    private void OnDestroy()
    {
        GameController.instance.player.AddCoin(Mathf.RoundToInt(Mathf.Abs(currentBoat.price / 6f)));
    }
}