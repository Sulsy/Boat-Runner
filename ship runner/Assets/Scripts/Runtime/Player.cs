using UnityEngine;

public class Player : MonoBehaviour, IBoatController
{
    public Boat currentBoat { get; set; }
    public Cannon currentCannon { get; set; }
    public Vector3 targetPosition { get; set; }

    public PlayerMetaData metaData;
    public float coin { get; set; }

    public void AddCoin(float coins)
    {
        coin += coins;
        SaveMetaData();
    }

    public void SaveMetaData()
    {
        metaData.coin = coin;
        metaData.boatType = currentBoat.type;
        metaData.cannonType = currentCannon.type;
    }

    public void LoadMetaData(PlayerMetaData playerMetaData)
    {
        metaData = playerMetaData;
        coin = playerMetaData.coin;
    }

    public void Fire()
    {
        if (!(Time.time >= currentCannon.nextFireTime)) return;
        currentCannon.nextFireTime = Time.time + currentCannon.fireRate;
        currentCannon.Shoot();
    }

    public void Moved()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float swipeDelta = touch.deltaPosition.x / Screen.width;

            if (swipeDelta < 0)
            {
                moveInput = -1f;
            }
            else if (swipeDelta > 0)
            {
                moveInput = 1f;
            }
        }

        targetPosition = transform.position + Vector3.right * moveInput;
        transform.position = Vector3.Lerp(transform.position, targetPosition, currentBoat.moveSpeed * Time.deltaTime);
    }

    private void Start()
    {
        metaData = new PlayerMetaData();
    }

    private void Update()
    {
        if (GameController.instance.isLevelRun)
        {
            Moved();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}

public class PlayerMetaData
{
    public float coin;
    public int boatType;
    public int cannonType;
}