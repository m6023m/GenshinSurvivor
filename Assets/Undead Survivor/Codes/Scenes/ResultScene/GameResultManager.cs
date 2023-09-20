using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameResultManager : MonoBehaviour
{
    public Button btnNext;
    public Transform resultLeft;
    public Transform resultRight;
    BattleResult battleResult;
    public SkillData skillData;
    ImageText[] imageTextsLeft;
    ImageText[] imageTextsRight;
    public Sprite[] textIconsLeft;
    public FormManager formManager;
    void Awake()
    {
        btnNext.onClick.AddListener(OnClickNext);
        Init();
    }

    void Init()
    {
        battleResult = GameDataManager.instance.saveData.gameInfoData.battleResult;
        imageTextsLeft = resultLeft.GetComponentsInChildren<ImageText>(true);
        imageTextsRight = resultRight.GetComponentsInChildren<ImageText>(true);
        WriteRecords();
        WriteGetItems();
        WriteDamageDealt();
        CheckForm();
    }

    void WriteRecords()
    {

        int hours = (int)(battleResult.suvivorTime / 3600);
        int minutes = (int)((battleResult.suvivorTime % 3600) / 60);
        int seconds = (int)(battleResult.suvivorTime % 60);


        WriteImageTextLeft(0, "Basic.Record".Localize(), null);
        WriteImageTextLeft(1, "Basic.SurvivorTime".Localize(hours, minutes, seconds), textIconsLeft[0]);
        WriteImageTextLeft(2, "Basic.Kill".Localize(battleResult.kill), textIconsLeft[1]);
        WriteImageTextLeft(3, "Basic.Level".Localize(battleResult.level), textIconsLeft[2]);
        WriteImageTextLeft(4, "Basic.ReceiveDamage".Localize(battleResult.receiveDamage), textIconsLeft[3]);
        WriteImageTextLeft(5, "Basic.HealHealth".Localize(battleResult.healHealth), textIconsLeft[4]);
        WriteImageTextLeft(6, "Basic.GainMora".Localize(battleResult.gainMora), textIconsLeft[5]);
        WriteImageTextLeft(7, "Basic.GainPrimoGem".Localize(battleResult.gainPrimoGem), textIconsLeft[6]);

    }

    void WriteGetItems()
    {

    }

    void WriteDamageDealt()
    {
        WriteImageTextRight(0, "Basic.DamageDelat".Localize(), null);
        int index = 1;

        Sprite skillIconWeapon = null;
        float damageWeapon = 0;
        foreach (KeyValuePair<SkillName, float> entry in battleResult.skillDamageSet)
        {
            if (entry.Key.ToString().Contains("Weapon"))
            {
                skillIconWeapon = skillData.skillsDictionary[entry.Key].icon;
                damageWeapon += entry.Value;
            }
            else
            {
                Sprite skillIcon = skillData.skillsDictionary[entry.Key].icon;
                string skillName = "Skill.".AddString(entry.Key.ToString()).Localize();
                string skillDamage = string.Format("{0:F0}", entry.Value);
                string text = skillName.AddString(" : ", skillDamage.ToString());
                WriteImageTextRight(index, text, skillIcon);
                index++;
            }
        }
        string skillWeapon = ("Basic.Weapon").Localize();
        string damageTextWeapon = string.Format("{0:F0}", damageWeapon);
        string textWeapon = skillWeapon.AddString(" : ", damageTextWeapon.ToString());
        WriteImageTextRight(index, textWeapon, skillIconWeapon);
    }

    void CheckForm()
    {
        if (GameDataManager.instance.saveData.record.formPlayCount != 10) return;
        formManager.gameObject.SetActive(true);
    }
    void OnClickNext()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        LoadingScreenController.instance.LoadScene("MainScene");
    }

    void WriteImageTextLeft(int index, string text, Sprite sprite)
    {
        WriteImageText(index, imageTextsLeft, text, sprite);
    }
    void WriteImageTextRight(int index, string text, Sprite sprite)
    {
        WriteImageText(index, imageTextsRight, text, sprite);
    }

    void WriteImageText(int index, ImageText[] imageTexts, string text, Sprite sprite)
    {
        if (!imageTexts[index].gameObject.activeSelf)
        {
            imageTexts[index].gameObject.SetActive(true);
        }
        imageTexts[index].textMesh.text = text;
        if (sprite != null)
        {
            imageTexts[index].image.sprite = sprite;
        }
        else
        {
            imageTexts[index].image.color = new Color(1, 1, 1, 0);
        }
    }
}
