using System;
using System.Collections.Generic;
using System.IO;
using Cinemachine;
using Dreamteck.Splines;
using Newtonsoft.Json;
using Spawners;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
   public static GameController instance;
   [SerializeField] 
   private List<GameObject> boatsPrefabs;
   [SerializeField] 
   private List<GameObject> gunPrefabs;
   [SerializeField] 
   private SplineFollower pSplineFollower;
   [SerializeField] 
   private UIManager uiManager;
   private string filePath;
   
   public Spline spline; 
   public float distanceTravelled = 0f;
   
   public bool IsLevelRun;
   public Player player;
   private void Awake()
   {
      instance = this;
      filePath = Application.persistentDataPath + "/player.json";
   }

   private void Start()
   {
      player.currentBoat=Instantiate(boatsPrefabs[0],player.transform).GetComponent<Boat>();
      player.currentCannon=Instantiate( gunPrefabs[0],player.currentBoat.GetComponent<Boat>().cannonPlace).GetComponent<Cannon>();
      player.currentCannon.tag = player.tag;
      player.coin = 210;
      uiManager.EndLevel(100, 100);
      IsLevelRun = false;
      ss();
   }

   private void Update()
   {
      uiManager.UpdateStats();
      if (Input.GetButtonDown("Fire2"))
      {
         StartLevel();
      }
   }

   public void UpdateBoats()
   {
      if (player.coin >= 100)
      {
         player.coin -= 100;
         Destroy(player.currentBoat.gameObject);
         player.currentBoat = Instantiate(boatsPrefabs[1],player.transform).GetComponent<Boat>();
         player.currentCannon = Instantiate(gunPrefabs[0], player.currentBoat.GetComponent<Boat>().cannonPlace)
            .GetComponent<Cannon>();
         uiManager.UpdateStats();
      }
   }
   public void UpdateCannon()
   {
      if (player.coin>=100)
      {
         Destroy(player.currentCannon.gameObject);
         player.currentCannon=Instantiate( gunPrefabs[1],player.currentBoat.GetComponent<Boat>().cannonPlace).GetComponent<Cannon>();   
         uiManager.UpdateStats();
      }
      
   }

   public void StartLevel()
   {
      pSplineFollower.enabled = true;
      uiManager.StartLevel();
      IsLevelRun = true;
   }
   public void ss()
   {
      pSplineFollower.enabled = false;
      IsLevelRun = false;
      uiManager.EndLevel(100,100);
   }

   public void Lose()
   {
      Restart();
   }

   private void Restart()
   {
      var currentSceneName = SceneManager.GetActiveScene().name;
      SceneManager.LoadScene(currentSceneName);
   }
   
   public void SavePlayer()
   {
      string jsonData = JsonConvert.SerializeObject(player);
      
      File.WriteAllText(filePath, jsonData);
   }
   
   public Player LoadPlayer()
   {
      if (File.Exists(filePath))
      {
         string jsonData = File.ReadAllText(filePath);
         return JsonConvert.DeserializeObject<Player>(jsonData);
      }
      else
      {
         return null;
      }
   }
}
