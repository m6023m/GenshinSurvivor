using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoManager : MonoBehaviour
{
    [Header("# Game Object")]
    public Transform contentDiscription;
    public Transform contentSkills;
    ImageText[] imageTexts;
    SkillInfoText[] skillInfoTexts;
    public Sprite[] textIcons;
    public Sprite[] elementIcons;
    int skillCount = 0;
    void Awake()
    {
        imageTexts = contentDiscription.GetComponentsInChildren<ImageText>(true);
        skillInfoTexts = contentSkills.GetComponentsInChildren<SkillInfoText>(true);
    }

    public void Init()
    {
        UpdateTooltip(GameManager.instance.statCalcuator.ToolTip());
        UpdateInfo();
        SetTooltipStatInfo();
    }

    void UpdateTooltip(List<string> tooltips)
    {
        for (int i = 0; i < tooltips.Count; i++)
        {
            if (!imageTexts[i].gameObject.activeSelf)
            {
                imageTexts[i].gameObject.SetActive(true);
            }
            imageTexts[i].textMesh.text = tooltips[i];
            if (i < textIcons.Length)
            {
                imageTexts[i + 1].image.sprite = textIcons[i];
            }
            else
            {
                imageTexts[i].image.color = new Color(1, 1, 1, 0);
            }
        }

        int elementSprite = (int)GameManager.instance.player.stat.elementType;
        imageTexts[0].image.sprite = elementIcons[elementSprite];
    }

    void UpdateInfo()
    {
        skillCount = 0;
        UpdateSkill();
        UpdateBurst();
        UpdateArtifact();
    }

    void UpdateSkill()
    {
        if (GameManager.instance.ownSkills == null) return;
        foreach (SkillData.ParameterWithKey skill in GameManager.instance.ownSkills)
        {
            skillInfoTexts[skillCount].gameObject.SetActive(true);
            skillInfoTexts[skillCount].Init(skill.name, ArtifactName.Artifact_None);
            skillCount++;
        }
    }

    void UpdateBurst()
    {
        if (GameManager.instance.ownBursts == null) return;

        foreach (SkillData.ParameterWithKey skill in GameManager.instance.ownBursts)
        {
            skillInfoTexts[skillCount].gameObject.SetActive(true);
            skillInfoTexts[skillCount].Init(skill.name, ArtifactName.Artifact_None);
            skillCount++;
        }
    }

    void UpdateArtifact()
    {
        if (GameManager.instance.ownArtifacts == null) return;

        foreach (ArtifactData.ParameterWithKey artifact in GameManager.instance.ownArtifacts)
        {
            skillInfoTexts[skillCount].gameObject.SetActive(true);
            skillInfoTexts[skillCount].Init(SkillName.Skill_None, artifact.name);
            skillCount++;
        }
    }


    void SetTooltipStatInfo()
    {
        UpgradeComponent[] upComps = GameDataManager.instance.saveData.userData.upgrade.upgradeComponents;
        int imageTextIndex = 1; //0은 속성
        foreach (UpgradeComponent upComp in upComps)
        {
            string targetText = "Upgrade.Name.".AddString(upComp.key).Localize();
            string targetTextInfo = upComp.GetTooltipInfo();
            ImageText imageText = imageTexts[imageTextIndex];
            imageText.mouseOverText = targetTextInfo;
            imageTextIndex++;
        }
    }

}
