using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPatternArea : MonoBehaviour
{

    public RuntimeAnimatorController patternCircle;
    public RuntimeAnimatorController patternSquare;
    Animator animator;
    UnityAction onAnimationEnd;
    EnemyAttack parentEnemyAttack;
    float animationTime = 0;
    float animationDuration = 9999;
    bool isPatterEnd = false;
    private void Update()
    {
        if (parentEnemyAttack == null)
        {
            SetPatternAlpha(0.0f);
            return;
        }

        if (parentEnemyAttack.attackData.patternType == EnemyAttack.PatternType.None)
        {
            SetPatternAlpha(0.0f);
            return;
        }
        if (!gameObject.activeInHierarchy) return;
        animationTime += Time.deltaTime;
        if (isPatterEnd) return;
        if (animationTime > animationDuration)
        {
            AnimationEnd();
            animationTime = 0;
            isPatterEnd = true;
        }
    }

    public EnemyPatternArea Init(EnemyAttackData attackData)
    {
        parentEnemyAttack = GetComponentInParent<EnemyAttack>(true);
        if (parentEnemyAttack == null) return this;
        SetPatternAlpha(0.0f);
        animator = GetComponent<Animator>();
        animationDuration = attackData.patternDelay;

        animationTime = 0;
        switch (attackData.patternType)
        {
            case EnemyAttack.PatternType.None:
                SetPatternAlpha(0.0f);
                animationDuration = 0.1f;
                break;
            case EnemyAttack.PatternType.Melee:
                isPatterEnd = false;
                transform.localScale = new Vector2(1f, 1f);
                transform.localPosition = Vector3.zero;
                animator.runtimeAnimatorController = patternCircle;
                SetPatternAlpha(1.0f);
                break;
            case EnemyAttack.PatternType.Range:
                isPatterEnd = false;
                transform.ScaleFront(parentEnemyAttack.transform, new Vector3(1.0f, 25.0f));
                animator.runtimeAnimatorController = patternSquare;
                SetPatternAlpha(1.0f);
                break;
            case EnemyAttack.PatternType.Breath:
                isPatterEnd = false;
                transform.localScale = new Vector2(1f, 1f);
                animator.runtimeAnimatorController = patternSquare;
                SetPatternAlpha(1.0f);
                break;
            case EnemyAttack.PatternType.Meteor:
                isPatterEnd = false;
                transform.localScale = new Vector2(1f, 1f);
                transform.localPosition = Vector3.zero;
                animator.runtimeAnimatorController = patternCircle;
                SetPatternAlpha(1.0f);
                break;
            case EnemyAttack.PatternType.Warp:
                isPatterEnd = false;
                transform.localScale = new Vector2(1f, 1f);
                animator.runtimeAnimatorController = patternCircle;
                SetPatternAlpha(1.0f);
                break;
            case EnemyAttack.PatternType.Howling:
                isPatterEnd = false;
                transform.localScale = new Vector2(1f, 1f);
                animator.runtimeAnimatorController = patternCircle;
                SetPatternAlpha(1.0f);
                break;
            case EnemyAttack.PatternType.Wave:
                isPatterEnd = false;
                transform.ScaleFront(parentEnemyAttack.transform, new Vector3(1.0f, 25.0f));
                animator.runtimeAnimatorController = patternSquare;
                SetPatternAlpha(1.0f);
                break;
            case EnemyAttack.PatternType.Charge:
                isPatterEnd = false;
                transform.ScaleFront(parentEnemyAttack.transform, new Vector3(1.0f, parentEnemyAttack.attackData.patternSize));
                animator.runtimeAnimatorController = patternSquare;
                SetPatternAlpha(1.0f);
                break;
            case EnemyAttack.PatternType.Suicide_Bomb:
                isPatterEnd = false;
                animator.runtimeAnimatorController = patternCircle;
                SetPatternAlpha(0.0f);
                break;
        }
        GetComponent<Animator>().speed = 1;
        if (animationDuration > 0)
        {
            animator.speed = animator.runtimeAnimatorController.animationClips[0].length / animationDuration;
        }
        animator.Rebind();
        return this;
    }

    public void SetPatternAlpha(float alpha)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    public EnemyPatternArea OnAnimationEnd(UnityAction action)
    {
        onAnimationEnd = action;
        return this;
    }
    void AnimationEnd()
    {
        onAnimationEnd.Invoke();
        SetPatternAlpha(0.0f);
    }

}