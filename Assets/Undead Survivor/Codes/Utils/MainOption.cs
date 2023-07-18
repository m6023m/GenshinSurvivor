using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainOption : MonoBehaviour
{
    Button buttonBack;
    void Awake()
    {
        buttonBack = GetComponentInChildren<Button>();
        buttonBack.onClick.AddListener(() =>
        {
            GameDataManager.instance.SaveInstance();
            gameObject.SetActive(false);
        });
    }
}
