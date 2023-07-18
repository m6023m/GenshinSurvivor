using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Discord : MonoBehaviour
{
    Button button;
    string discordLink = "https://discord.gg/4Ewqn9xxVP";

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickDiscord);
    }

    void OnClickDiscord()
    {
        discordLink.OpenURL();
    }
}
