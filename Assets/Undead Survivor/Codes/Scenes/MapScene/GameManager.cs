using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Rewired;
using Rewired.ComponentControls;
using Unity.VisualScripting;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [Header("# Prefabs")]
    public GameObject moraPrefab;
    public GameObject primogGemPrefab;


    [Header("# Game Controll")]
    public bool IsPause;
    public bool IsInfinityMode;
    public bool IsVictory;


    [Header("# Game Object")]
    public Player player;
    public Boss boss;
    public GameInfoData gameInfoData;
    public InfoManager infoManager;
    public PoolManager poolManager;
    public SkillData skillData;
    public ArtifactData artifactData;
    public ButtonManager buttonManager;
    public TouchController touchController;
    public LevelUpManager levelUpManager;
    public BattleResult battleResult;
    public CharacterData characterData;
    public WeaponData weaponData;
    ConstellationData constellationData;
    public StatCalculator statCalculator;
    public StatBuff statBuff;
    public GameObject playerSkills;
    public SkillPanel playerSkillIcons;
    public SkillPanel playerBurstIcons;
    public SkillPanel playerArtifactIcons;
    public SkillObject baseAttack;

    public Dictionary<SkillName, SkillObject> ownSkills;
    public Dictionary<ArtifactName, ArtifactData.ParameterWithKey> ownArtifacts;
    public Dictionary<SkillName, SkillObject> ownBursts;
    public DamageAttach damageAttach;
    public string mapName = "MapScene0";
    public GameObject[] maps;
    int deviceWidth = 1600;
    int maxBurstCount = 4;
    public ObjectTracker bossTracker;

    void Awake()
    {
        InitMap();
        instance = this;
        Pause(false);
        IsVictory = false;
        battleResult.suvivorTime = 0;
        skillData.Reset();
        artifactData.ResetArtifacts();
        statBuff = new StatBuff();
        statCalculator = new StatCalculator(player.stat).ArtifactData(artifactData).WeaponData(weaponData.Get(player.stat.weaponName)).StatBuff(statBuff);
        InitBattleSave();
        ownSkills = new Dictionary<SkillName, SkillObject>();
        ownBursts = new Dictionary<SkillName, SkillObject>();
        ownArtifacts = new Dictionary<ArtifactName, ArtifactData.ParameterWithKey>();
        constellationData = new ConstellationData();
        IsInfinityMode = GameDataManager.instance.saveData.option.isInfinityMode;
        PlayBattleBGM();
    }
    void PlayBattleBGM()
    {
        int currentMapNum = GameDataManager.instance.saveData.userData.currentMapNumber;
        if (currentMapNum < (int)GameInfoData.Mapnumber.MOND)
        {
            AudioManager.instance.PlayBGM(AudioManager.BGM.Battle0, true);
        }
        else if (currentMapNum < (int)GameInfoData.Mapnumber.LIYUE)
        {
            AudioManager.instance.PlayBGM(AudioManager.BGM.Battle1, true);
        }
        else if (currentMapNum < (int)GameInfoData.Mapnumber.INAZUMA)
        {
            AudioManager.instance.PlayBGM(AudioManager.BGM.Battle2, true);
        }
        else if (currentMapNum < (int)GameInfoData.Mapnumber.SUMERU)
        {
            AudioManager.instance.PlayBGM(AudioManager.BGM.Battle3, true);
        }
    }

    void InitBattleSave()
    {
        gameInfoData = GameDataManager.instance.saveData.gameInfoData;
        if (!gameInfoData.isGameContinue)
        {
            gameInfoData = new GameInfoData();
            gameInfoData.battleResult = battleResult;
            gameInfoData.reroll = (int)statCalculator.Reroll;
            gameInfoData.skip = (int)statCalculator.Skip;
            GameDataManager.instance.saveData.record.playCount++;
            GameDataManager.instance.saveData.record.formPlayCount++;
        }
        gameInfoData.isGameContinue = true;
        GameDataManager.instance.saveData.gameInfoData = gameInfoData;
        gameInfoData.currentScene = mapName;
        battleResult = gameInfoData.battleResult;
    }

    void InitMap()
    {
        GameObject map = maps[GameDataManager.instance.saveData.userData.currentMapNumber];
        Spawner spawner = map.GetComponentInChildren<Spawner>(true);
        spawner.transform.parent = player.transform;
        map.SetActive(true);
        boss = map.GetComponentInChildren<Boss>(true);
        bossTracker.trackedObject = boss.gameObject;
        bossTracker.objectIcon = boss.bossIcon;
    }

    void Start()
    {
        InitSkill();
        InitConstellation();
        InitEditorMode();
    }

    void InitEditorMode()
    {
    }

    void InitSkill()
    {
        Character[] characters = GameDataManager.instance.saveData.userData.selectChars;
        int charNum = (int)characters[0].charNum;
        SkillName skillNameBasic = characterData.characters[charNum].skillBasic;
        SkillName skillName = characterData.characters[charNum].skill;

        levelUpManager.SkillUp(skillNameBasic);
        levelUpManager.SkillUp(skillName);

#if UNITY_EDITOR
        SkillName skillBurst = skillData.skills[skillName].burst;
        AddBurst(skillBurst);
        gameInfoData.getBursts.AddOrUpdate(skillBurst, 1);
#endif

        for (int i = 1; i < GameDataManager.instance.saveData.userData.selectChars.Length; i++)
        {
            if (characters[i] == null) continue;
            if (characters[i].isMine)
            {
                SkillName skillNameSub = characterData.characters[(int)characters[i].charNum].skill;
                levelUpManager.SkillUp(skillNameSub);
#if UNITY_EDITOR
                // SkillName skillBurstSub = skillData.skillsDictionary[skillNameSub).burst;
                // AddBurst(skillBurstSub);
#endif
            }
        }
        AddSaveSkillsAndArtifacts();
    }
    void AddSaveSkillsAndArtifacts()
    {
        Dictionary<SkillName, int> bursts = gameInfoData.getBursts.DeepCopy();
        Dictionary<SkillName, int> skills = gameInfoData.getSkills.DeepCopy();
        Dictionary<ArtifactName, int> artifacts = gameInfoData.getArtifacts.DeepCopy();

        foreach (KeyValuePair<SkillName, int> burst in bursts)
        {
            for (int index = 0; index < burst.Value; index++)
            {
                AddBurst(burst.Key);
            }
        }

        foreach (KeyValuePair<SkillName, int> skill in skills)
        {
            for (int index = 0; index < skill.Value; index++)
            {
                levelUpManager.SkillUp(skill.Key);
            }
        }

        foreach (KeyValuePair<ArtifactName, int> artifact in artifacts)
        {
            for (int index = 0; index < artifact.Value; index++)
            {
                levelUpManager.ArtifactUp(artifact.Key);
            }
        }

    }

    void InitConstellation()
    {
        foreach (Character character in GameDataManager.instance.saveData.userData.selectChars)
        {
            if (character == null) continue;
            constellationData.InitConstellation(character);
        }
    }

    void Update()
    {
        float time = (1.0f * Time.deltaTime);
        gameInfoData.gameTime += time;
        GameDataManager.instance.saveData.record.totalSurvivalTime += time;
        battleResult.suvivorTime = gameInfoData.gameTime;
        int checkTime = (int)gameInfoData.gameTime;
        if (checkTime % 30 == 0)
        {
            GameDataManager.instance.SaveInstance();
        }
    }

    public void GetExp(float exp)
    {
        if (gameInfoData.level == gameInfoData.maxLevel) return;
        float result = exp * statCalculator.Exp;
        gameInfoData.exp += result;
        if (gameInfoData.exp >= ReqExp(gameInfoData.level))
        {
            gameInfoData.exp -= ReqExp(gameInfoData.level);
            gameInfoData.level++;
            battleResult.level++;
            levelUpManager.VisivleLevelUpWindow();
        }
    }

    public int ReqExp(int level)
    {
        if (level == 1)
        {
            return 5;
        }

        int value = 5;
        value += (20 * level);

        return value + 5;
    }

    public void Pause(bool pause)
    {
        IsPause = pause;
        touchController.gameObject.SetActive(!pause);
        AudioManager.instance.EffectBgm(pause);
        Time.timeScale = pause ? 0 : 1;
    }

    public void AddSkill(SkillName skillName)
    {
        if (ownSkills.ContainsKey(skillName)) return;

        SkillData.ParameterWithKey param = skillData.skills[skillName];
        SkillObject skillObj = poolManager.GetObject<SkillObject>();
        skillObj.transform.parent = playerSkills.transform;
        skillObj.transform.localPosition = Vector3.zero;
        skillObj.Init(param);

        if (param.name == SkillName.Basic_Catalist)
        {
            foreach (SkillSet.SkillSequence skillSequence in param.skillSet.sequences)
            {
                skillSequence.elementType = player.stat.elementType;
            }
        }
        param.level = 1;
        if (param.type == Skill.Type.Basic)
        {
            baseAttack = skillObj;
        }

        playerSkillIcons.AddSkillData(param, skillObj);

        ownSkills[skillName] = skillObj;
        GameDataManager.instance.saveData.record.levelUpSkillCount++;
    }

    public void AddBurst(SkillName skillName)
    {
        if (ownBursts.Count == maxBurstCount) return;
        if (ownBursts.ContainsKey(skillName)) return;

        SkillData.ParameterWithKey param = skillData.skills[skillName];
        SkillObject skillObj = poolManager.GetObject<SkillObject>();
        skillObj.transform.parent = playerSkills.transform;
        skillObj.transform.localPosition = Vector3.zero;
        skillObj.Init(param);

        playerBurstIcons.AddSkillData(param, skillObj);

        ownBursts[skillName] = skillObj;
        GameDataManager.instance.saveData.record.getBurstCount++;
    }


    public void AddArtifact(ArtifactName artifactName)
    {
        ArtifactData.ParameterWithKey param = artifactData.artifacts[artifactName];

        playerArtifactIcons.AddArtifactData(param);

        ownArtifacts[artifactName] = param;
        GameDataManager.instance.saveData.record.levelUpArtifactCount++;
    }


    public void GainMora(int mora)
    {
        if (mora < 1) return;
        float stageValue = 1.0f + GameDataManager.instance.saveData.userData.stageLevel * 0.1f;
        int moraResult = (int)(mora * statCalculator.Greed * stageValue);
        gameInfoData.mora += moraResult;
        battleResult.gainMora += moraResult;
        WriteMora(player.transform, moraResult);
    }
    public void GainPrimoGem(int primoGem)
    {
        if (primoGem < 1) return;
        float stageValue = 1.0f + GameDataManager.instance.saveData.userData.stageLevel * 0.1f;
        int primoGemResult = primoGem + (int)(primoGem * statCalculator.Greed * stageValue);
        gameInfoData.primoGem += primoGemResult;
        battleResult.gainPrimoGem += primoGemResult;
    }


    IEnumerator GainMora(Transform transform, float damage)
    {
        yield return null;
        player.moraAttach.WriteDamage(transform, damage, Element.Color(Element.Type.Physics));
    }

    public void WriteMora(Transform transform, float value)
    {
        StartCoroutine(GainMora(transform, value));
    }
    public void Defeat()
    {
        PopNext();
    }
    public void Victory()
    {
        IsVictory = true;

        if (IsInfinityMode) return;
        PopNext();
    }
    void PopNext()
    {
        if (IsVictory) buttonManager.PopNextVictory();
        if (!IsVictory) buttonManager.PopNextDefeat();
        Pause(true);
    }

    public void AddElementGauge(float value)
    {
        foreach (KeyValuePair<SkillName, SkillObject> skills in ownBursts)
        {
            SkillData.ParameterWithKey param = skills.Value.parameterWithKey;
            float result = value * statCalculator.Regen;
            result += artifactData.Scholar();
            param.parameter.elementGauge += result;
        }
    }
}
