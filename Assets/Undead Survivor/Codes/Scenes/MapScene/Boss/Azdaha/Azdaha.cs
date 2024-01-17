using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Azdaha : Boss
{
    enum PartsName
    {
        Body
    }
    enum AnimationType
    {
        Pattern_Square,
        Pattern_Circle,
        Meteor_Delay,
        Meteor,
        Meteor_Pyro,
        Meteor_Cyro,
        Stomp,
        Pizza_Delay,
        Pizza
    }
    Parts[] parts;
    float patternTime = 0;
    float nextPatternTime = 15;
    int currentPattern;
    public EnemyAttack enemyAttack;
    public AnimatorOverrideController[] overrideControllers;
    public RuntimeAnimatorController[] phaseForm;
    public AnimationClip[] animationClips;
    int phase = 0;
    bool isPhaseChange = false;
    public GameObject bossMap = null;
    Parts body;
    void Awake()
    {
        parts = GetComponentsInChildren<Parts>();
        patternTime = 0;
    }

    void PlayPattern()
    {
        switch (phase)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    public override void InitBoss(SpawnData spawnData)
    {
        base.InitBoss(spawnData);
        phase = 0;
        isPattern = false;
        currentPattern = 0;
    }
    void Phase2()
    {
        if (phase == 1) return;
        if (phase == 2) return;
        if ((health / maxHealth) > 0.66f) return;
        phase = 1;
        HydroRes = 0;
        ElectroRes = 0.5f;
        body.HydroRes = 0;
        body.ElectroRes = 0.5f;
    }
    void Phase3()
    {
        if (phase == 2) return;
        if ((health / maxHealth) > 0.33f) return;
        phase = 2;
        HydroRes = 0.7f;
        ElectroRes = 0.7f;
        body.HydroRes = 0.7f;
        body.ElectroRes = 0.7f;
    }

    protected override void Update()
    {
        Phase2();
        Phase3();

        patternTime += (1 * Time.deltaTime);

        if (isPattern) return;

        if (patternTime >= nextPatternTime)
        {
            PlayPattern();
        }

        if (isPattern) return;

        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance < 4f)
        {
            PatternStomp();
        }
    }

    public override void ResetPosition()
    {
        transform.position = playerTransform.position + new Vector3(0, 10.0f);
    }



    void PatternStomp()
    {
        isPattern = true;
        AnimationType melee = AnimationType.Stomp;
        Melee(melee, 0).OnComplete(() =>
        {
            PatternDelay(4.0f).OnComplete(() =>
            {
                isPattern = false;
            });
        });
    }

    Tween Melee(AnimationType animationType, int index)
    {
        float patternDelay = 1f;
        float damagePer = phase * damage * 0.5f;

        EnemyAttackData attackData = body.enemyAttacks[index].attackData;
        attackData.patternAnimationClip = animationClips[(int)AnimationType.Pattern_Circle];
        attackData.damageAnimationClip = animationClips[(int)animationType];

        attackData.damage = damagePer;
        attackData.patternDelay = patternDelay;
        attackData.duration = 0.5f;
        attackData.patternType = EnemyAttack.PatternType.Melee;
        attackData.patternSize = 4.0f;
        attackData.speed = 1.0f;
        attackData.isDamage = true;
        attackData.targetDirection = new Vector3(playerTransform.position.x, playerTransform.position.y);
        body.enemyAttacks[index].Init(attackData);
        body.enemyAttacks[index].AnimationStart();


        return PatternDelay(1.5f);
    }



    Tween PatternDelay(float delay)
    {
        return DOVirtual.DelayedCall(delay, () => { });
    }


    void PatternMeteor()
    {
        int patternCount = 5;

        CallMeteor(patternCount);
    }

    void CallMeteor(int count)
    {
        if (count <= 0)
        {
            isPattern = false;
            return;
        }

        Meteor().OnComplete(() =>
        {
            CallMeteor(count - 1);
        });
    }

    Tween Meteor()
    {
        float patternDelay = 1.0f;
        float patternCoolTime = 2.0f;
        Transform playerTransform = GameManager.instance.player.transform;


        EnemyAttackData attackData = new EnemyAttackData();
        attackData.patternAnimationClip = animationClips[(int)AnimationType.Meteor_Delay];
        attackData.damageAnimationClip = animationClips[(int)AnimationType.Meteor];
        attackData.damage = damage;
        attackData.patternDelay = patternDelay;
        attackData.duration = 0f;
        attackData.patternType = EnemyAttack.PatternType.Meteor;
        attackData.patternSize = phase + 2f;
        attackData.speed = 1.0f;
        attackData.isDamage = true;
        attackData.targetDirection = new Vector3(playerTransform.position.x, playerTransform.position.y);
        body.enemyAttack.Init(attackData);
        body.enemyAttack.AnimationStart();

        return PatternDelay(patternCoolTime);
    }

    protected override void Dead()
    {
        FadeOut();
    }

    void FadeOut()
    {
        body.FadeOutCompliteListener(() => base.Dead());
    }


    void PatternAll(bool isStart)
    {
        foreach (Parts part in parts)
        {
            if (isStart)
            {
                part.StartPattern();
            }
            else
            {
                part.EndPattern();
            }
        }
    }

    void PatternEnd()
    {
        isPattern = false;
        ResetPosition();
    }

    public override void Disable()
    {
        if (bossMap != null)
        {
            bossMap.gameObject.SetActive(false);
        }
        base.Disable();
    }

}
