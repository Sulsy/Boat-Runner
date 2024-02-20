using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDev : MonoBehaviour
{
    public float Coins { get; set; }

    private void Start()
    {
        Coins += 100;
        Debug.Log(Coins);
    }
}
