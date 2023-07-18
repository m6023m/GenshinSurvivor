using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchieveToggle : MonoBehaviour
{
    AchieveTitle _achieveTitle;
    AchieveDiscription _achieveDiscription;
    AchieveCount _achieveCount;
    public AchieveTitle achieveTitle
    {
        get
        {
            if (_achieveTitle == null)
            {
                _achieveTitle = GetComponentInChildren<AchieveTitle>(true);
            }
            return _achieveTitle;
        }
    }
    public AchieveDiscription achieveDiscription
    {
        get
        {
            if (_achieveDiscription == null)
            {
                _achieveDiscription = GetComponentInChildren<AchieveDiscription>(true);
            }
            return _achieveDiscription;
        }
    }
    public AchieveCount achieveCount
    {
        get
        {
            if (_achieveCount == null)
            {
                _achieveCount = GetComponentInChildren<AchieveCount>(true);
            }
            return _achieveCount;
        }
    }

}