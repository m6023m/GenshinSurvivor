using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AchieveManager : MonoBehaviour
{

    Dictionary<Achieve, int> checkAchieves;
    public static AchieveManager instance;
    public AchievePanel achievePanel;
    CanvasGroup _canvasGroup;
    CanvasGroup canvasGroup
    {
        get
        {
            if (_canvasGroup == null)
            {
                _canvasGroup = achievePanel.GetComponent<CanvasGroup>();
            }
            return _canvasGroup;
        }
    }
    Queue<Achieve> _achievedQueue;
    Queue<Achieve> achievedQueue
    {
        get
        {
            if (_achievedQueue == null)
            {
                _achievedQueue = new Queue<Achieve>();
            }
            return _achievedQueue;
        }
    }
    private void Awake()
    {
        instance = this;
        checkAchieves = GameDataManager.instance.saveData.achieveData.GetCheckAchieveList();
        StartCoroutine(VisibleAchieveQuques());
        DontDestroyOnLoad(gameObject);
    }

    private void LateUpdate()
    {
        List<Achieve> achievesToCheck = new List<Achieve>();

        foreach (KeyValuePair<Achieve, int> achieve in checkAchieves)
        {
            achievesToCheck.Add(achieve.Key);
        }
        foreach (Achieve achieve in achievesToCheck)
        {
            CheckAchieve(achieve);
        }
    }

    private void CheckAchieve(Achieve achieve)
    {
        if (!GameDataManager.instance.saveData.achieveData.AvailabeCheckAchieve(achieve)) return;
        bool isAchieve = false;

        switch (achieve)
        {
            case Achieve.Lead_Wind_6:
                isAchieve = Lead_Wind_6();
                break;
            case Achieve.Dead_1557_1:
                isAchieve = Dead_1557_1();
                break;
            case Achieve.Game_Start_1:
                isAchieve = Game_Start_1();
                break;
            case Achieve.Tree_Chicken_1:
                isAchieve = Tree_Chicken_1();
                break;
        }
        achieve.GetLocalizationExpanation();
        if (isAchieve)
        {
            GameDataManager.instance.saveData.achieveData.AttainmentAchieve(achieve, checkAchieves);
            GameDataManager.instance.SaveInstance();
            AddAchieveQueue(achieve);
        }
    }

    void AddAchieveQueue(Achieve achieve)
    {
        achievedQueue.Enqueue(achieve);
    }
    IEnumerator VisibleAchieveQuques()
    {
        while (true)
        {
            if (achievedQueue.Count == 0)
            {
                yield return new WaitForSecondsRealtime(1.0f);
            }
            else
            {
                Achieve achieve = achievedQueue.Dequeue();
                canvasGroup.alpha = 1.0f;
                achievePanel.text = achieve.GetLocalizationName();
                yield return new WaitForSecondsRealtime(3.0f);
                canvasGroup.alpha = 0.0f;
            }
        }
    }
    bool Lead_Wind_6()
    {
        int gameClearCount = GameDataManager.instance.saveData.record.gameClearCount;
        if (gameClearCount > checkAchieves[Achieve.Lead_Wind_6])
        {
            int defalutCharacter = GameDataManager.instance.saveData.userData.defaultChar;
            GameDataManager.instance.saveData.charactors[defalutCharacter].constellaCount++;
            return true;
        }
        return false;
    }

    bool Dead_1557_1()
    {
        if (GameManager.instance == null) return false;

        float gameTime = GameManager.instance.gameInfoData.gameTime;
        bool isLive = GameManager.instance.player.isLive;
        if (957 <= gameTime && gameTime <= 964 && !isLive) //15:57 ~ 16:04 on dead 
        {
            GameDataManager.instance.saveData.userData.mora += 1557;
            return true;
        }
        return false;
    }

    bool Game_Start_1()
    {
        if (GameManager.instance == null) return false;
        int gamePlayCount = GameDataManager.instance.saveData.record.playCount;
        if (gamePlayCount > checkAchieves[Achieve.Game_Start_1])
        {
            GameDataManager.instance.saveData.userData.primoGem += 1600;
            return true;
        }
        return false;
    }
    private bool Tree_Chicken_1()
    {
        int healItemCount = GameDataManager.instance.saveData.record.healItemCount;
        if (healItemCount >= 100)
        {
            GameDataManager.instance.saveData.userData.mora += 1000;
            return true;
        }
        return false;
    }

}