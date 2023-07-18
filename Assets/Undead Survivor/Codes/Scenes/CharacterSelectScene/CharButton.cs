using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharButton : SlotButton
{
    public void Init(Sprite charSprite, string charName, Character character, int characterIndex, System.Action<Character, int> onClickAction)
    {
        image.sprite = charSprite;
        title.text = charName;
        if (!character.isMine)
        {
            SetDisable();
        }
        if (characterIndex == UserData.AETHER || characterIndex == UserData.LUMINE)
        {
            title.text = GameDataManager.instance.saveData.userData.name;
            rarityBackgroundImage.sprite = raritySprite[1];
        }
        else
        {
            CharacterData.Type rarityType = GameDataManager.instance.characterData.Get(character.charNum).type;
            rarityBackgroundImage.sprite = raritySprite[(int)rarityType];
        }
        button.onClick.AddListener(() =>
        {
            AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
            onClickAction?.Invoke(character, characterIndex);
        });
    }
}

