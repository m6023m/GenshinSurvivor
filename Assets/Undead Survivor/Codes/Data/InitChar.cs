public class InitChar
{
    public static Character Default0()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Travler_MALE, constellation: constellation, toolTipKey: "Travler.Anemo");
        character.rankUpValue = 10;
        character.rankUpStat = Character.StatType.ATK;
        character.elementType = Element.Type.Anemo;
        return character;
    }
    public static Character Default1()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Travler_FEMALE, constellation: constellation, toolTipKey: "Travler.Anemo");
        character.rankUpValue = 10;
        character.rankUpStat = Character.StatType.ATK;
        character.elementType = Element.Type.Anemo;
        return character;
    }
    public static Character Amber()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Amber, constellation: constellation, toolTipKey: "Amber", hp: 90);
        character.rankUpValue = 10;
        character.rankUpStat = Character.StatType.ATK;
        character.elementType = Element.Type.Pyro;
        return character;
    }
    public static Character Xiangling()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Xiangling, constellation: constellation, toolTipKey: "Xiangling");
        character.rankUpValue = 24;
        character.rankUpStat = Character.StatType.ELEMENT_MASTERY;
        character.elementType = Element.Type.Pyro;
        return character;
    }
    public static Character Bennet()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Bennet, constellation: constellation, toolTipKey: "Bennet", atk: 4.5f, armor: 5.5f, hp: 110);
        character.rankUpValue = 10;
        character.rankUpStat = Character.StatType.REGEN;
        character.elementType = Element.Type.Pyro;
        return character;
    }
    public static Character Diluc()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Diluc, constellation: constellation, toolTipKey: "Diluc", atk: 7.5f, armor: 5.5f, hp: 130, area: 1.2f, speed: 3.5f);
        character.rankUpValue = 1;
        character.rankUpStat = Character.StatType.LUCK;
        character.elementType = Element.Type.Pyro;
        return character;
    }
    public static Character Klee()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Klee, constellation: constellation, toolTipKey: "Klee", atk: 7.5f, armor: 4.5f, area: 0.8f, speed: 2.5f);
        character.rankUpValue = 7.2f;
        character.rankUpStat = Character.StatType.PYRO_DMG;
        character.elementType = Element.Type.Pyro;
        return character;
    }
    public static Character Barbara()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Barbara, constellation: constellation, toolTipKey: "Barbara", atk: 4.0f);
        character.rankUpValue = 10;
        character.rankUpStat = Character.StatType.HP;
        character.elementType = Element.Type.Hydro;
        return character;
    }
    public static Character Xingqiu()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Xingqiu, constellation: constellation, toolTipKey: "Xingqiu", armor: 6.0f);
        character.rankUpValue = 10;
        character.rankUpStat = Character.StatType.ATK;
        character.elementType = Element.Type.Hydro;
        return character;
    }
    public static Character Mona()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Mona, constellation: constellation, toolTipKey: "Mona", atk: 6.0f);
        character.rankUpValue = 12;
        character.rankUpStat = Character.StatType.REGEN;
        character.elementType = Element.Type.Hydro;
        return character;
    }
    public static Character Sucrose()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Sucrose, constellation: constellation, toolTipKey: "Sucrose", atk: 4.5f, armor: 5.5f);
        character.rankUpValue = 6;
        character.rankUpStat = Character.StatType.ANEMO_DMG;
        character.elementType = Element.Type.Anemo;
        return character;
    }
    public static Character Jean()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Jean, constellation: constellation, toolTipKey: "Jean", armor: 6.0f, hp: 140, area: 1.1f, speed: 3.5f);
        character.rankUpValue = 6;
        character.rankUpStat = Character.StatType.HEAL;
        character.elementType = Element.Type.Anemo;
        return character;
    }
    public static Character Venti()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Venti, constellation: constellation, toolTipKey: "Venti", atk: 5.5f);
        character.rankUpValue = 12;
        character.rankUpStat = Character.StatType.REGEN;
        character.elementType = Element.Type.Anemo;
        return character;
    }
    public static Character Lisa()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Lisa, constellation: constellation, toolTipKey: "Lisa", armor: 4.5f, area: 1.1f, speed: 3.5f);
        character.rankUpValue = 24;
        character.rankUpStat = Character.StatType.ELEMENT_MASTERY;
        character.elementType = Element.Type.Electro;
        return character;
    }
    public static Character Razor()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Razor, constellation: constellation, toolTipKey: "Razor", armor: 6.0f, hp: 110);
        character.rankUpValue = 6.0f;
        character.rankUpStat = Character.StatType.PHYSICS_DMG;
        character.elementType = Element.Type.Electro;
        return character;
    }
    public static Character Beidou()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Beidou, constellation: constellation, toolTipKey: "Beidou", hp: 130, area: 1.1f, speed: 3.5f);
        character.rankUpValue = 6;
        character.rankUpStat = Character.StatType.ELECTRO_DMG;
        character.elementType = Element.Type.Electro;
        return character;
    }
    public static Character Fischl()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Fischl, constellation: constellation, toolTipKey: "Fischl", armor: 4.5f, hp: 90);
        character.rankUpValue = 10;
        character.rankUpStat = Character.StatType.ATK;
        character.elementType = Element.Type.Electro;
        return character;
    }
    public static Character Keqing()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Keqing, constellation: constellation, toolTipKey: "Keqing", atk: 7.5f, armor: 6.0f, hp: 130);
        character.rankUpValue = 1;
        character.rankUpStat = Character.StatType.LUCK;
        character.elementType = Element.Type.Electro;
        return character;
    }
    public static Character Kaeya()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Kaeya, constellation: constellation, toolTipKey: "Kaeya", armor: 6.5f, hp: 110);
        character.rankUpValue = 10;
        character.rankUpStat = Character.StatType.REGEN;
        character.elementType = Element.Type.Cyro;
        return character;
    }
    public static Character Chongyun()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Chongyun, constellation: constellation, toolTipKey: "Chongyun");
        character.rankUpValue = 10;
        character.rankUpStat = Character.StatType.ATK;
        character.elementType = Element.Type.Cyro;
        return character;
    }
    public static Character Qiqi()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Qiqi, constellation: constellation, toolTipKey: "Qiqi", atk: 6.0f, armor: 8.0f, hp: 120);
        character.rankUpValue = 6;
        character.rankUpStat = Character.StatType.HEAL;
        character.elementType = Element.Type.Cyro;
        return character;
    }
    public static Character Ningguang()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Ningguang, constellation: constellation, toolTipKey: "Ningguang", armor: 4.5f, hp: 90);
        character.rankUpValue = 6;
        character.rankUpStat = Character.StatType.GEO_DMG;
        character.elementType = Element.Type.Geo;
        return character;
    }
    public static Character Noelle()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Noelle, constellation: constellation, toolTipKey: "Noelle", atk: 4.5f, armor: 8.0f, hp: 120);
        character.rankUpValue = 10;
        character.rankUpStat = Character.StatType.ARMOR;
        character.elementType = Element.Type.Geo;
        return character;
    }
    public static Character Tartaglia()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Tartaglia, constellation: constellation, toolTipKey: "Tartaglia", atk: 6.5f, armor: 8.0f, hp: 130, speed: 3.5f);
        character.rankUpValue = 7.2f;
        character.rankUpStat = Character.StatType.HYDRO_DMG;
        character.elementType = Element.Type.Hydro;
        return character;
    }
    public static Character Diona()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Diona, constellation: constellation, toolTipKey: "Diona", atk: 5.0f, armor: 6.0f, hp: 95, speed: 2.5f);
        character.rankUpValue = 6;
        character.rankUpStat = Character.StatType.CYRO_DMG;
        character.elementType = Element.Type.Cyro;
        return character;
    }
    public static Character Zhongli()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Zhongli, constellation: constellation, toolTipKey: "Zhongli", atk: 5.5f, armor: 7.0f, hp: 150, speed: 3.5f);
        character.rankUpValue = 10;
        character.rankUpStat = Character.StatType.HP;
        character.elementType = Element.Type.Geo;
        return character;
    }
    public static Character Xinyan()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Xinyan, constellation: constellation, toolTipKey: "Xinyan", atk: 5.5f, armor: 8.0f, hp: 110);
        character.rankUpValue = 10;
        character.rankUpStat = Character.StatType.ARMOR;
        character.elementType = Element.Type.Pyro;
        return character;
    }
    public static Character Albedo()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Albedo, constellation: constellation, toolTipKey: "Albedo", atk: 5.5f, armor: 8.5f, hp: 130);
        character.rankUpValue = 7.2f;
        character.rankUpStat = Character.StatType.GEO_DMG;
        character.elementType = Element.Type.Geo;
        return character;
    }
    public static Character Ganyu()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Ganyu, constellation: constellation, toolTipKey: "Ganyu", atk: 7.5f, armor: 6.0f, hp: 95);
        character.rankUpValue = 1;
        character.rankUpStat = Character.StatType.LUCK;
        character.elementType = Element.Type.Cyro;
        return character;
    }
    public static Character Xiao()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Xiao, constellation: constellation, toolTipKey: "Xiao", atk: 7.5f, armor: 8.0f, hp: 125);
        character.rankUpValue = 1;
        character.rankUpStat = Character.StatType.LUCK;
        character.elementType = Element.Type.Anemo;
        return character;
    }
    public static Character Hutao()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Hutao, constellation: constellation, toolTipKey: "Hutao", atk: 2.0f, armor: 8.5f, hp: 155);
        character.rankUpValue = 1;
        character.rankUpStat = Character.StatType.LUCK;
        character.elementType = Element.Type.Pyro;
        return character;
    }
    public static Character Rosaria()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Rosaria, constellation: constellation, toolTipKey: "Rosaria", atk: 5.0f, armor: 7.0f, hp: 120);
        character.rankUpValue = 10;
        character.rankUpStat = Character.StatType.ATK;
        character.elementType = Element.Type.Cyro;
        return character;
    }
    public static Character Yanfei()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Yanfei, constellation: constellation, toolTipKey: "Yanfei", atk: 5.0f, armor: 5.5f, hp: 90);
        character.rankUpValue = 6.0f;
        character.rankUpStat = Character.StatType.PYRO_DMG;
        character.elementType = Element.Type.Pyro;
        return character;
    }
    public static Character Eula()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Eula, constellation: constellation, toolTipKey: "Eula", atk: 7.5f, armor: 7.5f, hp: 130);
        character.rankUpValue = 1;
        character.rankUpStat = Character.StatType.LUCK;
        character.elementType = Element.Type.Cyro;
        return character;
    }
    public static Character Kazuha()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Kazuha, constellation: constellation, toolTipKey: "Kazuha", atk: 6.5f, armor: 8.0f, hp: 130);
        character.rankUpValue = 29;
        character.rankUpStat = Character.StatType.ELEMENT_MASTERY;
        character.elementType = Element.Type.Anemo;
        return character;
    }
}