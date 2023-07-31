using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class MainOption : MonoBehaviour
{
    Button buttonBack;
    public UnityAction closeAction;
    public GameObject disablePanel;
    public OptionSettings optionSettings;
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

    void OnEnable()
    {
        if (disablePanel == null) return;
        disablePanel.SetActive(false);
        optionSettings.SelectFirst();
    }
    void OnDisable()
    {
        if (disablePanel == null) return;
        disablePanel.SetActive(true);
    }
}
