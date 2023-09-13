using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Skill : SkillMoveSet
{
    bool isChanged = false;
    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (parameterWithKey == null) return;
        if (!skillSequence.isChangedElement) return;

        if (!isChanged && parameterWithKey.changeElementType != Element.Type.Physics)
        {
            isChanged = true;
            parameterWithKey.parameter.AddExtendDamage(parameterWithKey.name, parameterWithKey.parameter.damage / 3.0f);
            InvokeElementChangeListeners(parameterWithKey.changeElementType);
        }

        if (isChanged && parameterWithKey.changeElementType != Element.Type.Physics)
        {
            GetComponent<SpriteRenderer>().color = Element.Color(parameterWithKey.changeElementType);
        }
    }
    public enum Type
    {
        Basic, //일반공격
        Skill,//원소스킬
        Burst,//원소폭발
        Reaction,//원소반응
        Weapon //무기스킬
    }

    public enum ObjectType
    {
        Skill, //대미지
        Summon, //소환물
        Sheild, //보호막
        Buff, //버프
        SkillEffect
    }

    public enum Aim
    { //스킬 방향 설정정
        Basic, //위쪽
        Player, //플레이어가 바라보는 방향
        Target, //적 방향
        SkillToTarget //스킬이 바라보는 방향
    }
    float skillDamage = 0;
    public float Damage
    {
        get
        {
            float damageResult = skillDamage;
            return damageResult;
        }
    }
    public Element.Type elementType;
    public override SkillMoveSet Init(SkillData.ParameterWithKey parameterWithKey, SkillSet.SkillSequence skillSequence, TransformValue prevTransform, int index)
    {
        this.skillSequence = skillSequence;
        float atk = GameManager.instance.statCalcuator.Atk;
        elementType = skillSequence.elementType;
        skillDamage = GameManager.instance.statCalcuator.CalcDamageSkill(skillSequence, parameterWithKey, elementType);
        ChangeDamageCalc(parameterWithKey);
        isChanged = false;
        base.Init(parameterWithKey, skillSequence, prevTransform, index);
        SkillEffect();
        return this;
    }

    void SkillEffect()
    {
        switch (parameterWithKey.name)
        {
            case SkillName.E_Xiao:
                float hp = player.health * 0.02f;
                player.health -= hp;
                break;
        }

    }

    void ChangeDamageCalc(SkillData.ParameterWithKey parameterWithKey)
    {
        SkillData.ParameterWithKey baseAttackParameter = GameManager.instance.ownSkills[0];
        SkillSet.SkillSequence skillSequence = baseAttackParameter.skillSet.sequences[0];
        float atk = GameManager.instance.statCalcuator.Atk;
        switch (parameterWithKey.name)
        {
            case SkillName.EB_Razor:
                foreach (SkillSet.SkillSequence sequence in baseAttackParameter.skillSet.sequences)
                {
                    if (sequence.objectType == Skill.ObjectType.Skill)
                    {
                        skillSequence = sequence;
                    }
                }
                skillDamage = GameManager.instance.statCalcuator.CalcDamageSkill(skillSequence, baseAttackParameter, Element.Type.Electro);
                break;
            case SkillName.EB_Hutao:
                if (GameManager.instance.player.health / GameManager.instance.statCalcuator.Health <= 0.5f)
                {
                    skillDamage *= 1.5f;
                    GameManager.instance.player.HealHealth(GameManager.instance.statCalcuator.Health * 0.45f);
                }
                else
                {
                    GameManager.instance.player.HealHealth(GameManager.instance.statCalcuator.Health * 0.3f);
                }
                break;
            case SkillName.EB_Eula:
                if (skillSequence.isConditionChange)
                {
                    float damagePer = 1.0f + (GameManager.instance.statBuff.eulaStack * 0.05f);
                    skillDamage *= damagePer;
                }
                break;
        }
        if (parameterWithKey.type == Skill.Type.Reaction)
        {
            skillDamage = GameManager.instance.statCalcuator.ReactionDamage(parameterWithKey, reactedDamage, skillSequence.elementType);
        }
    }
    void InvokeElementChangeListeners(Element.Type elmentType)
    {
        foreach (UnityAction<Element.Type> action in parameterWithKey.elementChangeListener)
        {
            action.Invoke(elmentType);
        }
    }
}
