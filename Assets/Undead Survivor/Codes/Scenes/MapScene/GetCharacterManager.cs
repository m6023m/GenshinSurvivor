using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GetCharacterManager : MonoBehaviour
{
    public Button panelGetCharacter;
    public Image wishImage;
    public TextMeshProUGUI textName;
    private void Awake()
    {
        panelGetCharacter.onClick.AddListener(Close);
    }
    public void DisplayGetCharacter(CharacterData.ParameterWithKey characterData)
    {
        GameManager.instance.Pause(true);
        panelGetCharacter.gameObject.SetActive(true);
        Sprite characterSprite = characterData.wishImage;
        wishImage.sprite = characterSprite;
        textName.text = "Character.".AddString(characterData.name.ToString()).Localize();
    }

    void Close()
    {
        GameManager.instance.Pause(false);
        panelGetCharacter.gameObject.SetActive(false);
    }
}