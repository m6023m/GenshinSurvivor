using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDebuff : MonoBehaviour
{
    Dictionary<SkillName, float> _debuffTimes;
    Dictionary<SkillName, float> debuffTimes
    {
        get
        {
            if (_debuffTimes == null)
            {
                _debuffTimes = new Dictionary<SkillName, float>();
            }
            return _debuffTimes;
        }
    }
    Dictionary<SkillName, float> _debuffValues;
    Dictionary<SkillName, float> debuffValues
    {
        get
        {
            if (_debuffValues == null)
            {
                _debuffValues = new Dictionary<SkillName, float>();
            }
            return _debuffValues;
        }
    }
    Enemy _enemy;
    Enemy enemy
    {
        get
        {
            if (_enemy == null)
            {
                _enemy = GetComponent<Enemy>();
            }
            return _enemy;
        }
    }

    private void Update()
    {
        CheckDebuffTime();
    }

    private void CheckDebuffTime()
    {

        if (debuffTimes == null) return;
        if (debuffTimes.Count == 0) return;

        List<SkillName> keys = new List<SkillName>(debuffTimes.Keys);

        foreach (SkillName key in keys)
        {
            debuffTimes[key] += 1 * Time.deltaTime;
            if (debuffTimes[key] > GetDebuffTimeValue(key))
            {
                RemoveDebuff(key);
            }
        }
    }

    private float GetDebuffTimeValue(SkillName skillName)
    {
        float result = 9999;
        switch (skillName)
        {
            case SkillName.Superconduct:
                result = 12;
                break;
            case SkillName.EB_Travler_Anemo:
                result = 6;
                break;
            case SkillName.E_Xiangling:
                result = 6;
                break;
            case SkillName.E_Klee:
                result = 10;
                break;
            case SkillName.EB_Mona:
                result = 8;
                break;
            case SkillName.E_Venti:
                result = 8;
                break;
            case SkillName.EB_Venti:
                result = 10;
                break;
            case SkillName.E_Razor:
                result = 7;
                break;
            case SkillName.E_Qiqi:
                result = 5;
                break;
            case SkillName.E_Xinyan:
                result = 6;
                break;
            case SkillName.E_Eula:
                result = 10;
                break;
            case SkillName.E_Rosaria:
                result = 10;
                break;
        }
        return result;
    }

    private void RemoveDebuff(SkillName skillName)
    {
        switch (skillName)
        {
            case SkillName.Superconduct:
                enemy.PhysicsRes += debuffValues[skillName];
                debuffTimes.Remove(skillName);
                break;
            case SkillName.EB_Travler_Anemo:
                enemy.AnemoRes += debuffValues[skillName];
                debuffTimes.Remove(skillName);
                break;
            case SkillName.E_Xiangling:
                enemy.PyroRes += debuffValues[skillName];
                debuffTimes.Remove(skillName);
                break;
            case SkillName.E_Klee:
                enemy.PyroRes += debuffValues[skillName];
                debuffTimes.Remove(skillName);
                break;
            case SkillName.EB_Mona:
                enemy.PhysicsRes += debuffValues[skillName];
                enemy.PyroRes += debuffValues[skillName];
                enemy.HydroRes += debuffValues[skillName];
                enemy.AnemoRes += debuffValues[skillName];
                enemy.DendroRes += debuffValues[skillName];
                enemy.ElectroRes += debuffValues[skillName];
                enemy.CyroRes += debuffValues[skillName];
                enemy.GeoRes += debuffValues[skillName];
                debuffTimes.Remove(skillName);
                break;
            case SkillName.E_Venti:
                enemy.PhysicsRes += debuffValues[skillName];
                enemy.AnemoRes += debuffValues[skillName];
                break;
            case SkillName.EB_Venti:
                enemy.PhysicsRes += debuffValues[skillName];
                enemy.PyroRes += debuffValues[skillName];
                enemy.HydroRes += debuffValues[skillName];
                enemy.AnemoRes += debuffValues[skillName];
                enemy.DendroRes += debuffValues[skillName];
                enemy.ElectroRes += debuffValues[skillName];
                enemy.CyroRes += debuffValues[skillName];
                enemy.GeoRes += debuffValues[skillName];
                break;
            case SkillName.E_Razor:
                enemy.armor += debuffValues[skillName];
                break;
            case SkillName.E_Qiqi:
                enemy.damage += debuffValues[skillName];
                break;
            case SkillName.E_Xinyan:
                enemy.PhysicsRes += debuffValues[skillName];
                break;
            case SkillName.EB_Ganyu:
                enemy.PhysicsRes += debuffValues[skillName];
                enemy.PyroRes += debuffValues[skillName];
                enemy.HydroRes += debuffValues[skillName];
                enemy.AnemoRes += debuffValues[skillName];
                enemy.DendroRes += debuffValues[skillName];
                enemy.ElectroRes += debuffValues[skillName];
                enemy.CyroRes += debuffValues[skillName];
                enemy.GeoRes += debuffValues[skillName];
                break;
            case SkillName.E_Eula:
                enemy.PhysicsRes += debuffValues[skillName];
                enemy.CyroRes += debuffValues[skillName];
                break;
            case SkillName.E_Rosaria:
                enemy.PhysicsRes += debuffValues[skillName];
                break;
        }
    }

    public void AddDebuff(SkillName skillName)
    {
        float debuffValue = 0.0f;
        SkillData.ParameterWithKey parameterWithKey = GameManager.instance.skillData.skills[skillName];
        if (debuffTimes.ContainsKey(skillName)) //이미 존재하는 디버프면 시간만 갱신하고 리턴
        {
            debuffTimes[skillName] = 0;
            return;
        }
        switch (skillName)
        {
            case SkillName.Superconduct:
                debuffValues.AddOrUpdate(skillName, 0.4f);
                enemy.PhysicsRes -= debuffValues[skillName];
                debuffTimes.AddOrUpdate(skillName, 0);
                break;
            case SkillName.EB_Travler_Anemo:
                debuffValues.AddOrUpdate(skillName, 0.2f);
                enemy.AnemoRes -= debuffValues[skillName];
                break;
            case SkillName.E_Xiangling:
                debuffValues.AddOrUpdate(skillName, 0.15f);
                enemy.PyroRes -= debuffValues[skillName];
                break;
            case SkillName.E_Klee:
                float armor = enemy.armor * 0.2f;
                debuffValues.AddOrUpdate(skillName, armor);
                enemy.armor -= debuffValues[skillName];
                break;
            case SkillName.EB_Mona:
                debuffValue = 0.6f;
                if (parameterWithKey.constellations.num0)
                {
                    debuffValue += 0.2f;
                }
                if (parameterWithKey.constellations.num3)
                {
                    debuffValue += 0.2f;
                }
                debuffValues.AddOrUpdate(skillName, debuffValue);
                enemy.PhysicsRes -= debuffValues[skillName];
                enemy.PyroRes -= debuffValues[skillName];
                enemy.HydroRes -= debuffValues[skillName];
                enemy.AnemoRes -= debuffValues[skillName];
                enemy.DendroRes -= debuffValues[skillName];
                enemy.ElectroRes -= debuffValues[skillName];
                enemy.CyroRes -= debuffValues[skillName];
                enemy.GeoRes -= debuffValues[skillName];
                break;
            case SkillName.E_Venti:
                if (parameterWithKey.constellations.num2)
                {
                    debuffValue = 0.12f;
                }
                debuffValues.AddOrUpdate(skillName, debuffValue);
                enemy.PhysicsRes -= debuffValues[skillName];
                enemy.AnemoRes -= debuffValues[skillName];
                break;
            case SkillName.EB_Venti:
                if (parameterWithKey.constellations.num5)
                {
                    debuffValue = 0.2f;
                }
                debuffValues.AddOrUpdate(skillName, debuffValue);
                enemy.PhysicsRes -= debuffValues[skillName];
                enemy.PyroRes -= debuffValues[skillName];
                enemy.HydroRes -= debuffValues[skillName];
                enemy.AnemoRes -= debuffValues[skillName];
                enemy.DendroRes -= debuffValues[skillName];
                enemy.ElectroRes -= debuffValues[skillName];
                enemy.CyroRes -= debuffValues[skillName];
                enemy.GeoRes -= debuffValues[skillName];
                break;
            case SkillName.E_Razor:
                if (parameterWithKey.constellations.num3)
                {
                    debuffValue += 2f;
                }
                debuffValues.AddOrUpdate(skillName, debuffValue);
                enemy.armor -= debuffValues[skillName];
                break;
            case SkillName.E_Qiqi:
                float debuffAtk = enemy.damage * 0.2f;
                debuffValue += debuffAtk;
                debuffValues.AddOrUpdate(skillName, debuffValue);
                enemy.damage -= debuffValues[skillName];
                break;
            case SkillName.E_Xinyan:
                if (parameterWithKey.constellations.num3)
                {
                    debuffValue = 0.2f;
                    debuffValues.AddOrUpdate(skillName, debuffValue);
                    enemy.PhysicsRes -= debuffValues[skillName];
                }
                break;
            case SkillName.EB_Ganyu:
                if (parameterWithKey.constellations.num3)
                {
                    debuffValue = 0.2f;
                }
                debuffValues.AddOrUpdate(skillName, debuffValue);
                enemy.PhysicsRes -= debuffValues[skillName];
                enemy.PyroRes -= debuffValues[skillName];
                enemy.HydroRes -= debuffValues[skillName];
                enemy.AnemoRes -= debuffValues[skillName];
                enemy.DendroRes -= debuffValues[skillName];
                enemy.ElectroRes -= debuffValues[skillName];
                enemy.CyroRes -= debuffValues[skillName];
                enemy.GeoRes -= debuffValues[skillName];
                break;
            case SkillName.E_Eula:
                debuffValues.AddOrUpdate(skillName, 0.2f);
                enemy.PhysicsRes -= debuffValues[skillName];
                enemy.CyroRes -= debuffValues[skillName];
                break;
            case SkillName.E_Rosaria:
                debuffValues.AddOrUpdate(skillName, 0.2f);
                enemy.PhysicsRes -= debuffValues[skillName];
                break;
        }
    }

}