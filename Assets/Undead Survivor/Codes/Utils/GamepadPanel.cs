using System;
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
public class GamepadPanel : MonoBehaviour
{
    RectTransform panel;  // The UI panel
    public GamepadData gamepadData;
    public Vector2 hiddenPosition;  // The position of the panel when the gamepad is disconnected
    public Vector2 visiblePosition;  // The position of the panel when the gamepad is connected
    private int lastCount;
    GamepadUI[] panelGamepadUIs;
    int index = 0;
    [Serializable]
    public struct GamepadPair
    {
        public GamepadData.GamepadType Key;
        public string Value;
    }

    [ShowInInspector]
    public List<GamepadPair> gamepadUIs = new List<GamepadPair>();
    void Awake()
    {
        panel = GetComponent<RectTransform>();
        lastCount = Input.GetJoystickNames().Length;
        panelGamepadUIs = GetComponentsInChildren<GamepadUI>(true);
        InitPanel();
    }
    public void InitPanel(List<GamepadPair> gamepadUIs)
    {
        this.gamepadUIs = gamepadUIs;
        InitPanel();
    }

    public void InitPanel()
    {
        foreach (GamepadUI gamepadUI in panelGamepadUIs)
        {
            gamepadUI.gameObject.SetActive(false);
        }
        index = 0;
        foreach (GamepadPair gamepadUI in gamepadUIs)
        {
            panelGamepadUIs[index].gameObject.SetActive(true);
            Sprite gamepadTexture = gamepadData.Get(gamepadUI.Key).gamepadSprite;
            panelGamepadUIs[index].sprite = gamepadTexture;
            panelGamepadUIs[index].text = gamepadUI.Value.Localize();
            index++;
        }
    }

    void Update()
    {
        int count = Input.GetJoystickNames().Length;

        if (count != lastCount)
        {
            if (count > lastCount)
            {
                // If a gamepad is connected, move the panel to the visible position
                panel.anchoredPosition = visiblePosition;
            }
            else
            {
                // If a gamepad is disconnected, move the panel to the hidden position
                panel.anchoredPosition = hiddenPosition;
            }

            lastCount = count;
        }
    }
}
