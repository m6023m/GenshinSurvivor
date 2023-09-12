using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector;
using System.Linq;


[CreateAssetMenu(fileName = "SkillData", menuName = "GenshinSurvivor/SkillData", order = 0)]
public class SkillData : ScriptableObject
{

    public enum UpgradeName
    {
        Damage,
        Penetrate,
        Area,
        Duration,
        Speed,
        KnockBack,
        Magnet,
        MagnetSpeed,
        Amount,
        SkillTick,
        CoolTime,
        HealPer,
        SheildPer

    }
    public Dictionary<SkillName, ParameterWithKey> skillsDictionary = new Dictionary<SkillName, ParameterWithKey>();

    [Searchable]
    public List<ParameterWithKey> skillDefaults;

    [System.Serializable]
    public class Constellations
    {
        public bool num0;
        public bool num1;
        public bool num2;
        public bool num3;
        public bool num4;
        public bool num5;
    }

    [System.Serializable]
    public class ParameterWithKey : Item
    {
        public SkillName name;
        public Skill.Type type;
        public SkillName burst;
        public Skill_Parameter parameter;
        public SkillSet skillSet;
        public List<SkillUp> skillUps;
        public bool isDuplicate;
        Constellations _constellations;
        public Constellations constellations
        {
            get
            {
                if (_constellations == null)
                {
                    _constellations = new Constellations();
                }
                return _constellations;
            }
        }

        public Element.Type changeElementType = Element.Type.Physics;
        List<UnityAction> _skillStartListener;
        public List<UnityAction> skillStartListener
        {
            get
            {
                if (_skillStartListener == null)
                {
                    _skillStartListener = new List<UnityAction>();
                }
                return _skillStartListener;
            }
        }

        List<UnityAction> _skillEndListener;
        public List<UnityAction> skillEndListener
        {
            get
            {
                if (_skillEndListener == null)
                {
                    _skillEndListener = new List<UnityAction>();
                }
                return _skillEndListener;
            }
        }

        List<UnityAction<Element.Type>> _elementChangeListener;
        public List<UnityAction<Element.Type>> elementChangeListener
        {
            get
            {
                if (_elementChangeListener == null)
                {
                    _elementChangeListener = new List<UnityAction<Element.Type>>();
                }
                return _elementChangeListener;
            }
        }

        public ParameterWithKey()
        {
        }

        public string GetToolTip()
        {
            if (skillUps.Count == 0) return "";
            if (skillUps.Count < level) return "";
            StringBuilder result = new StringBuilder();
            result.Append("Skill.".AddString(name.ToString()).Localize());
            if (level == 0)
            {
                result.Append("\n");
                result.Append("Skill.Discription.".AddString(name.ToString()).Localize());
            }

            if (level != 0)
            {
                SkillUp skillUp = skillUps[level - 1];
                result.Append("\n");
                result.Append("Upgrade.Skill.".AddString(skillUp.name.ToString()).Localize(skillUp.value));
            }

            if (skillUps.Count - 1 == level && type == Skill.Type.Skill) //원소 폭발 추가
            {
                if (GameManager.instance.ownBursts.Count >= 4) return result.ToString();//이미 원소폭발이 가득차있을 경우

                result.Append("\n");
                result.Append("Upgrade.Skill.Burst".Localize("Skill.".AddString(burst.ToString()).Localize()));
            }
            return result.ToString();
        }
        public void AddStartListener(UnityAction action)
        {
            skillStartListener.Add(action);
        }
        public void AddEndListener(UnityAction action)
        {
            skillEndListener.Add(action);
        }
        public void AddElementChangeListener(UnityAction<Element.Type> action)
        {
            elementChangeListener.Add(action);
        }
    }


    private void OnValidate()
    {
        if (skillDefaults == null)
        {
            skillDefaults = new List<ParameterWithKey>();
        }

        skillsDictionary.Clear();
        foreach (ParameterWithKey defaultParam in skillDefaults)
        {
            ParameterWithKey newParam = new ParameterWithKey();
            newParam.name = defaultParam.name;
            newParam.icon = defaultParam.icon;
            newParam.type = defaultParam.type;
            newParam.skillSet = defaultParam.skillSet;
            newParam.burst = defaultParam.burst;
            newParam.parameter = new Skill_Parameter();
            newParam.parameter.SetData(defaultParam.parameter);
            newParam.skillUps = new List<SkillUp>(defaultParam.skillUps);
            newParam.changeElementType = defaultParam.changeElementType;
            if (defaultParam.type != Skill.Type.Basic && defaultParam.type != Skill.Type.Skill)
            {
                defaultParam.skillUps = new List<SkillUp>();
            }

            skillsDictionary[defaultParam.name] = newParam;
        }

    }

    public void Reset()
    {
        OnValidate();
    }

    [System.Serializable]
    public class SkillUp
    {
        public UpgradeName name;
        public float value;
    }

    public ParameterWithKey Get(SkillName name)
    {
        if (skillsDictionary.TryGetValue(name, out ParameterWithKey param))
        {
            return param;
        }
        return null;
    }
}
