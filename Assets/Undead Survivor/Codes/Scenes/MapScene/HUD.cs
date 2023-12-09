using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HUD : MonoBehaviour
{
    public enum InfoType
    {
        EXP,
        Level,
        Kill,
        Time,
        Health,
        Mora,
        PrimoGem,
        BossHealth,
        BossName
    }
    public InfoType type;

    TextMeshProUGUI text;
    Slider slider;
    CanvasGroup canvasGroup;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        slider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        InfoManager infoManager = GameManager.instance.infoManager;
        Player player = GameManager.instance.player;
        GameInfoData gameInfoData = GameManager.instance.gameInfoData;

        float enableAlpha = GameManager.instance.boss.isEnable ? 255f : 0;
        switch (type)
        {
            case InfoType.EXP:
                float curExp = gameInfoData.exp;
                float maxExp = GameManager.instance.ReqExp(gameInfoData.level);
                float progressExp = (curExp / maxExp) * 100;
                slider.value = progressExp;
                break;
            case InfoType.Level:
                text.text = string.Format("Lv.{0:D3}", gameInfoData.level);
                break;
            case InfoType.Kill:
                text.text = string.Format("{0}", gameInfoData.kill);
                break;
            case InfoType.Time:
                float time = GameManager.instance.gameInfoData.gameTime;
                int min = Mathf.FloorToInt(time / 60);
                int sec = Mathf.FloorToInt(time % 60);
                text.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case InfoType.Health:
                float curHP = player.health;
                float maxHP = GameManager.instance.statCalculator.Health;
                float progressHP = (curHP / maxHP) * 100;
                slider.value = progressHP;
                break;
            case InfoType.Mora:
                text.text = string.Format("{0}", gameInfoData.mora);
                break;
            case InfoType.PrimoGem:
                text.text = string.Format("{0}", gameInfoData.primoGem);
                break;
            case InfoType.BossName:
                text.color = new Color(255, 255, 255, enableAlpha);
                text.text = "Boss.Name.".AddString(GameManager.instance.boss.bossName.ToString()).Localize();
                break;
            case InfoType.BossHealth:
                CanvasGroup canvasGroup = slider.GetComponent<CanvasGroup>();
                canvasGroup.alpha = enableAlpha;
                float currentBossHP = GameManager.instance.boss.health;
                float currentBossMaxHP = GameManager.instance.boss.maxHealth;
                float progressBossHP = (currentBossHP / currentBossMaxHP) * 100;
                slider.value = progressBossHP;
                break;
        }
    }

}