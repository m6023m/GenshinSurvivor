using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public enum PatternType
    {
        None = -1,
        Melee,
        Range,
        Breath,
        Meteor,
        Warp,
        Howling,
        Wave,
        Charge
    }
    public EnemyAttackData attackData;
    RuntimeAnimatorController animatorController;

    bool isInit = false;
    bool isPattenStart = false;
    EnemyPatternArea patternArea;
    public EnemyDamage patternDamage;
    Enemy enemy;




    public void Init(EnemyAttackData data)
    {
        isPattenStart = false;
        isInit = false;
        attackData = data;
        transform.localPosition = Vector3.zero;
        patternArea = GetComponentInChildren<EnemyPatternArea>(true);
        patternDamage = GetComponentInChildren<EnemyDamage>(true);
        patternDamage.gameObject.SetActive(false);
        enemy = GetComponentInParent<Enemy>();
        InitAttack();
    }

    void InitAttack()
    {
        if (isInit) return;
        isInit = true;

        switch (attackData.patternType)
        {
            case PatternType.Melee:
                transform.localPosition = Vector3.zero;
                PatternNormal();
                break;
            case PatternType.Range:
                PatternNormal();
                break;
            case PatternType.Breath:
                transform.ScaleFront(enemy.transform, new Vector3(1.0f * attackData.patternSize, 25.0f));
                break;
            case PatternType.Meteor:
                transform.localScale = new Vector2(1.0f * attackData.patternSize, 1.0f * attackData.patternSize);
                transform.position = GameManager.instance.player.transform.position;
                break;
            case PatternType.Warp:
                transform.localScale = new Vector2(1.0f * attackData.patternSize, 1.0f * attackData.patternSize);
                transform.position = attackData.targetDirection;
                break;
            case PatternType.Howling:
                transform.localScale = new Vector2(1.0f * attackData.patternSize, 1.0f * attackData.patternSize);
                break;
            case PatternType.Wave:
                PatternWave();
                break;
            case PatternType.Charge:
                Transform nearestTarget = GameManager.instance.player.scanner.nearestTarget;
                Vector3 targetPos = GameManager.instance.player.transform.position;
                Vector3 dir = targetPos - transform.position;
                float size = 1.0f * attackData.patternSize;
                dir = dir.normalized;
                attackData.targetDirection = dir;
                transform.ScaleFront(enemy.transform, new Vector3(1.0f, attackData.patternSize));
                transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
                break;
        }
        PatternAreaCheck();
    }

    void PatternAreaCheck()
    {
        patternArea.Init(attackData).OnAnimationEnd(() =>
        {
            patternDamage.gameObject.SetActive(true);
            patternDamage.Init(attackData);
        });
        patternArea.SetPatternAlpha(1.0f);
    }
    void PatternNormal()
    {
        Transform nearestTarget = GameManager.instance.player.scanner.nearestTarget;
        Vector3 targetPos = GameManager.instance.player.transform.position;
        Vector3 dir = targetPos - transform.position;
        float size = 1.0f * attackData.patternSize;
        dir = dir.normalized;
        attackData.targetDirection = dir;
        transform.localScale = new Vector3(size, size);
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }

    void PatternWave()
    {
        float size = 1.0f * attackData.patternSize;
        transform.localScale = new Vector3(size, size);
        transform.rotation = Quaternion.FromToRotation(Vector3.up, attackData.targetDirection);
    }
}

[System.Serializable]
public class EnemyAttackData
{
    public EnemyAttack.PatternType patternType;
    public Vector3 targetDirection;
    public float patternSize = 1.0f;
    public float damage;
    public float speed;
    public float patternDelay;
    public float duration;
    public bool isDamage;
    public EnemyAttackData(EnemyAttackData data)
    {
        patternType = data.patternType;
        targetDirection = data.targetDirection;
        patternSize = data.patternSize;
        damage = data.damage;
        speed = data.speed;
        patternDelay = data.patternDelay;
        duration = data.duration;
        isDamage = data.isDamage;
    }

    public EnemyAttackData()
    {
        patternType = EnemyAttack.PatternType.None;
        targetDirection = new Vector3(0, 0, 0);
        patternSize = 1.0f;
        damage = 0f;
        speed = 1f;
        patternDelay = 1f;
        duration = 0f;
        isDamage = false;
    }
}