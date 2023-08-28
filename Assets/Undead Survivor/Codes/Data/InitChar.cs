public class InitChar
{
    public static Character Default0()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Travler_MALE, constellation: constellation, toolTipKey: "Travler.Anemo")
        {
            rankUpValue = 10,
            rankUpStat = Character.StatType.ATK,
            elementType = Element.Type.Anemo
        };
        return character;
    }
    public static Character Default1()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Travler_FEMALE, constellation: constellation, toolTipKey: "Travler.Anemo")
        {
            rankUpValue = 10,
            rankUpStat = Character.StatType.ATK,
            elementType = Element.Type.Anemo
        };
        return character;
    }
    public static Character Amber()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Amber, constellation: constellation, toolTipKey: "Amber", hp: 90)
        {
            rankUpValue = 10,
            rankUpStat = Character.StatType.ATK,
            elementType = Element.Type.Pyro
        };
        return character;
    }
    public static Character Xiangling()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Xiangling, constellation: constellation, toolTipKey: "Xiangling")
        {
            rankUpValue = 24,
            rankUpStat = Character.StatType.ELEMENT_MASTERY,
            elementType = Element.Type.Pyro
        };
        return character;
    }
    public static Character Bennet()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Bennet, constellation: constellation, toolTipKey: "Bennet", atk: 4.5f, armor: 5.5f, hp: 110)
        {
            rankUpValue = 10,
            rankUpStat = Character.StatType.REGEN,
            elementType = Element.Type.Pyro
        };
        return character;
    }
    public static Character Diluc()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Diluc, constellation: constellation, toolTipKey: "Diluc", atk: 7.5f, armor: 5.5f, hp: 130, area: 1.2f, speed: 3.5f)
        {
            rankUpValue = 1,
            rankUpStat = Character.StatType.LUCK,
            elementType = Element.Type.Pyro
        };
        return character;
    }
    public static Character Klee()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Klee, constellation: constellation, toolTipKey: "Klee", atk: 7.5f, armor: 4.5f, area: 0.8f, speed: 2.5f)
        {
            rankUpValue = 7.2f,
            rankUpStat = Character.StatType.PYRO_DMG,
            elementType = Element.Type.Pyro
        };
        return character;
    }
    public static Character Barbara()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Barbara, constellation: constellation, toolTipKey: "Barbara", atk: 4.0f)
        {
            rankUpValue = 10,
            rankUpStat = Character.StatType.HP,
            elementType = Element.Type.Hydro
        };
        return character;
    }
    public static Character Xingqiu()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Xingqiu, constellation: constellation, toolTipKey: "Xingqiu", armor: 6.0f)
        {
            rankUpValue = 10,
            rankUpStat = Character.StatType.ATK,
            elementType = Element.Type.Hydro
        };
        return character;
    }
    public static Character Mona()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Mona, constellation: constellation, toolTipKey: "Mona", atk: 6.0f)
        {
            rankUpValue = 12,
            rankUpStat = Character.StatType.REGEN,
            elementType = Element.Type.Hydro
        };
        return character;
    }
    public static Character Sucrose()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Sucrose, constellation: constellation, toolTipKey: "Sucrose", atk: 4.5f, armor: 5.5f)
        {
            rankUpValue = 6,
            rankUpStat = Character.StatType.ANEMO_DMG,
            elementType = Element.Type.Anemo
        };
        return character;
    }
    public static Character Jean()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Jean, constellation: constellation, toolTipKey: "Jean", armor: 6.0f, hp: 140, area: 1.1f, speed: 3.5f)
        {
            rankUpValue = 6,
            rankUpStat = Character.StatType.HEAL,
            elementType = Element.Type.Anemo
        };
        return character;
    }
    public static Character Venti()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Venti, constellation: constellation, toolTipKey: "Venti", atk: 5.5f)
        {
            rankUpValue = 12,
            rankUpStat = Character.StatType.REGEN,
            elementType = Element.Type.Anemo
        };
        return character;
    }
    public static Character Lisa()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Lisa, constellation: constellation, toolTipKey: "Lisa", armor: 4.5f, area: 1.1f, speed: 3.5f)
        {
            rankUpValue = 24,
            rankUpStat = Character.StatType.ELEMENT_MASTERY,
            elementType = Element.Type.Electro
        };
        return character;
    }
    public static Character Razor()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Razor, constellation: constellation, toolTipKey: "Razor", armor: 6.0f, hp: 110)
        {
            rankUpValue = 6.0f,
            rankUpStat = Character.StatType.PHYSICS_DMG,
            elementType = Element.Type.Electro
        };
        return character;
    }
    public static Character Beidou()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Beidou, constellation: constellation, toolTipKey: "Beidou", hp: 130, area: 1.1f, speed: 3.5f)
        {
            rankUpValue = 6,
            rankUpStat = Character.StatType.ELECTRO_DMG,
            elementType = Element.Type.Electro
        };
        return character;
    }
    public static Character Fischl()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Fischl, constellation: constellation, toolTipKey: "Fischl", armor: 4.5f, hp: 90)
        {
            rankUpValue = 10,
            rankUpStat = Character.StatType.ATK,
            elementType = Element.Type.Electro
        };
        return character;
    }
    public static Character Keqing()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Keqing, constellation: constellation, toolTipKey: "Keqing", atk: 7.5f, armor: 6.0f, hp: 130)
        {
            rankUpValue = 1,
            rankUpStat = Character.StatType.LUCK,
            elementType = Element.Type.Electro
        };
        return character;
    }
    public static Character Kaeya()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Kaeya, constellation: constellation, toolTipKey: "Kaeya", armor: 6.5f, hp: 110)
        {
            rankUpValue = 10,
            rankUpStat = Character.StatType.REGEN,
            elementType = Element.Type.Cyro
        };
        return character;
    }
    public static Character Chongyun()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Chongyun, constellation: constellation, toolTipKey: "Chongyun")
        {
            rankUpValue = 10,
            rankUpStat = Character.StatType.ATK,
            elementType = Element.Type.Cyro
        };
        return character;
    }
    public static Character Qiqi()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Qiqi, constellation: constellation, toolTipKey: "Qiqi", atk: 6.0f, armor: 8.0f, hp: 120)
        {
            rankUpValue = 6,
            rankUpStat = Character.StatType.HEAL,
            elementType = Element.Type.Cyro
        };
        return character;
    }
    public static Character Ningguang()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Ningguang, constellation: constellation, toolTipKey: "Ningguang", armor: 4.5f, hp: 90)
        {
            rankUpValue = 6,
            rankUpStat = Character.StatType.GEO_DMG,
            elementType = Element.Type.Geo
        };
        return character;
    }
    public static Character Noelle()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Noelle, constellation: constellation, toolTipKey: "Noelle", atk: 4.5f, armor: 8.0f, hp: 120)
        {
            rankUpValue = 10,
            rankUpStat = Character.StatType.ARMOR,
            elementType = Element.Type.Geo
        };
        return character;
    }
    public static Character Tartaglia()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Tartaglia, constellation: constellation, toolTipKey: "Tartaglia", atk: 6.5f, armor: 8.0f, hp: 130, speed: 3.5f)
        {
            rankUpValue = 7.2f,
            rankUpStat = Character.StatType.HYDRO_DMG,
            elementType = Element.Type.Hydro
        };
        return character;
    }
    public static Character Diona()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Diona, constellation: constellation, toolTipKey: "Diona", atk: 5.0f, armor: 6.0f, hp: 95, speed: 2.5f)
        {
            rankUpValue = 6,
            rankUpStat = Character.StatType.CYRO_DMG,
            elementType = Element.Type.Cyro
        };
        return character;
    }
    public static Character Zhongli()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Zhongli, constellation: constellation, toolTipKey: "Zhongli", atk: 5.5f, armor: 7.0f, hp: 150, speed: 3.5f)
        {
            rankUpValue = 10,
            rankUpStat = Character.StatType.HP,
            elementType = Element.Type.Geo
        };
        return character;
    }
    public static Character Xinyan()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Xinyan, constellation: constellation, toolTipKey: "Xinyan", atk: 5.5f, armor: 8.0f, hp: 110)
        {
            rankUpValue = 10,
            rankUpStat = Character.StatType.ARMOR,
            elementType = Element.Type.Pyro
        };
        return character;
    }
    public static Character Albedo()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Albedo, constellation: constellation, toolTipKey: "Albedo", atk: 5.5f, armor: 8.5f, hp: 130)
        {
            rankUpValue = 7.2f,
            rankUpStat = Character.StatType.GEO_DMG,
            elementType = Element.Type.Geo
        };
        return character;
    }
    public static Character Ganyu()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Ganyu, constellation: constellation, toolTipKey: "Ganyu", atk: 7.5f, armor: 6.0f, hp: 95)
        {
            rankUpValue = 1,
            rankUpStat = Character.StatType.LUCK,
            elementType = Element.Type.Cyro
        };
        return character;
    }
    public static Character Xiao()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Xiao, constellation: constellation, toolTipKey: "Xiao", atk: 7.5f, armor: 8.0f, hp: 125)
        {
            rankUpValue = 1,
            rankUpStat = Character.StatType.LUCK,
            elementType = Element.Type.Anemo
        };
        return character;
    }
    public static Character Hutao()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Hutao, constellation: constellation, toolTipKey: "Hutao", atk: 2.0f, armor: 8.5f, hp: 155)
        {
            rankUpValue = 1,
            rankUpStat = Character.StatType.LUCK,
            elementType = Element.Type.Pyro
        };
        return character;
    }
    public static Character Rosaria()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Rosaria, constellation: constellation, toolTipKey: "Rosaria", atk: 5.0f, armor: 7.0f, hp: 120)
        {
            rankUpValue = 10,
            rankUpStat = Character.StatType.ATK,
            elementType = Element.Type.Cyro
        };
        return character;
    }
    public static Character Yanfei()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Yanfei, constellation: constellation, toolTipKey: "Yanfei", atk: 5.0f, armor: 5.5f, hp: 90)
        {
            rankUpValue = 6.0f,
            rankUpStat = Character.StatType.PYRO_DMG,
            elementType = Element.Type.Pyro
        };
        return character;
    }
    public static Character Eula()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Eula, constellation: constellation, toolTipKey: "Eula", atk: 7.5f, armor: 7.5f, hp: 130)
        {
            rankUpValue = 1,
            rankUpStat = Character.StatType.LUCK,
            elementType = Element.Type.Cyro
        };
        return character;
    }
    public static Character Kazuha()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Kazuha, constellation: constellation, toolTipKey: "Kazuha", atk: 6.5f, armor: 8.0f, hp: 130)
        {
            rankUpValue = 29,
            rankUpStat = Character.StatType.ELEMENT_MASTERY,
            elementType = Element.Type.Anemo
        };
        return character;
    }
    public static Character Ayaka()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Ayaka, constellation: constellation, toolTipKey: "Ayaka", atk: 7.5f, armor: 8.0f, hp: 130)
        {
            rankUpValue = 1,
            rankUpStat = Character.StatType.LUCK,
            elementType = Element.Type.Cyro
        };
        return character;
    }
    public static Character Yoimiya()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Yoimiya, constellation: constellation, toolTipKey: "Yoimiya", atk: 7f, armor: 6f, hp: 100)
        {
            rankUpValue = 1,
            rankUpStat = Character.StatType.LUCK,
            elementType = Element.Type.Pyro
        };
        return character;
    }
    public static Character Sayu()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Sayu, constellation: constellation, toolTipKey: "Sayu", atk: 5f, armor: 7.5f, hp: 120)
        {
            rankUpValue = 24,
            rankUpStat = Character.StatType.ELEMENT_MASTERY,
            elementType = Element.Type.Anemo
        };
        return character;
    }
    public static Character Raiden()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Raiden, constellation: constellation, toolTipKey: "Raiden", atk: 7.5f, armor: 8.0f, hp: 130)
        {
            regen = 8,
            rankUpStat = Character.StatType.REGEN,
            elementType = Element.Type.Electro
        };
        return character;
    }
    public static Character Sara()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Sara, constellation: constellation, toolTipKey: "Sara", atk: 6.5f, armor: 8.0f, hp: 130)
        {
            rankUpValue = 29,
            rankUpStat = Character.StatType.ELEMENT_MASTERY,
            elementType = Element.Type.Anemo
        };
        return character;
    }
    public static Character Kokomi()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Kokomi, constellation: constellation, toolTipKey: "Kokomi", atk: 5f, armor: 6.5f, hp: 135)
        {
            rankUpValue = 7.2f,
            rankUpStat = Character.StatType.HYDRO_DMG,
            elementType = Element.Type.Hydro
        };
        return character;
    }
    public static Character Ito()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Ito, constellation: constellation, toolTipKey: "Ito", atk: 4.5f, armor: 9.5f, hp: 130)
        {
            rankUpValue = 1,
            rankUpStat = Character.StatType.LUCK,
            elementType = Element.Type.Geo
        };
        return character;
    }
    public static Character Goro()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Goro, constellation: constellation, toolTipKey: "Goro", atk: 4.0f, armor: 6.5f, hp: 95)
        {
            rankUpValue = 6,
            rankUpStat = Character.StatType.GEO_DMG,
            elementType = Element.Type.Geo
        };
        return character;
    }
    public static Character Thoma()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Thoma, constellation: constellation, toolTipKey: "Thoma", atk: 4.5f, armor: 7.5f, hp: 100)
        {
            rankUpValue = 6,
            rankUpStat = Character.StatType.ATK,
            elementType = Element.Type.Pyro
        };
        return character;
    }
    public static Character Shenhe()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Shenhe, constellation: constellation, toolTipKey: "Shenhe", atk: 4.5f, armor: 7.5f, hp: 100)
        {
            rankUpValue = 6,
            rankUpStat = Character.StatType.ATK,
            elementType = Element.Type.Cyro
        };
        return character;
    }
    public static Character Yunjin()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Yunjin, constellation: constellation, toolTipKey: "Yunjin", atk: 4.0f, armor: 7.5f, hp: 105)
        {
            rankUpValue = 6.7f,
            rankUpStat = Character.StatType.REGEN,
            elementType = Element.Type.Geo
        };
        return character;
    }
    public static Character Miko()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Miko, constellation: constellation, toolTipKey: "Miko", atk: 7.5f, armor: 5.5f, hp: 100)
        {
            rankUpValue = 1,
            rankUpStat = Character.StatType.LUCK,
            elementType = Element.Type.Electro
        };
        return character;
    }
    public static Character Ayato()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Ayato, constellation: constellation, toolTipKey: "Ayato", atk: 6.5f, armor: 7.5f, hp: 135)
        {
            rankUpValue = 1,
            rankUpStat = Character.StatType.LUCK,
            elementType = Element.Type.Hydro
        };
        return character;
    }
    public static Character Yelan()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Yelan, constellation: constellation, toolTipKey: "Yelan", atk: 5.0f, armor: 5.5f, hp: 145)
        {
            rankUpValue = 1,
            rankUpStat = Character.StatType.LUCK,
            elementType = Element.Type.Hydro
        };
        return character;
    }
    public static Character Heizo()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Heizo, constellation: constellation, toolTipKey: "Heizo", atk: 5.0f, armor: 6.5f, hp: 105)
        {
            rankUpValue = 6,
            rankUpStat = Character.StatType.ANEMO_DMG,
            elementType = Element.Type.Anemo
        };
        return character;
    }
    public static Character Shinobu()
    {
        bool[] constellation = { false, false, false, false, false, false };
        Character character = new Character(CharacterData.Name.Shinobu, constellation: constellation, toolTipKey: "Shinobu", atk: 5.0f, armor: 7.5f, hp: 120)
        {
            rankUpValue = 6,
            rankUpStat = Character.StatType.HP,
            elementType = Element.Type.Electro
        };
        return character;
    }
}