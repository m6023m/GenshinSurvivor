using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    public Button buttonClose;
    TextMeshProUGUI _textName;
    TextMeshProUGUI _textDiscription;
    TextMeshProUGUI _textInfo;
    Image _image;
    TextMeshProUGUI textName
    {
        get
        {
            if (_textName == null)
            {
                _textName = GetComponentsInChildren<TextMeshProUGUI>(true)[0];
            }
            return _textName;
        }
    }
    TextMeshProUGUI textDiscription
    {
        get
        {
            if (_textDiscription == null)
            {
                _textDiscription = GetComponentsInChildren<TextMeshProUGUI>(true)[1];
            }
            return _textDiscription;
        }
    }
    TextMeshProUGUI textInfo
    {
        get
        {
            if (_textInfo == null)
            {
                _textInfo = GetComponentsInChildren<TextMeshProUGUI>(true)[2];
            }
            return _textInfo;
        }
    }
    Image image
    {
        get
        {
            if (_image == null)
            {
                _image = GetComponentInChildren<Image>();
            }
            return _image;
        }
    }

    SkillData.ParameterWithKey skillParameter;
    ArtifactData.ParameterWithKey artifactParameter;

    public void Init(SkillName skillName, ArtifactName artifactName)
    {
        buttonClose.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        if (skillName != SkillName.Skill_None) DisplaySkill(skillName);
        if (artifactName != ArtifactName.Artifact_None) DisplayArtifact(artifactName);
    }

    void DisplaySkill(SkillName skillName)
    {
        StringBuilder stringDiscription = new StringBuilder();
        StringBuilder stringInfo = new StringBuilder();
        skillParameter = GameManager.instance.skillData.Get(skillName);
        textName.text = "Skill.".AddString(skillName.ToString()).Localize();
        stringDiscription.Append("\n");
        stringDiscription.Append("Skill.Discription.".AddString(skillName.ToString()).Localize());
        for (int i = 0; i < skillParameter.level; i++)
        {
            SkillData.SkillUp skillUp = skillParameter.skillUps[i];
            stringDiscription.Append("\n");
            stringDiscription.Append("Upgrade.Skill.".AddString(skillUp.name.ToString()).Localize(skillUp.value));
        }
        textDiscription.text = stringDiscription.ToString();
        foreach (SkillSet.SkillSequence skillSequence in skillParameter.skillSet.sequences)
        {
            if (skillSequence.objectType != Skill.ObjectType.Skill
            && skillSequence.objectType != Skill.ObjectType.Sheild) continue;

            float skillDamage = GameManager.instance.statCalcuator.CalcDamageSkill(skillSequence, skillParameter, skillSequence.elementType);
            float sheildHealth = CalcHealth(skillParameter, skillSequence);
            float calc = skillDamage;
            if (skillSequence.objectType == Skill.ObjectType.Sheild)
            {
                calc = sheildHealth;
            }
            stringInfo.Append("\n");
            stringInfo.Append("Element.Type.".AddString(skillSequence.elementType.ToString()).Localize());
            stringInfo.Append(" ");
            stringInfo.Append("Skill.ObjectType.".AddString(skillSequence.objectType.ToString()).Localize());
            stringInfo.Append(" ");
            stringInfo.Append("SkillDamageStat.".AddString(skillSequence.damageStat.ToString()).Localize(skillSequence.damage * 100.0f));
            stringInfo.Append(" = ");
            stringInfo.Append(calc.FormatDecimalPlaces().ToString());
        }
        textInfo.text = stringInfo.ToString();
    }

    void DisplayArtifact(ArtifactName artifactName)
    {
        StringBuilder stringDiscription = new StringBuilder();
        artifactParameter = GameManager.instance.artifactData.Get(artifactName);
        for (int i = 0; i < artifactParameter.level; i++)
        {
            stringDiscription.Append("\n");
            stringDiscription.Append("Artifact.Up.".AddString(artifactName.ToString()).Localize());
        }
        if (artifactParameter.level >= 4)
        {
            stringDiscription.Append("\n");
            stringDiscription.Append("Artifact.Up.Set.".AddString(artifactName.ToString()).Localize());
        }
        else
        {
            stringDiscription.Append("\n<#aaaaaa>");
            stringDiscription.Append("Artifact.Up.Set.".AddString(artifactName.ToString()).Localize());
            stringDiscription.Append("</color>");
        }

        textName.text = "Artifact.".AddString(artifactName.ToString()).Localize();
        textDiscription.text = stringDiscription.ToString();
        textInfo.text = "";
    }


    float CalcHealth(SkillData.ParameterWithKey parameterWithKey, SkillSet.SkillSequence skillSequence)
    {
        float health = 9999;
        float sheild = parameterWithKey.parameter.sheildPer;
        float sheildPer = skillSequence.damage;
        switch (skillSequence.damageStat)
        {
            case SkillSet.SkillDamageStat.ATK:
                health = sheild * GameManager.instance.statCalcuator.Atk * sheildPer * GameManager.instance.statCalcuator.SheildMultipllier;
                break;
            case SkillSet.SkillDamageStat.ARMOR:
                health = sheild * GameManager.instance.statCalcuator.Armor * sheildPer * GameManager.instance.statCalcuator.SheildMultipllier;
                break;
            case SkillSet.SkillDamageStat.HP:
                health = sheild * GameManager.instance.statCalcuator.Health * sheildPer * GameManager.instance.statCalcuator.SheildMultipllier;
                break;
            case SkillSet.SkillDamageStat.ELEMENT_MASTERY:
                health = sheild * GameManager.instance.statCalcuator.ElementalMastery * sheildPer * GameManager.instance.statCalcuator.SheildMultipllier;
                break;
        }
        return health;
    }
}