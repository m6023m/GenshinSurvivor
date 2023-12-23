using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Andrius : Boss
{
    enum PartsName
    {
        Body,
        Head,
        ArmLeft,
        ArmRight,
        WingLeft,
        WingRight,
        Tail,
        LegLeft,
        LegRight
    }
    enum Pattern
    {
        Meteor,
        Howling
    }
    enum AnimationType
    {
        ChargeEffect,
        PatternArea,
        Howling
    }
    Parts[] parts;
    float patternTime = 0;
    float nextPatternTime = 30;
    int currentPattern;
    public EnemyAttack enemyAttack;
    bool isPhase2 = false;
    public GameObject bossMap = null;
    string sineGlowId = "_SineGlowFade";
    void Awake()
    {
        parts = GetComponentsInChildren<Parts>();
        patternTime = 0;
    }

    void PlayPattern()
    {
        if (!isPhase2) return;
        switch (currentPattern)
        {
            case (int)Pattern.Meteor:
                isPattern = true;
                PatternMeteor();
                break;
            case (int)Pattern.Howling:
                isPattern = true;
                PatternHowling();
                break;
        }
        currentPattern++;
        if (currentPattern >= 2)
        {
            currentPattern = 0;
        }
        patternTime = 0;
    }

    public override void InitBoss(SpawnData spawnData)
    {
        isPhase2 = false;
        base.InitBoss(spawnData);
    }
    protected override void Update()
    {

        patternTime += (1 * Time.deltaTime);
        if (isPattern) return;

        if (!isPhase2 && !isPattern)
        {
            PatternCharge();
        }

        if (isPattern) return;

        if (patternTime >= nextPatternTime)
        {
            PlayPattern();
        }

        if (isPattern) return;

        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance < 4f)
        {
            PatternMelee();
        }
        else
        {
            if (isPhase2)
            {
                PatternWave();
            }
            else
            {
                PatternJump();
            }
        }
    }
    void PatternCharge()
    {
        if ((health / maxHealth) > 0.6f) return;
        isPattern = true;
        isPhase2 = true;
        StartCoroutine(MoveInCircle(15.0f, 360, () =>
        {
            MoveToPlayer().OnComplete(() =>
            {
                StartCoroutine(MoveInCircle(15.0f, 360, () =>
                {
                    MoveToPlayer().OnComplete(() =>
                    {
                        PatternJump(() =>
                        {
                            Phase(1.0f);
                            PatternAll(false);
                            PatternEnd();
                        });
                    });
                }));
            });
        }));
    }

    public override void ResetPosition()
    {
        if (bossMap != null)
        {
            transform.position = bossMap.transform.position + new Vector3(0, 4.0f);
            LookPlayer();
        }
        else
        {
            base.ResetPosition();
        }
    }


    private Tween MoveToPlayer()
    {
        float distance = 100.0f;
        float duration = 3f;

        LookPlayer();

        DOVirtual.DelayedCall(1.0f, () =>
        {
            Vector3 targetPosition = transform.up * distance;
            transform.DOMove(targetPosition, duration).SetEase(Ease.InOutSine);
        });

        return DOVirtual.DelayedCall(4.0f, () => { });
    }


    private IEnumerator MoveInCircle(float distance, float angle, UnityAction action)
    {
        float currentAngle = 0f;
        float playerX = playerTransform.position.x;
        float playerY = playerTransform.position.y;
        if (bossMap != null)
        {

            playerX = bossMap.transform.position.x;
            playerY = bossMap.transform.position.y;
        }
        PatternAll(true);
        while (true)
        {
            float radian = currentAngle * Mathf.Deg2Rad;

            float x = playerX + (Mathf.Cos(radian) * distance);
            float y = playerY + (Mathf.Sin(radian) * distance);

            transform.position = new Vector3(x, y, transform.position.z);

            currentAngle += Time.deltaTime * 360f / 3.0f;
            transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
            if (currentAngle >= angle)
            {
                action.Invoke();
                yield break;
            }

            yield return null;
        }
    }


    void PatternMeteor()
    {
        int patternCount = 5;

        CallMeteor(patternCount);

        isPattern = false;
    }

    void CallMeteor(int count)
    {
        if (count <= 0) return;

        Meteor().OnComplete(() =>
        {
            CallMeteor(count - 1);
        });
    }

    Tween Meteor()
    {
        Parts tail = parts[(int)PartsName.Tail];
        float patternDelay = 1.0f;
        float patternCoolTime = 2.0f;


        EnemyAttackData attackData = tail.enemyAttack.attackData;

        attackData.damage = damage;
        attackData.patternDelay = patternDelay;
        attackData.duration = 0f;
        attackData.patternType = EnemyAttack.PatternType.Meteor;
        attackData.speed = 1.0f;
        attackData.isDamage = true;
        tail.enemyAttack.Init(attackData);
        tail.enemyAttack.AnimationStart();

        return DOVirtual.DelayedCall(patternCoolTime, () => { });
    }
    void PatternMelee()
    {
        float patternDelay = 2.0f;
        float moveTime = 0.2f;
        isPattern = true;
        LookPlayer();
        Parts body = parts[(int)PartsName.Body];

        EnemyAttackData attackData = body.enemyAttack.attackData;

        attackData.damage = damage;
        attackData.patternDelay = patternDelay + moveTime;
        attackData.duration = 0.8f;
        attackData.patternType = EnemyAttack.PatternType.Melee;
        attackData.patternSize = 5.0f;
        attackData.isDamage = true;
        body.enemyAttack.Init(attackData);
        body.enemyAttack.AnimationStart();

        float currentAngle = transform.localEulerAngles.z;

        Tweener tweener = transform.DOLocalRotate(new Vector3(0, 0, currentAngle - 90f), patternDelay).SetEase(Ease.InQuad);
        tweener.OnComplete(() =>
        {
            transform.DOLocalRotate(new Vector3(0, 0, currentAngle + 359f), moveTime, RotateMode.FastBeyond360).SetEase(Ease.InQuad).OnComplete(() =>
            {
                DOVirtual.DelayedCall(4.0f, () =>
                            {
                                isPattern = false;
                            });
            });
        });
    }


    void PatternHowling()
    {
        float patternDelay = 3.0f;
        isPattern = true;
        LookPlayer();
        Parts head = parts[(int)PartsName.Head];

        EnemyAttackData attackData = head.enemyAttack.attackData;

        attackData.damage = damage / 2.0f;
        attackData.patternDelay = patternDelay;
        attackData.duration = 10.0f;
        attackData.patternType = EnemyAttack.PatternType.Howling;
        attackData.patternSize = 10.0f;
        attackData.speed = 1.0f;
        attackData.isDamage = true;
        attackData.targetDirection = new Vector3(playerTransform.position.x, playerTransform.position.y);
        head.enemyAttack.Init(attackData);
        head.enemyAttack.AnimationStart();

        DOVirtual.DelayedCall(patternDelay + attackData.duration, () =>
        {
            isPattern = false;
        });
    }
    void PatternJump(UnityAction action = null)
    {
        if (enemyAttack == null) return;
        float patternDelay = 1.5f;
        float moveDuration = 0.5f;
        isPattern = true;
        LookPlayer();
        Parts armRight = parts[(int)PartsName.ArmRight];
        EnemyAttackData attackData = armRight.enemyAttack.attackData;

        attackData.damage = damage;
        attackData.patternDelay = moveDuration + patternDelay;
        attackData.duration = 0.5f;
        attackData.patternType = EnemyAttack.PatternType.Meteor;
        attackData.patternSize = 8.0f;
        attackData.isDamage = true;
        attackData.targetDirection = GameManager.instance.player.transform.position;
        attackData.endPatternListener = () =>
        {
            transform.DOMove(attackData.targetDirection, moveDuration).SetEase(Ease.InQuad).OnComplete(() =>
            {
                DOVirtual.DelayedCall(4.0f, () =>
                            {
                                if (action != null)
                                {
                                    action.Invoke();
                                }
                                else
                                {
                                    isPattern = false;
                                }
                            });
            });
        };
        enemyAttack.Init(attackData);
        enemyAttack.AnimationStart();

    }

    protected override void Dead()
    {
        FadeOut();
    }

    void FadeOut()
    {
        parts[(int)PartsName.Body].FadeOut();
        parts[(int)PartsName.Head].FadeOut();
        parts[(int)PartsName.ArmLeft].FadeOut();
        parts[(int)PartsName.ArmRight].FadeOut();
        parts[(int)PartsName.WingLeft].FadeOut();
        parts[(int)PartsName.WingRight].FadeOut();
        parts[(int)PartsName.Tail].FadeOut();
        parts[(int)PartsName.LegLeft].FadeOut();
        parts[(int)PartsName.LegRight].FadeOutCompliteListener(() => base.Dead());
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

    void PatternWave()
    {
        isPattern = true;
        LookPlayer();
        RightWave().OnComplete(() =>
        {
            DOVirtual.DelayedCall(4.0f, () =>
            {
                isPattern = false;
            });
        });
    }

    Tween RightWave()
    {
        float patternDelay = 1.0f;
        Parts rightWing = parts[(int)PartsName.WingRight];
        EnemyAttackData attackData = rightWing.enemyAttack.attackData;
        attackData.damage = damage;
        attackData.patternDelay = patternDelay;
        attackData.duration = 0f;
        attackData.patternType = EnemyAttack.PatternType.Wave;
        attackData.speed = 20.0f;
        attackData.isDamage = true;
        Transform nearestTarget = GameManager.instance.player.scanner.nearestTarget;
        Vector3 targetPos = GameManager.instance.player.transform.position;
        Vector3 dir = targetPos - transform.position;
        int count = 0;
        foreach (EnemyAttack enemyAttack in rightWing.enemyAttacks)
        {
            EnemyAttackData newAttackData = new EnemyAttackData(attackData);
            newAttackData.angle = -15 + (count * 15);
            enemyAttack.Init(newAttackData);
            enemyAttack.AnimationStart();
            count++;
        }

        return DOVirtual.DelayedCall(4.0f, () => { });
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
