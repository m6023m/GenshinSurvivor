using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class MainOption : MonoBehaviour
{
    Button buttonBack;
    public UnityAction closeAction;
    void Awake()
    {
        buttonBack = GetComponentInChildren<Button>();
        buttonBack.onClick.AddListener(() =>
        {
            GameDataManager.instance.SaveInstance();
            closeAction.Invoke();
            gameObject.SetActive(false);
        });
        buttonBack.gameObject.SelectObject();
    }
}
