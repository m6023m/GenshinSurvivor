using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Skill_Parameter
{
    public Element.Type type;
    public bool isTypeFix;
    public float damage = 1;
    public Dictionary<SkillName, float> extendDamageDictionary;
    public float healPer = 1;
    public float sheildPer = 1;
    [Tooltip("관통 -1은 무한관통")]
    public int penetrate;
    public float area = 1;
    public float duration = 1;
    public float speed;
    [Tooltip("관통 -1은 무한관통")]
    public float knockBack;

    [Tooltip("끌어당기는 거리")]
    public float magnet;
    public float magnetSpeed;

    [Tooltip("지속피해 틱 -1은 한번만 때림")]
    public float skillTick;


    public int count;
    public float coolTime;
    public float elementGaugeMax;
    public float elementGauge;
    public bool isDebuffable;
    public Color Color()
    {
        return Element.Color(type);
    }

    public void SetData(Skill_Parameter parameter)
    {
        this.type = parameter.type;
        this.isTypeFix = parameter.isTypeFix;
        this.damage = parameter.damage;
        this.extendDamageDictionary = parameter.extendDamageDictionary;
        this.healPer = parameter.healPer;
        this.sheildPer = parameter.sheildPer;
        this.penetrate = parameter.penetrate;
        this.area = parameter.area;
        this.duration = parameter.duration;
        this.speed = parameter.speed;
        this.knockBack = parameter.knockBack;
        this.magnet = parameter.magnet;
        this.magnetSpeed = parameter.magnetSpeed;
        this.skillTick = parameter.skillTick;
        this.count = parameter.count;
        this.coolTime = parameter.coolTime;
        this.elementGaugeMax = parameter.elementGaugeMax;
    }

    public void SetElementType(SkillData.ParameterWithKey parameterWithKey, Element.Type elementType)
    {
        if (isTypeFix) return;
        if (parameterWithKey.name != SkillName.Basic_Sword
         && parameterWithKey.name != SkillName.Basic_Spear
         && parameterWithKey.name != SkillName.Basic_Arrow
         && parameterWithKey.name != SkillName.Basic_Claymore
         && parameterWithKey.name != SkillName.Basic_Hutao
         && parameterWithKey.name != SkillName.Basic_Eula) return;
        foreach (SkillSet.SkillSequence skillSequence in parameterWithKey.skillSet.sequences)
        {
            skillSequence.elementType = elementType;
        }
        type = elementType;
    }

    public void AddExtendDamage(SkillName skillName, float damage)
    {
        if (extendDamageDictionary == null) extendDamageDictionary = new Dictionary<SkillName, float>();
        extendDamageDictionary.AddOrUpdate(skillName, damage);
    }
    public void RemoveExtendDamage(SkillName skillName)
    {
        if (extendDamageDictionary == null) extendDamageDictionary = new Dictionary<SkillName, float>();
        if (!extendDamageDictionary.ContainsKey(skillName)) return;
        extendDamageDictionary.Remove(skillName);
    }
}
