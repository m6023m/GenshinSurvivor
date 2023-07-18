using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    public enum PatternType
    {
        None,
        MoveToPlayer
    }
    float time = 0;
    float disableTime = 9999;
    bool isPattern = false;
    Enemy[] enemies;
    Scanner scanner;
    PatternData data;
    public PatternType patternType;
    Vector3 targetVector;
    Vector3 originalPosition;
    private void Update()
    {
        time += Time.deltaTime;

        if (time >= disableTime)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.isLive = false;
                enemy.gameObject.SetActive(false);
            }
            isPattern = false;
        }
        MoveToPlayer();
    }

    public void Init(PatternData data)
    {
        this.data = data;
        if (isPattern) return;
        switch (patternType)
        {
            case PatternType.None:
                InitNone();
                break;
            case PatternType.MoveToPlayer:
                InitMoveToPlayer();
                break;

        }
        disableTime = data.duration;
        time = 0;
        enemies = GetComponentsInChildren<Enemy>(true);
        foreach (Enemy enemy in enemies)
        {
            enemy.Init(data.spawnData);
            enemy.gameObject.SetActive(true);
            enemy.isLive = true;
        }
        isPattern = true;
    }

    void InitNone()
    {
        transform.position = GameManager.instance.player.transform.position;
    }

    void InitMoveToPlayer()
    {
        int randomX = Random.Range(0, 2) == 0 ? 1 : -1;
        int randomY = Random.Range(0, 2) == 0 ? 1 : -1;
        Vector3 playerVector = new Vector3(GameManager.instance.player.transform.position.x, GameManager.instance.player.transform.position.y);
        transform.position = playerVector + new Vector3(10.0f * randomX, 10.0f * randomY);
        originalPosition = playerVector + new Vector3(10.0f * randomX, 10.0f * randomY);
        targetVector = transform.CalcTarget(playerVector);
    }

    void MoveToPlayer()
    {
        if (patternType != PatternType.MoveToPlayer) return;
        if (targetVector != null && targetVector != Vector3.zero)
        {
            originalPosition = transform.MoveTargetDirectionLinear(originalPosition, targetVector, 5.0f);
        }

    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}

[System.Serializable]
public class PatternData
{
    public float time;
    public float duration;
    public SpawnData spawnData;
}