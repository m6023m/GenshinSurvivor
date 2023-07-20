using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    public Sprite defaultSprite;
    public WeaponData weaponData;
    public WeaponData.Parameter selectedWeaponData;
    WeaponButton selectedWeaponButton;
    public UnityAction<WeaponData.Parameter> onSelect;

    private static float defalutPrice = 230.0f;
    private static float addPrice = 20.0f;
    private static int maxWeaponLevel = 90;


    [Header("# UI")]
    public GameObject weaponGridPanel;
    public Image weaponDiscriptionImage;
    public TextMeshProUGUI weaponDiscription;
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI mora;
    public CanvasGroup canvasGroup;
    public Button buttonUpgrade, buttonSelect;
    LongClickButton longClickUpgrade;
    public WeaponButton[] weaponButtons;
    int buttonIndex = 0;
    public GameObject activeGroup;
    private void Awake()
    {
        instance = this;
        InitButton();
    }

    void DisableAllWeapon()
    {
        buttonIndex = 0;
        foreach (WeaponButton weaponButton in weaponButtons)
        {
            weaponButton.gameObject.SetActive(false);
        }
        selectedWeaponData = null;
        weaponDiscriptionImage.sprite = defaultSprite;
        weaponDiscription.text = "";
        weaponName.text = "";

    }

    public void InitUI()
    {
        SetEnable(true);
        DisableAllWeapon();
        WriteMora();
        SetupWeaponObjects(WeaponData.Type.Rare);
        SetupWeaponObjects(WeaponData.Type.Normal);
        SetupWeaponObjectsOnNotMine(WeaponData.Type.Rare);
        SetupWeaponObjectsOnNotMine(WeaponData.Type.Normal);
        VisibleSelectButton(false);
    }

    public void InitUIWithPlayer(WeaponType weaponType)
    {
        SetEnable(true);
        DisableAllWeapon();
        WriteMora();
        SetupWeaponPlayer(weaponType);
        VisibleSelectButton(true);
    }
    void WriteMora()
    {
        mora.text = GameDataManager.instance.saveData.userData.mora.ToString("N0");
    }
    void InitButton()
    {
        longClickUpgrade = buttonUpgrade.GetComponent<LongClickButton>();
        longClickUpgrade.onLongClick = () =>
        {
            if (selectedWeaponData == null) return;
            if (selectedWeaponData.stat.level >= maxWeaponLevel) return;
            float upgradePrice = defalutPrice.CalcUpgradeWeapon(addPrice, selectedWeaponData.stat.level, maxWeaponLevel, GameDataManager.instance.saveData.userData.mora);
            int upgradeLevel = defalutPrice.CalcUpgradeWeaponLevel(addPrice, selectedWeaponData.stat.level, maxWeaponLevel, GameDataManager.instance.saveData.userData.mora);
            UpgradeWeapon(upgradePrice, upgradeLevel);
        };
        longClickUpgrade.onClick = () =>
        {
            if (selectedWeaponData == null) return;
            if (selectedWeaponData.stat.level >= maxWeaponLevel) return;
            float upgradePrice = defalutPrice.CalcUpgradeWeapon(addPrice, selectedWeaponData.stat.level, selectedWeaponData.stat.level + 1, GameDataManager.instance.saveData.userData.mora);
            int upgradeLevel = selectedWeaponData.stat.level + 1;
            UpgradeWeapon(upgradePrice, upgradeLevel);
        };
        longClickUpgrade.interactable = false;
        longClickUpgrade.onLongClickStart = () =>
        {
            if (selectedWeaponData == null) return;
            ChangeUpgradePrice(defalutPrice.CalcUpgradeWeapon(addPrice, selectedWeaponData.stat.level, maxWeaponLevel, GameDataManager.instance.saveData.userData.mora));
        };
        longClickUpgrade.onLongClickCancel = () =>
        {
            if (selectedWeaponData == null) return;
            ChangeUpgradePrice(defalutPrice.CalcUpgradeWeapon(addPrice, selectedWeaponData.stat.level, selectedWeaponData.stat.level + 1, GameDataManager.instance.saveData.userData.mora));
        };

        buttonSelect.onClick.AddListener(() =>
        {
            if (selectedWeaponData == null)
            {
                onSelect.Invoke(null);
                SetEnable(false);
                return;
            }
            if (onSelect != null)
            {
                onSelect.Invoke(selectedWeaponData);
                SetEnable(false);
            }
        });
    }

    void UpgradeWeapon(float price, int level)
    {
        if (GameDataManager.instance.saveData.userData.mora < price) return;
        selectedWeaponData.stat.level = level;
        selectedWeaponButton.Reset();
        GameDataManager.instance.saveData.userData.mora -= (int)price;
        GameDataManager.instance.SaveInstance();
        WriteMora();
        UpdateWeaponUI(selectedWeaponData);
    }

    void ChangeUpgradePrice(float calcMora)
    {
        buttonUpgrade.GetComponentsInChildren<TextMeshProUGUI>()[1].text = calcMora.ToString("N0");
    }


    private void SetupWeaponPlayer(WeaponType rarityType)
    {
        for (int i = 0; i < weaponData.weapons.Count; i++)
        {
            int idx = i;
            WeaponData.Parameter weapon = weaponData.weapons[idx];
            if (weapon.weaponType != rarityType) continue;
            if (weapon.stat.rank != 0)
            {
                WeaponButton weaponButton = weaponButtons[buttonIndex];
                weaponButton.gameObject.SetActive(true);
                SetupWeaponButton(weaponButton, weapon, idx);
                buttonIndex++;
            }
        }
    }
    private void SetupWeaponObjects(WeaponData.Type rarityType)
    {
        for (int i = 0; i < weaponData.weapons.Count; i++)
        {
            int idx = i;
            WeaponData.Parameter weapon = weaponData.weapons[idx];
            if (weapon.type != rarityType) continue;
            if (weapon.stat.rank != 0)
            {
                WeaponButton weaponButton = weaponButtons[buttonIndex];
                weaponButton.gameObject.SetActive(true);
                SetupWeaponButton(weaponButton, weapon, idx);
                buttonIndex++;
            }
        }
    }
    private void SetupWeaponObjectsOnNotMine(WeaponData.Type rarityType)
    {
        for (int i = 0; i < weaponData.weapons.Count; i++)
        {
            int idx = i;
            WeaponData.Parameter weapon = weaponData.weapons[idx];
            if (weapon.type != rarityType) continue;
            if (weapon.stat.rank == 0)
            {
                WeaponButton weaponButton = weaponButtons[buttonIndex];
                weaponButton.gameObject.SetActive(true);
                SetupWeaponButton(weaponButton, weapon, idx);
                buttonIndex++;
            }
        }
    }
    private void SetupWeaponButton(WeaponButton weaponButton, WeaponData.Parameter weapon, int idx)
    {
        weaponButton.Init(weapon, idx, (selectedWeapon, selectedIndex
        ) =>
        {
            selectedWeaponButton = weaponButton;
            HandleSelectedWeapon(selectedWeapon);
        });
    }

    private void HandleSelectedWeapon(WeaponData.Parameter selectedWeapon)
    {
        Sprite selectedWeaponIcon = selectedWeapon.icon;

        UpdateWeaponUI(selectedWeapon);
        if (selectedWeapon.stat.rank == 0) return;
        selectedWeaponData = selectedWeapon;
    }

    private void UpdateWeaponUI(WeaponData.Parameter selectedWeapon)
    {
        string name = "Weapon.".AddString(selectedWeapon.weaponName.ToString()).Localize();
        string discription = selectedWeapon.Tooltip;
        Sprite selectedWeaponIcon = selectedWeapon.icon;

        weaponDiscriptionImage.sprite = selectedWeaponIcon;
        weaponDiscription.text = discription;
        weaponName.text = name;

        longClickUpgrade.interactable = selectedWeapon.stat.level < maxWeaponLevel;
        if (selectedWeapon.stat.rank == 0)
        {
            longClickUpgrade.interactable = false;
        }

        ChangeUpgradePrice(defalutPrice.CalcUpgradeWeapon(addPrice, selectedWeapon.stat.level, selectedWeapon.stat.level + 1, GameDataManager.instance.saveData.userData.mora));
    }

    public void SetEnable(bool enabled)
    {
        canvasGroup.alpha = enabled ? 1.0f : 0.0f;
        canvasGroup.blocksRaycasts = enabled;
        activeGroup.SetActive(enabled);
    }

    public void VisibleSelectButton(bool visible)
    {
        buttonSelect.gameObject.SetActive(visible);
    }
}
