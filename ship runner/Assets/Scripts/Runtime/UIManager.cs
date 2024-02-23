using System;
using System.Collections.Generic;
using CustomUi;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    private List<GameObject> uiPrefabs;
    [SerializeField]
    private Slider levelProgressBar;
    [SerializeField]
    private Text playerCoins;
    [SerializeField] 
    private Text playerHp;
    [SerializeField] 
    private Transform uiPlace;
    [SerializeField] 
    private Transform uiStats;

    private Dictionary<UiState, GameObject> uiElements;
    private Dictionary<EnumUiType, UiUpdateCase> uiUpdateCases;

    public void UpdateUiState(UiState uiState)
    {
        void DestroyOldUi()
        {
            foreach (var uiElement in uiElements)
            {
                Destroy(uiElement.Value);
            }
        }

        switch (uiState)
        {
            case UiState.UpdateBoatAndCannon:
                DestroyOldUi();
                uiElements.Add(uiState, Instantiate(uiPrefabs[0], uiPlace));

                break;
            case UiState.LevelProgress:
                DestroyOldUi();
                uiElements.Add(uiState, Instantiate(uiPrefabs[1], uiStats));
                levelProgressBar = uiElements[uiState].GetComponent<Slider>();
                break;
            case UiState.EndLevel:
                DestroyOldUi();
                uiElements.Add(uiState, Instantiate(uiPrefabs[2], uiPlace));
                break;
            case UiState.Ecs:
                DestroyOldUi();
                uiElements.Add(uiState, Instantiate(uiPrefabs[3], uiPlace));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(uiState), uiState, null);
        }

        UpdateUi(uiElements[uiState]);
    }

    public void UpdateStats(float coins, float hp)
    {
        playerCoins.text = coins.ToString();
        playerHp.text = hp.ToString();
    }

    private void Awake()
    {
        uiElements = new Dictionary<UiState, GameObject>();
        uiUpdateCases = new Dictionary<EnumUiType, UiUpdateCase>();
    }

    private void UpdateUi(GameObject menuPrefab)
    {
        var controller = GameController.instance;
        foreach (var button in menuPrefab.GetComponentsInChildren<CustomButton>())
        {
            switch (button.buttonType)
            {
                case ButtonType.BoatUpdate:
                    button.onClick.AddListener(GameController.instance.UpdateBoats);
                    break;
                case ButtonType.CannonUpdate:
                    button.onClick.AddListener(GameController.instance.UpdateCannon);
                    break;
                case ButtonType.Next:
                    button.onClick.AddListener(GameController.instance.Restart);
                    break;
                case ButtonType.Exit:
                    button.onClick.AddListener(GameController.instance.Exit);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        foreach (var slider in menuPrefab.GetComponentsInChildren<CustomSlider>())
        {
            if (!uiUpdateCases.ContainsKey(slider.uiType))
            {
                uiUpdateCases.Add(slider.uiType, new UiUpdateCase()
                {
                    slider = slider
                });
            }

            uiUpdateCases[slider.uiType].slider = slider;
        }

        foreach (var text in menuPrefab.GetComponentsInChildren<CustomText>())
        {
            if (!uiUpdateCases.ContainsKey(text.uiType))
            {
                uiUpdateCases.Add(text.uiType, new UiUpdateCase()
                {
                    text = text
                });
            }

            uiUpdateCases[text.uiType].text = text;
        }

        controller.onUpdatePriceChange += OnUpdatePriceChange;
        controller.onLevelProgressChange += LevelProgressBar;
        controller.onLevelFinish += LevelFinish;
    }

    private void LevelProgressBar(float value)
    {
        levelProgressBar.value = value;
    }

    private void OnUpdatePriceChange(EnumUiType uiType, float priceValue, float currentValue)
    {
        uiUpdateCases[uiType].slider.maxValue = priceValue;
        uiUpdateCases[uiType].slider.value = currentValue;
        uiUpdateCases[uiType].text.text = $"{currentValue}/{priceValue}";
    }

    private void LevelFinish(string levelState)
    {
        uiUpdateCases[EnumUiType.Next].text.text = $"You {levelState}";
    }
}

public enum UiState
{
    UpdateBoatAndCannon,
    LevelProgress,
    EndLevel,
    Ecs
}

public class UiUpdateCase
{
    public CustomSlider slider;
    public CustomText text;
}