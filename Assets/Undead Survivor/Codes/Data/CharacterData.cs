using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CharacterData", menuName = "GenshinSurvivor/CharacterData", order = 0)]
public class CharacterData : ScriptableObject
{
    public enum Name
    {
        Chracter_None = -1,
        Travler_MALE,
        Travler_FEMALE,
        Amber,
        Xiangling,
        Bennet,
        Diluc,
        Klee,
        Barbara,
        Xingqiu,
        Mona,
        Sucrose,
        Jean,
        Venti,
        Lisa,
        Razor,
        Beidou,
        Fischl,
        Keqing,
        Kaeya,
        Chongyun,
        Qiqi,
        Ningguang,
        Noelle,
        Tartaglia,
        Diona,
        Zhongli,
        Xinyan,
        Albedo,
        Ganyu,
        Xiao,
        Hutao,
        Rosaria,
        Yanfei,
        Eula,
        Kazuha,
        Ayaka,
        Yoimiya,
        Sayu,
        Raiden,
        Sara,
        Kokomi,
        Ito,
        Goro,
        Thoma,
        Shenhe,
        Yunjin,
        Miko,
        Ayato,
        Yelan,
        Heizo,
        Shinobu
    }

    public enum Type
    {
        None = -1,
        Normal,
        Rare,
        Player
    }

    public enum NationalType
    {
        None,
        Mondstadt,
        Liyue,
        Inazuma,
        Sumeru,
        Fontaine,
        Natlan,
        Snezhnaya,
        Khaenriah
    }

    public enum PickUpType
    {
        Normal,
        PickUp,
    }
    public List<ParameterWithKey> characters;
    [System.Serializable]
    public class ParameterWithKey
    {
        public Name name;
        public Type type;
        public WeaponType weaponType;
        public PickUpType pickUpType;
        public Element.Type elementType;
        public NationalType nationalType;
        public Sprite icon;
        public RuntimeAnimatorController anim;
        public Sprite wishImage;
        public Sprite wishPannel;
        public Sprite wishIcon;
        public Character stat;
        public SkillName skillBasic;
        public SkillName skill;
    }


    public void Init()
    {
        if (GameDataManager.instance != null)
        {
            for (int i = 0; i < GameDataManager.instance.saveData.charactors.Count; i++)
            {
                Character character = GameDataManager.instance.saveData.charactors[i];
                characters[i].stat = character;
            }
        }
    }

    public RuntimeAnimatorController GetAnim(Name name)
    {
        foreach (ParameterWithKey anim in characters)
        {
            if (anim.name.Equals(name))
            {
                return anim.anim;
            }
        }
        return null;
    }
    public ParameterWithKey Get(Name name)
    {
        foreach (ParameterWithKey param in characters)
        {
            if (param.name.Equals(name))
            {
                return param;
            }
        }
        return null;
    }
}
