using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    private Slider boatUpdateProgress;
    [SerializeField] 
    private Slider cannonUpdateProgress;
    [SerializeField] 
    private Slider levelProgressBar;
    [SerializeField] 
    private Button boatUpdateButton;
    [SerializeField] 
    private Button cannonUpdateButton;
    [SerializeField]
    private Text hp;
    [SerializeField] 
    private Text coins;
    [SerializeField] 
    private TextMeshProUGUI boatPrice;
    [SerializeField] 
    private TextMeshProUGUI cannonPrice;

    private Player player;

    private void Awake()
    {
        player = GameController.instance.player;
    }

    public void UpdateStats()
    {
        hp.text = String.Empty;
        hp.text += player.currentBoat.Hp;
        coins.text = String.Empty;
        coins.text += player.coin;
    }

    public void UpdateProgress(float progress)
    {
        levelProgressBar.value = progress;
    }

    public void EndLevel(float boatUpdatePrice,float cannonUpdatePrice )
    {
       
    }

    public void MenuScreen(float boatUpdatePrice, float cannonUpdatePrice)
    {
        UpdateMenu(boatUpdatePrice, cannonUpdatePrice, true);
    }

    public void StartLevel()
    {
        UpdateMenu(0f, 0f, false);
    }

    private void UpdateMenu(float boatUpdatePrice, float cannonUpdatePrice, bool isActive)
    {
        coins.text += player.coin;
        hp.text += player.currentBoat.Hp;

        SetActiveMenu(isActive);
    
        if (isActive)
        {
            boatUpdateProgress.maxValue = boatUpdatePrice;
            cannonUpdateProgress.maxValue = cannonUpdatePrice;
            boatPrice.text = "Boat Price: " + boatUpdatePrice;
            cannonPrice.text = "Cannon Price: " + cannonUpdatePrice;
            boatUpdateProgress.value = player.coin;
            cannonUpdateProgress.value = player.coin;
        }
    }

    private void SetActiveMenu(bool stateMenu)
    {
        boatUpdateProgress.gameObject.SetActive(stateMenu);
        cannonUpdateProgress.gameObject.SetActive(stateMenu);
        boatUpdateButton.gameObject.SetActive(stateMenu);
        cannonUpdateButton.gameObject.SetActive(stateMenu);
        cannonPrice.gameObject.SetActive(stateMenu);
        boatPrice.gameObject.SetActive(stateMenu);
        levelProgressBar.gameObject.SetActive(!stateMenu);
        Debug.Log(stateMenu);
    }

}