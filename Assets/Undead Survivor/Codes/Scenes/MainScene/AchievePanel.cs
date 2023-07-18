using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievePanel : MonoBehaviour
{
    TextMeshProUGUI _achieveTitle;
    public string text
    {
        get
        {
            if (_achieveTitle == null)
            {
                _achieveTitle = GetComponentInChildren<TextMeshProUGUI>(true);
            }
            return _achieveTitle.text;
        }
        set
        {
            if (_achieveTitle == null)
            {
                _achieveTitle = GetComponentInChildren<TextMeshProUGUI>(true);
            }
            _achieveTitle.text = value;
        }
    }
}