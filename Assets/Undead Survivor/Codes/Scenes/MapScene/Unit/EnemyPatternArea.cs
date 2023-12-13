using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPatternArea : MonoBehaviour
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
    public UnityAction onAnimationEnd;
    public UnityAction onAnimationStart;
    EnemyAttack parentEnemyAttack;
    float animationTime = 0;
    float animationDuration = 9999;
    public bool isInit = false;

    private void Update()
    {
        if (parentEnemyAttack == null || !gameObject.activeInHierarchy) return;
        if (parentEnemyAttack.attackData.patternAnimationClip == null) return;

        if (parentEnemyAttack.attackData.patternType == EnemyAttack.PatternType.None)
        {
            return;
        }
        if (isInit) animationTime += Time.deltaTime;
        if (animationTime > animationDuration && animationDuration != 0)
        {
            AnimationEnd();
            animationTime = 0;
        }
    }



    public EnemyPatternArea Init(EnemyAttack enemyAttack)
    {
        parentEnemyAttack = enemyAttack;
        animationDuration = enemyAttack.attackData.patternDelay;
        SetAnimation(enemyAttack.attackData.patternAnimationClip);

        animationTime = 0;
        switch (parentEnemyAttack.attackData.patternType)
        {
            case EnemyAttack.PatternType.None:
                animationDuration = 0.1f;
                break;
            case EnemyAttack.PatternType.Melee:
                transform.localScale = new Vector2(1f, 1f);
                transform.localPosition = Vector3.zero;
                break;
            case EnemyAttack.PatternType.Range:
                transform.ScaleFront(parentEnemyAttack.transform, new Vector3(1.0f, 25.0f));
                break;
            case EnemyAttack.PatternType.Breath:
                transform.localScale = new Vector2(1f, 1f);
                break;
            case EnemyAttack.PatternType.Meteor:
                transform.localScale = new Vector2(1f, 1f);
                transform.localPosition = Vector3.zero;
                break;
            case EnemyAttack.PatternType.Warp:
                transform.localScale = new Vector2(1f, 1f);
                break;
            case EnemyAttack.PatternType.Howling:
                transform.localScale = new Vector2(1f, 1f);
                break;
            case EnemyAttack.PatternType.Wave:
                transform.ScaleFront(parentEnemyAttack.transform, new Vector3(1.0f, 25.0f));
                break;
            case EnemyAttack.PatternType.Charge:
                transform.ScaleFront(parentEnemyAttack.transform, new Vector3(1.0f, parentEnemyAttack.attackData.patternSize));
                break;
            case EnemyAttack.PatternType.Suicide_Bomb:
                break;
        }
        animator.speed = 1;
        CalcAnimationEndDuration();
        isInit = true;
        return this;
    }

    void CalcAnimationEndDuration()
    {
        AnimationClip animationClip = parentEnemyAttack.attackData.patternAnimationClip;
        animationDuration = animationClip.length;
        if(parentEnemyAttack.attackData.duration != 0) {
            animator.speed = animationClip.length / parentEnemyAttack.attackData.patternDelay;
        }
    }
    void SetAnimation(AnimationClip animationClip)
    {
        if (animationClip == null) return;
        AnimatorOverrideController aoc = new AnimatorOverrideController(animatorController);
        aoc["Skill_None"] = animationClip;
        animator.runtimeAnimatorController = aoc;
    }
    void OnEnable()
    {
        AnimationStart();
    }

    void OnDisable()
    {
        isInit = false;
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