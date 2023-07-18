using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormManager : MonoBehaviour
{
    string formLink = "Basic.Forms.Link";
    public Button buttonYes, buttonNo;

    private void Awake()
    {
        buttonYes.onClick.AddListener(GoFormLink);
        buttonNo.onClick.AddListener(Close);
    }

    void GoFormLink()
    {
        formLink.Localize().OpenURL();
        GetReward();
        gameObject.SetActive(false);
    }

    void GetReward()
    {
        GameDataManager.instance.saveData.userData.primoGem += 1600;
        GameDataManager.instance.SaveInstance();
    }

    void Close()
    {
        gameObject.SetActive(false);
    }
}