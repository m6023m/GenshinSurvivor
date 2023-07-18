using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "ArtifactData", menuName = "GenshinSurvivor/ArtifactData", order = 0)]
public class ArtifactData : ScriptableObject
{
    public List<ParameterWithKey> artifacts;
    public List<ParameterWithKey> artifactDefaults;
    int SET_COUNT = 4;

    [System.Serializable]
    public class ParameterWithKey : Item
    {
        public ArtifactName name;
        public string GetToolTip()
        {
            StringBuilder result = new StringBuilder();
            result.Append("Artifact.".AddString(name.ToString()).Localize());
            result.Append("\n");
            result.Append("Artifact.Up.".AddString(name.ToString()).Localize());

            if (level == 3)
            {
                result.Append("\n");
                result.Append("Artifact.Up.Set.".AddString(name.ToString()).Localize());
            }
            return result.ToString();
        }
    }

    private void OnValidate()
    {
        ResetArtifacts();
    }

    public void ResetArtifacts()
    {
        if (artifactDefaults == null)
        {
            artifactDefaults = new List<ParameterWithKey>();
        }

        artifacts = new List<ParameterWithKey>();
        foreach (ParameterWithKey defaultParam in artifactDefaults)
        {
            ParameterWithKey newParam = new ParameterWithKey();
            newParam.name = defaultParam.name;
            newParam.icon = defaultParam.icon;
            artifacts.Add(newParam);
        }
    }

    public void Reset()
    {
        OnValidate();
    }

    public ParameterWithKey Get(ArtifactName name)
    {
        foreach (ParameterWithKey param in artifacts)
        {
            if (param.name.Equals(name))
            {
                return param;
            }
        }
        return new ParameterWithKey();
    }


    public float FinalDamage
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Resolution_of_Sojourner).level;
            return result;
        }
    }

    public float ElementalMastery
    {
        get
        {
            float result = 0f;
            result = result + (Get(ArtifactName.Instructor).level * 20);
            result = result + Instructor();
            result = result + (Get(ArtifactName.Wanderers_Troupe).level * 20);
            return result;
        }
    }


    public float Armor
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Defenders_Will).level;
            result = result + Defenders_Will();
            return result;
        }
    }
    public float Atk
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Brave_Heart).level;
            result = result + Noblesse_Oblige();
            return result;
        }
    }
    public float BaseDamageMultiplier(SkillName skillName)//일반공격 피해
    {
        float result = 0f;
        result = result + Get(ArtifactName.Martial_Artist).level * 10 / 100.0f;
        result = result + Martial_Artist();
        result = result + Gladiators_Finale(skillName);
        result = result + Wanderers_Troupe(skillName);
        result = result + Heart_of_Depth(skillName);
        result = result + Retracing_Bolide();
        return result;
    }
    public float SkillDamageMultiplier
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Gambler).level * 10 / 100.0f;
            return result;
        }
    }
    public float RegenMultiplier
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Scholar).level * 10 / 100.0f;
            result = result + Get(ArtifactName.Emblem_of_Severed_Fate).level * 10 / 100.0f;
            return result;
        }
    }
    public float MoveSpeedMultiplier
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Berserker).level * 10 / 100.0f;
            return result;
        }
    }
    public float Luck
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Tiny_Miracle).level;
            result = result + Berserker();
            result = result + Blizzard_Strayer();
            return result;
        }
    }
    public float AtkMultiplier
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Gladiators_Finale).level * 10 / 100.0f;
            result = result + Tenacity_of_the_Millelith();
            return result;
        }
    }
    public float PhysicsDamageMultiplier
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Bloodstained_Chivalry).level * 10 / 100.0f;
            result = result + Get(ArtifactName.Pale_Flame).level * 10 / 100.0f;
            result = result + Pale_Flame();
            return result;
        }
    }
    public float BurstDamageMultiplier
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Noblesse_Oblige).level * 10 / 100.0f;
            return result;
        }
    }
    public float DurationMultiplier
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Blizzard_Strayer).level * 6 / 100.0f;
            return result;
        }
    }
    public float CoolTimeMultiplier(Skill.Type skillType)
    {
        float result = 0f;
        result = result + Get(ArtifactName.Heart_of_Depth).level * 6 / 100.0f;
        result = result + Bloodstained_Chivalry(skillType);
        return result;
    }
    public float HealthMultiplier
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Tenacity_of_the_Millelith).level * 10 / 100.0f;
            return result;
        }
    }
    public float HealBonusMultiplier
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Maiden_Beloved).level * 10 / 100.0f;
            result = result + Get(ArtifactName.Ocean_Hued_Clam).level * 10 / 100.0f;
            result = result + Maiden_Beloved();
            return result;
        }
    }
    public float MagnetMultiplier
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Viridescent_Venerer).level * 10 / 100.0f;
            result = result + Viridescent_Venerer(SkillName.Swirl);
            return result;
        }
    }
    public float ShieldMultiplier
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Retracing_Bolide).level * 10 / 100.0f;
            result = result + Tenacity_of_the_Millelith();
            result = result + Archaic_Petra();
            return result;
        }
    }
    public float DamageMultiplier
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Crimson_Witch_of_Flames).level * 10 / 100.0f;
            result = result + Husk_of_Opulent_Dreams();
            return result;
        }
    }
    public float AttackSpeedMultiplier
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Thundering_Fury).level * 10 / 100.0f;
            return result;
        }
    }
    public float AreaMultiplier
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Archaic_Petra).level * 10 / 100.0f;
            return result;
        }
    }
    public float BaseAttackCooltimeMultiplier
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Reminiscence_of_Shime).level * 10 / 100.0f;
            return result;
        }
    }
    public float ArmorMultiplier
    {
        get
        {
            float result = 0f;
            result = result + Get(ArtifactName.Husk_of_Opulent_Dreams).level * 10 / 100.0f;
            return result;
        }
    }


    public float BaseAttackCriticalRate
    {
        get
        {
            float result = 0f;
            result = result + Resolution_of_Sojourner();
            return result;
        }
    }

    public int Rebirth
    {
        get
        {
            int result = 0;
            result = result + Tiny_Miracle();
            return result;
        }
    }

    public float Instructor()
    {
        if (Get(ArtifactName.Instructor).level < SET_COUNT) return 0;
        return 120;
    }
    public void The_Exile(SkillName skillName)//원소 폭발 발동 시 다른 원소폭발 게이지 +5
    {
        if (Get(ArtifactName.The_Exile).level < SET_COUNT) return;
        foreach (SkillData.ParameterWithKey param in GameManager.instance.ownBursts)
        {
            if (param.name != skillName)
            {
                param.parameter.elementGauge += 5;
            }
        }
    }

    public float Brave_Heart(float maxHP, float HP)//HP가 50%를 초과하는 적에게 가하는 피래 +30%
    {
        if (Get(ArtifactName.The_Exile).level < SET_COUNT) return 0;
        if (maxHP / HP * 100 > 50)
        {
            return 0.3f;
        }
        return 0;
    }

    public bool Gambler()//스킬 사용 1초 후에 20% 확률로 재사용 대기시간 초기화
    {
        if (Get(ArtifactName.Gambler).level < SET_COUNT) return false;
        if (Random.Range(0, 5) == 1) return true;
        return false;
    }

    public float Scholar()
    {
        if (Get(ArtifactName.Scholar).level < SET_COUNT) return 0;
        return 1;
    }

    float Gladiators_Finale(SkillName skillName) //근접 일반 공격 피해 40%
    {
        if (Get(ArtifactName.Gladiators_Finale).level < SET_COUNT) return 0;
        switch (skillName)
        {
            case SkillName.Basic_Claymore:
            case SkillName.Basic_Spear:
            case SkillName.Basic_Sword:
                return 0.4f;
        }
        return 0;
    }

    float Tenacity_of_the_Millelith()
    {
        if (Get(ArtifactName.Tenacity_of_the_Millelith).level < SET_COUNT) return 0;
        return 0.2f;
    }

    float Archaic_Petra()
    {
        if (Get(ArtifactName.Archaic_Petra).level < SET_COUNT) return 0;
        return 0.4f;
    }

    float Wanderers_Troupe(SkillName skillName) //원거리 일반 공격 피해 40%
    {
        if (Get(ArtifactName.Wanderers_Troupe).level < SET_COUNT) return 0;
        switch (skillName)
        {
            case SkillName.Basic_Catalist:
            case SkillName.Basic_Arrow:
                return 0.4f;
        }
        return 0;
    }

    float Bloodstained_Chivalry(Skill.Type skillType) //일반공격 재사용 대기시간 -20%
    {
        if (Get(ArtifactName.Bloodstained_Chivalry).level < SET_COUNT) return 0;
        if (skillType == Skill.Type.Basic) return 0.2f;
        return 0;
    }

    float Pale_Flame()
    {
        if (Get(ArtifactName.Pale_Flame).level < SET_COUNT) return 0;
        return 0.6f;
    }
    float Noblesse_Oblige()//공격력 +5
    {
        if (Get(ArtifactName.Noblesse_Oblige).level < SET_COUNT) return 0;
        return 5;
    }

    float Blizzard_Strayer()//행운 +10
    {
        if (Get(ArtifactName.Blizzard_Strayer).level < SET_COUNT) return 0;
        return 5;
    }

    float Heart_of_Depth(SkillName skillName)
    {
        if (Get(ArtifactName.Heart_of_Depth).level < SET_COUNT) return 0;
        switch (skillName)
        {
            case SkillName.Basic_Claymore:
            case SkillName.Basic_Spear:
            case SkillName.Basic_Sword:
            case SkillName.Basic_Catalist:
            case SkillName.Basic_Arrow:
                return 0.4f;
        }
        return 0;
    }

    float Maiden_Beloved()
    {
        if (Get(ArtifactName.Maiden_Beloved).level < SET_COUNT) return 0;
        return 0.3f;
    }

    public float Viridescent_Venerer(SkillName skillName)
    {
        if (Get(ArtifactName.Viridescent_Venerer).level < SET_COUNT) return 0;

        switch (skillName)
        {
            case SkillName.Swirl:
                return 0.4f;
        }
        return 0;
    }
    float Retracing_Bolide()
    {
        if (Get(ArtifactName.Retracing_Bolide).level < SET_COUNT) return 0;
        if (GameManager.instance.player.sheilds.Count > 0) return 0.4f;
        return 0;
    }
    public float Crimson_Witch_of_Flames(SkillName skillName)
    {
        if (Get(ArtifactName.Crimson_Witch_of_Flames).level < SET_COUNT) return 0;

        switch (skillName)
        {
            case SkillName.Vaporize:
            case SkillName.Burgeon:
            case SkillName.Melt:
            case SkillName.Overloaded:
                return 0.4f;
        }
        return 0;
    }
    public float Thundering_Fury(SkillName skillName)
    {
        if (Get(ArtifactName.Thundering_Fury).level < SET_COUNT) return 0;

        switch (skillName)
        {
            case SkillName.Superconduct:
            case SkillName.ElectroCharged:
            case SkillName.Quicken:
            case SkillName.Overloaded:
                if (!GameManager.instance.statBuff.isThundering_FuryCooltime)
                {
                    foreach (SkillObject skiilObject in GameManager.instance.player.GetComponentsInChildren<SkillObject>())
                    {
                        skiilObject.skillTime += 1;
                    }
                    GameManager.instance.statBuff.isThundering_FuryCooltime = true;
                    GameManager.instance.StartCoroutine(Thundering_FuryCooltime());
                }
                return 0.4f;
        }
        return 0;
    }

    IEnumerator Thundering_FuryCooltime() //번개같은 분노 쿨타임
    {
        yield return new WaitForSecondsRealtime(4.0f);
        GameManager.instance.statBuff.isThundering_FuryCooltime = false;
    }
    float Husk_of_Opulent_Dreams() //보호막 개수 x 20% 피해 증가
    {
        if (Get(ArtifactName.Husk_of_Opulent_Dreams).level < SET_COUNT) return 0;
        int sheildCount = 0;
        // foreach (SkillData.ParameterWithKey param in GameManager.instance.ownSkills)
        // {
        //     if (param.objectType != Skill.ObjectType.Sheild)
        //     {
        //         sheildCount++;
        //     }
        // }
        return sheildCount * 20 / 100.0f;
    }
    float Defenders_Will()
    {
        if (Get(ArtifactName.Defenders_Will).level < SET_COUNT) return 0;

        List<Element.Type> elementTypes = new List<Element.Type>();
        foreach (Character character in GameDataManager.instance.saveData.userData.selectChars)
        {
            if (character == null) continue;
            if (!elementTypes.Contains(character.elementType))
            {
                elementTypes.Add(character.elementType);
            }
        }
        return (elementTypes.Count * 2);
    }
    float Resolution_of_Sojourner()
    {
        if (Get(ArtifactName.Resolution_of_Sojourner).level < SET_COUNT) return 0;
        return 0.3f;
    }
    float Martial_Artist()//일반 공격 피해 +30%
    {
        if (Get(ArtifactName.Martial_Artist).level < SET_COUNT) return 0;
        return 0.3f;
    }
    int Tiny_Miracle()
    {
        if (Get(ArtifactName.Tiny_Miracle).level < SET_COUNT) return 0;
        return 1;
    }

    int Berserker()
    {
        if (Get(ArtifactName.Berserker).level < SET_COUNT) return 0;
        int result = 0;
        if (GameManager.instance.player.maxHealth /
        GameManager.instance.player.health * 100 < 70)
        {
            result = result + 10;
        }
        return result;
    }


    //TODO: 미등록 스킬
    // Artifact.Up.Reminiscence_of_Shime
    // Artifact.Up.Ocean_Hued_Clam
    // Artifact.Up.Husk_of_Opulent_Dreams
    // Artifact.Up.Vermillion_Hereafter
    // Artifact.Up.Echoes_of_an_Offering
    // Artifact.Up.Memory_of_the_Forest
    // Artifact.Up.Gilded_Dream
    // Artifact.Up.Desert_Pavilion_Chronicle
    // Artifact.Up.Lost_Flower_of_Paradise
    // Artifact.Up.Nymphs_Dream
    // Artifact.Up.Vourukashas_Glow
}
