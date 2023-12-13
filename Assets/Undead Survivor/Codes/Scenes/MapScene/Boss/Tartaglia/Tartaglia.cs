using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Tartaglia : Boss
{
    enum PartsName
    {
        Body
    }
    enum Pattern
    {
        Meteor,
        Whale
    }
    enum AnimationType
    {
    }
    Parts[] parts;
    float patternTime = 0;
    float nextPatternTime = 30;
    int currentPattern;
    public EnemyAttack enemyAttack;
    public AnimatorOverrideController[] overrideControllers;
    public RuntimeAnimatorController[] phaseForm;
    public AnimationClip[] meleeClips;
    int phase = 0;
    public GameObject bossMap = null;
    string sineGlowId = "_SineGlowFade";
    Parts body;
    void Awake()
    {
        parts = GetComponentsInChildren<Parts>();
        patternTime = 0;
    }

    void PlayPattern()
    {
        switch (currentPattern)
        {
            case (int)Pattern.Meteor:
                isPattern = true;
                PatternMeteor();
                break;
            case (int)Pattern.Whale:
                isPattern = true;
                PatternWhale().OnComplete(() =>
                {
                    isPattern = false;
                });
                break;
        }
        currentPattern++;
        if (currentPattern > 1)
        {
            currentPattern = 0;
        }
        patternTime = 0;
    }

    public override void InitBoss(SpawnData spawnData)
    {
        base.InitBoss(spawnData);
        phase = 0;
        body = parts[(int)PartsName.Body];
        body.partsAnimation = phaseForm[phase];
        bossName = BossName.Tartaglia0;
        Phase(0);
    }
    void Phase2()
    {
        if (phase == 1) return;
        if (phase == 2) return;
        if ((health / maxHealth) > 0.75f) return;
        phase = 1;
        body.partsAnimation = phaseForm[phase];
        bossName = BossName.Tartaglia1;
        HydroRes = 0;
        ElectroRes = 0.5f;
        body.HydroRes = 0;
        body.ElectroRes = 0.5f;
    }
    void Phase3()
    {
        if (phase == 2) return;
        if ((health / maxHealth) > 0.5f) return;
        phase = 2;
        body.partsAnimation = phaseForm[phase];
        bossName = BossName.Tartaglia2;
        Phase(1.0f);
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
            if (Random.Range(0, 2) == 0 && phase < 2)
            {
                PatternBackStep();
            }
            else
            {
                PatternMelee();
            }
        }
        else
        {
            if (Random.Range(0, 2) == 0 && phase > 0)
            {
                PatternCharge();
            }
            else
            {
                PatternRange();
            }
        }
    }

    public override void ResetPosition()
    {
        transform.position = playerTransform.position + new Vector3(0, 10.0f);
    }



    void PatternMelee()
    {
        isPattern = true;

        CallMelee(2);
    }
    void CallMelee(int count)
    {
        if (count <= 0)
        {
            PatternDelay(4.0f).OnComplete(() =>
                        {
                            isPattern = false;
                        });
            return;
        }

        Melee(count).OnComplete(() =>
        {
            CallMelee(count - 1);
        });
    }

    Tweener Melee(int attackNum)
    {
        float patternDelay = 1f;
        int attackNumber = attackNum;
        float damagePer = phase * damage * 0.5f;
        if (phase > 0)
        {
            attackNumber += 2;
        }

        EnemyAttackData attackData = new EnemyAttackData();

        attackData.damage = damagePer;
        attackData.patternDelay = patternDelay;
        attackData.duration = 0f;
        attackData.patternType = EnemyAttack.PatternType.Melee;
        attackData.patternSize = 5.0f;
        attackData.speed = 1.0f;
        attackData.isDamage = true;
        attackData.targetDirection = new Vector3(playerTransform.position.x, playerTransform.position.y);
        body.enemyAttack.Init(attackData);


        return PatternDelay(1.5f);
    }

    void PatternBackStep()
    {
        isPattern = true;
        Vector3 targetVector = playerTransform.position - transform.position;
        transform.DOMove(-targetVector.normalized * 8.0f, 0.5f).OnComplete(() =>
        {
            isPattern = false;
        });
    }

    void PatternCharge()
    {
        isPattern = true;
        Vector3 targetVector = playerTransform.position - transform.position;
        float patternDelay = 1.0f;
        // EnemyAttackData attackData = new EnemyAttackData();
        // Transform nearestTarget = GameManager.instance.player.scanner.nearestTarget;
        // Vector3 targetPos = GameManager.instance.player.transform.position;
        // Vector3 dir = targetPos - transform.position;
        // float damagePer = phase * damage * 0.5f;

        // attackData.damage = damagePer;
        // attackData.patternDelay = patternDelay;
        // attackData.duration = 0f;
        // attackData.patternType = EnemyAttack.PatternType.Charge;
        // Quaternion rotation = Quaternion.Euler(0f, 0f, 0);
        // Vector3 targetDirection = rotation * dir;
        // attackData.targetDirection = targetDirection;
        // attackData.patternSize = 8.0f;
        // attackData.speed = 15.0f;
        // attackData.isDamage = false;
        // body.enemyAttack.Init(attackData);
        PatternDelay(patternDelay).OnComplete(() =>
        {
            transform.DOMove(transform.position + targetVector.normalized * 8.0f, 0.5f).OnComplete(() =>
            {
                isPattern = false;
            });
        });

    }

    void PatternRange()
    {
        isPattern = true;
        body.StartPattern();
        CallRange(5);
    }
    void CallRange(int count)
    {
        if (count <= 0)
        {
            PatternDelay(4.0f).OnComplete(() =>
                        {
                            isPattern = false;
                            body.EndPattern();
                        });
            return;
        }

        Range().OnComplete(() =>
        {
            CallRange(count - 1);
        });
    }

    Tweener Range()
    {
        float patternDelay = 0.5f;
        EnemyAttackData attackData = new EnemyAttackData();
        float damagePer = phase * damage * 0.5f;

        attackData.damage = damagePer;
        attackData.patternDelay = patternDelay;
        attackData.duration = 0f;
        attackData.patternType = EnemyAttack.PatternType.Range;
        attackData.speed = 15.0f;
        attackData.isDamage = true;
        body.enemyAttack.Init(attackData);

        return PatternDelay(1.0f);
    }


    Tweener PatternDelay(float delay)
    {
        return body.spriteRenderer.DOColor(new Color(1, 1, 1, 1), 0f).SetDelay(delay);
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

    Tweener Meteor()
    {
        float patternDelay = 1.0f;
        float patternCoolTime = 2.0f;


        EnemyAttackData attackData = new EnemyAttackData();

        attackData.damage = damage;
        attackData.patternDelay = patternDelay;
        attackData.duration = 0f;
        attackData.patternType = EnemyAttack.PatternType.Meteor;
        attackData.patternSize = phase + 2f;
        attackData.speed = 1.0f;
        attackData.isDamage = true;
        body.enemyAttack.Init(attackData);

        return PatternDelay(patternCoolTime);
    }


    Tweener PatternWhale()
    {

        float patternDelay = 3f;
        float damagePer = phase * damage * 2f;

        EnemyAttackData attackData = new EnemyAttackData();

        attackData.damage = damagePer;
        attackData.patternDelay = patternDelay;
        attackData.duration = 0f;
        attackData.patternType = EnemyAttack.PatternType.Melee;
        attackData.patternSize = 25.0f;
        attackData.speed = 1.0f;
        attackData.isDamage = true;
        attackData.targetDirection = new Vector3(playerTransform.position.x, playerTransform.position.y);
        enemyAttack.Init(attackData);

        return PatternDelay(4.5f);
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


    void Phase(float fade)
    {
        if (!isLive) return;
        foreach (Parts part in parts)
        {
            part.spriteRenderer.material.SetFloat(sineGlowId, fade);
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
