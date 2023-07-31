using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSettings : MonoBehaviour
{
    private Slider volMaster;
    private Slider volBGM;
    private Slider volSE;
    private Toggle joystickVisible;
    void Awake()
    {
        volMaster = GetComponentsInChildren<Slider>()[0];
        volBGM = GetComponentsInChildren<Slider>()[1];
        volSE = GetComponentsInChildren<Slider>()[2];
        joystickVisible = GetComponentInChildren<Toggle>();

        Init();
    }

    void Init()
    {
        volMaster.value = GameDataManager.instance.saveData.option.masterVolume;
        volBGM.value = GameDataManager.instance.saveData.option.bgmVolume;
        volSE.value = GameDataManager.instance.saveData.option.seVolume;
        joystickVisible.isOn = GameDataManager.instance.saveData.option.isVisibleJoystick;

        volMaster.onValueChanged.AddListener((float value) =>
        {
            GameDataManager.instance.saveData.option.masterVolume = value;
            AudioManager.instance.OnChangeVolume();
        });

        volBGM.onValueChanged.AddListener((float value) =>
        {
            GameDataManager.instance.saveData.option.bgmVolume = value;
            AudioManager.instance.OnChangeVolume();
        });

        volSE.onValueChanged.AddListener((float value) =>
        {
            GameDataManager.instance.saveData.option.seVolume = value;
            AudioManager.instance.OnChangeVolume();
        });

        joystickVisible.onValueChanged.AddListener((bool isVisible) =>
        {
            GameDataManager.instance.saveData.option.isVisibleJoystick = isVisible;
        });
    }

    public void SelectFirst(){
        volMaster.gameObject.SelectObject();
    }
}
