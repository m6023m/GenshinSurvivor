using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Sheild : SkillMoveSet
{
    public float health = 9999;

    protected override void LateUpdate()
    {
        base.LateUpdate();

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public override SkillMoveSet Init(SkillData.ParameterWithKey parameterWithKey, SkillSet.SkillSequence skillSequence, TransformValue prevTransform, int index)
    {
        this.skillSequence = skillSequence;
        base.Init(parameterWithKey, skillSequence, prevTransform, index);
        InitHealth(parameterWithKey, skillSequence);
        player.AddSheild(this);
        return this;
    }

    void InitHealth(SkillData.ParameterWithKey parameterWithKey, SkillSet.SkillSequence skillSequence)
    {
        health = 9999;
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
                health = sheild * GameManager.instance.statCalcuator.Helath * sheildPer * GameManager.instance.statCalcuator.SheildMultipllier;
                break;
            case SkillSet.SkillDamageStat.ELEMENT_MASTERY:
                health = sheild * GameManager.instance.statCalcuator.ElementalMastery * sheildPer * GameManager.instance.statCalcuator.SheildMultipllier;
                break;
        }
        switch (parameterWithKey.name)
        {
            case SkillName.Crystalize:
                health = GameManager.instance.statCalcuator.ReactionSheid;
                break;
        }
    }

    public void ReceiveDamage(float damage, Enemy enemy)
    {
        OnReceiveDamage(damage, enemy);
        health -= CalcDamage(damage);
    }

    public float CalcDamage(float damage)
    {
        float armor = player.stat.armor + GameManager.instance.artifactData.Armor;
        armor = armor + (armor * GameManager.instance.artifactData.ArmorMultiplier);
        float damageResult = (damage - armor);
        if (damageResult <= 0) damageResult = 0.2f;

        return damageResult;

    }
    void OnReceiveDamage(float damage, Enemy enemy)
    {
        switch (parameterWithKey.name)
        {
            case SkillName.E_Zhongli:
                if (!parameterWithKey.constellations.num5) return;
                if (enemy == null) return;
                enemy.ReceiveDamage(parameterWithKey.name, damage, Element.Type.Geo);
                break; 
        }
    }
    protected override void OnDisable()
    {
        if (player.sheilds.ContainsKey(parameterWithKey.name))
        {
            if (player.sheilds[parameterWithKey.name].health <= 0)
            {
                player.RemoveSheild(this);
            }
        }
        base.OnDisable();
    }
}