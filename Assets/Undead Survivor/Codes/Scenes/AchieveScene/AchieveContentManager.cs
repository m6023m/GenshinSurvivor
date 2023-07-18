using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchieveContentManager : MonoBehaviour
{
    public RectTransform content;
    AchieveData achieveData;
    AchieveToggle[] _achieveToggles;
    AchieveToggle[] achieveToggles
    {
        get
        {
            if (_achieveToggles == null)
            {
                _achieveToggles = content.GetComponentsInChildren<AchieveToggle>(true);
            }
            return _achieveToggles;
        }
    }

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        achieveData = GameDataManager.instance.saveData.achieveData;
        InitAchieveData();
    }

    void InitAchieveData()
    {
        int index = 0;
        foreach (KeyValuePair<Achieve, int> achieve in GameDataManager.instance.saveData.achieveData.allAchieves)
        {
            AchieveToggle achieveToggle = achieveToggles[index];
            if (!achieveToggle.gameObject.activeSelf)
            {
                achieveToggle.gameObject.SetActive(true);
            }
            achieveToggle.achieveTitle.text = achieve.Key.GetLocalizationName();
            achieveToggle.achieveDiscription.text = achieve.Key.GetLocalizationExpanation();
            achieveToggle.achieveCount.text = achieve.Value.ToString().AddString(" / ", achieve.Key.GetMaxAchieveCount().ToString());
            index++;
        }
    }
}