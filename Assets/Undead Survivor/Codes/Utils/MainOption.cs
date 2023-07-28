using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class MainOption : MonoBehaviour
{
    Button buttonBack;
    EventSystem eventSystem;
    public UnityAction closeAction;
    void Awake()
    {
        eventSystem = EventSystem.current;
        buttonBack = GetComponentInChildren<Button>();
        buttonBack.onClick.AddListener(() =>
        {
            GameDataManager.instance.SaveInstance();
            closeAction.Invoke();
            gameObject.SetActive(false);
        });
        eventSystem.SetSelectedGameObject(buttonBack.gameObject);
    }
}
