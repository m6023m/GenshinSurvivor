using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

public class Spawner : MonoBehaviour
{
    public EnemyData enemyData;
    public Transform[] spawnPoint;
    [Searchable]
    public SpawnData[] spawnData;
    public Pattern[] patterns;
    public PatternData[] patternDatas;

    public float[] timers;
    public float bossSpawnTime = 1200;//1200 = 20분에 보스 출현
    public SpawnData bossData;
    public bool isTriggerSpawnBoss = false;
    public BossMap bossMap;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        timers = new float[spawnData.Count()];
    }


    void Update()
    {
        SpawnEnemyCheck();
        SpawnPatternCheck();
        SpawnBossCheck();
    }

    void SpawnEnemyCheck()
    {
        if (BossTriggerEnemySpawnCheck()) return;
        float gameLevel = GameManager.instance.gameInfoData.gameLevel;
        float gameTime = Mathf.FloorToInt(GameManager.instance.gameInfoData.gameTime / gameLevel);
        foreach (var item in spawnData.Select((spawnData, index) => (spawnData, index)))
        {
            timers[item.index] += Time.deltaTime;
            float spawnTime = item.spawnData.spawnTime;
            spawnTime /= GameManager.instance.statCalcuator.Curse;

            if (item.spawnData.spawnTimeMin < gameTime && item.spawnData.spawnTimeMax > gameTime && timers[item.index] > spawnTime)
            {
                timers[item.index] = 0f;
                Spawn(item.spawnData);
            }
        }
    }
    bool BossTriggerEnemySpawnCheck()
    {
        if (bossMap == null) return false;
        if (!bossMap.gameObject.activeSelf) return false;
        return !bossMap.bossTrigger.isTriggered;
    }
    void SpawnPatternCheck()
    {
        float gameLevel = GameManager.instance.gameInfoData.gameLevel;
        float gameTime = Mathf.FloorToInt(GameManager.instance.gameInfoData.gameTime / gameLevel);

        for (int patternIndex = 0; patternIndex < patterns.Length; patternIndex++)
        {
            if (patternDatas[patternIndex].time == gameTime)
            {
                patterns[patternIndex].Init(patternDatas[patternIndex]);
            }
        }
    }

    void SpawnBossCheck()
    {
        float gameLevel = GameManager.instance.gameInfoData.gameLevel;
        float gameTime = Mathf.FloorToInt(GameManager.instance.gameInfoData.gameTime / gameLevel);
        if (gameTime > bossSpawnTime)
        {
            if (isTriggerSpawnBoss)
            {
                SpwanBossMap();
            }
            else
            {
                SpawnBoss();
            }
        }
    }

    void SpwanBossMap()
    {
        if (bossMap == null) return;
        if (bossMap.gameObject.activeSelf) return;
        bossMap.Init();
    }

    void Spawn(SpawnData spawnData)
    {
        SpawnData newSpawnData = spawnData;
        newSpawnData.enemyStat = NewEnemyStat(newSpawnData.spriteName, newSpawnData);
        GameObject enemy = GameManager.instance.poolManager.Get((int)PoolManager.Type.Enemy);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<EnemyNormal>().Init(newSpawnData);
    }

    EnemyData.EnemyStat NewEnemyStat(EnemyNormal.Name newEnemy, SpawnData spawnData)
    {
        int timeValue = (int)(spawnData.spawnTimeMin / 60.0f);
        float damagePer = 1.0f + (0.05f * timeValue);
        float healthPer = 1.0f + (1.0f * timeValue);
        EnemyData.EnemyStat newEnemyStat = new EnemyData.EnemyStat();
        EnemyData.EnemyStat enemyStat = enemyData.Get(newEnemy);
        newEnemyStat.name = enemyStat.name;
        newEnemyStat.anim = enemyStat.anim;
        newEnemyStat.pattern = enemyStat.pattern;
        newEnemyStat.enemyAttackData = new EnemyAttackData(enemyStat.enemyAttackData);
        newEnemyStat.patternCoolTime = enemyStat.patternCoolTime;
        newEnemyStat.patternRange = enemyStat.patternRange;
        newEnemyStat.elementType = enemyStat.elementType;
        newEnemyStat.health = enemyStat.health * healthPer;
        newEnemyStat.damage = enemyStat.damage * damagePer;
        newEnemyStat.armor = enemyStat.armor;
        newEnemyStat.speed = enemyStat.speed;
        newEnemyStat.size = enemyStat.size;
        newEnemyStat.exp = enemyStat.exp;
        newEnemyStat.PhysicsRes = enemyStat.PhysicsRes;
        newEnemyStat.PyroRes = enemyStat.PyroRes;
        newEnemyStat.HydroRes = enemyStat.HydroRes;
        newEnemyStat.AnemoRes = enemyStat.AnemoRes;
        newEnemyStat.DendroRes = enemyStat.DendroRes;
        newEnemyStat.ElectroRes = enemyStat.ElectroRes;
        newEnemyStat.CyroRes = enemyStat.CyroRes;
        newEnemyStat.GeoRes = enemyStat.GeoRes;

        if (spawnData.enemyType == Enemy.Type.Elite)
        {
            newEnemyStat.health *= 20f;
            newEnemyStat.damage *= 2f;
            newEnemyStat.exp *= 10f;
            newEnemyStat.size *= 1.5f;
            if (spawnData.spriteName == EnemyNormal.Name.Chuchu_Big_Basic ||
            spawnData.spriteName == EnemyNormal.Name.Chuchu_Big_Shield ||
            spawnData.spriteName == EnemyNormal.Name.Whopperflower_Pyro ||
            spawnData.spriteName == EnemyNormal.Name.Whopperflower_Cyro ||
            spawnData.spriteName == EnemyNormal.Name.Whopperflower_Electro)
            {
                newEnemyStat.health *= 0.5f;
            }
            else if (spawnData.spriteName.ToString().Contains("Slime"))
            {
                newEnemyStat.health *= 2f;
            }
        }
        return newEnemyStat;
    }


    public void SpawnBoss()
    {
        Boss boss = GameManager.instance.boss;
        if (!boss.gameObject.activeSelf)
        {
            boss.InitBoss(bossData);
            boss.ResetPosition();
        }
    }

}

[System.Serializable]
public class SpawnData
{
    public EnemyNormal.Name spriteName;
    public Enemy.Type enemyType;
    public float spawnTime;
    public float spawnTimeMin;
    public float spawnTimeMax;
    public EnemyData.EnemyStat enemyStat;
}