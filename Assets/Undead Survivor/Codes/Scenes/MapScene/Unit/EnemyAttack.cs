using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : PoolingObject
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

    EnemyPatternArea _patternArea;
    EnemyDamage _patternDamage;
    EnemyPatternArea patternArea
    {
        get
        {
            if (_patternArea == null) _patternArea = GetComponentInChildren<EnemyPatternArea>(true);
            return _patternArea;
        }
    }
    EnemyDamage patternDamage
    {
        get
        {
            if (_patternDamage == null) _patternDamage = GetComponentInChildren<EnemyDamage>(true);
            return _patternDamage;
        }
    }
    Enemy _enemy;
    public Enemy enemy
    {
        get
        {
            if (_enemy == null) _enemy = GetComponentInParent<Enemy>();
            return _enemy;
        }
    }




    public void Init(EnemyAttackData data)
    {
        attackData = new EnemyAttackData(data);
        patternArea.onAnimationStart = () =>
        {
            PatternStart();
        };
        patternArea.onAnimationEnd = () =>
        {
            PatternEnd();
            patternDamage.AnimationStart();
        };
        patternDamage.onAnimationStart = () =>
        {
            DamageStart();
        };
        patternDamage.onAnimationEnd = () =>
        {
            DamageEnd();
        };
        patternArea.Init(this);
        patternDamage.Init(this);
    }
    public void AnimationStart()
    {
        patternArea.AnimationStart();
    }

    void PatternStart()
    {
        if (attackData.startPatternListener == null) return;
        attackData.startPatternListener.Invoke();
    }
    void PatternEnd()
    {
        if (attackData.endPatternListener == null) return;
        attackData.endPatternListener.Invoke();
    }
    void DamageStart()
    {
        if (attackData.startDamageListener == null) return;
        attackData.startDamageListener.Invoke();

    }
    void DamageEnd()
    {
        if (attackData.endDamageListener == null) return;
        attackData.endDamageListener.Invoke();
    }

    public void ResetAnimation()
    {
        patternArea.ResetAnimation();
        patternDamage.ResetAnimation();
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
    public float animationSpeed = 0;
    public UnityAction startPatternListener;
    public UnityAction endPatternListener;
    public UnityAction startDamageListener;
    public UnityAction endDamageListener;
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
        animationSpeed = data.animationSpeed;
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