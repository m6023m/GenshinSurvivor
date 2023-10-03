using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Character
{
    public static string Constellation_Sheet = "Constellation.";

    public CharacterData.Name charNum;
    public bool isMine = false;
    public Element.Type elementType = Element.Type.Physics;
    public bool[] constellation = { false, false, false, false, false, false };
    public string toolTipKey = "";
    public int constellaCount = 0;
    public float atk = 5.0f;
    public float armor = 5.0f;
    public float hp = 100;
    public float heal = 0.0f;
    public float cooltime = 0.0f;
    public float area = 1.0f;
    public float aspeed = 1.0f;
    public float duration = 1.0f;
    public float amount = 0.0f;
    public float speed = 3.0f;
    public float magnet = 1.0f;
    public float luck = 5.0f;
    public float regen = 1.0f;
    public float exp = 1.0f;
    public float greed = 1.0f;
    public float curse = 1.0f;
    public float resurration = 0.0f;
    public float reroll = 0.0f;
    public float skip = 0.0f;
    public float physicsDmg = 0.0f;
    public float pyroDmg = 0.0f;
    public float hydroDmg = 0.0f;
    public float anemoDmg = 0.0f;
    public float dendroDmg = 0.0f;
    public float electroDmg = 0.0f;
    public float cyroDmg = 0.0f;
    public float geoDmg = 0.0f;
    public float elementMastery = 0.0f;
    public SkillName ownSkill;
    public StatType rankUpStat;
    public float rankUpValue;
    public WeaponName weaponName;
    public enum StatType
    {
        ATK,
        ARMOR,
        HP,
        HEAL,
        COOLTIME,
        AREA,
        ASPEED,
        DURATION,
        Penetrate,
        AMOUNT,
        SPEED,
        MAGNET,
        LUCK,
        REGEN,
        EXP,
        GREED,
        CURSE,
        RESURRATION,
        REROLL,
        SKIP,
        PHYSICS_DMG,
        PYRO_DMG,
        HYDRO_DMG,
        ANEMO_DMG,
        DENDRO_DMG,
        ELECTRO_DMG,
        CYRO_DMG,
        GEO_DMG,
        ELEMENT_MASTERY
    }
    public Character()
    {
        int a = 0;
    }

    public Character(
        CharacterData.Name charNum,
    bool isMine = false,
    bool[]? constellation = null,
    string toolTipKey = "Default",
    Element.Type elementType = Element.Type.Physics,
    int constellaCount = 0,
    float atk = 5.0f,
    float armor = 5.0f,
    float hp = 100,
    float heal = 0.0f,
    float cooltime = 0.0f,
    float area = 1.0f,
    float aspeed = 1.0f,
    float duration = 1.0f,
    float amount = 0.0f,
    float speed = 3.0f,
    float magnet = 1.0f,
    float luck = 5.0f,
    float regen = 1.0f,
    float exp = 1.0f,
    float greed = 1.0f,
    float curse = 1.0f,
    float resurration = 0.0f,
    float reroll = 0.0f,
    float skip = 0.0f,
    float physicsDmg = 0.0f,
    float pyroDmg = 0.0f,
    float hydroDmg = 0.0f,
    float anemoDmg = 0.0f,
    float dendroDmg = 0.0f,
    float electroDmg = 0.0f,
    float cyroDmg = 0.0f,
    float geoDmg = 0.0f,
    float elementMastery = 0.0f
    )
    {
        this.charNum = charNum;
        this.isMine = isMine;
        this.constellation = constellation;
        this.elementType = elementType;
        this.toolTipKey = toolTipKey;
        this.constellaCount = constellaCount;
        this.atk = atk;
        this.armor = armor;
        this.hp = hp;
        this.heal = heal;
        this.cooltime = cooltime;
        this.area = area;
        this.aspeed = aspeed;
        this.duration = duration;
        this.amount = amount;
        this.speed = speed;
        this.magnet = magnet;
        this.luck = luck;
        this.regen = regen;
        this.exp = exp;
        this.greed = greed;
        this.curse = curse;
        this.resurration = resurration;
        this.reroll = reroll;
        this.skip = skip;
        this.physicsDmg = physicsDmg;
        this.pyroDmg = pyroDmg;
        this.hydroDmg = hydroDmg;
        this.anemoDmg = anemoDmg;
        this.dendroDmg = dendroDmg;
        this.electroDmg = electroDmg;
        this.cyroDmg = cyroDmg;
        this.geoDmg = geoDmg;
        this.elementMastery = elementMastery;
    }

    public Character(Character stat)
    {

        this.charNum = stat.charNum;
        this.isMine = stat.isMine;
        this.constellation = stat.constellation;
        this.elementType = stat.elementType;
        this.toolTipKey = stat.toolTipKey;
        this.constellaCount = stat.constellaCount;
        this.atk = stat.atk;
        this.armor = stat.armor;
        this.hp = stat.hp;
        this.heal = stat.heal;
        this.cooltime = stat.cooltime;
        this.area = stat.area;
        this.aspeed = stat.aspeed;
        this.duration = stat.duration;
        this.amount = stat.amount;
        this.speed = stat.speed;
        this.magnet = stat.magnet;
        this.luck = stat.luck;
        this.regen = stat.regen;
        this.exp = stat.exp;
        this.greed = stat.greed;
        this.curse = stat.curse;
        this.resurration = stat.resurration;
        this.reroll = stat.reroll;
        this.skip = stat.skip;
        this.physicsDmg = stat.physicsDmg;
        this.pyroDmg = stat.pyroDmg;
        this.hydroDmg = stat.hydroDmg;
        this.anemoDmg = stat.anemoDmg;
        this.dendroDmg = stat.dendroDmg;
        this.electroDmg = stat.electroDmg;
        this.cyroDmg = stat.cyroDmg;
        this.geoDmg = stat.geoDmg;
        this.elementMastery = stat.elementMastery;
    }

    public string Name()
    {
        if (toolTipKey == null) return "";
        if ("Travler".Contains(toolTipKey))
        {
            return GameDataManager.instance.saveData.userData.name;
        }
        else
        {
            return "Character.".AddString(toolTipKey).Localize();
        }
    }
}