using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPatternArea : MonoBehaviour
{

    public RuntimeAnimatorController patternCircle;
    public RuntimeAnimatorController patternSquare;
    public RuntimeAnimatorController patternNone;
    Animator animator;
    UnityAction onAnimationEnd;
    EnemyAttack parentEnemyAttack;
    float animationTime = 0;
    float animationDuration = 9999;

    private void Update()
    {
        if (parentEnemyAttack == null || !gameObject.activeInHierarchy) return;

        if (parentEnemyAttack.attackData.patternType == EnemyAttack.PatternType.None)
        {
            SetPatternAlpha(0.0f);
            return;
        }
        animationTime += Time.deltaTime;
        if (animationTime > animationDuration && animationDuration != 0)
        {
            AnimationEnd();
            animationTime = 0;
        }
    }



    public EnemyPatternArea Init(EnemyAttackData attackData)
    {
        parentEnemyAttack = GetComponentInParent<EnemyAttack>(true);
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = null;
        if (parentEnemyAttack == null) return this;
        SetPatternAlpha(0.0f);
        animationDuration = attackData.patternDelay;

        animationTime = 0;
        switch (attackData.patternType)
        {
            case EnemyAttack.PatternType.None:
                SetPatternAlpha(0.0f);
                animationDuration = 0.1f;
                break;
            case EnemyAttack.PatternType.Melee:
                transform.localScale = new Vector2(1f, 1f);
                transform.localPosition = Vector3.zero;
                animator.runtimeAnimatorController = patternCircle;
                SetPatternAlpha(1.0f);
                break;
            case EnemyAttack.PatternType.Range:
                transform.ScaleFront(parentEnemyAttack.transform, new Vector3(1.0f, 25.0f));
                animator.runtimeAnimatorController = patternSquare;
                SetPatternAlpha(1.0f);
                break;
            case EnemyAttack.PatternType.Breath:
                transform.localScale = new Vector2(1f, 1f);
                animator.runtimeAnimatorController = patternSquare;
                SetPatternAlpha(1.0f);
                break;
            case EnemyAttack.PatternType.Meteor:
                transform.localScale = new Vector2(1f, 1f);
                transform.localPosition = Vector3.zero;
                animator.runtimeAnimatorController = patternCircle;
                SetPatternAlpha(1.0f);
                break;
            case EnemyAttack.PatternType.Warp:
                transform.localScale = new Vector2(1f, 1f);
                animator.runtimeAnimatorController = patternCircle;
                SetPatternAlpha(1.0f);
                break;
            case EnemyAttack.PatternType.Howling:
                transform.localScale = new Vector2(1f, 1f);
                animator.runtimeAnimatorController = patternCircle;
                SetPatternAlpha(1.0f);
                break;
            case EnemyAttack.PatternType.Wave:
                transform.ScaleFront(parentEnemyAttack.transform, new Vector3(1.0f, 25.0f));
                animator.runtimeAnimatorController = patternSquare;
                SetPatternAlpha(1.0f);
                break;
            case EnemyAttack.PatternType.Charge:
                transform.ScaleFront(parentEnemyAttack.transform, new Vector3(1.0f, parentEnemyAttack.attackData.patternSize));
                animator.runtimeAnimatorController = patternSquare;
                SetPatternAlpha(1.0f);
                break;
            case EnemyAttack.PatternType.Suicide_Bomb:
                animator.runtimeAnimatorController = patternCircle;
                SetPatternAlpha(1.0f);
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

    void AnimationEnd()
    {
        parentEnemyAttack.ActivateEnemyDamage();
        SetPatternAlpha(0.0f);
    }

}