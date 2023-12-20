using System.Collections;
using System.Collections.Generic;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
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
    Rigidbody2D _rigid;
    Rigidbody2D rigid
    {
        get
        {
            if (_rigid == null) _rigid = GetComponent<Rigidbody2D>();
            return _rigid;
        }
    }
    SpriteRenderer _spriteRenderer;
    SpriteRenderer spriteRenderer
    {
        get
        {
            if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
            return _spriteRenderer;
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
        animationTime += Time.fixedDeltaTime;
        if (animationTime > animationDuration && animationDuration != 0)
        {
            AnimationEnd();
            animationTime = 0;
        }
        if (parentEnemyAttack.attackData.patternType != EnemyAttack.PatternType.Range &&
        parentEnemyAttack.attackData.patternType != EnemyAttack.PatternType.Wave) return;
        Vector2 nextVec = originalPosition + (targetDir.normalized * parentEnemyAttack.attackData.speed * Time.fixedDeltaTime);
        originalPosition = nextVec;
        transform.position = originalPosition;
    }

    protected virtual void Update()
    {
        OnStayDamage();
    }

    public void Init(EnemyAttack enemyAttack)
    {
        isInit = false;
        spriteRenderer.sprite = null;
        parentEnemyAttack = enemyAttack;
        if (parentEnemyAttack.attackData.damageAnimationClip == null) return;
        summons = new Dictionary<int, Summon>();
        player = null;
        transform.localPosition = Vector3.zero;
        originalPosition = transform.position;
        transform.localScale = new Vector3(1f, 1f, 1f);
        targetDir = transform.up;
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
                rigid.bodyType = RigidbodyType2D.Dynamic;
                break;
            case EnemyAttack.PatternType.Breath:
                rigid.bodyType = RigidbodyType2D.Kinematic;
                break;
            case EnemyAttack.PatternType.Meteor:
                transform.RotationFix(Vector3.up);
                rigid.bodyType = RigidbodyType2D.Dynamic;
                break;
            case EnemyAttack.PatternType.Warp:
                rigid.bodyType = RigidbodyType2D.Kinematic;
                return;
            case EnemyAttack.PatternType.Howling:
                transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
                rigid.bodyType = RigidbodyType2D.Dynamic;
                return;
            case EnemyAttack.PatternType.Wave:
                rigid.bodyType = RigidbodyType2D.Dynamic;
                break;
            case EnemyAttack.PatternType.Melee:
                rigid.bodyType = RigidbodyType2D.Kinematic;
                break;
            case EnemyAttack.PatternType.Charge:
                rigid.bodyType = RigidbodyType2D.Kinematic;
                break;
            case EnemyAttack.PatternType.Suicide_Bomb:
                rigid.bodyType = RigidbodyType2D.Kinematic;
                transform.localScale = new Vector2(parentEnemyAttack.attackData.patternSize, parentEnemyAttack.attackData.patternSize);
                break;

        }
        CalcAnimationEndDuration();

    }


    void CalcAnimationEndDuration()
    {
        AnimationClip animationClip = parentEnemyAttack.attackData.damageAnimationClip;
        float duration = animationClip.length;
        if (parentEnemyAttack.attackData.patternDelay != 0)
        {
            animator.speed = animationClip.length / parentEnemyAttack.attackData.duration;
            duration = parentEnemyAttack.attackData.duration * 1.05f;//AnimationEnd가 애니메이션이 끝나는 것 보다 먼저 호출되는 것 방지
        }
        if (parentEnemyAttack.attackData.patternType == EnemyAttack.PatternType.Range)
        {
            duration = 3.0f; //원거리 공격이면 3초만 활성화 됨
        }
        animationDuration = duration;
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
        spriteRenderer.sprite = null;
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
    public void AnimationStart()
    {
        animator.enabled = true;
        isInit = true;
        if (onAnimationStart == null) return;
        onAnimationStart.Invoke();
    }
    void AnimationEnd()
    {
        Debug.Log("AnimationEndDamage");
        animator.enabled = false;
        spriteRenderer.sprite = null;
        if (onAnimationEnd == null) return;
        onAnimationEnd.Invoke();
    }
}

