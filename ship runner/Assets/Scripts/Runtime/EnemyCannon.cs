using UnityEngine;

public class EnemyCannon : Cannon
{
    private void Update()
    {
        if (!(Time.time >= nextFireTime)) return;
        nextFireTime = Time.time + fireRate;
        Shoot();
    }
}