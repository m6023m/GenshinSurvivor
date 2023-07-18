using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MapSelectManager : MonoBehaviour
{
    public string[] scenes;
    public string[] sceneNames;
    public Sprite[] mapSprites;
    public Image imgDiscription;
    public TextMeshProUGUI nameDiscription;
    public GameObject panel;
    public GameObject mapPrefab;
    public Button btnBack;
    public Button btnNext;
    public Toggle toggleInfinity;
    public TMP_Dropdown stageLevelDropdown;

    private string selectedMap = null;
    private void Awake()
    {
        Init();
        btnBack.onClick.AddListener(OnClickBack);
        btnNext.onClick.AddListener(OnClickNext);
        toggleInfinity.onValueChanged.AddListener(OnValueChangedInfinity);
        stageLevelDropdown.onValueChanged.AddListener(OnValueChangedStageLevel);
    }

    private void OnValueChangedStageLevel(int level)
    {
        GameDataManager.instance.saveData.userData.stageLevel = level;
    }

    private void OnValueChangedInfinity(bool isInfinityMode)
    {
        GameDataManager.instance.saveData.option.isInfinityMode = isInfinityMode;
    }

    void OnClickBack()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        SceneManager.LoadScene("CharacterSelectScene");
    }

    void OnClickNext()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        if (selectedMap != null)
        {
            GameDataManager.instance.SaveInstance();
            SceneManager.LoadScene(selectedMap);
        }
    }


    void Init()
    {
        InitUI();
        InitStageLevel();
    }

    void InitUI()
    {
        for (int i = 0; i < mapSprites.Length; i++)
        {
            int idx = i;
            GameObject mapButtonObj = Instantiate(mapPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Button mapButton = mapButtonObj.GetComponentInChildren<Button>();
            TextMeshProUGUI mapName = mapButtonObj.GetComponentInChildren<TextMeshProUGUI>();
            mapButtonObj.transform.parent = panel.transform;
            mapButtonObj.GetComponentInChildren<Image>().sprite = mapSprites[idx];
            mapName.text = sceneNames[idx].Localize();
            mapButton.onClick.AddListener(() =>
            {
                AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
                selectedMap = scenes[idx];
                imgDiscription.sprite = mapSprites[idx];
                nameDiscription.text = sceneNames[idx].Localize();
                btnNext.interactable = true;
            });
        }

        toggleInfinity.isOn = GameDataManager.instance.saveData.option.isInfinityMode;
    }


    void InitStageLevel()
    {
        stageLevelDropdown.value = GameDataManager.instance.saveData.userData.stageLevel;
    }
}
