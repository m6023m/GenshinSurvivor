using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Character;
public class StatCalculator
{
    Character stat;
    WeaponData.Parameter weaponStat;
    ArtifactData artifactData;
    UpgradeComponent[] upgradeComponents;
    public Dictionary<StatType, bool> isStatusChangedType = new Dictionary<StatType, bool>();
    bool CheckChangedType(StatType type)
    {
        if (isStatusChangedType.ContainsKey(type))
        {
            return isStatusChangedType[type];
        }
        return true;
    }
    public void OnValidateStatAll()
    {
        foreach (var key in isStatusChangedType.Keys.ToList())
        {
            isStatusChangedType[key] = true;
        }
    }
    StatBuff _statBuff;
    StatBuff statBuff
    {
        get
        {
            if (_statBuff == null)
            {
                _statBuff = new StatBuff();
            }
            return _statBuff;
        }
        set
        {
            _statBuff = value;
        }
    }
    int level = 0;
    public StatCalculator(Character stat)
    {
        this.stat = stat;
        level = 0;
        if (GameManager.instance != null) level = GameManager.instance.gameInfoData.level;
        upgradeComponents = GameDataManager.instance.saveData.userData.upgrade.upgradeComponents;

    }



    public StatCalculator ArtifactData(ArtifactData artifactData)
    {
        this.artifactData = artifactData;
        return this;
    }

    public StatCalculator WeaponData(WeaponData.Parameter weaponStat)
    {
        this.weaponStat = weaponStat;
        return this;
    }
    float _Atk;
    public float Atk
    {
        get
        {
            if (!CheckChangedType(StatType.ATK)) return _Atk;
            float result = stat.atk;
            if (artifactData != null)
            {
                result += (result * artifactData.AtkMultiplier);
                result += artifactData.Atk;
            }
            if (weaponStat != null)
            {
                result += (result * weaponStat.ATK_PER);
                result += weaponStat.ATK;
                result += Health * statBuff.HealthAtkPer;
            }
            result += GetRankUpValue(stat, StatType.ATK);
            result += (stat.atk * upgradeComponents[(int)UpgradeType.ATK].GetValue());
            result += statBuff.Atk;
            _Atk = result;
            isStatusChangedType[StatType.ATK] = false;
            return _Atk;
        }
    }
    float _Armor;
    public float Armor
    {
        get
        {
            if (!CheckChangedType(StatType.ARMOR)) return _Armor;
            float result = stat.armor;
            if (artifactData != null)
            {
                result += (result * artifactData.ArmorMultiplier);
                result += artifactData.Armor;
            }
            if (weaponStat != null)
            {
                result += (result * weaponStat.ARMOR_PER);
            }
            result += GetRankUpValue(stat, StatType.ARMOR);
            result += upgradeComponents[(int)UpgradeType.ARMOR].GetValue();
            result += statBuff.Armor;
            _Armor = result;
            isStatusChangedType[StatType.ARMOR] = false;
            return _Armor;
        }
    }
    float _Health;
    public float Health
    {
        get
        {
            if (!CheckChangedType(StatType.HP)) return _Health;
            float result = stat.hp;
            if (artifactData != null)
            {
                result += (result * artifactData.HealthMultiplier);
            }
            if (weaponStat != null)
            {
                result += (result * weaponStat.HP_PER);
            }
            result += GetRankUpValue(stat, StatType.HP);
            result += (stat.hp * upgradeComponents[(int)UpgradeType.HP].GetValue());
            result += statBuff.Hp;
            _Health = result;
            isStatusChangedType[StatType.HP] = false;
            return _Health;
        }
    }

    float _Heal;
    public float HealBonus
    {
        get
        {
            if (!CheckChangedType(StatType.HEAL)) return _Heal;
            float result = stat.heal;
            if (artifactData != null)
            {
                result += (result * artifactData.HealBonusMultiplier);
            }
            result += GetRankUpValue(stat, StatType.HEAL);
            result += upgradeComponents[(int)UpgradeType.HEAL].GetValue();
            result += statBuff.Heal;
            _Heal = result;
            isStatusChangedType[StatType.HEAL] = false;
            return _Heal;
        }
    }
    float _Cooltime;
    public float Cooltime
    {
        get
        {
            if (!CheckChangedType(StatType.COOLTIME)) return _Cooltime;
            float result = stat.cooltime;
            result += GetRankUpValue(stat, StatType.COOLTIME);
            result += upgradeComponents[(int)UpgradeType.COOL].GetValue();
            result += statBuff.Cooltime;
            _Cooltime = result;
            isStatusChangedType[StatType.COOLTIME] = false;
            return _Cooltime;
        }
    }
    public float CooltimeWithArtifact(Skill.Type skillType)
    {
        float result = Cooltime;
        if (artifactData != null)
        {
            result += artifactData.CoolTimeMultiplier(skillType);
        }
        if (skillType == Skill.Type.Basic)
        {
            result += statBuff.BaseCooltime;
        }
        return result;
    }
    float _Area;
    public float Area
    {
        get
        {
            if (!CheckChangedType(StatType.AREA)) return _Area;
            float result = stat.area;
            if (artifactData != null)
            {
                result += (result * artifactData.AreaMultiplier);
            }
            result += GetRankUpValue(stat, StatType.AREA);
            result += upgradeComponents[(int)UpgradeType.AREA].GetValue();
            result += statBuff.Area;
            _Area = result;
            isStatusChangedType[StatType.AREA] = false;
            return _Area;
        }
    }
    float _Aspeed;
    public float Aspeed
    {
        get
        {
            if (!CheckChangedType(StatType.ASPEED)) return _Aspeed;
            float result = stat.aspeed;
            if (artifactData != null)
            {
                result += (result * artifactData.AttackSpeedMultiplier);
            }
            result += GetRankUpValue(stat, StatType.ASPEED);
            result += (stat.aspeed * upgradeComponents[(int)UpgradeType.SPEED].GetValue());
            result += statBuff.Aspeed;
            _Aspeed = result;
            isStatusChangedType[StatType.ASPEED] = false;
            return _Aspeed;
        }
    }
    float _Duration;
    public float Duration
    {
        get
        {
            if (!CheckChangedType(StatType.DURATION)) return _Duration;
            float result = stat.duration;
            if (artifactData != null)
            {
                result += (result * artifactData.DurationMultiplier);
            }
            result += GetRankUpValue(stat, StatType.DURATION);
            result += upgradeComponents[(int)UpgradeType.DURATION].GetValue();
            result += statBuff.Duration;
            _Duration = result;
            isStatusChangedType[StatType.DURATION] = false;
            return _Duration;
        }
    }
    float _Penetrate;
    public float Penetrate
    {
        get
        {
            if (!CheckChangedType(StatType.Penetrate)) return _Penetrate;
            float result = stat.amount;
            if (artifactData != null)
            {
                result += artifactData.Penetrate;
            }
            result += GetRankUpValue(stat, StatType.Penetrate);
            result += statBuff.Penetrate;
            _Penetrate = result;
            isStatusChangedType[StatType.Penetrate] = false;
            return _Penetrate;
        }
    }
    float _Amount;
    public float Amount
    {
        get
        {
            if (!CheckChangedType(StatType.AMOUNT)) return _Amount;
            float result = stat.amount;
            if (artifactData != null)
            {
                result += artifactData.Amount;
            }
            result += GetRankUpValue(stat, StatType.AMOUNT);
            result += upgradeComponents[(int)UpgradeType.AMOUNT].GetValue();
            result += statBuff.Amount;
            _Amount = result;
            isStatusChangedType[StatType.AMOUNT] = false;
            return _Amount;
        }
    }
    float _Speed;
    public float Speed
    {
        get
        {
            if (!CheckChangedType(StatType.SPEED)) return _Speed;
            float result = stat.speed;
            if (artifactData != null)
            {
                result += (result * artifactData.MoveSpeedMultiplier);
            }
            result += GetRankUpValue(stat, StatType.SPEED);
            result += (stat.speed * upgradeComponents[(int)UpgradeType.SPEED].GetValue());
            result += statBuff.Speed;
            _Speed = result;
            isStatusChangedType[StatType.SPEED] = false;
            return _Speed;
        }
    }

    float _Magent;
    public float Magnet
    {
        get
        {
            if (!CheckChangedType(StatType.MAGNET)) return _Magent;
            float result = stat.magnet;
            if (artifactData != null)
            {
                result += artifactData.MagnetMultiplier;
            }
            result += GetRankUpValue(stat, StatType.MAGNET);
            result += upgradeComponents[(int)UpgradeType.MAGNET].GetValue();
            result += statBuff.Magnet;
            _Magent = result;
            isStatusChangedType[StatType.MAGNET] = false;
            return _Magent;
        }
    }
    float _Luck;
    public float Luck
    {
        get
        {
            if (!CheckChangedType(StatType.LUCK)) return _Luck;
            float result = stat.luck;
            if (artifactData != null)
            {
                result += artifactData.Luck;
            }
            if (weaponStat != null)
            {
                result += weaponStat.LUCK;
            }
            result += GetRankUpValue(stat, StatType.LUCK);
            result += upgradeComponents[(int)UpgradeType.LUCK].GetValue();
            result += statBuff.Luck;
            _Luck = result;
            isStatusChangedType[StatType.LUCK] = false;
            return _Luck;
        }
    }
    float _Regen;
    public float Regen
    {
        get
        {
            if (!CheckChangedType(StatType.REGEN)) return _Regen;
            float result = stat.regen;
            if (artifactData != null)
            {
                result += artifactData.RegenMultiplier;
            }
            if (weaponStat != null)
            {
                result += weaponStat.REGEN;
            }
            result += GetRankUpValue(stat, StatType.REGEN);
            result += (stat.regen * upgradeComponents[(int)UpgradeType.GROWTH].GetValue());
            result += statBuff.Regen;
            _Regen = result;
            isStatusChangedType[StatType.REGEN] = false;
            return _Regen;
        }
    }
    float _Exp;
    public float Exp
    {
        get
        {
            if (!CheckChangedType(StatType.EXP)) return _Exp;
            float result = stat.exp;
            if (artifactData != null)
            {
                result += artifactData.ExpMultiplier;
            }
            result += GetRankUpValue(stat, StatType.EXP);
            result += (stat.regen * upgradeComponents[(int)UpgradeType.GROWTH].GetValue());
            result += statBuff.Exp;
            _Exp = result;
            isStatusChangedType[StatType.EXP] = false;
            return _Exp;
        }
    }
    float _Greed;
    public float Greed
    {
        get
        {
            if (!CheckChangedType(StatType.GREED)) return _Greed;
            float result = stat.greed;
            if (artifactData != null)
            {
                result += artifactData.GreedMultiplier;
            }
            result += GetRankUpValue(stat, StatType.GREED);
            result += upgradeComponents[(int)UpgradeType.GREED].GetValue();
            result += statBuff.Greed;
            _Greed = result;
            isStatusChangedType[StatType.GREED] = false;
            return _Greed;
        }
    }
    float _Curse;
    public float Curse
    {
        get
        {
            if (!CheckChangedType(StatType.CURSE)) return _Curse;
            float result = stat.curse;
            if (artifactData != null)
            {
            }
            result += GetRankUpValue(stat, StatType.CURSE);
            result += upgradeComponents[(int)UpgradeType.CURSE].GetValue();
            result += statBuff.Curse;
            _Curse = result;
            isStatusChangedType[StatType.CURSE] = false;
            return _Curse;
        }
    }
    float _Rebirth;
    public float Rebirth
    {
        get
        {
            if (!CheckChangedType(StatType.RESURRATION)) return _Rebirth;
            float result = stat.resurration;
            if (artifactData != null)
            {
                result += artifactData.Rebirth;
            }
            result += GetRankUpValue(stat, StatType.RESURRATION);
            result += upgradeComponents[(int)UpgradeType.REBIRTH].GetValue();
            result += statBuff.Resurraction;
            _Rebirth = result;
            isStatusChangedType[StatType.RESURRATION] = false;
            return _Rebirth;
        }
    }
    float _Reroll;
    public float Reroll
    {
        get
        {
            if (!CheckChangedType(StatType.REROLL)) return _Reroll;
            float result = stat.reroll;
            if (artifactData != null)
            {
            }
            result += GetRankUpValue(stat, StatType.REROLL);
            result += upgradeComponents[(int)UpgradeType.REROLL].GetValue();
            result += statBuff.Reroll;
            _Reroll = result;
            isStatusChangedType[StatType.REROLL] = false;
            return _Reroll;
        }
    }
    float _Skip;
    public float Skip
    {
        get
        {
            if (!CheckChangedType(StatType.SKIP)) return _Skip;
            float result = stat.skip;
            if (artifactData != null)
            {
            }
            result += GetRankUpValue(stat, StatType.SKIP);
            result += upgradeComponents[(int)UpgradeType.SKIP].GetValue();
            result += statBuff.Skip;
            _Skip = result;
            isStatusChangedType[StatType.SKIP] = false;
            return _Skip;
        }
    }
    float _PhysicsDamage;
    public float PhysicsDamage
    {
        get
        {
            if (!CheckChangedType(StatType.PHYSICS_DMG)) return _PhysicsDamage;
            float result = stat.physicsDmg;
            if (artifactData != null)
            {
                result += artifactData.PhysicsDamageMultiplier;
                result += artifactData.DamageMultiplier;
                result += statBuff.KnwooDamagePer;
                if (GameManager.instance.player.sheilds.Count > 0)
                {
                    result += statBuff.KnwooDamagePer;
                }
            }
            if (weaponStat != null)
            {
                result += weaponStat.PHYSICS_PER;
            }
            result += GetRankUpValue(stat, StatType.PHYSICS_DMG);
            result += statBuff.PhysicsDmg;
            _PhysicsDamage = result;
            isStatusChangedType[StatType.PHYSICS_DMG] = false;
            return _PhysicsDamage;
        }
    }
    float _PyroDamage;
    public float PyroDamage
    {
        get
        {
            if (!CheckChangedType(StatType.PYRO_DMG)) return _PyroDamage;
            float result = stat.pyroDmg;
            if (artifactData != null)
            {
                result += artifactData.DamageMultiplier;
                result += statBuff.KnwooDamagePer;
                if (GameManager.instance.player.sheilds.Count > 0)
                {
                    result += statBuff.KnwooDamagePer;
                }
            }
            result += GetRankUpValue(stat, StatType.PYRO_DMG);
            result += statBuff.PyroDmg;
            _PyroDamage = result;
            isStatusChangedType[StatType.PYRO_DMG] = false;
            return _PyroDamage;
        }
    }
    float _HydroDamage;
    public float HydroDamage
    {
        get
        {
            if (!CheckChangedType(StatType.HYDRO_DMG)) return _HydroDamage;
            float result = stat.hydroDmg;
            if (artifactData != null)
            {
                result += artifactData.DamageMultiplier;
                result += statBuff.KnwooDamagePer;
                if (GameManager.instance.player.sheilds.Count > 0)
                {
                    result += statBuff.KnwooDamagePer;
                }
            }
            result += GetRankUpValue(stat, StatType.HYDRO_DMG);
            result += statBuff.HydroDmg;
            _HydroDamage = result;
            isStatusChangedType[StatType.HYDRO_DMG] = false;
            return _HydroDamage;
        }
    }
    float _AnemoDamage;
    public float AnemoDamage
    {
        get
        {
            if (!CheckChangedType(StatType.ANEMO_DMG)) return _AnemoDamage;
            float result = stat.anemoDmg;
            if (artifactData != null)
            {
                result += artifactData.DamageMultiplier;
                result += statBuff.KnwooDamagePer;
                if (GameManager.instance.player.sheilds.Count > 0)
                {
                    result += statBuff.KnwooDamagePer;
                }
            }
            result += GetRankUpValue(stat, StatType.ANEMO_DMG);
            result += statBuff.AnemoDmg;
            _AnemoDamage = result;
            isStatusChangedType[StatType.ANEMO_DMG] = false;
            return _AnemoDamage;
        }
    }
    float _DendroDamage;
    public float DendroDamage
    {
        get
        {
            if (!CheckChangedType(StatType.DENDRO_DMG)) return _DendroDamage;
            float result = stat.dendroDmg;
            if (artifactData != null)
            {
                result += artifactData.DamageMultiplier;
                result += statBuff.KnwooDamagePer;
                if (GameManager.instance.player.sheilds.Count > 0)
                {
                    result += statBuff.KnwooDamagePer;
                }
            }
            result += GetRankUpValue(stat, StatType.DENDRO_DMG);
            result += statBuff.DendroDmg;
            _DendroDamage = result;
            isStatusChangedType[StatType.DENDRO_DMG] = false;
            return _DendroDamage;
        }
    }
    float _ElectroDamage;
    public float ElectroDamage
    {
        get
        {
            if (!CheckChangedType(StatType.ELECTRO_DMG)) return _ElectroDamage;
            float result = stat.electroDmg;
            if (artifactData != null)
            {
                result += artifactData.DamageMultiplier;
                result += statBuff.KnwooDamagePer;
                if (GameManager.instance.player.sheilds.Count > 0)
                {
                    result += statBuff.KnwooDamagePer;
                }
            }
            result += GetRankUpValue(stat, StatType.ELECTRO_DMG);
            result += statBuff.ElectroDmg;
            _ElectroDamage = result;
            isStatusChangedType[StatType.ELECTRO_DMG] = false;
            return _ElectroDamage;
        }
    }
    float _CyroDamage;
    public float CyroDamage
    {
        get
        {
            if (!CheckChangedType(StatType.CYRO_DMG)) return _CyroDamage;
            float result = stat.cyroDmg;
            if (artifactData != null)
            {
                result += artifactData.DamageMultiplier;
                result += statBuff.KnwooDamagePer;
                if (GameManager.instance.player.sheilds.Count > 0)
                {
                    result += statBuff.KnwooDamagePer;
                }
            }
            result += GetRankUpValue(stat, StatType.CYRO_DMG);
            result += statBuff.CyroDmg;
            _CyroDamage = result;
            isStatusChangedType[StatType.CYRO_DMG] = false;
            return _CyroDamage;
        }
    }
    float _GeoDamage;
    public float GeoDamage
    {
        get
        {
            if (!CheckChangedType(StatType.GEO_DMG)) return _GeoDamage;
            float result = stat.geoDmg;
            if (artifactData != null)
            {
                result += artifactData.DamageMultiplier;
                result += statBuff.KnwooDamagePer;
                if (GameManager.instance.player.sheilds.Count > 0)
                {
                    result += statBuff.KnwooDamagePer;
                }
            }
            result += GetRankUpValue(stat, StatType.GEO_DMG);
            result += statBuff.GeoDmg;
            _GeoDamage = result;
            isStatusChangedType[StatType.GEO_DMG] = false;
            return _GeoDamage;
        }
    }
    float _ElementalMastery;
    public float ElementalMastery
    {
        get
        {
            if (!CheckChangedType(StatType.ELEMENT_MASTERY)) return _ElementalMastery;
            float result = stat.elementMastery;
            if (artifactData != null)
            {
                result += artifactData.ElementalMastery;
            }
            result += GetRankUpValue(stat, StatType.ELEMENT_MASTERY);
            result += statBuff.ElementMastery;
            _ElementalMastery = result;
            isStatusChangedType[StatType.ELEMENT_MASTERY] = false;
            return _ElementalMastery;
        }
    }


    float DamageMultiplier(SkillData.ParameterWithKey parameterWithKey, Element.Type elementType)
    {
        float result = 0.0f;
        result += artifactData.Crimson_Witch_of_Flames(parameterWithKey.name);
        result += artifactData.Thundering_Fury(parameterWithKey.name);
        if (parameterWithKey.type == Skill.Type.Skill)
        {
            result += artifactData.SkillDamageMultiplier;
            result += statBuff.SkillDamage;
        }
        if (parameterWithKey.type == Skill.Type.Burst)
        {
            result += artifactData.BurstDamageMultiplier;
            result += statBuff.BurstDamage;
        }
        if (parameterWithKey.type == Skill.Type.Basic)
        {
            result += artifactData.BaseDamageMultiplier(parameterWithKey.name);
            result += statBuff.BaseDamagePer;
        }
        if (elementType == Element.Type.Physics)
        {
            result += PhysicsDamage;
        }
        if (elementType == Element.Type.Pyro)
        {
            result += PyroDamage;
        }
        if (elementType == Element.Type.Hydro)
        {
            result += HydroDamage;
        }
        if (elementType == Element.Type.Anemo)
        {
            result += AnemoDamage;
        }
        if (elementType == Element.Type.Dendro)
        {
            result += DendroDamage;
        }
        if (elementType == Element.Type.Electro)
        {
            result += ElectroDamage;
        }
        if (elementType == Element.Type.Cyro)
        {
            result += CyroDamage;
        }
        if (elementType == Element.Type.Geo)
        {
            result += GeoDamage;
        }
        if (artifactData != null)
        {
            result += artifactData.Maiden_Beloved();
        }
        return result;
    }
    public float CalcDamage(Enemy enemy, SkillData.ParameterWithKey parameterWithKey, float damage, Element.Type elementType)
    {
        float damageMultipllier = 1.0f;
        float result = 0;

        damageMultipllier += CalcElemenatlRes(enemy, elementType);

        damageMultipllier += GameManager.instance.artifactData.Brave_Heart(enemy.maxHealth, enemy.health);

        damageMultipllier *= CalcCritical(enemy, parameterWithKey);

        damageMultipllier += statBuff.Claymore_Serpent_SpineStack * statBuff.Claymore_Serpent_SpineDamage;

        if (typeof(EnemyNormal) == enemy.GetType())
        {
            damageMultipllier *= Sword_Lions_Loar((EnemyNormal)enemy);
            damageMultipllier *= Claymore_Rainslasher((EnemyNormal)enemy);
            damageMultipllier *= Spear_Dragons_Bane((EnemyNormal)enemy);
        } 
        float armorMultiplier = 10 / (10 + enemy.armor);
        result = damage * damageMultipllier * armorMultiplier;

        if (parameterWithKey.parameter.type == Element.Type.Cyro)
        {
            if (GameManager.instance.statBuff.E_Shenhe_Stack > 0)
            {
                result += GameManager.instance.statCalculator.Atk * 0.3f;
            }
        }

        if (result < 0)
        {
            result = 0;
        }
        return result + FinalDamage;
    }

    float Sword_Lions_Loar(EnemyNormal enemy)
    {
        float damageMultipllier = 1.0f;
        switch (enemy.elementReaction.elementAttach1.elementType)
        {
            case Element.Type.Pyro:
            case Element.Type.Electro:
                damageMultipllier = 1.0f + statBuff.Sword_Lions_Loar;
                break;
        }

        switch (enemy.elementReaction.elementAttach2.elementType)
        {
            case Element.Type.Pyro:
            case Element.Type.Electro:
                damageMultipllier = 1.0f + statBuff.Sword_Lions_Loar;
                break;
        }

        return damageMultipllier;
    }

    float Claymore_Rainslasher(EnemyNormal enemy)
    {
        float damageMultipllier = 1.0f;
        switch (enemy.elementReaction.elementAttach1.elementType)
        {
            case Element.Type.Hydro:
            case Element.Type.Electro:
                damageMultipllier = 1.0f + statBuff.Claymore_Rainslasher;
                break;
        }

        switch (enemy.elementReaction.elementAttach2.elementType)
        {
            case Element.Type.Hydro:
            case Element.Type.Electro:
                damageMultipllier = 1.0f + statBuff.Claymore_Rainslasher;
                break;
        }

        return damageMultipllier;
    }

    float Spear_Dragons_Bane(EnemyNormal enemy)
    {
        float damageMultipllier = 1.0f;
        switch (enemy.elementReaction.elementAttach1.elementType)
        {
            case Element.Type.Pyro:
            case Element.Type.Hydro:
                damageMultipllier = 1.0f + statBuff.Spear_Dragons_Bane;
                break;
        }

        switch (enemy.elementReaction.elementAttach2.elementType)
        {
            case Element.Type.Pyro:
            case Element.Type.Hydro:
                damageMultipllier = 1.0f + statBuff.Spear_Dragons_Bane;
                break;
        }

        return damageMultipllier;
    }

    float FinalDamage
    {
        get
        {
            return GameManager.instance.artifactData.FinalDamage;
        }
    }

    public float CalcDamageSkill(SkillSet.SkillSequence skillSequence, SkillData.ParameterWithKey parameterWithKey, Element.Type elementType)
    {
        float damage = parameterWithKey.parameter.damage;
        float stat = SkillDamageStat(skillSequence);
        float damageMultipllier = 1.0f;
        float result = 0;

        damageMultipllier += DamageMultiplier(parameterWithKey, skillSequence.elementType);
        result = stat * damageMultipllier * damage * skillSequence.damage;

        if (parameterWithKey.type == Skill.Type.Basic)
        {
            foreach (Character character in GameDataManager.instance.saveData.userData.selectChars)
            {
                if (character == null) continue;
                if (character.constellation == null) continue;
                if (character.charNum == CharacterData.Name.Chracter_None) continue;
                if (character.charNum == CharacterData.Name.Xinyan)
                {
                    if (character.constellation[5])
                    {
                        result += (Armor * 0.2f);
                    }
                }
                if (character.charNum == CharacterData.Name.Hutao)
                {
                    if (character.constellation[1])
                    {
                        result += (Health * 0.02f);
                    }
                }
                if (character.charNum == CharacterData.Name.Kazuha)
                {
                    if (character.constellation[5])
                    {
                        result += (ElementalMastery * 0.02f);
                    }
                }
            }
            result += (Health * statBuff.EB_Kokomi);
            result += (Health * statBuff.HealthBaseDamagePer);
            result += (Armor * statBuff.ArmorBaseDamagePer);
        }
        if (parameterWithKey.type == Skill.Type.Burst)
        {
            result += (result * statBuff.BurstDamagePer);
        }
        result += (result * statBuff.RegenDamagePer);


        return result;
    }

    public float SkillDamageStat(SkillSet.SkillSequence skillSequence)
    {
        float result = Atk;
        switch (skillSequence.damageStat)
        {
            case SkillSet.SkillDamageStat.ATK:
                result = Atk;
                break;
            case SkillSet.SkillDamageStat.ARMOR:
                result = Armor;
                break;
            case SkillSet.SkillDamageStat.HP:
                result = Health;
                break;
            case SkillSet.SkillDamageStat.ELEMENT_MASTERY:
                result = ElementalMastery;
                break;
        }
        return result;
    }

    float CalcElemenatlRes(Enemy enemy, Element.Type elementType)
    {
        float damageMultipllier = 0;
        switch (elementType)
        {
            case Element.Type.Physics:
                damageMultipllier -= enemy.PhysicsRes;
                break;
            case Element.Type.Pyro:
                damageMultipllier -= enemy.PyroRes;
                break;
            case Element.Type.Hydro:
                damageMultipllier -= enemy.HydroRes;
                break;
            case Element.Type.Anemo:
                damageMultipllier -= enemy.AnemoRes;
                break;
            case Element.Type.Dendro:
                damageMultipllier -= enemy.DendroRes;
                break;
            case Element.Type.Electro:
                damageMultipllier -= enemy.ElectroRes;
                break;
            case Element.Type.Cyro:
                damageMultipllier -= enemy.CyroRes;
                break;
            case Element.Type.Geo:
                damageMultipllier -= enemy.GeoRes;
                break;
            case Element.Type.Fix:
                damageMultipllier = 1;
                break;
        }

        return damageMultipllier;
    }

    float CalcCritical(Enemy enemy, SkillData.ParameterWithKey parameterWithKey)
    {
        float result = 1.0f;
        int randomNum = Random.Range(0, 100);
        float luck = GameManager.instance.statCalculator.Luck;
        float criticalRate = luck;
        float criticalDamage = 0.5f + (luck * 5 / 100.0f);
        if (parameterWithKey.type == Skill.Type.Basic)
        {
            criticalRate += criticalRate * GameManager.instance.artifactData.BaseAttackCriticalRate;
        }
        if (parameterWithKey.name == SkillName.EB_Xinyan)
        {
            if (parameterWithKey.constellations.num1)
            {
                result += criticalDamage;
                return result;
            }
        }
        if (parameterWithKey.name == SkillName.E_Yoimiya)
        {
            if (parameterWithKey.constellations.num1)
            {
                result += criticalDamage;
                return result;
            }
        }
        if (randomNum < criticalRate || GameManager.instance.statBuff.allCritical)
        {
            result += criticalDamage;
        }
        return result;
    }

    public float SheildMultipllier
    {
        get
        {
            float result = 1.0f;
            result += artifactData.ShieldMultiplier;
            result += statBuff.SheildPer;
            return result;
        }
    }

    float CataclysmDamage
    {
        get
        {
            float defaultDamage = GameManager.instance.gameInfoData.level * 0.54f;
            float scala = 1 + 16 * (ElementalMastery / (ElementalMastery + 2000));
            return defaultDamage * scala;
        }
    }

    float AmplificationDamage
    {
        get
        {
            return 1 + 2.78f * (ElementalMastery / (ElementalMastery + 1400));
        }
    }

    public float ReactionDamage(SkillData.ParameterWithKey parameterWithKey, float damage, Element.Type elementType)
    {
        float result = 0;
        switch (parameterWithKey.name)
        {
            case SkillName.Burning:
                result = CataclysmDamage * 0.25f;
                break;
            case SkillName.Superconduct:
                result = CataclysmDamage * 0.5f;
                break;
            case SkillName.Swirl:
                result = CataclysmDamage * 0.6f;
                break;
            case SkillName.ElectroCharged:
                result = CataclysmDamage * 1.2f;
                break;
            case SkillName.Overloaded:
                result = CataclysmDamage * 2.0f;
                break;
            case SkillName.Bloom:
                result = CataclysmDamage * 2.0f;
                break;
            case SkillName.Burgeon:
                result = CataclysmDamage * 3.0f;
                break;
            case SkillName.Hyperbloom:
                result = CataclysmDamage * 3.0f;
                break;
            case SkillName.Vaporize:
            case SkillName.Melt:
                result = damage * 2.0f;
                result += result * AmplificationDamage;
                result -= damage;
                break;
        }
        result += (result * DamageMultiplier(parameterWithKey, elementType)) + FinalDamage;
        return result;
    }

    public float ReactionSheid
    {
        get
        {
            float result = CataclysmDamage * 1.0f;
            result += result * SheildMultipllier;
            return result;
        }
    }

    public float GetRankUpValue(Character character, StatType statType)
    {
        int rank = 0;
        if (GameManager.instance != null)
        {
            rank = GameManager.instance.gameInfoData.level / 10;
        }
        float multiplier = (character.rankUpValue * rank / 100.0f);
        float result = 0;

        if (statType != character.rankUpStat) return 0;

        switch (character.rankUpStat)
        {
            case StatType.ATK:
                result += character.atk * multiplier;
                break;
            case StatType.ARMOR:
                result += character.armor * multiplier;
                break;
            case StatType.HP:
                result += character.hp * multiplier;
                break;
            case StatType.HEAL:
                result += multiplier;
                break;
            case StatType.COOLTIME:
                result += multiplier;
                break;
            case StatType.AREA:
                result += multiplier;
                break;
            case StatType.ASPEED:
                result += multiplier;
                break;
            case StatType.DURATION:
                result += multiplier;
                break;
            case StatType.AMOUNT:
                result += (character.rankUpValue * rank);
                break;
            case StatType.SPEED:
                result += character.speed * multiplier;
                break;
            case StatType.MAGNET:
                result += multiplier;
                break;
            case StatType.LUCK:
                result += (character.rankUpValue * rank);
                break;
            case StatType.REGEN:
                result += multiplier;
                break;
            case StatType.EXP:
                result += multiplier;
                break;
            case StatType.GREED:
                result += multiplier;
                break;
            case StatType.CURSE:
                result += (character.rankUpValue * rank);
                break;
            case StatType.RESURRATION:
                result += (character.rankUpValue * rank);
                break;
            case StatType.REROLL:
                result += (character.rankUpValue * rank);
                break;
            case StatType.SKIP:
                result += (character.rankUpValue * rank);
                break;
            case StatType.PHYSICS_DMG:
                result += multiplier;
                break;
            case StatType.PYRO_DMG:
                result += multiplier;
                break;
            case StatType.HYDRO_DMG:
                result += multiplier;
                break;
            case StatType.ANEMO_DMG:
                result += multiplier;
                break;
            case StatType.DENDRO_DMG:
                result += multiplier;
                break;
            case StatType.ELECTRO_DMG:
                result += multiplier;
                break;
            case StatType.CYRO_DMG:
                result += multiplier;
                break;
            case StatType.GEO_DMG:
                result += multiplier;
                break;
            case StatType.ELEMENT_MASTERY:
                result += character.elementMastery * rank;
                break;
            default:
                break;
        }

        if (artifactData != null)
        {
            result *= artifactData.Echoes_of_an_Offering();
        }

        return result;
    }

    public float GameLevelCorrection
    {
        get
        {
            float result = 1.0f;
            float gameLevel = GameManager.instance.gameInfoData.gameLevel - 1;
            result *= GameManager.instance.statCalculator.Curse;
            if (gameLevel > 0)
            {
                result += (result * (GameManager.instance.gameInfoData.gameLevel * 0.2f));
            }
            return result;
        }
    }



    public List<string> ToolTip()
    {
        List<string> result = new List<string>();
        UpgradeComponent[] upComps = GameDataManager.instance.saveData.userData.upgrade.upgradeComponents;
        result.Add(("Basic.ElementType.".AddString(stat.elementType.ToString())).Localize());
        result.Add(upComps[(int)UpgradeType.ATK].GetTooltip(stat.atk, Atk));
        result.Add(upComps[(int)UpgradeType.ARMOR].GetTooltip(stat.armor, Armor));
        result.Add(upComps[(int)UpgradeType.HP].GetTooltip(stat.hp, Health));
        result.Add(upComps[(int)UpgradeType.HEAL].GetTooltip(stat.heal, HealBonus));
        result.Add(upComps[(int)UpgradeType.COOL].GetTooltip(stat.cooltime, Cooltime));
        result.Add(upComps[(int)UpgradeType.AREA].GetTooltip(stat.area, Area));
        result.Add(upComps[(int)UpgradeType.SPEED].GetTooltip(stat.aspeed, Aspeed));
        result.Add(upComps[(int)UpgradeType.DURATION].GetTooltip(stat.duration, Duration));
        result.Add(upComps[(int)UpgradeType.AMOUNT].GetTooltip(stat.amount, Amount));
        result.Add(upComps[(int)UpgradeType.MSPEED].GetTooltip(stat.speed, Speed));
        result.Add(upComps[(int)UpgradeType.MAGNET].GetTooltip(stat.magnet, Magnet));
        result.Add(upComps[(int)UpgradeType.LUCK].GetTooltip(stat.luck, Luck));
        result.Add(upComps[(int)UpgradeType.GROWTH].GetTooltip(stat.regen, Regen));
        result.Add(upComps[(int)UpgradeType.GREED].GetTooltip(stat.greed, Greed));
        result.Add(upComps[(int)UpgradeType.CURSE].GetTooltip(stat.curse, Curse));
        result.Add(upComps[(int)UpgradeType.REBIRTH].GetTooltip(stat.resurration, Rebirth));
        result.Add(upComps[(int)UpgradeType.REROLL].GetTooltip(stat.reroll, Reroll));
        result.Add(upComps[(int)UpgradeType.SKIP].GetTooltip(stat.skip, Skip));

        result.Add("Skill.".AddString(stat.toolTipKey, "0").Localize());
        result.Add("Skill.".AddString(stat.toolTipKey, "1").Localize());

        result.Add("Basic.PhysicsDmg".Localize(PhysicsDamage.GetPercetage()));
        result.Add("Basic.PyroDmg".Localize(PyroDamage.GetPercetage()));
        result.Add("Basic.HydroDmg".Localize(HydroDamage.GetPercetage()));
        result.Add("Basic.AnemoDmg".Localize(AnemoDamage.GetPercetage()));
        result.Add("Basic.DendroDmg".Localize(DendroDamage.GetPercetage()));
        result.Add("Basic.ElectroDmg".Localize(ElectroDamage.GetPercetage()));
        result.Add("Basic.CyroDmg".Localize(CyroDamage.GetPercetage()));
        result.Add("Basic.GeoDmg".Localize(GeoDamage.GetPercetage()));
        result.Add("Basic.ElementalMastery".Localize(stat.elementMastery));


        if (stat.constellation != null && stat.toolTipKey != null)
        {
            for (int i = 0; i < stat.constellation.Length; i++)
            {
                bool isStella = stat.constellation[i];
                string constellationToolTipKey = Character.Constellation_Sheet + stat.toolTipKey + i; //"Constellation.Travler.Anemo1"
                string constellationToolTip = constellationToolTipKey.Localize();
                if (isStella)
                {
                    result.Add("\n".AddString(constellationToolTip, "\n"));
                }
                else
                {
                    result.Add("\n<#aaaaaa>".AddString(constellationToolTip, "</color>\n"));
                }
            }
        }
        return result;
    }

    public StatCalculator StatBuff(StatBuff statBuff)
    {
        this.statBuff = statBuff;
        return this;
    }
}
