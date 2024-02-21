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
   private List<GameObject> cannonPrefabs;
   [SerializeField] 
   private SplineFollower pSplineFollower;
   [SerializeField] 
   private UIManager uiManager;
   [SerializeField] 
   private SplineComputer  spline;
   [SerializeField] 
   private string filePath;
   
   public bool isLevelRun;
   public Player player;
   private void Awake()
   {
      instance = this; 
      filePath = Application.persistentDataPath + "/player.json";
   }

   private void Start()
   {
      if (!LoadPlayer())
      {
         SpawnBoat(0,0);
         player.AddCoin(100);
      }
      uiManager.MenuScreen(100,100);
      MenuScreen();
   }

   private void SpawnBoat(int boatType,int cannonType)
   {
      player.currentBoat = Instantiate(boatsPrefabs[boatType], player.transform).GetComponent<Boat>();
      SpawnCannon(cannonType);
   }

   private void SpawnCannon(int cannonType)
   {
      player.currentCannon = Instantiate(cannonPrefabs[cannonType], player.currentBoat.GetComponent<Boat>().cannonPlace)
         .GetComponent<Cannon>();
      player.currentCannon.tag = player.tag;
   }
   
   public void UpdateBoats()
   {
      var updateBoatType = player.currentBoat.type + 1;
      if (boatsPrefabs.Count<=updateBoatType) return;
      var updateBoat=boatsPrefabs[updateBoatType].GetComponent<Boat>();
      if (!(player.coin >= updateBoat.price)) return;
      
      player.coin -=  updateBoat.price;
      Destroy(player.currentBoat.gameObject);
      SpawnBoat(updateBoatType,player.currentCannon.type);
      uiManager.UpdateStats();
   }
   public void UpdateCannon()
   {
      var updateCannonType = player.currentCannon.type + 1;
      if (cannonPrefabs.Count<=updateCannonType) return;
      var updateCannon=cannonPrefabs[updateCannonType].GetComponent<Cannon>();
      if (!(player.coin >= updateCannon.price)) return;
      
      player.coin -= updateCannon.price;
      Destroy(player.currentCannon.gameObject);
      SpawnCannon(updateCannonType);
      uiManager.UpdateStats();

   }

   private void Update()
   {
      uiManager.UpdateStats();
      if (isLevelRun)
      {
         uiManager.UpdateProgress((float)spline.Project(player.transform.position).percent);
      }
      else if (Input.GetButtonDown("Fire2"))
      {
         SavePlayer();
         StartLevel();
      }
   }

   private void StartLevel()
   {
      pSplineFollower.enabled = true;
      isLevelRun = true;
      uiManager.StartLevel();
   }

   private void MenuScreen()
   {
      pSplineFollower.enabled = false;
      isLevelRun = false;
      uiManager.EndLevel(100,100);
   }
   public void Win()
   {
      player.AddCoin(100);
      SavePlayer();
      Restart();
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
      player.SaveMetaData();
      string jsonData = JsonConvert.SerializeObject(player.metaData);
      
      File.WriteAllText(filePath, jsonData);
   }
   
   public bool LoadPlayer()
   {
      if (File.Exists(filePath))
      {
         string jsonData = File.ReadAllText(filePath);
         var metaData=JsonConvert.DeserializeObject<PlayerMetaData>(jsonData);
         player.AddCoin(metaData.coin); 
         SpawnBoat(metaData.boatType,metaData.cannonType);
         return true;
      }
      else
      {
         return false;
      }
   }
}
