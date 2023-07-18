using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetCharacter : MonoBehaviour
{
    public CharacterData.Name getCharacterName;
    GetCharacterManager getCharacterManager;
    private void Awake()
    {
        getCharacterManager = GetComponentInParent<GetCharacterManager>();
        foreach (CharacterData.Name getCharacter in GameDataManager.instance.saveData.userData.getCharacters)
        {
            if (getCharacter == getCharacterName)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        Get();
    }

    void Get()
    {
        Character getCharacter = GameDataManager.instance.saveData.GetCharacter(getCharacterName.ToString());
        if (!getCharacter.isMine)
        {
            getCharacter.isMine = true;
            GameDataManager.instance.saveData.record.getCharacterCount++;
        }
        else if (getCharacter.constellaCount < 6)
        {
            getCharacter.constellaCount++;
        }
        else
        {
            GameDataManager.instance.saveData.record.getTotalMora += 500;
            GameDataManager.instance.saveData.userData.mora += 500;
        }
        GameDataManager.instance.saveData.userData.getCharacters.Add(getCharacterName);
        CharacterData.ParameterWithKey characterData = GameDataManager.instance.characterData.Get(getCharacterName);
        getCharacterManager.DisplayGetCharacter(characterData);
        gameObject.SetActive(false);
        GameDataManager.instance.SaveInstance();
    }
}