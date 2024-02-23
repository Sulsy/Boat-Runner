using System;
using System.Collections.Generic;
using System.IO;
using Cinemachine;
using Dreamteck.Splines;
using Enums;
using Newtonsoft.Json;
using Spawners;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public bool isLevelRun;
    public Player player;
    public SplineComputer spline;

    [SerializeField] 
    private List<GameObject> boatsPrefabs;
    [SerializeField] 
    private List<GameObject> cannonPrefabs;
    [SerializeField] 
    private SplineFollower pSplineFollower;
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField]
    private EnemyShipSpawner enemyShipSpawner;
    [SerializeField]
    private int levelId;

    private string filePath;
    private bool _isWin;
    public event Action<EnumUiType, float, float> onUpdatePriceChange;
    public event Action<float> onLevelProgressChange;
    public event Action<string> onLevelFinish;

    public void UpdateBoats()
    {
        var (canUpdate, boat) = CanUpdate<Boat>(boatsPrefabs, player.currentBoat.type, player.coin);
        if (!canUpdate) return;

        player.coin -= boat.price;
        onUpdatePriceChange?.Invoke(EnumUiType.BoatUpdate, boat.price, player.coin);
        Destroy(player.currentBoat.gameObject);
        SpawnBoat(boat.type, player.currentCannon.type);
        MenuScreen();
    }

    public void UpdateCannon()
    {
        var (canUpdate, cannon) = CanUpdate<Cannon>(cannonPrefabs, player.currentCannon.type, player.coin);
        if (!canUpdate) return;
        player.coin -= cannon.price;
        onUpdatePriceChange?.Invoke(EnumUiType.CannonUpdate, cannon.price, player.coin);
        Destroy(player.currentBoat.gameObject);
        SpawnBoat(player.currentBoat.type, cannon.type);
        MenuScreen();
    }

    public void DeSpawnEnemyShip()
    {
        enemyShipSpawner.DeSpawn();
        enemyShipSpawner.gameObject.SetActive(false);
    }

    public void Win()
    {
        enemyShipSpawner.Stop();
        enemyShipSpawner.gameObject.SetActive(false);
        _isWin = true;
        player.AddCoin(100);
        SavePlayer();
        LevelFinish("Win");
    }

    public void Lose()
    {
        LevelFinish("Lose");
    }

    public void Restart()
    {
        var nextSceneBuildIndex =0;
        if (!_isWin)
        {
            nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex ;
        }
        else
        {
            switch (levelId)
            {
                case 1:
                    nextSceneBuildIndex = 1; 
                    break;
                case 2:
                    nextSceneBuildIndex = 0; 
                    break;
            }
        }
        SceneManager.LoadScene(nextSceneBuildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Escape()
    {
        uiManager.UpdateUiState(UiState.Ecs);
    }

    private void Awake()
    {
        instance = this;
        filePath = Application.persistentDataPath + "/player.json";
    }

    private void Start()
    {
        if (!LoadPlayer())
        {
            SpawnBoat(0, 0);
            player.AddCoin(100);
        }

        uiManager.UpdateUiState(UiState.UpdateBoatAndCannon);
        MenuScreen();
    }

    private void SpawnBoat(int boatType, int cannonType)
    {
        player.currentBoat = Instantiate(boatsPrefabs[boatType], player.transform).GetComponent<Boat>();
        SpawnCannon(cannonType);
        var o = player.currentBoat.gameObject;
        virtualCamera.Follow = o.transform;
        virtualCamera.LookAt = o.transform;
    }

    private void SpawnCannon(int cannonType)
    {
        player.currentCannon =
            Instantiate(cannonPrefabs[cannonType], player.currentBoat.GetComponent<Boat>().cannonPlace)
                .GetComponent<Cannon>();
        player.currentCannon.Owner = player.currentBoat.gameObject;
    }

    private (bool, T) CanUpdate<T>(List<GameObject> prefabs, int currentType, float playerCoins)
        where T : MonoBehaviour, IUpgradable
    {
        var updateType = currentType + 1;
        if (prefabs.Count <= updateType)
            return (false, null);

        var updateComponent = prefabs[updateType].GetComponent<T>();
        if (!(playerCoins >= updateComponent.GetPrice()))
            return (false, updateComponent);

        return (true, updateComponent);
    }

    private void Update()
    {
        uiManager.UpdateStats(player.coin, player.currentBoat.Hp);
        if (isLevelRun)
        {
            onLevelProgressChange?.Invoke((float)spline.Project(player.transform.position).percent);
        }
        else
        {
            if (Input.GetButtonDown("Fire2"))
            {
                uiManager.UpdateUiState(UiState.LevelProgress);
                StartLevel();
            }
            /*if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                float swipeDelta = touch.deltaPosition.x / Screen.width;

                if (swipeDelta < 0)
                {
                    uiManager.UpdateUiState(UiState.LevelProgress);
                    StartLevel();
                }
            }*/
        }
    }

    private void StartLevel()
    {
        pSplineFollower.enabled = true;
        isLevelRun = true;
    }

    private void MenuScreen()
    {
        pSplineFollower.enabled = false;
        isLevelRun = false;

        var (canUpdateBoat, updateBoat) = CanUpdate<Boat>(boatsPrefabs, player.currentBoat.type, player.coin);
        if (updateBoat == null) return;
        onUpdatePriceChange?.Invoke(EnumUiType.BoatUpdate, updateBoat.price, player.coin);

        var (canUpdateCannon, updateCannon) = CanUpdate<Cannon>(cannonPrefabs, player.currentCannon.type, player.coin);
        if (updateCannon == null) return;
        onUpdatePriceChange?.Invoke(EnumUiType.CannonUpdate, updateCannon.price, player.coin);
    }

    private void LevelFinish(string levelState)
    {
        uiManager.UpdateUiState(UiState.EndLevel);
        pSplineFollower.enabled = false;
        onLevelFinish?.Invoke(levelState);
    }

    private void SavePlayer()
    {
        player.SaveMetaData();
        string jsonData = JsonConvert.SerializeObject(player.metaData);

        File.WriteAllText(filePath, jsonData);
    }

    private bool LoadPlayer()
    {
        if (File.Exists(filePath))
        {
            var jsonData = File.ReadAllText(filePath);
            var metaData = JsonConvert.DeserializeObject<PlayerMetaData>(jsonData);
            player.LoadMetaData(metaData);
            if (metaData != null) SpawnBoat(metaData.boatType, metaData.cannonType);
            return true;
        }
        else
        {
            return false;
        }
    }
}