using System.Linq;
using Dreamteck.Splines;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public float moveInterval = 3f; // Интервал между движениями
    public float moveDistance = 3f; // Интервал между движениями
    public float raycastDistance = 2f;
    public float shootingDistance = 2f;// Расстояние для райкаста по бокам
    public Boat currentBoat { get; set; }
    public Cannon currentGun { get; set; }
    
    private Rigidbody rb;
    private SplineFollower splineFollower;
    private Player player;
    private float nextMoveTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        splineFollower = GetComponent<SplineFollower>();
        currentBoat = GetComponent<Boat>();
        currentGun = GetComponentInChildren<Cannon>();
        currentGun.tag = tag;
        player = FindObjectOfType<Player>(); 

        nextMoveTime = Time.time + moveInterval;
    }

    private void Update()
    {
        if (GameController.instance.IsLevelRun)
        {
            // Проверка на необходимость движения по оси X
            if (Time.time >= nextMoveTime)
            {
                MoveOnXAxis();
                nextMoveTime = Time.time + moveInterval;
            }
            if (Time.time >= currentGun.nextFireTime)
            {
                currentGun. nextFireTime = Time.time + currentGun.fireRate;
                currentGun.Shoot();
            }
        }
        
    }

    private void MoveOnXAxis()
    {
        if (!IsObstacleOnSides())
        {
            int direction = Random.Range(0, 2) * 2 - 1;
            float targetX = direction * moveDistance;
            rb.velocity = new Vector3(currentBoat.moveSpeed*targetX, rb.velocity.y, rb.velocity.z);
        }
    }

    private bool IsObstacleOnSides()
{
    // Проверка наличия препятствий с помощью райкастов
    RaycastHit[] hitsRight = Physics.RaycastAll(transform.position, transform.right, raycastDistance);
    RaycastHit[] hitsLeft = Physics.RaycastAll(transform.position, -transform.right, raycastDistance);

    if (hitsRight.Any(hit => hit.collider != null && hit.collider.gameObject != gameObject && hit.collider.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast")))
    {
        Debug.Log("sides");
        return true; // Есть препятствие по бокам
    }

    if (hitsLeft.Any(hit => hit.collider != null && hit.collider.gameObject != gameObject && hit.collider.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast")))
    {
        Debug.Log("sides");
        return true; // Есть препятствие по бокам
    }

    return false; // Препятствий нет
}


    
}