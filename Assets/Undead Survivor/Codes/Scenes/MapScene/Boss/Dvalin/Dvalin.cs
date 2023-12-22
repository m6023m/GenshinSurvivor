using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Dvalin : Boss
{
    enum PartsName
    {
        Body,
        Head,
        ArmLeft,
        ArmRight,
        WingBack,
        WingLeft,
        WingRight,
        Tail
    }
    enum Pattern
    {
        Charge,
        Breath,
        Area,
        Meteor
    }
    enum AnimationType
    {
        BreathDelay,
        CrackDealy,
        MeteorArea
    }
    Parts[] parts;
    float patternTime = 0;
    float nextPatternTime = 30;
    int currentPattern;
    public AnimatorOverrideController[] overrideControllers;
    void Awake()
    {
        parts = GetComponentsInChildren<Parts>();
        patternTime = 0;
    }

    void PlayPattern()
    {
        isPattern = true;
        switch (currentPattern)
        {
            case (int)Pattern.Charge:
                PatternCharge();
                break;
            case (int)Pattern.Breath:
                PatternBreath();
                break;
            case (int)Pattern.Area:
                PatternCrack();
                break;
            case (int)Pattern.Meteor:
                PatternMeteor();
                break;
        }
        currentPattern++;
        if (currentPattern >= 4)
        {
            currentPattern = 0;
        }
        patternTime = 0;
    }
    protected override void Update()
    {

        patternTime += (1 * Time.deltaTime);
        if (isPattern) return;

        if (patternTime >= nextPatternTime)
        {
            PlayPattern();
            return;
        }

        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance < 4f)
        {
            PatternMeele();
        }
        else if (distance > 20f)//거리가 너무 멀어지면 플레이어 쪽으로 이동
        {
            isPattern = true;
            ResetPositionDoTween().OnComplete(() =>
            {
                LookPlayer();
                isPattern = false;
            });
        }
        else
        {
            PatternRange();
        }
    }
    void PatternCharge()
    {
        isPattern = true;
        StartCoroutine(MoveInCircle(10.0f, 450, () =>
        {
            MoveToPlayer().OnComplete(() =>
            {
                PatternAll(false);
                PatternEnd();
            });
        }));
    }


    private Tween MoveToPlayer()
    {
        float distance = 100.0f;
        float duration = 3f;

        LookPlayer();

        PatternDelay(1.0f).OnComplete(() =>
        {
            Vector3 targetPosition = transform.up * distance;
            transform.DOMove(targetPosition, duration).SetEase(Ease.InOutSine);
        });

        return PatternDelay(4.0f);
    }

    private IEnumerator MoveInCircle(float distance, float angle, UnityAction action)
    {
        float currentAngle = 0f;
        float playerX = playerTransform.position.x;
        float playerY = playerTransform.position.y;
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

    IEnumerator RotatePlayerInDelay(float rotateSpeed, float duration, UnityAction action)
    {
        float elapsedTime = 0;

        float angle = transform.rotation.eulerAngles.z;

        Vector2 direction = playerTransform.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float targetAngleDirection = Mathf.Sign(angle - targetAngle);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            angle += targetAngleDirection * rotateSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            yield return null;
        }

        action.Invoke();
    }


    void PatternBreath()
    {
        Parts head = parts[(int)PartsName.Head];
        float patternDuration = 5.0f;
        float patternDelay = 3.0f;
        ResetPositionDoTween().OnComplete(() =>
        {
            LookPlayer();
            head.patternAnimationController = overrideControllers[(int)AnimationType.BreathDelay];
            head.patternAnimation.gameObject.transform.localScale = new Vector2(1.0f, 1.0f);

            EnemyAttackData attackData = head.enemyAttack.attackData;
            attackData.endPatternListener = () =>
            {
                StartCoroutine(RotatePlayerInDelay(10.0f, patternDuration, () =>
                {
                    head.patternAnimation.gameObject.SetActive(false);
                    PatternEnd();
                }));
            };

            attackData.damage = damage;
            attackData.patternDelay = patternDelay;
            attackData.duration = patternDuration;
            attackData.patternType = EnemyAttack.PatternType.Breath;
            attackData.targetDirection = Vector2.up;
            attackData.speed = 1.0f;
            attackData.isDamage = true;
            head.enemyAttack.Init(attackData);
            head.enemyAttack.AnimationStart();
        });
    }

    void PatternCrack()
    {
        Parts body = parts[(int)PartsName.Body];
        ResetPositionDoTween().OnComplete(() =>
        {
            LookPlayer();
            body.patternAnimationController = overrideControllers[(int)AnimationType.CrackDealy];
            body.patternAnimation.gameObject.transform.localScale = new Vector2(15.0f, 15.0f);
            PatternDelay(3.0f).OnComplete(
                () =>
                {
                    DvalinCrackArea crackArea = GameManager.instance.poolManager.GetObject<DvalinCrackArea>();
                    crackArea.transform.position = playerTransform.position;
                    body.patternAnimation.gameObject.SetActive(false);
                    PatternEnd();
                }
            );
        });
    }

    Tween PatternDelay(float delay)
    {
        return DOVirtual.DelayedCall(delay, () => { });
    }
    void PatternMeteor()
    {
        float patternDelay = 3.0f;
        int patternCount = 5;
        ResetPositionDoTween().OnComplete(() =>
        {
            LookPlayer();
            transform.position = playerTransform.position + new Vector3(0, 20.0f);

            CallMeteor(patternCount);

            PatternDelay(patternDelay * patternCount).OnComplete(() =>
            {
                PatternEnd();
            });
        });

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
        float patternDelay = 2.0f;
        float patternCoolTime = 3.0f;


        EnemyAttackData attackData = tail.enemyAttack.attackData;

        attackData.damage = damage;
        attackData.patternDelay = patternDelay;
        attackData.duration = 0f;
        attackData.patternType = EnemyAttack.PatternType.Meteor;
        attackData.patternSize = 2.0f;
        attackData.speed = 1.0f;
        attackData.isDamage = true;
        tail.enemyAttack.Init(attackData);
        tail.enemyAttack.AnimationStart();

        return PatternDelay(patternCoolTime);
    }
    void PatternMeele()
    {
        isPattern = true;
        int randomNum = Random.Range(0, 2);
        Tweener tweener;
        tweener = randomNum == 0 ? RightPunch() : LeftPunch();

        tweener.OnComplete(() =>
        {
            PatternDelay(2.0f).OnComplete(() =>
            {
                isPattern = false;
            });
        });
    }
    void PatternRange()
    {
        isPattern = true;
        LookPlayer();
        RightRange().OnComplete(() => LeftRange().OnComplete(() =>
        {
            PatternDelay(4.0f).OnComplete(() =>
            {
                isPattern = false;
            });
        }));
    }
    Tween RightRange()
    {
        float patternDelay = 1.0f;
        Parts rightWing = parts[(int)PartsName.WingRight];
        EnemyAttackData attackData = rightWing.enemyAttack.attackData;

        attackData.damage = damage / 2;
        attackData.patternDelay = patternDelay;
        attackData.duration = 0f;
        attackData.patternType = EnemyAttack.PatternType.Range;
        attackData.targetDirection = playerTransform.position - rightWing.transform.position;
        attackData.speed = 10.0f;
        attackData.animationSpeed = 4.0f;
        attackData.isDamage = true;
        rightWing.enemyAttack.Init(attackData);
        rightWing.enemyAttack.AnimationStart();

        return PatternDelay(1.0f);
    }
    Tween LeftRange()
    {
        float patternDelay = 1.0f;
        Parts leftWing = parts[(int)PartsName.WingLeft];
        EnemyAttackData attackData = leftWing.enemyAttack.attackData;

        attackData.damage = damage / 2;
        attackData.patternDelay = patternDelay;
        attackData.duration = 0f;
        attackData.patternType = EnemyAttack.PatternType.Range;
        attackData.targetDirection = playerTransform.position - leftWing.transform.position;
        attackData.speed = 10.0f;
        attackData.animationSpeed = 4.0f;
        attackData.isDamage = true;
        leftWing.enemyAttack.Init(attackData);
        leftWing.enemyAttack.AnimationStart();

        return PatternDelay(4.0f);
    }

    Tweener RightPunch()
    {
        Parts armRight = parts[(int)PartsName.ArmRight];
        LookPlayer();
        armRight.StartPattern();
        armRight.PatternDelayColorChange(1.0f);
        Tweener tweener = armRight.transform.DOMove(playerTransform.position, 0.2f).SetDelay(1.0f).SetEase(Ease.InOutSine);
        return tweener.OnComplete(() =>
        {
            armRight.EndPattern();
            armRight.ResetPosition();
        });
    }
    Tweener LeftPunch()
    {
        Parts armLeft = parts[(int)PartsName.ArmLeft];
        LookPlayer();
        armLeft.StartPattern();
        armLeft.PatternDelayColorChange(1.0f);
        Tweener tweener = armLeft.transform.DOMove(playerTransform.position, 0.2f).SetDelay(1.0f).SetEase(Ease.InOutSine);
        return tweener.OnComplete(() =>
        {
            armLeft.EndPattern();
            armLeft.ResetPosition();
        });
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
        parts[(int)PartsName.WingBack].FadeOut();
        parts[(int)PartsName.WingLeft].FadeOut();
        parts[(int)PartsName.WingRight].FadeOut();
        parts[(int)PartsName.Tail].FadeOutCompliteListener(() => base.Dead());
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
        ResetPositionDoTween().OnComplete(() =>
        {
            LookPlayer();
            isPattern = false;
        });
    }
}
