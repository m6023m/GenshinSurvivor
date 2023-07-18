using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponButton : SlotButton
{
    private TextMeshProUGUI rank;
    WeaponData.Parameter weapon;
    int index;
    System.Action<WeaponData.Parameter, int> onClickAction;
    public void Init(WeaponData.Parameter weapon, int index, System.Action<WeaponData.Parameter, int> onClickAction)
    {
        rank = GetComponentsInChildren<TextMeshProUGUI>()[1];
        this.weapon = weapon;
        this.index = index;
        this.onClickAction = onClickAction;
        Reset();
    }

    public void Reset()
    {
        image.sprite = weapon.icon;
        rank.text = "".AddString(weapon.stat.rank.ToString());
        title.text = "Lv.".AddString(weapon.stat.level.ToString());
        if (weapon.stat.rank == 0)
        {
            SetDisable();
        }

        if (weapon.type != WeaponData.Type.None)
        {
            WeaponData.Type rarityType = GameDataManager.instance.weaponData.Get(weapon.weaponName).type;
            rarityBackgroundImage.sprite = raritySprite[(int)rarityType];
        }
        button.onClick.AddListener(() =>
        {
            AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
            onClickAction?.Invoke(weapon, index);
        });
    }
}

