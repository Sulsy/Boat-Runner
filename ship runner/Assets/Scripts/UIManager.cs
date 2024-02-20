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
        if (levelProgressBar.IsActive())
        {
            //levelProgressBar.value = GameController.instance.distanceTravelled;
            //levelProgressBar.maxValue = GameController.instance.spline.CalculateLength();
        }
    }

    public void EndLevel(float boatUpdatePrice,float cannonUpdatePrice )
    {
        coins.text += player.coin;
        hp.text += player.currentBoat.Hp.ToString();
        SetActiveMenu(true);
        boatUpdateProgress.maxValue = boatUpdatePrice;
        boatUpdateProgress.maxValue = cannonUpdatePrice;
        boatPrice.text += boatUpdatePrice;
        cannonPrice.text += cannonUpdatePrice;
        boatUpdateProgress.value = player.coin;
        cannonUpdateProgress.value = player.coin;
        //boatUpdateButton.enabled = boatUpdatePrice<=player.coin;
        //cannonUpdateButton.enabled = boatUpdatePrice<=player.coin;     
    }
    public void StartLevel()
    {
        coins.text += player.coin;
        hp.text += player.currentBoat.Hp;
        SetActiveMenu(false);
    }

    private void SetActiveMenu(bool stateMenu)
    {
        boatUpdateProgress.gameObject.SetActive(stateMenu);
        cannonUpdateProgress.gameObject.SetActive(stateMenu);
        boatUpdateButton.gameObject.SetActive(stateMenu);
        cannonUpdateButton.gameObject.SetActive(stateMenu);
        cannonPrice.gameObject.SetActive(stateMenu);
        boatPrice.gameObject.SetActive(stateMenu);
       // levelProgressBar.gameObject.SetActive(!stateMenu);
        Debug.Log(stateMenu);
    }
}