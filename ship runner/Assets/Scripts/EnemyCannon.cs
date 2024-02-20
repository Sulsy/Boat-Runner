using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : Cannon
{
    void Update()
    {

        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }
}
