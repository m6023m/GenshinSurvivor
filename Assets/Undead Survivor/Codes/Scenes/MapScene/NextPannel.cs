using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextPannel : MonoBehaviour
{
    TextMeshProUGUI gameWinLoseStatus;
    private void Awake()
    {
        gameWinLoseStatus = GetComponentInChildren<TextMeshProUGUI>();

    }

    private void OnEnable()
    {
        if (GameManager.instance.IsVictory)
        {
            gameWinLoseStatus.text = "Basic.Victory".Localize();
        }
        else
        {
            gameWinLoseStatus.text = "Basic.Lose".Localize();
        }
    }
}