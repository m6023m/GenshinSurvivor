using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
public class LevelUpManager : MonoBehaviour
{
    public EventButton[] buttons;
    public InfoButton[] InfoButtons;
    public TextMeshProUGUI[] tooltips;
    public Image[] images;
    public Button btnSkip;
    public Button btnReroll;
    public TextMeshProUGUI skipCount;
    public TextMeshProUGUI rerollCount;
    public GameObject levelUpManager;
    public SkillData skillData;
    public ArtifactData artifactData;
    public Sprite spriteMora;
    public Sprite spriteHeal;
    int skip = 0;
    int reroll = 0;
    int currentSkillCount = 0;
    int currentArtifactCount = 0;
    int maxSkillCount = 6;
    int maxSkillLevel = 7;
    int maxArtifactCount = 4;
    int maxArtifactLevel = 5;
    string textMora = "Basic.LevelUp.Mora";
    string textHeal = "Basic.LevelUp.Heal";
    int selectedButtonIndex = 0;
    Rewired.Player rewiredPlayer;

    private void Awake()
    {
        skip = GameDataManager.instance.saveData.userData.upgrade.upgradeComponents[(int)UpgradeType.SKIP].cnt;
        reroll = GameDataManager.instance.saveData.userData.upgrade.upgradeComponents[(int)UpgradeType.REROLL].cnt;
        skipCount.text = skip.ToString();
        rerollCount.text = reroll.ToString();
        btnSkip.onClick.AddListener(OnClickSkip);
        btnReroll.onClick.AddListener(OnClickReroll);
        rewiredPlayer = Rewired.ReInput.players.GetPlayer(0);
    }
    private void Update()
    {
        if (rewiredPlayer.GetButtonDown("special"))
        {
            if (selectedButtonIndex == -1) return;
            InfoButtons[selectedButtonIndex].Click();
        }
    }

    void OnClickSkip()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        if (skip == 0) return;
        skip--;
        skipCount.text = skip.ToString();
        CloseLevelUpDisplayOnLevelUp();
    }
    void OnClickReroll()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        if (reroll == 0) return;
        reroll--;
        rerollCount.text = reroll.ToString();
        ShowRandomSkillOrArtifact();
    }

    void OnClickLevelUpSkill(SkillData.ParameterWithKey skill)
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        SkillUp(skill.name);
        CloseLevelUpDisplayOnLevelUp();
    }
    void OnClickLevelUpArtifact(ArtifactData.ParameterWithKey artifact)
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        ArtifactUp(artifact.name);
        CloseLevelUpDisplayOnLevelUp();
    }

    void CloseLevelUpDisplayOnLevelUp()
    {
        GameManager.instance.Pause(false);
        levelUpManager.gameObject.SetActive(false);
        GameManager.instance.GetExp(0);
        selectedButtonIndex = -1;
        GameDataManager.instance.SaveInstance();
    }

    public void SkillUp(SkillName skillName)
    {
        SkillData.ParameterWithKey skill = GameManager.instance.skillData.skillsDictionary[skillName];
        if (skill.level >= maxSkillLevel) return;
        if (skill.level == 0)
        {
            GameManager.instance.AddSkill(skill.name);
            return;
        }
        int skillLevel = skill.level;
        SkillData.SkillUp skillUp = skill.skillUps[skillLevel - 1];

        switch (skillUp.name)
        {
            case SkillData.UpgradeName.Damage:
                skill.parameter.damage += (skill.parameter.damage * skillUp.value);
                break;
            case SkillData.UpgradeName.Penetrate:
                skill.parameter.penetrate += (int)skillUp.value;
                break;
            case SkillData.UpgradeName.Area:
                skill.parameter.area += (skill.parameter.area * skillUp.value);
                break;
            case SkillData.UpgradeName.Duration:
                skill.parameter.duration += (skill.parameter.duration * skillUp.value);
                break;
            case SkillData.UpgradeName.Speed:
                skill.parameter.speed += (skill.parameter.speed * skillUp.value);
                break;
            case SkillData.UpgradeName.KnockBack:
                skill.parameter.knockBack += (skill.parameter.knockBack * skillUp.value);
                break;
            case SkillData.UpgradeName.Magnet:
                skill.parameter.magnet += (skill.parameter.magnet * skillUp.value);
                break;
            case SkillData.UpgradeName.MagnetSpeed:
                skill.parameter.magnetSpeed += (skill.parameter.magnetSpeed * skillUp.value);
                break;
            case SkillData.UpgradeName.Amount:
                skill.parameter.count += (int)skillUp.value;
                break;
            case SkillData.UpgradeName.SkillTick:
                skill.parameter.skillTick -= skillUp.value;
                break;
            case SkillData.UpgradeName.CoolTime:
                skill.parameter.coolTime -= (skill.parameter.coolTime * skillUp.value);
                break;
            case SkillData.UpgradeName.HealPer:
                skill.parameter.healPer += skillUp.value;
                break;
            case SkillData.UpgradeName.SheildPer:
                skill.parameter.sheildPer += skillUp.value;
                break;
        }

        skill.level++;

        if (skillLevel == skill.skillUps.Count - 1 && skill.type == Skill.Type.Skill)
        {
            GameManager.instance.AddBurst(skill.burst);
            GameManager.instance.gameInfoData.getBursts.AddOrUpdate(skill.burst, 1);
        }
        GameManager.instance.gameInfoData.getSkills.AddOrUpdate(skillName, skillLevel);
    }

    public void ArtifactUp(ArtifactName artifactName)
    {
        ArtifactData.ParameterWithKey artifact = GameManager.instance.artifactData.Get(artifactName);
        if (artifact.level >= maxArtifactLevel) return;
        if (artifact.level == 0)
        {
            GameManager.instance.AddArtifact(artifact.name);
        }
        artifact.level++;
        GameManager.instance.gameInfoData.getArtifacts.AddOrUpdate(artifactName, artifact.level);
    }


    public void VisivleLevelUpWindow()
    {
        GameManager.instance.Pause(true);
        AudioManager.instance.PlaySFX(AudioManager.SFX.LevelUp);
        levelUpManager.gameObject.SetActive(true);
        ShowRandomSkillOrArtifact();
        buttons[0].gameObject.SelectObject();
    }

    void ShowRandomSkillOrArtifact()
    {
        List<SkillData.ParameterWithKey> availableSkills = GetAvailableSkills();
        List<ArtifactData.ParameterWithKey> availableArtifacts = GetAvailableArtifacts();
        int availCount = availableSkills.Count + availableArtifacts.Count;
        for (int i = 0; i < 4; i++)
        {
            int index = i;
            if (index >= availCount)
            {
                DisplayRandomItem(index);
            }
            else
            {
                DisplaySkillOrArtifact(index, availableSkills, availableArtifacts);
            }

        }
    }

    private void DisplaySkillOrArtifact(int index, List<SkillData.ParameterWithKey> availableSkills, List<ArtifactData.ParameterWithKey> availableArtifacts)
    {
        while (availableSkills.Count > 0 || availableArtifacts.Count > 0)
        {
            bool showSkill = UnityEngine.Random.Range(0, 2) == 0; // 0 또는 1을 무작위로 생성하여 스킬과 아이템의 확률을 결정

            if (showSkill && availableSkills.Count > 0 && currentSkillCount < maxSkillCount)
            {
                int randomIndex = UnityEngine.Random.Range(0, availableSkills.Count);
                SkillData.ParameterWithKey randomSkill = availableSkills[randomIndex]; // 무작위 스킬 선택
                availableSkills.RemoveAt(randomIndex); // 선택한 스킬을 사용 가능한 스킬 목록에서 제거
                DisplaySkill(randomSkill, index); // 선택한 스킬 표시
                break;
            }
            else if (!showSkill && availableArtifacts.Count > 0 && currentArtifactCount < maxArtifactCount)
            {
                int randomIndex = UnityEngine.Random.Range(0, availableArtifacts.Count);
                ArtifactData.ParameterWithKey randomArtifact = availableArtifacts[randomIndex]; // 무작위 아이템 선택
                availableArtifacts.RemoveAt(randomIndex); // 선택한 아이템을 사용 가능한 아이템 목록에서 제거
                DisplayArtifact(randomArtifact, index); // 선택한 아이템 표시
                break;
            }
        }
    }

    List<SkillData.ParameterWithKey> GetAvailableSkills()
    {
        List<SkillData.ParameterWithKey> availableSkills = new List<SkillData.ParameterWithKey>();
        foreach (SkillData.ParameterWithKey skill in skillData.skillsDictionary.Values)
        {
            if (CheckSkillTerm(skill))
            {
                availableSkills.Add(skill);
            }
        }

        return availableSkills;
    }
    List<ArtifactData.ParameterWithKey> GetAvailableArtifacts()
    {
        List<ArtifactData.ParameterWithKey> availableArtifacts = new List<ArtifactData.ParameterWithKey>();
        foreach (ArtifactData.ParameterWithKey artifact in artifactData.artifacts)
        {
            if (CheckArtifactTerm(artifact))
            {
                availableArtifacts.Add(artifact);
            }
        }
        return availableArtifacts;
    }
    void DisplaySkill(SkillData.ParameterWithKey skill, int idx)
    {
        int index = idx;
        images[index].sprite = skill.icon;
        string tooltipText = skill.GetToolTip();
        tooltips[index].text = tooltipText;
        InfoButtons[index].Init(skill.name, ArtifactName.Artifact_None);
        buttons[index].onClick.RemoveAllListeners();
        buttons[index].onClick.AddListener(() => OnClickLevelUpSkill(skill));
        buttons[index].onSelect = () =>
        {
            selectedButtonIndex = index;
        };
    }

    void DisplayArtifact(ArtifactData.ParameterWithKey artifact, int idx)
    {
        int index = idx;
        images[index].sprite = artifact.icon;
        string tooltipText = artifact.GetToolTip();
        tooltips[index].text = tooltipText;
        InfoButtons[index].Init(SkillName.Skill_None, artifact.name);
        buttons[index].onClick.RemoveAllListeners();
        buttons[index].onClick.AddListener(() => OnClickLevelUpArtifact(artifact));
        buttons[index].onSelect = () =>
        {
            selectedButtonIndex = index;
        };
    }

    void DisplayRandomItem(int index)
    {
        bool showItem = UnityEngine.Random.Range(0, 2) == 0;

        if (showItem)
        {
            DisplayMora(index);
        }
        else
        {
            DisplayHeal(index);
        }
    }

    void DisplayMora(int idx)
    {
        images[idx].sprite = spriteMora;
        string tooltipText = textMora.Localize();
        tooltips[idx].text = tooltipText;
        buttons[idx].onClick.RemoveAllListeners();
        buttons[idx].onClick.AddListener(() => OnClickMora());
    }

    void OnClickMora()
    {
        int mora = Random.Range(10, 100);
        GameManager.instance.GainMora(mora);
        CloseLevelUpDisplayOnLevelUp();
    }
    void DisplayHeal(int idx)
    {
        images[idx].sprite = spriteHeal;
        string tooltipText = textHeal.Localize();
        tooltips[idx].text = tooltipText;
        buttons[idx].onClick.RemoveAllListeners();
        buttons[idx].onClick.AddListener(() => OnClickHeal());

    }

    void OnClickHeal()
    {
        float healValue = 30.0f;
        GameManager.instance.player.HealHealth(healValue);
        CloseLevelUpDisplayOnLevelUp();
    }

    bool CheckSkillTerm(SkillData.ParameterWithKey skill)
    {
        if (skill.level >= maxSkillLevel)
        {
            return false;
        }

        if (skill.type != Skill.Type.Basic && skill.type != Skill.Type.Skill) //기본공격과 스킬만 해당
        {
            return false;
        }

        if (skill.type == Skill.Type.Basic && skill.name != GameManager.instance.baseAttack.parameterWithKey.name) //기본공격은 하나만
        {
            return false;
        }

        if (GameManager.instance.ownSkills.Count >= maxSkillCount) //최대 스킬 수량에 도달했을 경우
        {
            foreach (KeyValuePair<SkillName, SkillObject> ownSkill in GameManager.instance.ownSkills) //스킬이 최대 개수일 경우
            {
                if (skill.name == ownSkill.Key)
                {
                    return true;
                }
            }
            return false;
        }

        return true;
    }

    bool CheckArtifactTerm(ArtifactData.ParameterWithKey artifact)
    {
        if (artifact.level >= maxArtifactLevel) //만랩성유물일 경우 표시되지 않음
        {
            return false;
        }


        if (GameManager.instance.ownArtifacts.Count >= maxArtifactCount) //최대 성유물 수량에 도달했을 경우
        {
            foreach (KeyValuePair<ArtifactName, ArtifactData.ParameterWithKey> ownArtifact in GameManager.instance.ownArtifacts) //성유물이 최대 개수일 경우
            {
                if (artifact.name == ownArtifact.Key)
                {
                    return true;
                }
            }
            return false;
        }

        return true;
    }

}
