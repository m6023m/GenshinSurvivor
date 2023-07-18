using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchieveDiscription : MonoBehaviour
{
    TextMeshProUGUI _achieveDiscription;
    public string text
    {
        get
        {
            if (_achieveDiscription == null)
            {
                _achieveDiscription = GetComponent<TextMeshProUGUI>();
            }
            return _achieveDiscription.text;
        }
        set
        {
            if (_achieveDiscription == null)
            {
                _achieveDiscription = GetComponent<TextMeshProUGUI>();
            }
            _achieveDiscription.text = value;
        }
    }
}