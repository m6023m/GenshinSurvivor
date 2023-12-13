using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        Charge,
        Suicide_Bomb
    }
    public EnemyAttackData attackData;

    bool isInit = false;
    EnemyPatternArea patternArea;
    public EnemyDamage patternDamage;
    Enemy enemy;




    public void Init(EnemyAttackData data)
    {
        isInit = false;
        attackData = data;
        transform.localPosition = Vector3.zero;
        patternArea = GetComponentInChildren<EnemyPatternArea>(true);
        patternDamage = GetComponentInChildren<EnemyDamage>(true);
        patternDamage.gameObject.SetActive(false);
        enemy = GetComponentInParent<Enemy>();
        DeactivateEnemyDamage();
        DeactivateEnemyPatternArea();
        InitAttack();
    }

    void InitAttack()
    {
        if (isInit) return;
        isInit = true;

        // 공통적으로 필요한 설정
        transform.localScale = new Vector2(1.0f * attackData.patternSize, 1.0f * attackData.patternSize);

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
                transform.position = GameManager.instance.player.transform.position;
                break;
            case PatternType.Warp:
                transform.position = attackData.targetDirection;
                break;
            case PatternType.Howling:
                // 특별한 설정이 필요 없는 경우
                break;
            case PatternType.Wave:
                PatternWave();
                break;
            case PatternType.Charge:
                SetChargePattern();
                break;
            case PatternType.Suicide_Bomb:
                transform.localPosition = Vector2.zero;
                break;
        }
    }

    void SetChargePattern()
    {
        Transform nearestTarget = GameManager.instance.player.scanner.nearestTarget;
        Vector3 targetPos = GameManager.instance.player.transform.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;
        attackData.targetDirection = dir;
        transform.ScaleFront(enemy.transform, new Vector3(1.0f, attackData.patternSize));
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
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
    public void ActivateEnemyDamage()
    {
        if (patternDamage != null)
        {
            patternDamage.gameObject.SetActive(true);
            patternDamage.Init(attackData);
        }
    }

    public void DeactivateEnemyDamage()
    {
        if (patternDamage != null)
        {
            patternDamage.gameObject.SetActive(false);
        }
    }
    public void ActivateEnemyPatternArea()
    {
        if (patternArea != null)
        { 
            patternArea.gameObject.SetActive(true);
            patternArea.Init(attackData);
        }
    }


    public void DeactivateEnemyPatternArea()
    {
        if (patternArea != null)
        {
            patternArea.gameObject.SetActive(false);
        }
    }

    private void DeactivateObjectsAfterAnimation()
    {
        DeactivateEnemyDamage();
        DeactivateEnemyPatternArea();
    }

    void PatternWave()
    {
        float size = 1.0f * attackData.patternSize;
        transform.localScale = new Vector3(size, size);
        transform.rotation
         = Quaternion.FromToRotation(Vector3.up, attackData.targetDirection);
    }
}

[System.Serializable]
public class EnemyAttackData
{
    public EnemyAttack.PatternType patternType;
    public AnimationClip patternAnimationClip;
    public AnimationClip damageAnimationClip;
    public Vector3 targetDirection;
    public float patternSize = 1.0f;
    public float damage;
    public float speed;
    public float patternDelay;
    public float duration;
    public bool isDamage;
    public UnityAction startDamageListener;
    public UnityAction endDamageListener;
    public UnityAction startPatternListener;
    public UnityAction endPatternListener;
    public EnemyAttackData(EnemyAttackData data)
    {
        float gameLevelCorrection = GameManager.instance.statCalculator.GameLevelCorrection;
        UserData userData = GameDataManager.instance.saveData.userData;
        patternType = data.patternType;
        patternAnimationClip = data.patternAnimationClip;
        damageAnimationClip = data.damageAnimationClip;
        targetDirection = data.targetDirection;
        patternSize = data.patternSize;
        damage = data.damage * gameLevelCorrection * userData.stageATK;
        speed = data.speed;
        patternDelay = data.patternDelay;
        duration = data.duration;
        isDamage = data.isDamage;
        startDamageListener = data.startDamageListener;
        endDamageListener = data.endDamageListener;
        startPatternListener = data.startPatternListener;
        endPatternListener = data.endPatternListener;
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