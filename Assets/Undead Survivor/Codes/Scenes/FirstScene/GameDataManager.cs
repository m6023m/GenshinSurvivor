using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;
    public CharacterData characterData;
    public WeaponData weaponData;
    public SaveData saveData;
    public bool isMobile = false;
    public static int version = 4;
    private void Awake()
    {

        instance = this;
        isMobile = UnityEngine.Device.SystemInfo.deviceType == UnityEngine.DeviceType.Handheld;
        saveData = SaveManager.Load();
        if (saveData == null)
        {
            saveData = new SaveData();
            saveData.InitCharacter();
            saveData.option.isVisibleJoystick = isMobile;
        }
        else
        {
            saveData.AddNewCharacterOnVersionChange(version, saveData.currentVersion);
        }
        saveData.InitWeapons();
        characterData.Init();
        weaponData.Init();
        if (saveData.userData.defaultChar != -1)
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    public void SaveInstance()
    {
        SaveManager.Save(GameDataManager.instance.saveData);
    }

}
