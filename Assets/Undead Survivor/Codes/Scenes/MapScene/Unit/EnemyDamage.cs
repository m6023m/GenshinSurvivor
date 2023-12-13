using System.Collections;
using System.Collections.Generic;
using Unity.Entities.UniversalDelegates;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDamage : MonoBehaviour
{
    Animator _animator;
    Animator animator
    {
        get
        {
            if (_animator == null) _animator = GetComponent<Animator>();
            return _animator;
        }
    }
    RuntimeAnimatorController _animatorController;
    RuntimeAnimatorController animatorController
    {
        get
        {
            if (_animatorController == null) _animatorController = animator.runtimeAnimatorController;
            return _animatorController;
        }
    }
    public UnityAction onAnimationStart;
    public UnityAction onAnimationEnd;
    float damageTime = 0;
    float damageTimeMax = 0.5f;

    public bool isInit = false;
    Vector3 originalPosition = Vector3.zero;
    Vector3 targetDir;

    EnemyAttack parentEnemyAttack;
    Player player;
    Dictionary<int, Summon> summons = new Dictionary<int, Summon>();
    float animationTime = 0;
    float animationDuration = 9999;


    private void FixedUpdate()
    {
        if (!isInit) return;
        if (parentEnemyAttack.attackData.patternType != EnemyAttack.PatternType.Range &&
        parentEnemyAttack.attackData.patternType != EnemyAttack.PatternType.Wave) return;
        Vector2 nextVec = originalPosition + (targetDir.normalized * parentEnemyAttack.attackData.speed * Time.fixedDeltaTime);
        originalPosition = nextVec;
        transform.position = originalPosition;
    }

    protected virtual void Update()
    {
        OnStayDamage();
        if (isInit) animationTime += Time.deltaTime;
        if (animationTime > animationDuration && animationDuration != 0)
        {
            animationTime = 0;
            AnimationEnd();
        }
    }

    public void Init(EnemyAttack enemyAttack)
    {
        parentEnemyAttack = enemyAttack;
        if (parentEnemyAttack.attackData.damageAnimationClip == null) return;
        summons = new Dictionary<int, Summon>();
        player = null;
        transform.localPosition = Vector3.zero;
        originalPosition = transform.position;
        transform.localScale = new Vector3(1f, 1f, 1f);
        targetDir = parentEnemyAttack.attackData.targetDirection;
        if (parentEnemyAttack.attackData.patternType == EnemyAttack.PatternType.Wave)
        {
            targetDir = transform.up;
        }
        damageTime = 0;
        SetAnimation(parentEnemyAttack.attackData.damageAnimationClip);
        InitAttack();
    }

    void InitAttack()
    {
        if (isInit) return;
        switch (parentEnemyAttack.attackData.patternType)
        {
            case EnemyAttack.PatternType.Range:
                break;
            case EnemyAttack.PatternType.Breath:
                break;
            case EnemyAttack.PatternType.Meteor:
                transform.RotationFix(Vector3.up);
                break;
            case EnemyAttack.PatternType.Warp:
                return;
            case EnemyAttack.PatternType.Howling:
                transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
                return;
            case EnemyAttack.PatternType.Wave:
                break;
            case EnemyAttack.PatternType.Melee:
                break;
            case EnemyAttack.PatternType.Charge:
                break;
            case EnemyAttack.PatternType.Suicide_Bomb:
                break;

        }
        CalcAnimationEndDuration();
        AnimationStart();

        isInit = true;
    }


    void CalcAnimationEndDuration()
    {
        AnimationClip animationClip = parentEnemyAttack.attackData.damageAnimationClip;
        animationDuration = animationClip.length;
        if(parentEnemyAttack.attackData.duration != 0) {
            animator.speed = animationClip.length / parentEnemyAttack.attackData.duration;
        }
        if (parentEnemyAttack.attackData.patternType == EnemyAttack.PatternType.Range)
        {
            animationDuration = 3.0f; //원거리 공격이면 3초만 활성화 됨
        }
    }

    void SetAnimation(AnimationClip animationClip)
    {
        if (animationClip == null) return;
        AnimatorOverrideController aoc = new AnimatorOverrideController(animatorController);
        aoc["Skill_None"] = animationClip;
        animator.runtimeAnimatorController = aoc;
    }

    void OnStayDamage()
    {
        if (!isInit) return;
        damageTime += Time.deltaTime;
        if (damageTime > damageTimeMax)
        {
            if (player != null)
            {
                player.Hit(parentEnemyAttack.attackData.damage, 0.0f, parentEnemyAttack.enemy);
            }
            foreach (KeyValuePair<int, Summon> summon in summons)
            {
                summon.Value.Hit(parentEnemyAttack.attackData.damage, 0.0f, parentEnemyAttack.enemy);
            }
            damageTime = 0;
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnterDamage(collision);
    }
    void OnDisable()
    {
        isInit = false;
    }

    void OnEnterDamage(Collider2D collision)
    {
        if (!isInit) return;
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponentInParent<Player>();
            player.Hit(parentEnemyAttack.attackData.damage, 0.0f, parentEnemyAttack.enemy);
            CheckDisable();
        }
        if (collision.CompareTag("Summon"))
        {
            int coliderId = collision.gameObject.GetInstanceID();
            Summon summon = collision.GetComponent<Summon>();
            summons.AddOrUpdate(coliderId, summon);
            summon.Hit(parentEnemyAttack.attackData.damage, 0.0f, parentEnemyAttack.enemy);
            CheckDisable();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        OnExitDamage(collision);
    }

    void OnExitDamage(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
        }
        if (collision.CompareTag("Summon"))
        {
            int coliderId = collision.gameObject.GetInstanceID();
            summons.Remove(coliderId);
        }
    }


    void CheckDisable()
    {
        if (parentEnemyAttack.attackData.patternType != EnemyAttack.PatternType.Range) return;
        AnimationEnd();
    }
    void AnimationStart()
    {
        if (onAnimationStart == null) return;
        onAnimationStart.Invoke();
    }
    void AnimationEnd()
    {
        if (onAnimationEnd == null) return;
        onAnimationEnd.Invoke();
    }
}

