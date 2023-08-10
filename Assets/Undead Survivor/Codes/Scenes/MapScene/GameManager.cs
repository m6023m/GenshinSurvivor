using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Rewired;
using Rewired.ComponentControls;
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
    public DamageManager damageManager;
    public TouchController touchController;
    public LevelUpManager levelUpManager;
    public BattleResult battleResult;
    public CharacterData characterData;
    public WeaponData weaponData;
    ConstellationData constellationData;
    public StatCalculator statCalcuator;
    public StatBuff statBuff;
    public GameObject playerSkills;
    public SkillPanel playerSkillIcons;
    public SkillPanel playerBurstIcons;
    public SkillPanel playerArtifactIcons;
    public List<SkillData.ParameterWithKey> ownSkills;
    public List<ArtifactData.ParameterWithKey> ownArtifacts;
    public List<SkillData.ParameterWithKey> ownBursts;
    public string mapName = "MapScene0";
    int deviceWidth = 1600;
    int maxBurstCount = 4;
    void Awake()
    {
        InitBattleSave();
        instance = this;
        Pause(false);
        IsVictory = false;
        battleResult.suvivorTime = 0;
        skillData.Reset();
        artifactData.ResetArtifacts();
        statBuff = new StatBuff();
        statCalcuator = new StatCalculator(player.stat).ArtifactData(artifactData).WeaponData(weaponData.Get(player.stat.weaponName)).StatBuff(statBuff);
        ownSkills = new List<SkillData.ParameterWithKey>();
        ownBursts = new List<SkillData.ParameterWithKey>();
        ownArtifacts = new List<ArtifactData.ParameterWithKey>();
        constellationData = new ConstellationData();
        IsInfinityMode = GameDataManager.instance.saveData.option.isInfinityMode;
        GameDataManager.instance.saveData.record.playCount++;
        GameDataManager.instance.saveData.record.formPlayCount++;
        PlayBattleBGM();
    }
    void PlayBattleBGM()
    {
        if (mapName.Equals("MapScene0") || mapName.Equals("MapScene1"))
        {
            AudioManager.instance.PlayBGM(AudioManager.BGM.Battle0, true);
        }
        else if (mapName.Equals("MapScene2") || mapName.Equals("MapScene3"))
        {
            AudioManager.instance.PlayBGM(AudioManager.BGM.Battle1, true);
        }
        else if (mapName.Equals("MapScene4") || mapName.Equals("MapScene5"))
        {
            AudioManager.instance.PlayBGM(AudioManager.BGM.Battle2, true);
        }
        else if (mapName.Equals("MapScene6") || mapName.Equals("MapScene7"))
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
        }
        gameInfoData.isGameContinue = true;
        GameDataManager.instance.saveData.gameInfoData = gameInfoData;
        gameInfoData.currentScene = mapName;
        battleResult = gameInfoData.battleResult;
    }

    void Start()
    {
        InitSkill();
        InitConstellation();
        InitEditorMode();
        GameDataManager.instance.SaveInstance();
    }

    void InitEditorMode()
    {
    }

    void InitSkill()
    {
        GameObject skillObjectBasic = poolManager.Get(PoolManager.Type.SkillObject);
        GameObject skillObject = poolManager.Get(PoolManager.Type.SkillObject);
        Character[] characters = GameDataManager.instance.saveData.userData.selectChars;
        int charNum = (int)characters[0].charNum;
        SkillName skillNameBasic = characterData.characters[charNum].skillBasic;
        SkillName skillName = characterData.characters[charNum].skill;

        levelUpManager.SkillUp(skillNameBasic);
        levelUpManager.SkillUp(skillName);

#if UNITY_EDITOR
        SkillName skillBurst = GameManager.instance.skillData.Get(skillName).burst;
        AddBurst(skillBurst);
        GameManager.instance.gameInfoData.getBursts.AddOrUpdate(skillBurst, 1);
#endif

        for (int i = 1; i < GameDataManager.instance.saveData.userData.selectChars.Length; i++)
        {
            if (characters[i] == null) continue;
            if (characters[i].isMine)
            {
                SkillName skillNameSub = characterData.characters[(int)characters[i].charNum].skill;
                levelUpManager.SkillUp(skillNameSub);
#if UNITY_EDITOR
                // SkillName skillBurstSub = GameManager.instance.skillData.Get(skillNameSub).burst;
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
        float result = exp * GameManager.instance.statCalcuator.Exp;
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
        GameObject skillObject = poolManager.Get(PoolManager.Type.SkillObject);
        SkillData.ParameterWithKey param = skillData.Get(skillName);
        SkillObject skillObj = skillObject.GetComponent<SkillObject>();
        skillObj.Init(param);
        skillObject.transform.parent = playerSkills.transform;
        skillObject.transform.localPosition = Vector3.zero;
        if (param.name == SkillName.Basic_Catalist)
        {
            foreach (SkillSet.SkillSequence skillSequence in param.skillSet.sequences)
            {
                skillSequence.elementType = player.stat.elementType;
            }
        }
        param.level = 1;

        playerSkillIcons.AddSkillData(param, skillObj);

        ownSkills.Add(param);
        GameDataManager.instance.saveData.record.levelUpSkillCount++;
    }

    public void AddBurst(SkillName skillName)
    {
        if (ownBursts.Count == maxBurstCount) return;
        foreach (SkillData.ParameterWithKey parameter in ownBursts)
        {
            if (parameter.name == skillName) return;
        }

        GameObject skillObject = poolManager.Get(PoolManager.Type.SkillObject);
        SkillData.ParameterWithKey param = skillData.Get(skillName);
        SkillObject skillObj = skillObject.GetComponent<SkillObject>();
        skillObj.Init(param);
        skillObject.transform.parent = playerSkills.transform;
        skillObject.transform.localPosition = Vector3.zero;

        playerBurstIcons.AddSkillData(param, skillObj);

        ownBursts.Add(param);
        GameDataManager.instance.saveData.record.getBurstCount++;
    }


    public void AddArtifact(ArtifactName artifactName)
    {
        ArtifactData.ParameterWithKey param = artifactData.Get(artifactName);

        playerArtifactIcons.AddArtifactData(param);

        ownArtifacts.Add(param);
        GameDataManager.instance.saveData.record.levelUpArtifactCount++;
    }


    public void GainMora(int mora)
    {
        if (mora < 1) return;
        int moraResult = (int)(mora * statCalcuator.Greed);
        gameInfoData.mora += mora;
        battleResult.gainMora += mora;
        WriteMora(player.transform, mora);
    }
    public void GainPrimoGem(int primoGem)
    {
        if (primoGem < 1) return;
        int primoGemResult = primoGem + (int)(primoGem * statCalcuator.Greed);
        gameInfoData.primoGem += primoGem;
        battleResult.gainPrimoGem += primoGem;
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
        GameManager.instance.IsVictory = true;

        if (IsInfinityMode) return;
        PopNext();
    }
    void PopNext()
    {
        if (IsVictory) GameManager.instance.buttonManager.PopNextVictory();
        if (!IsVictory) GameManager.instance.buttonManager.PopNextDefeat();
        GameManager.instance.Pause(true);
    }
}
