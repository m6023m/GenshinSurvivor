using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "ArtifactData", menuName = "GenshinSurvivor/ArtifactData", order = 0)]
public class ArtifactData : ScriptableObject
{
    public Dictionary<ArtifactName, ParameterWithKey> artifacts = new Dictionary<ArtifactName, ParameterWithKey>();
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

        artifacts.Clear();
        foreach (ParameterWithKey defaultParam in artifactDefaults)
        {
            ParameterWithKey newParam = new ParameterWithKey();
            newParam.name = defaultParam.name;
            newParam.icon = defaultParam.icon;
            artifacts[defaultParam.name] = newParam;
        }
    }

    public void Reset()
    {
        OnValidate();
    }


    public float FinalDamage
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Resolution_of_Sojourner].level;
            return result;
        }
    }

    public float ElementalMastery
    {
        get
        {
            float result = 0f;
            result += (artifacts[ArtifactName.Instructor].level * 20);
            result += Instructor();
            result += (artifacts[ArtifactName.Wanderers_Troupe].level * 20);
            return result;
        }
    }


    public float Armor
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Defenders_Will].level;
            result += Defenders_Will();
            return result;
        }
    }
    public float Atk
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Brave_Heart].level;
            result += Noblesse_Oblige();
            return result;
        }
    }
    public float BaseDamageMultiplier(SkillName skillName)//일반공격 피해
    {
        float result = 0f;
        result += artifacts[ArtifactName.Martial_Artist].level * 10 / 100.0f;
        result += Martial_Artist();
        result += Gladiators_Finale(skillName);
        result += Retracing_Bolide();
        if (Reminiscence_of_Shime())
        {
            result *= 3;
        }
        return result;
    }
    public float SkillDamageMultiplier
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Gambler].level * 10 / 100.0f;
            return result;
        }
    }
    public float RegenMultiplier
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.The_Exile].level * 10 / 100.0f;
            result += artifacts[ArtifactName.Scholar].level * 10 / 100.0f;
            result += artifacts[ArtifactName.Emblem_of_Severed_Fate].level * 20 / 100.0f;
            return result;
        }
    }
    public float ExpMultiplier
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.The_Exile].level * 10 / 100.0f;
            result += artifacts[ArtifactName.Scholar].level * 10 / 100.0f;
            return result;
        }
    }
    public float GreedMultiplier
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Echoes_of_an_Offering].level * 10 / 100.0f;
            return result;
        }
    }

    public float MoveSpeedMultiplier
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Berserker].level * 10 / 100.0f;
            return result;
        }
    }
    public float Luck
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Tiny_Miracle].level;
            result += Berserker();
            result += Blizzard_Strayer();
            return result;
        }
    }
    public float AtkMultiplier
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Gladiators_Finale].level * 10 / 100.0f;
            result += Tenacity_of_the_Millelith();
            return result;
        }
    }
    public float PhysicsDamageMultiplier
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Bloodstained_Chivalry].level * 10 / 100.0f;
            result += artifacts[ArtifactName.Pale_Flame].level * 10 / 100.0f;
            result += Pale_Flame();
            return result;
        }
    }
    public float BurstDamageMultiplier
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Noblesse_Oblige].level * 10 / 100.0f;
            result += Emblem_of_Severed_Fate();
            return result;
        }
    }
    public float DurationMultiplier
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Blizzard_Strayer].level * 6 / 100.0f;
            return result;
        }
    }
    public float CoolTimeMultiplier(Skill.Type skillType)
    {
        float result = 0f;
        result += artifacts[ArtifactName.Heart_of_Depth].level * 6 / 100.0f;
        result += Bloodstained_Chivalry(skillType);
        return result;
    }
    public float HealthMultiplier
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Tenacity_of_the_Millelith].level * 10 / 100.0f;
            result += Ocean_Hued_Clam();
            return result;
        }
    }
    public float HealBonusMultiplier
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Maiden_Beloved].level * 10 / 100.0f;
            result += artifacts[ArtifactName.Ocean_Hued_Clam].level * 10 / 100.0f;
            return result;
        }
    }
    public float MagnetMultiplier
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Viridescent_Venerer].level * 10 / 100.0f;
            result += Viridescent_Venerer(SkillName.Swirl);
            return result;
        }
    }
    public float ShieldMultiplier
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Retracing_Bolide].level * 10 / 100.0f;
            result += Tenacity_of_the_Millelith();
            result += Archaic_Petra();
            return result;
        }
    }
    public float DamageMultiplier
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Crimson_Witch_of_Flames].level * 10 / 100.0f;
            result += Vermillion_Hereafter();
            return result;
        }
    }
    public float AttackSpeedMultiplier
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Thundering_Fury].level * 10 / 100.0f;
            return result;
        }
    }
    public float AreaMultiplier
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Archaic_Petra].level * 10 / 100.0f;
            return result;
        }
    }
    public float Penetrate
    {
        get
        {
            float result = 0f;
            result += Wanderers_Troupe();
            return result;
        }
    }

    public float Amount
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Reminiscence_of_Shime].level * 0.6f;
            result += Heart_of_Depth();
            return result;
        }
    }
    public float BaseAttackCooltimeMultiplier
    {
        get
        {
            float result = 0f;
            return result;
        }
    }
    public float ArmorMultiplier
    {
        get
        {
            float result = 0f;
            result += artifacts[ArtifactName.Husk_of_Opulent_Dreams].level * 10 / 100.0f;
            result += Husk_of_Opulent_Dreams();
            return result;
        }
    }


    public float BaseAttackCriticalRate
    {
        get
        {
            float result = 0f;
            return result;
        }
    }

    public int Rebirth
    {
        get
        {
            int result = 0;
            result += Tiny_Miracle();
            return result;
        }
    }

    public float Curse
    {
        get
        {
            float result = 0;
            result += artifacts[ArtifactName.Vermillion_Hereafter].level * 10 / 100.0f;
            return result;
        }
    }

    public float Instructor()
    {
        if (artifacts[ArtifactName.Instructor].level < SET_COUNT) return 0;
        return 120;
    }
    public void The_Exile(SkillName skillName)//원소 폭발 발동 시 다른 원소폭발 게이지 +5
    {
        if (artifacts[ArtifactName.The_Exile].level < SET_COUNT) return;
        foreach (KeyValuePair<SkillName, SkillObject> skills in GameManager.instance.ownBursts)
        {
            SkillData.ParameterWithKey param = skills.Value.parameterWithKey;
            if (param.name != skillName)
            {
                param.parameter.elementGauge += 5;
            }
        }
    }

    public float Brave_Heart(float maxHP, float HP)//HP가 50%를 초과하는 적에게 가하는 피래 +30%
    {
        if (artifacts[ArtifactName.The_Exile].level < SET_COUNT) return 0;
        if (maxHP / HP * 100 > 50)
        {
            return 0.3f;
        }
        return 0;
    }

    public bool Gambler()//스킬 사용 1초 후에 20% 확률로 재사용 대기시간 초기화
    {
        if (artifacts[ArtifactName.Gambler].level < SET_COUNT) return false;
        if (Random.Range(0, 5) == 1) return true;
        return false;
    }

    public float Scholar()
    {
        if (artifacts[ArtifactName.Scholar].level < SET_COUNT) return 0;
        return 1;
    }

    float Gladiators_Finale(SkillName skillName) //근접 일반 공격 피해 40%
    {
        if (artifacts[ArtifactName.Gladiators_Finale].level < SET_COUNT) return 0;
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
        if (artifacts[ArtifactName.Tenacity_of_the_Millelith].level < SET_COUNT) return 0;
        return 0.2f;
    }

    float Archaic_Petra()
    {
        if (artifacts[ArtifactName.Archaic_Petra].level < SET_COUNT) return 0;
        return 0.4f;
    }

    float Wanderers_Troupe() //관통 +2
    {
        return artifacts[ArtifactName.Wanderers_Troupe].level < SET_COUNT ? 0 : 2;
    }

    float Bloodstained_Chivalry(Skill.Type skillType) //일반공격 재사용 대기시간 -20%
    {
        if (artifacts[ArtifactName.Bloodstained_Chivalry].level < SET_COUNT) return 0;
        if (skillType == Skill.Type.Basic) return 0.2f;
        return 0;
    }

    float Pale_Flame()
    {
        if (artifacts[ArtifactName.Pale_Flame].level < SET_COUNT) return 0;
        return 0.6f;
    }
    float Noblesse_Oblige()//공격력 +5
    {
        if (artifacts[ArtifactName.Noblesse_Oblige].level < SET_COUNT) return 0;
        return 5;
    }

    float Blizzard_Strayer()//행운 +10
    {
        if (artifacts[ArtifactName.Blizzard_Strayer].level < SET_COUNT) return 0;
        return 10;
    }

    float Heart_of_Depth()
    {
        return artifacts[ArtifactName.Heart_of_Depth].level < SET_COUNT ? 0 : 2;
    }

    public float Maiden_Beloved()//치유보너스를 대미지 비율에 추가
    {
        if (artifacts[ArtifactName.Maiden_Beloved].level < SET_COUNT) return 0;
        return GameManager.instance.statCalcuator.HealBonus;
    }

    public float Viridescent_Venerer(SkillName skillName)
    {
        if (artifacts[ArtifactName.Viridescent_Venerer].level < SET_COUNT) return 0;

        switch (skillName)
        {
            case SkillName.Swirl:
                return 0.4f;
        }
        return 0;
    }
    float Retracing_Bolide()
    {
        if (artifacts[ArtifactName.Retracing_Bolide].level < SET_COUNT) return 0;
        if (GameManager.instance.player.sheilds.Count > 0) return 0.4f;
        return 0;
    }
    public float Crimson_Witch_of_Flames(SkillName skillName)
    {
        if (artifacts[ArtifactName.Crimson_Witch_of_Flames].level < SET_COUNT) return 0;

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
        if (artifacts[ArtifactName.Thundering_Fury].level < SET_COUNT) return 0;

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
        return artifacts[ArtifactName.Husk_of_Opulent_Dreams].level < SET_COUNT ? 0 : 0.75f;
    }
    float Defenders_Will()
    {
        if (artifacts[ArtifactName.Defenders_Will].level < SET_COUNT) return 0;

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
    public float Resolution_of_Sojourner() // 지속 피해 스킬의 스킬 대미지 주기 50% 감소
    {
        return artifacts[ArtifactName.Resolution_of_Sojourner].level < SET_COUNT ? 1.0f : 0.5f;
    }
    float Martial_Artist()//일반 공격 피해 +30%
    {
        if (artifacts[ArtifactName.Martial_Artist].level < SET_COUNT) return 0;
        return 0.3f;
    }
    int Tiny_Miracle()
    {
        if (artifacts[ArtifactName.Tiny_Miracle].level < SET_COUNT) return 0;
        return 1;
    }

    int Berserker()
    {
        if (artifacts[ArtifactName.Berserker].level < SET_COUNT) return 0;
        int result = 0;
        if (GameManager.instance.player.maxHealth /
        GameManager.instance.player.health * 100 < 70)
        {
            result += 10;
        }
        return result;
    }

    public bool Reminiscence_of_Shime()
    {
        return artifacts[ArtifactName.Reminiscence_of_Shime].level < SET_COUNT;
    }

    float Emblem_of_Severed_Fate()
    {
        return artifacts[ArtifactName.Emblem_of_Severed_Fate].level < SET_COUNT ? 0 : GameManager.instance.statCalcuator.Regen;
    }

    float Ocean_Hued_Clam()
    {
        return artifacts[ArtifactName.Ocean_Hued_Clam].level < SET_COUNT ? 0 : GameManager.instance.statCalcuator.HealBonus;
    }

    float Vermillion_Hereafter()
    {
        return artifacts[ArtifactName.Vermillion_Hereafter].level < SET_COUNT ? 0 : GameManager.instance.battleResult.kill * 0.05f / 100.0f;
    }

    public float Echoes_of_an_Offering()
    {
        return artifacts[ArtifactName.Echoes_of_an_Offering].level < SET_COUNT ? 1 : 3;
    }

    //TODO: 미등록 아티팩트
    // Artifact.Up.Memory_of_the_Forest
    // Artifact.Up.Gilded_Dream
    // Artifact.Up.Desert_Pavilion_Chronicle
    // Artifact.Up.Lost_Flower_of_Paradise
    // Artifact.Up.Nymphs_Dream
    // Artifact.Up.Vourukashas_Glow
}
