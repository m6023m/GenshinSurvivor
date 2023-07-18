using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillIcon : MonoBehaviour
{
    Image imageSkill;
    Image imageBackground;
    Image imageElementalGauge;
    TextMeshProUGUI textLevel;
    public SkillObject skillObject;
    public SkillData.ParameterWithKey skillParameter;
    public ArtifactData.ParameterWithKey artifactParameter;
    public bool isSkill = false;
    public bool isArtifact = false;
    public Sprite sprite
    {
        get
        {
            if (imageSkill == null) return null;
            return imageSkill.sprite;
        }
        set
        {
            if (imageSkill == null) return;
            imageSkill.sprite = value;
            if (imageBackground == null) return;
            imageBackground.sprite = value;
        }
    }


    private void Update()
    {
        ModifySkillLevel();
        ModifyArtifactLevel();
        ModifyCooltime();
        ModifyElementalGauge();
    }
    private void Awake()
    {
        imageBackground = GetComponent<Image>();
        imageSkill = GetComponentInChildren<Icon>().GetComponent<Image>();
        textLevel = GetComponentInChildren<TextMeshProUGUI>();
        imageElementalGauge = GetComponentsInChildren<Image>()[2];
    }

    void ModifySkillLevel()
    {
        if (!isSkill) return;
        if (skillParameter == null || textLevel == null) return;
        if (skillParameter.level >= 2)
        {
            textLevel.text = skillParameter.level.ToString();
        }
        else
        {
            textLevel.text = "";
        }
    }
    void ModifyArtifactLevel()
    {
        if (!isArtifact) return;
        if (artifactParameter == null || textLevel == null) return;
        if (artifactParameter.level >= 2)
        {
            textLevel.text = artifactParameter.level.ToString();
        }
        else
        {
            textLevel.text = "";
        }
    }

    void ModifyCooltime()
    {
        if (skillObject == null || imageSkill == null) return;
        float skillTime = skillObject.skillTime;
        float coolTime = skillParameter.parameter.coolTime;
        SkillSet skillSet = skillParameter.skillSet;
        if (skillSet == null) return;
        coolTime -= coolTime * GameManager.instance.statCalcuator.CooltimeWithArtifact(skillParameter.type);
        float fillAmount = (skillTime / coolTime);
        foreach (SkillSet.SkillSequence skillSequence in skillSet.sequences)
        {
            if (skillSequence.duration < 0) fillAmount = 1;
        }
        imageSkill.fillAmount = fillAmount;
    }

    void ModifyElementalGauge()
    {
        if (skillObject == null || imageElementalGauge == null) return;
        float elementGauge = skillParameter.parameter.elementGauge;
        float elementGaugeMax = skillParameter.parameter.elementGaugeMax;
        float fillAmount = (elementGauge / elementGaugeMax);


        imageElementalGauge.fillAmount = fillAmount;
        float r = skillParameter.parameter.Color().r;
        float g = skillParameter.parameter.Color().g;
        float b = skillParameter.parameter.Color().b;
        float a = 0.5f;
        Color color = new Color(r, g, b, a);
        imageElementalGauge.color = color;
    }
}