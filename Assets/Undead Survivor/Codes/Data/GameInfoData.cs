using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameInfoData
{
    public bool isGameContinue = false;
    public string currentScene = "";
    public float gameTime = 0;
    public int level = 1;
    public int kill = 0;
    public float exp = 0;
    public int maxLevel = 90;
    public int mora = 0;
    public int primoGem = 0;
    public int gameLevel = 1;
    public Dictionary<SkillName, int> getSkills;
    public Dictionary<SkillName, int> getBursts;
    public Dictionary<ArtifactName, int> getArtifacts;
    public BattleResult battleResult;
    public enum Mapnumber
    {
        MOND = 2,
        LIYUE = 4,
        INAZUMA = 6,
        SUMERU = 8,
    }
    public GameInfoData()
    {
        isGameContinue = false;
        level = 1;
        gameLevel = 1;
        gameTime = 0;
        kill = 0;
        exp = 0;
        mora = 0;
        primoGem = 0;
        getSkills = new Dictionary<SkillName, int>();
        getBursts = new Dictionary<SkillName, int>();
        getArtifacts = new Dictionary<ArtifactName, int>();
        battleResult = new BattleResult();
    }
}
