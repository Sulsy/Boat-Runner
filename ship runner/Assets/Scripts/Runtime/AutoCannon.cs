using UnityEngine;

public class AutoCannon : Cannon
{
    [SerializeField] 
    private float maxPlayerDistanceZ = 120f;

    private void Update()
    {
        var distanceToPlayerZ = Mathf.Abs(GameController.instance.player.transform.position.z - transform.position.z);
        if (!(distanceToPlayerZ <= maxPlayerDistanceZ) || !(Time.time >= nextFireTime) || isReloading ||
            currentAmmo <= 0) return;
        Shoot();
        nextFireTime = Time.time + 1f / fireRate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.AddComponent<MoveObject>().direction = Vector3.down;
        }
    }
}