using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "WeaponData", menuName = "GenshinSurvivor/WeaponData", order = 0)]
public class WeaponData : ScriptableObject
{
    [Searchable]
    public List<Parameter> weapons;
    [System.Serializable]
    public class Parameter
    {
        public WeaponName weaponName;
        public WeaponType weaponType;
        public Type type;
        public PickUpType pickUpType;
        public Sprite icon;
        public Sprite wishImage;
        public Sprite wishPannel;
        public Sprite wishIcon;
        public WeaponStat stat;
        public AtkTear atkTear;
        public Substat subStat;
        public Vector2[] values;
        public float[] valueSums
        {
            get
            {
                float[] valueSum = new float[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    float valueMin = values[i].x;
                    float value = values[i].y;
                    valueSum[i] = valueMin + (value * stat.rank);
                }
                return valueSum;
            }
        }
        public float ATK
        {
            get
            {
                float atkPer = 0.041f;
                switch (atkTear)
                {
                    case AtkTear.Tear_42:
                        atkPer = 0.042f;
                        break;
                    case AtkTear.Tear_44:
                        atkPer = 0.044f;
                        break;
                    case AtkTear.Tear_45:
                        atkPer = 0.045f;
                        break;
                    case AtkTear.Tear_46:
                        atkPer = 0.046f;
                        break;
                    case AtkTear.Tear_48:
                        atkPer = 0.048f;
                        break;
                    case AtkTear.Tear_49:
                        atkPer = 0.049f;
                        break;
                }

                return atkPer * stat.level;
            }
        }

        public float ATK_PER
        {
            get
            {
                if (subStat.type != SubstatType.ATK_PER) return 0.0f;
                return subStat.value * stat.level;
            }
        }

        public float ARMOR_PER
        {
            get
            {
                if (subStat.type != SubstatType.ARMOR_PER) return 0.0f;
                return subStat.value * stat.level;
            }
        }

        public float HP_PER
        {
            get
            {
                if (subStat.type != SubstatType.HP_PER) return 0.0f;
                return 0.0f + subStat.value * stat.level;
            }
        }

        public float LUCK
        {
            get
            {
                if (subStat.type != SubstatType.LUCK) return 0f;
                return subStat.value * stat.level;
            }
        }

        public float ELEMENT_MASTERY
        {
            get
            {
                if (subStat.type != SubstatType.ELEMENT_MASTERY) return 0f;
                return subStat.value * stat.level;
            }
        }

        public float REGEN
        {
            get
            {
                if (subStat.type != SubstatType.REGEN) return 0.0f;
                return subStat.value * stat.level;
            }
        }
        public float PHYSICS_PER
        {
            get
            {
                if (subStat.type != SubstatType.PHYSICS_PER) return 0f;
                return subStat.value * stat.level;
            }
        }

        public string Tooltip
        {
            get
            {
                string atk = "Weapon.Stat.ATK".Localize(ATK);
                string sub = "\n".AddString("Weapon.Stat.".AddString(subStat.type.ToString()).Localize(ATK_PER));
                string discription = "\n".AddString("Weapon.Discription.".AddString(weaponName.ToString()).Localize());
                if (valueSums.Length > 0)
                {
                    discription = "\n".AddString("Weapon.Discription.".AddString(weaponName.ToString()).LocalizeFloatArray(valueSums));
                }
                switch (subStat.type)
                {
                    case SubstatType.ATK_PER:
                        sub = "\n".AddString("Weapon.Stat.".AddString(subStat.type.ToString()).Localize(ATK_PER));
                        break;
                    case SubstatType.ARMOR_PER:
                        sub = "\n".AddString("Weapon.Stat.".AddString(subStat.type.ToString()).Localize(ARMOR_PER));
                        break;
                    case SubstatType.HP_PER:
                        sub = "\n".AddString("Weapon.Stat.".AddString(subStat.type.ToString()).Localize(HP_PER));
                        break;
                    case SubstatType.LUCK:
                        sub = "\n".AddString("Weapon.Stat.".AddString(subStat.type.ToString()).Localize(LUCK));
                        break;
                    case SubstatType.ELEMENT_MASTERY:
                        sub = "\n".AddString("Weapon.Stat.".AddString(subStat.type.ToString()).Localize(ELEMENT_MASTERY));
                        break;
                    case SubstatType.REGEN:
                        sub = "\n".AddString("Weapon.Stat.".AddString(subStat.type.ToString()).Localize(REGEN));
                        break;
                    case SubstatType.PHYSICS_PER:
                        sub = "\n".AddString("Weapon.Stat.".AddString(subStat.type.ToString()).Localize(PHYSICS_PER));
                        break;
                }


                return atk + sub + discription;
            }
        }
    }

    public void Init()
    {
        if (GameDataManager.instance != null)
        {
            foreach (KeyValuePair<WeaponName, WeaponStat> weapon in GameDataManager.instance.saveData.weaponStats)
            {
                weapons[(int)weapon.Key].stat = weapon.Value;
            }
        }
    }
    public enum AtkTear
    {
        Tear_41,
        Tear_42,
        Tear_44,
        Tear_45,
        Tear_46,
        Tear_48,
        Tear_49
    }
    public enum SubstatType
    {
        ATK_PER,
        ARMOR_PER,
        HP_PER,
        LUCK,
        ELEMENT_MASTERY,
        REGEN,
        PHYSICS_PER
    }

    public enum Type
    {
        None = -1,
        Normal,
        Rare,
        Player
    }

    public enum PickUpType
    {
        Normal,
        PickUp,
    }


    public Parameter Get(WeaponName name)
    {
        foreach (Parameter param in weapons)
        {
            if (param.weaponName.Equals(name))
            {
                return param;
            }
        }
        return null;
    }

    [System.Serializable]
    public class Substat
    {
        public SubstatType type;
        public float value;
    }
}
