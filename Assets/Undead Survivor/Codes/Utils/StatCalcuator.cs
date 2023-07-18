using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StatCalculator
{
    Character stat;
    WeaponData.Parameter weaponStat;
    ArtifactData artifactData;
    UpgradeComponent[] upgradeComponents;
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

    public float Atk
    {
        get
        {
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
                result += Helath * statBuff.healthDamagePer;
            }
            result += GetRankUpValue(stat, Character.StatType.ATK);
            result += (stat.atk * upgradeComponents[(int)UpgradeType.ATK].GetValue());
            return result + statBuff.atk;
        }
    }
    public float Armor
    {
        get
        {
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
            result += GetRankUpValue(stat, Character.StatType.ARMOR);
            result += upgradeComponents[(int)UpgradeType.ARMOR].GetValue();
            return result + statBuff.armor;
        }
    }
    public float Helath
    {
        get
        {
            float result = stat.hp;
            if (artifactData != null)
            {
                result += (result * artifactData.HealthMultiplier);
            }
            if (weaponStat != null)
            {
                result += (result * weaponStat.HP_PER);
            }
            result += GetRankUpValue(stat, Character.StatType.HP);
            result += (stat.hp * upgradeComponents[(int)UpgradeType.HP].GetValue());
            return result + statBuff.hp;
        }
    }

    public float HealBonus
    {
        get
        {
            float result = stat.heal;
            if (artifactData != null)
            {
                result += (result * artifactData.HealBonusMultiplier);
            }
            result += GetRankUpValue(stat, Character.StatType.HEAL);
            result += upgradeComponents[(int)UpgradeType.HEAL].GetValue();
            return result + statBuff.heal;
        }
    }
    public float Cooltime
    {
        get
        {
            float result = stat.cooltime;
            result += GetRankUpValue(stat, Character.StatType.COOLTIME);
            result += upgradeComponents[(int)UpgradeType.COOL].GetValue();
            return result + statBuff.cooltime;
        }
    }
    public float CooltimeWithArtifact(Skill.Type skillType)
    {
        float result = stat.cooltime;
        if (artifactData != null)
        {
            result += artifactData.CoolTimeMultiplier(skillType);
        }
        if (skillType == Skill.Type.Basic)
        {
            result += statBuff.baseCooltime;
        }
        result += GetRankUpValue(stat, Character.StatType.COOLTIME);
        result += upgradeComponents[(int)UpgradeType.COOL].GetValue();
        return result + statBuff.cooltime;
    }

    public float Area
    {
        get
        {
            float result = stat.area;
            if (artifactData != null)
            {
                result += (result * artifactData.AreaMultiplier);
            }
            result += GetRankUpValue(stat, Character.StatType.AREA);
            result += upgradeComponents[(int)UpgradeType.AREA].GetValue();
            return result + statBuff.area;
        }
    }
    public float Aspeed
    {
        get
        {
            float result = stat.aspeed;
            if (artifactData != null)
            {
                result += (result * artifactData.AttackSpeedMultiplier);
            }
            result += GetRankUpValue(stat, Character.StatType.ASPEED);
            result += (stat.aspeed * upgradeComponents[(int)UpgradeType.SPEED].GetValue());
            return result + statBuff.aspeed;
        }
    }
    public float Duration
    {
        get
        {
            float result = stat.duration;
            if (artifactData != null)
            {
                result += (result * artifactData.DurationMultiplier);
            }
            result += GetRankUpValue(stat, Character.StatType.DURATION);
            result += upgradeComponents[(int)UpgradeType.DURATION].GetValue();
            return result + statBuff.duration;
        }
    }
    public float Amount
    {
        get
        {
            float result = stat.amount;
            if (artifactData != null)
            {
            }
            result += GetRankUpValue(stat, Character.StatType.AMOUNT);
            result += upgradeComponents[(int)UpgradeType.AMOUNT].GetValue();
            return result + statBuff.amount;
        }
    }
    public float Speed
    {
        get
        {
            float result = stat.speed;
            if (artifactData != null)
            {
                result += (result * artifactData.MoveSpeedMultiplier);
            }
            result += GetRankUpValue(stat, Character.StatType.SPEED);
            result += (stat.speed * upgradeComponents[(int)UpgradeType.SPEED].GetValue());
            return result + statBuff.speed;
        }
    }

    public float Magnet
    {
        get
        {
            float result = stat.magnet;
            if (artifactData != null)
            {
                result += artifactData.MagnetMultiplier;
            }
            result += GetRankUpValue(stat, Character.StatType.MAGNET);
            result += upgradeComponents[(int)UpgradeType.MAGNET].GetValue();
            return result + statBuff.magnet;
        }
    }
    public float Luck
    {
        get
        {
            float result = stat.luck;
            if (artifactData != null)
            {
                result += artifactData.Luck;
            }
            if (weaponStat != null)
            {
                result += weaponStat.LUCK;
            }
            result += GetRankUpValue(stat, Character.StatType.LUCK);
            result += upgradeComponents[(int)UpgradeType.LUCK].GetValue();
            return result + statBuff.luck;
        }
    }
    public float Regen
    {
        get
        {
            float result = stat.regen;
            if (artifactData != null)
            {
                result += artifactData.RegenMultiplier;
            }
            if (weaponStat != null)
            {
                result += weaponStat.REGEN;
            }
            result += GetRankUpValue(stat, Character.StatType.REGEN);
            result += (stat.regen * upgradeComponents[(int)UpgradeType.GROWTH].GetValue());
            return result + statBuff.regen;
        }
    }
    public float Exp
    {
        get
        {
            float result = stat.exp;
            if (artifactData != null)
            {
                result += artifactData.RegenMultiplier;
            }
            result += GetRankUpValue(stat, Character.StatType.EXP);
            result += (stat.regen * upgradeComponents[(int)UpgradeType.GROWTH].GetValue());
            return result + statBuff.regen;
        }
    }
    public float Greed
    {
        get
        {
            float result = stat.greed;
            if (artifactData != null)
            {
            }
            result += GetRankUpValue(stat, Character.StatType.GREED);
            result += upgradeComponents[(int)UpgradeType.GREED].GetValue();
            return result + statBuff.greed;
        }
    }
    public float Curse
    {
        get
        {
            float result = stat.curse;
            if (artifactData != null)
            {
            }
            result += GetRankUpValue(stat, Character.StatType.CURSE);
            result += upgradeComponents[(int)UpgradeType.CURSE].GetValue();
            return result + statBuff.curse;
        }
    }
    public float Rebirth
    {
        get
        {
            float result = stat.resurration;
            if (artifactData != null)
            {
                result += artifactData.Rebirth;
            }
            result += GetRankUpValue(stat, Character.StatType.RESURRATION);
            result += upgradeComponents[(int)UpgradeType.REBIRTH].GetValue();
            return result + statBuff.resurration;
        }
    }
    public float Reroll
    {
        get
        {
            float result = stat.reroll;
            if (artifactData != null)
            {
            }
            result += GetRankUpValue(stat, Character.StatType.REROLL);
            result += upgradeComponents[(int)UpgradeType.REROLL].GetValue();
            return result + statBuff.reroll;
        }
    }
    public float Skip
    {
        get
        {
            float result = stat.skip;
            if (artifactData != null)
            {
            }
            result += GetRankUpValue(stat, Character.StatType.SKIP);
            result += upgradeComponents[(int)UpgradeType.SKIP].GetValue();
            return result + statBuff.skip;
        }
    }
    public float PhysicsDamage
    {
        get
        {
            float result = stat.physicsDmg;
            if (artifactData != null)
            {
                result += artifactData.PhysicsDamageMultiplier;
                result += artifactData.DamageMultiplier;
                result += statBuff.knwooDamagePer;
                if (GameManager.instance.player.sheilds.Count > 0)
                {
                    result += statBuff.knwooDamagePer;
                }
            }
            if (weaponStat != null)
            {
                result += weaponStat.PHYSICS_PER;
            }
            result += GetRankUpValue(stat, Character.StatType.PHYSICS_DMG);
            return result + statBuff.physicsDmg;
        }
    }
    public float PyroDamage
    {
        get
        {
            float result = stat.pyroDmg;
            if (artifactData != null)
            {
                result += artifactData.DamageMultiplier;
                result += statBuff.knwooDamagePer;
                if (GameManager.instance.player.sheilds.Count > 0)
                {
                    result += statBuff.knwooDamagePer;
                }
            }
            result += GetRankUpValue(stat, Character.StatType.PYRO_DMG);
            return result + statBuff.pyroDmg;
        }
    }
    public float HydroDamage
    {
        get
        {
            float result = stat.hydroDmg;
            if (artifactData != null)
            {
                result += artifactData.DamageMultiplier;
                result += statBuff.knwooDamagePer;
                if (GameManager.instance.player.sheilds.Count > 0)
                {
                    result += statBuff.knwooDamagePer;
                }
            }
            result += GetRankUpValue(stat, Character.StatType.HYDRO_DMG);
            return result + statBuff.hydroDmg;
        }
    }
    public float AnemoDamage
    {
        get
        {
            float result = stat.anemoDmg;
            if (artifactData != null)
            {
                result += artifactData.DamageMultiplier;
                result += statBuff.knwooDamagePer;
                if (GameManager.instance.player.sheilds.Count > 0)
                {
                    result += statBuff.knwooDamagePer;
                }
            }
            result += GetRankUpValue(stat, Character.StatType.ANEMO_DMG);
            return result + statBuff.anemoDmg;
        }
    }
    public float DendroDamage
    {
        get
        {
            float result = stat.dendroDmg;
            if (artifactData != null)
            {
                result += artifactData.DamageMultiplier;
                result += statBuff.knwooDamagePer;
                if (GameManager.instance.player.sheilds.Count > 0)
                {
                    result += statBuff.knwooDamagePer;
                }
            }
            result += GetRankUpValue(stat, Character.StatType.DENDRO_DMG);
            return result + statBuff.dendroDmg;
        }
    }
    public float ElectroDamage
    {
        get
        {
            float result = stat.electroDmg;
            if (artifactData != null)
            {
                result += artifactData.DamageMultiplier;
                result += statBuff.knwooDamagePer;
                if (GameManager.instance.player.sheilds.Count > 0)
                {
                    result += statBuff.knwooDamagePer;
                }
            }
            result += GetRankUpValue(stat, Character.StatType.ELECTRO_DMG);
            return result + statBuff.electroDmg;
        }
    }
    public float CyroDamage
    {
        get
        {
            float result = stat.cyroDmg;
            if (artifactData != null)
            {
                result += artifactData.DamageMultiplier;
                result += statBuff.knwooDamagePer;
                if (GameManager.instance.player.sheilds.Count > 0)
                {
                    result += statBuff.knwooDamagePer;
                }
            }
            result += GetRankUpValue(stat, Character.StatType.CYRO_DMG);
            return result + statBuff.cyroDmg;
        }
    }
    public float GeoDamage
    {
        get
        {
            float result = stat.geoDmg;
            if (artifactData != null)
            {
                result += artifactData.DamageMultiplier;
                result += statBuff.knwooDamagePer;
                if (GameManager.instance.player.sheilds.Count > 0)
                {
                    result += statBuff.knwooDamagePer;
                }
            }
            result += GetRankUpValue(stat, Character.StatType.GEO_DMG);
            return result + statBuff.geoDmg;
        }
    }
    public float ElementalMastery
    {
        get
        {
            float result = stat.elementMastery;
            if (artifactData != null)
            {
                result += artifactData.ElementalMastery;
            }
            result += GetRankUpValue(stat, Character.StatType.ELEMENT_MASTERY);
            return result + statBuff.elementMastery;
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
            result += statBuff.skillDamage;
        }
        if (parameterWithKey.type == Skill.Type.Burst)
        {
            result += artifactData.BurstDamageMultiplier;
            result += statBuff.burstDamage;
        }
        if (parameterWithKey.type == Skill.Type.Basic)
        {
            result += artifactData.BaseDamageMultiplier(parameterWithKey.name);
            result += statBuff.baseDamage;
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

        return result;
    }
    public float CalcDamage(Enemy enemy, SkillData.ParameterWithKey parameterWithKey, float damage, Element.Type elementType)
    {
        float damageMultipllier = 1.0f;
        float result = 0;

        damageMultipllier += CalcElemenatlRes(enemy, elementType);

        damageMultipllier += GameManager.instance.artifactData.Brave_Heart(enemy.maxHealth, enemy.health);

        damageMultipllier *= CalcCritical(enemy, parameterWithKey);
        result = ((damage * damageMultipllier) - enemy.armor);
        if (typeof(EnemyNormal) == enemy.GetType())
        {
            damageMultipllier *= Sword_Lions_Loar((EnemyNormal)enemy);
            damageMultipllier *= Claymore_Rainslasher((EnemyNormal)enemy);
            damageMultipllier *= Spear_Dragons_Bane((EnemyNormal)enemy);
        }

        damageMultipllier += statBuff.Claymore_Serpent_SpineStack * statBuff.Claymore_Serpent_SpineDamage;

        if (result < 0)
        {
            result = 0;
        }
        return (result) + FinalDamage;
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
                        result += (Helath * 0.02f);
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
        }

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
                result = Helath;
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
        }

        return damageMultipllier;
    }

    float CalcCritical(Enemy enemy, SkillData.ParameterWithKey parameterWithKey)
    {
        float result = 1.0f;
        int randomNum = Random.Range(0, 100);
        float luck = GameManager.instance.statCalcuator.Luck;
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
            result += statBuff.sheildPer;
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

    public float GetRankUpValue(Character character, Character.StatType statType)
    {
        int rank = level / 10;
        float multiplier = (character.rankUpValue * rank / 100.0f);
        float result = 0;

        if (statType != character.rankUpStat) return 0;

        switch (character.rankUpStat)
        {
            case Character.StatType.ATK:
                result += character.atk * multiplier;
                break;
            case Character.StatType.ARMOR:
                result += character.armor * multiplier;
                break;
            case Character.StatType.HP:
                result += character.hp * multiplier;
                break;
            case Character.StatType.HEAL:
                result += multiplier;
                break;
            case Character.StatType.COOLTIME:
                result += multiplier;
                break;
            case Character.StatType.AREA:
                result += multiplier;
                break;
            case Character.StatType.ASPEED:
                result += multiplier;
                break;
            case Character.StatType.DURATION:
                result += multiplier;
                break;
            case Character.StatType.AMOUNT:
                result += (character.rankUpValue * rank);
                break;
            case Character.StatType.SPEED:
                result += character.speed * multiplier;
                break;
            case Character.StatType.MAGNET:
                result += multiplier;
                break;
            case Character.StatType.LUCK:
                result += (character.rankUpValue * rank);
                break;
            case Character.StatType.REGEN:
                result += multiplier;
                break;
            case Character.StatType.EXP:
                result += multiplier;
                break;
            case Character.StatType.GREED:
                result += multiplier;
                break;
            case Character.StatType.CURSE:
                result += (character.rankUpValue * rank);
                break;
            case Character.StatType.RESURRATION:
                result += (character.rankUpValue * rank);
                break;
            case Character.StatType.REROLL:
                result += (character.rankUpValue * rank);
                break;
            case Character.StatType.SKIP:
                result += (character.rankUpValue * rank);
                break;
            case Character.StatType.PHYSICS_DMG:
                result += multiplier;
                break;
            case Character.StatType.PYRO_DMG:
                result += multiplier;
                break;
            case Character.StatType.HYDRO_DMG:
                result += multiplier;
                break;
            case Character.StatType.ANEMO_DMG:
                result += multiplier;
                break;
            case Character.StatType.DENDRO_DMG:
                result += multiplier;
                break;
            case Character.StatType.ELECTRO_DMG:
                result += multiplier;
                break;
            case Character.StatType.CYRO_DMG:
                result += multiplier;
                break;
            case Character.StatType.GEO_DMG:
                result += multiplier;
                break;
            case Character.StatType.ELEMENT_MASTERY:
                result += character.elementMastery * rank;
                break;
            default:
                break;
        }

        return result;
    }

    public float GameLevelCorrection
    {
        get
        {
            float result = 1.0f;
            float gameLevel = GameManager.instance.gameInfoData.gameLevel - 1;
            result *= GameManager.instance.statCalcuator.Curse;
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
        result.Add(upComps[(int)UpgradeType.HP].GetTooltip(stat.hp, Helath));
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
