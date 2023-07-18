using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BattleResult
{
    public float suvivorTime;
    public int kill;
    public int level;
    public float receiveDamage;
    public float healHealth;
    public long gainMora;
    public long gainPrimoGem;
    public Dictionary<SkillName, float> skillDamageSet;
    public List<DropItem> items;
    public BattleResult()
    {
        suvivorTime = 0;
        kill = 0;
        level = 0;
        receiveDamage = 0;
        healHealth = 0;
        gainMora = 0;
        gainPrimoGem = 0;
        skillDamageSet = new Dictionary<SkillName, float>();
        items = new List<DropItem>();
    }

    public void UpdateDamage(SkillName skillName, float damage)
    {
        if (skillDamageSet.ContainsKey(skillName))
        {
            skillDamageSet[skillName] += damage;
        }
        else
        {
            skillDamageSet.Add(skillName, damage);
        }
        GameDataManager.instance.saveData.record.totalDamage += damage;
    }
}