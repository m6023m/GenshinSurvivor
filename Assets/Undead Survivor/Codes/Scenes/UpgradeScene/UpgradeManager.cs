using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public Image imgDiscription;
    public TextMeshProUGUI discription;
    public TextMeshProUGUI mora;
    public TextMeshProUGUI priceDiscription;
    private Upgrade upgrade;

    public GameObject panel;

    public GameObject togglePrefab;

    public Button btnBack;

    public Button btnUpgrade;

    public Button btnReset;

    public Button[] upgrades;

    private int selectNum = -1;

    private List<List<GameObject>> togglesArray;

    private void Awake()
    {
        upgrade = GameDataManager.instance.saveData.userData.upgrade;
        if (upgrade.upgradeComponents == null)
        {
            ResetUpgrade();
        }

        Init();

        btnBack.onClick.AddListener(OnClickBack);
        btnReset.onClick.AddListener(OnClickReset);
        btnUpgrade.onClick.AddListener(OnClickUpgrade);
    }

    void OnClickBack()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        MoveMain();
    }
    void OnClickReset()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        ResetUpgrade();
    }

    void OnClickUpgrade()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        if (selectNum == -1) return;
        UpgradeComponent upComp = upgrade.upgradeComponents[selectNum];
        if (upComp.max == upComp.cnt) return;
        if(GameDataManager.instance.saveData.userData.mora < upComp.price) return;
        upComp.cnt += 1;
        List<GameObject> tArray = togglesArray[selectNum];
        tArray[upComp.cnt - 1].GetComponent<Toggle>().isOn = true;
        GameDataManager.instance.saveData.userData.upgrade = upgrade;
        GameDataManager.instance.saveData.userData.mora -= upComp.price;
        WriteMora();
        GameDataManager.instance.SaveInstance();
    }

    void Init()
    {
        InitUI();
        InitData();
    }

    void InitUI()
    {
        togglesArray = new List<List<GameObject>>();
        upgrades = panel.GetComponentsInChildren<Button>();
        for (int i = 0; i < upgrades.Length; i++)
        {
            int idx = i;
            UpgradeComponent upComponent = upgrade.upgradeComponents[idx];
            upgrades[idx].onClick.AddListener(() =>
            {
                AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
                selectNum = idx;
                discription.text = string.Format(upComponent.GetDiscription(), upComponent.unit);
                Image image = upgrades[idx].GetComponentsInChildren<Image>()[1];
                imgDiscription.sprite = image.sprite;
                priceDiscription.text = upComponent.price.ToString("N0");

                btnUpgrade.interactable = GameDataManager.instance.saveData.userData.mora > upComponent.price;
            });
            List<GameObject> toggles = new List<GameObject>();
            for (int j = 0; j < upComponent.max; j++)
            {
                GameObject toggle = Instantiate(togglePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                GridLayoutGroup upgradeCounter = upgrades[idx].GetComponentInChildren<GridLayoutGroup>();
                toggle.transform.parent = upgradeCounter.transform;
                toggles.Add(toggle);
            }
            togglesArray.Add(toggles);
        }
    }

    void InitData()
    {
        WriteMora();
        upgrades = panel.GetComponentsInChildren<Button>();
        for (int i = 0; i < upgrades.Length; i++)
        {
            int idx = i;
            UpgradeComponent upComponent = upgrade.upgradeComponents[idx];
            List<GameObject> toggles = togglesArray[idx];
            for (int j = 0; j < upComponent.max; j++)
            {
                GameObject toggle = toggles[j];
                toggle.GetComponent<Toggle>().isOn = j < upgrade.upgradeComponents[idx].cnt;
            }
        }
    }
    void MoveMain()
    {
        LoadingScreenController.instance.LoadScene("MainScene");
    }

    void WriteMora()
    {
        mora.text = GameDataManager.instance.saveData.userData.mora.ToString("N0");
    }

    void ResetUpgrade()
    {
        long returnMora = 0;
        foreach (UpgradeComponent component in upgrade.upgradeComponents)
        {
            returnMora += (component.cnt * component.price);
        }
        upgrade = new Upgrade();
        GameDataManager.instance.saveData.userData.mora += returnMora;
        GameDataManager.instance.saveData.userData.upgrade = upgrade;
        GameDataManager.instance.SaveInstance();
        InitData();
    }
}
