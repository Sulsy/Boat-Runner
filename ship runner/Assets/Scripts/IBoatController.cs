using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoatController 
{
  public Boat currentBoat { get; set; }
  public Cannon currentCannon{ get; set; }
  public Vector3 targetPosition { get; set; }

  public void Moved();
}
