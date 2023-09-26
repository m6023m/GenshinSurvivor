using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Character;
public class StatBuff
{
    public float _Atk = 0.0f;
    public float _Armor = 0.0f;
    public float _Hp = 0.0f;
    public float _Heal = 0.0f;
    public float _Cooltime = 0.0f;
    public float _Area = 0.0f;
    public float _Aspeed = 0.0f;
    public float _Duration = 0.0f;
    public float _Amount = 0.0f;
    public float _Speed = 0.0f;
    public float _Magnet = 0.0f;
    public float _Luck = 0.0f;
    public float _Regen = 0.0f;
    public float _Exp = 0.0f;
    public float _Greed = 0.0f;
    public float _Curse = 0.0f;
    public float _Resurraction = 0.0f;
    public float _Reroll = 0.0f;
    public float _Skip = 0.0f;
    public float _PhysicsDmg = 0.0f;
    public float _PyroDmg = 0.0f;
    public float _HydroDmg = 0.0f;
    public float _AnemoDmg = 0.0f;
    public float _DendroDmg = 0.0f;
    public float _ElectroDmg = 0.0f;
    public float _CyroDmg = 0.0f;
    public float _GeoDmg = 0.0f;
    public float _BaseCooltime = 0.0f;
    public float _BaseDamage = 0.0f;
    public float _SkillDamage = 0.0f;
    public float _BurstDamage = 0.0f;
    public float _SheildPer = 0.0f;
    public float _KnwooDamagePer = 0.0f;
    public float _HealthDamagePer = 0.0f;
    public float Atk
    {
        get
        {
            return _Atk;
        }
        set
        {
            if (value != _Atk)
            {
                _Atk = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.ATK] = true;
            }
        }
    }
    public float Armor
    {
        get
        {
            return _Armor;
        }
        set
        {
            if (value != _Armor)
            {
                _Armor = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.ARMOR] = true;
            }
        }
    }
    public float Hp
    {
        get
        {
            return _Hp;
        }
        set
        {
            if (value != _Hp)
            {
                _Hp = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.HP] = true;
            }
        }
    }
    public float Heal
    {
        get
        {
            return _Heal;
        }
        set
        {
            if (value != _Heal)
            {
                _Heal = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.HEAL] = true;
            }
        }
    }
    public float Cooltime
    {
        get
        {
            return _Cooltime;
        }
        set
        {
            if (value != _Cooltime)
            {
                _Cooltime = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.COOLTIME] = true;
            }
        }
    }
    public float Area
    {
        get
        {
            return _Area;
        }
        set
        {
            if (value != _Area)
            {
                _Area = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.AREA] = true;
            }
        }
    }
    public float Aspeed
    {
        get
        {
            return _Aspeed;
        }
        set
        {
            if (value != _Aspeed)
            {
                _Aspeed = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.ASPEED] = true;
            }
        }
    }
    public float Duration
    {
        get
        {
            return _Duration;
        }
        set
        {
            if (value != _Duration)
            {
                _Duration = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.DURATION] = true;
            }
        }
    }
    public float Amount
    {
        get
        {
            return _Amount;
        }
        set
        {
            if (value != _Amount)
            {
                _Amount = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.AMOUNT] = true;
            }
        }
    }
    public float Speed
    {
        get
        {
            return _Speed;
        }
        set
        {
            if (value != _Speed)
            {
                _Speed = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.SPEED] = true;
            }
        }
    }
    public float Magnet
    {
        get
        {
            return _Magnet;
        }
        set
        {
            if (value != _Magnet)
            {
                _Magnet = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.MAGNET] = true;
            }
        }
    }
    public float Luck
    {
        get
        {
            return _Luck;
        }
        set
        {
            if (value != _Luck)
            {
                _Luck = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.LUCK] = true;
            }
        }
    }
    public float Regen
    {
        get
        {
            return _Regen;
        }
        set
        {
            if (value != _Regen)
            {
                _Regen = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.REGEN] = true;
            }
        }
    }
    public float Exp
    {
        get
        {
            return _Exp;
        }
        set
        {
            if (value != _Exp)
            {
                _Exp = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.EXP] = true;
            }
        }
    }
    public float Greed
    {
        get
        {
            return _Greed;
        }
        set
        {
            if (value != _Greed)
            {
                _Greed = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.GREED] = true;
            }
        }
    }
    public float Curse
    {
        get
        {
            return _Curse;
        }
        set
        {
            if (value != _Curse)
            {
                _Curse = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.CURSE] = true;
            }
        }
    }
    public float Resurraction
    {
        get
        {
            return _Resurraction;
        }
        set
        {
            if (value != _Resurraction)
            {
                _Resurraction = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.RESURRATION] = true;
            }
        }
    }
    public float Reroll
    {
        get
        {
            return _Reroll;
        }
        set
        {
            if (value != _Reroll)
            {
                _Reroll = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.REROLL] = true;
            }
        }
    }
    public float Skip
    {
        get
        {
            return _Skip;
        }
        set
        {
            if (value != _Skip)
            {
                _Skip = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.SKIP] = true;
            }
        }
    }
    public float PhysicsDmg
    {
        get
        {
            return _PhysicsDmg;
        }
        set
        {
            if (value != _PhysicsDmg)
            {
                _PhysicsDmg = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.PHYSICS_DMG] = true;
            }
        }
    }
    public float PyroDmg
    {
        get
        {
            return _PyroDmg;
        }
        set
        {
            if (value != _PyroDmg)
            {
                _PyroDmg = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.PYRO_DMG] = true;
            }
        }
    }
    public float HydroDmg
    {
        get
        {
            return _HydroDmg;
        }
        set
        {
            if (value != _HydroDmg)
            {
                _HydroDmg = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.HYDRO_DMG] = true;
            }
        }
    }
    public float AnemoDmg
    {
        get
        {
            return _AnemoDmg;
        }
        set
        {
            if (value != _AnemoDmg)
            {
                _AnemoDmg = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.ANEMO_DMG] = true;
            }
        }
    }
    public float DendroDmg
    {
        get
        {
            return _DendroDmg;
        }
        set
        {
            if (value != _DendroDmg)
            {
                _DendroDmg = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.DENDRO_DMG] = true;
            }
        }
    }
    public float ElectroDmg
    {
        get
        {
            return _ElectroDmg;
        }
        set
        {
            if (value != _ElectroDmg)
            {
                _ElectroDmg = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.ELECTRO_DMG] = true;
            }
        }
    }
    public float CyroDmg
    {
        get
        {
            return _CyroDmg;
        }
        set
        {
            if (value != _CyroDmg)
            {
                _CyroDmg = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.CYRO_DMG] = true;
            }
        }
    }
    public float GeoDmg
    {
        get
        {
            return _GeoDmg;
        }
        set
        {
            if (value != _GeoDmg)
            {
                _GeoDmg = value;
                GameManager.instance.statCalcuator.isStatusChangedType[StatType.GEO_DMG] = true;
            }
        }
    }
    public float BaseCooltime = 0.0f;
    public float BaseDamage = 0.0f;
    public float SkillDamage = 0.0f;
    public float BurstDamage = 0.0f;
    public float SheildPer = 0.0f;
    public float KnwooDamagePer = 0.0f;
    public float HealthDamagePer = 0.0f;
    public void allDamageAdd(float value)
    {
        PhysicsDmg += value;
        PyroDmg += value;
        HydroDmg += value;
        AnemoDmg += value;
        DendroDmg += value;
        ElectroDmg += value;
        CyroDmg += value;
        GeoDmg += value;
    }
    public void allDamageMinus(float value)
    {
        PhysicsDmg -= value;
        PyroDmg -= value;
        HydroDmg -= value;
        AnemoDmg -= value;
        DendroDmg -= value;
        ElectroDmg -= value;
        CyroDmg -= value;
        GeoDmg -= value;
    }
    public void elementalDamageAdd(float value)
    {
        PyroDmg += value;
        HydroDmg += value;
        AnemoDmg += value;
        DendroDmg += value;
        ElectroDmg += value;
        CyroDmg += value;
        GeoDmg += value;
    }
    public void elementalDamageMinus(float value)
    {
        PyroDmg -= value;
        HydroDmg -= value;
        AnemoDmg -= value;
        DendroDmg -= value;
        ElectroDmg -= value;
        CyroDmg -= value;
        GeoDmg -= value;
    }
    public float ElementMastery = 0.0f;
    public bool isResonance = false;
    public bool isThundering_FuryCooltime = false;
    public float eulaSkillStack = 0;
    public float eulaStack = 0;
    public bool hutaoConstell5 = false;
    public bool allCritical = false;

    public float Sword_Lions_Loar = 0;
    public float Claymore_Rainslasher = 0;
    public float Spear_Dragons_Bane = 0;
    public float Claymore_Serpent_SpineStack = 0;
    public float Claymore_Serpent_SpineDamage = 0;
    public float Claymore_Serpent_SpineReceiveDamage = 0;
    public float Claymore_Skyward_PrideStack = 0;

    public int E_Travler_Electro = 1;
    public float immuneTimeRaiden = 3.0f;
    public float E_Shenhe_Stack = 0;
    public float E_Shenhe_Stack_Minus = 1;
    public float E_Heizo_Stack = 0;
    public float E_Heizo_Stack_Plus = 1;
    public float EB_Kokomi = 0.00f;
    public float EB_Miko_Stack = 0;
}
