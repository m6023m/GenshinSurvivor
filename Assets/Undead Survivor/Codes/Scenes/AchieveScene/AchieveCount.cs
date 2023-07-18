using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchieveCount : MonoBehaviour
{
    TextMeshProUGUI _achieveCount;
    public string text
    {
        get
        {
            if (_achieveCount == null)
            {
                _achieveCount = GetComponent<TextMeshProUGUI>();
            }
            return _achieveCount.text;
        }
        set
        {
            if (_achieveCount == null)
            {
                _achieveCount = GetComponent<TextMeshProUGUI>();
            }
            _achieveCount.text = value;
        }
    }
}