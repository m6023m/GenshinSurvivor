using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchieveTitle : MonoBehaviour
{
    TextMeshProUGUI _achieveTitle;
    public string text
    {
        get
        {
            if (_achieveTitle == null)
            {
                _achieveTitle = GetComponent<TextMeshProUGUI>();
            }
            return _achieveTitle.text;
        }
        set
        {
            if (_achieveTitle == null)
            {
                _achieveTitle = GetComponent<TextMeshProUGUI>();
            }
            _achieveTitle.text = value;
        }
    }
}