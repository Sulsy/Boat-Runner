using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour,IBoatController
{
    public Boat currentBoat { get; set; }
    public Cannon currentCannon{ get; set; } 
    public Vector3 targetPosition { get; set; }

    public PlayerMetaData metaData;
    public float coin { get; set; }

    private void Start()
    {
        metaData = new PlayerMetaData();
    }

    public void AddCoin(float coins)
    {
        coin += coins;
        //SaveMetaData();
    }
    public void SaveMetaData()
    {
        metaData.coin = coin;
        metaData.boatType = currentBoat.type;
        metaData.cannonType = currentCannon.type;
    }
    void Update()
    {
        if ( GameController.instance.isLevelRun)
        {
            Moved();
        }
      
    }
    public void Moved()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Fire1")&&Time.time >= currentCannon.nextFireTime)
        { 
            currentCannon.nextFireTime = Time.time + currentCannon.fireRate; 
            currentCannon.Shoot();
        }

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
}

public class PlayerMetaData
{
    public float coin;
    public int boatType;
    public int cannonType;

}
