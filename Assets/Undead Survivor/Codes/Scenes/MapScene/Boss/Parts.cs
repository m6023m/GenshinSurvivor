using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Parts : Enemy
{
    Animator animator;
    public SpriteRenderer spriteRenderer;
    public RuntimeAnimatorController patternAnimationController
    {
        get
        {
            if (patternAnimation == null) return null;
            return patternAnimation.GetComponent<Animator>().runtimeAnimatorController;
        }
        set
        {
            if (patternAnimation == null) return;
            patternAnimation.GetComponent<Animator>().runtimeAnimatorController = value;
            patternAnimation.gameObject.SetActive(true);
        }
    }
    public RuntimeAnimatorController partsAnimation
    {
        get
        {
            return GetComponent<Animator>().runtimeAnimatorController;
        }
        set
        {
            GetComponent<Animator>().runtimeAnimatorController = value;
        }
    }


    public EnemyAttack enemyAttack
    {
        get
        {
            if (GetComponentInChildren<EnemyAttack>(true) == null) return null;
            return GetComponentInChildren<EnemyAttack>(true);
        }
    }
    public EnemyAttack[] enemyAttacks
    {
        get
        {
            if (GetComponentInChildren<EnemyAttack>(true) == null) return null;
            return GetComponentsInChildren<EnemyAttack>(true);
        }
    }

    public PatternAnimation patternAnimation
    {
        get
        {
            if (GetComponentInChildren<PatternAnimation>(true) == null) return null;
            return GetComponentInChildren<PatternAnimation>(true);
        }
    }
    Boss boss;
    Vector3 _defalutPosition;
    public Vector3 defalutPosition
    {
        get
        {
            if (_defalutPosition == Vector3.zero)
            {
                _defalutPosition = transform.localPosition;
            }
            return _defalutPosition;
        }
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        InitStat();
    }
    void InitStat()
    {
        boss = GetComponentInParent<Boss>();
        isLive = true;
        speed = boss.speed;
        health = boss.health;
        maxHealth = boss.maxHealth;
        damage = boss.damage;
        PhysicsRes = boss.PhysicsRes;
        PyroRes = boss.PyroRes;
        HydroRes = boss.HydroRes;
        AnemoRes = boss.AnemoRes;
        DendroRes = boss.DendroRes;
        ElectroRes = boss.ElectroRes;
        CyroRes = boss.CyroRes;
        exp = boss.exp;
        type = boss.type;
        Vector3 initPosition = defalutPosition;
    }

    public virtual void StartPattern()
    {
        animator.SetTrigger("Pattern_Start");
    }
    public virtual void EndPattern()
    {
        animator.SetTrigger("Pattern_End");
    }

    public override void ReceiveDamage(SkillName skillName, float damage, Element.Type elementType)
    {
        GameManager.instance.battleResult.UpdateDamage(skillName, damage);
        boss.health -= damage;
        boss.OnHit();
    }

    public void FadeOut()
    {
        Tweener tweener = spriteRenderer.DOFade(0f, 1f);
    }
    public void FadeOutCompliteListener(DG.Tweening.TweenCallback callback)
    {
        Tweener tweener = spriteRenderer.DOFade(0f, 1f);
        tweener.OnComplete(callback);
    }
    public void FadeIn()
    {
        spriteRenderer.DOFade(1.0f, 1.0f);
    }

    public void ResetPosition()
    {
        transform.localPosition = defalutPosition;
    }

    public void PatternDelayColorChange(float delay)
    {
        spriteRenderer.DOColor(new Color(255, 0, 0), delay);
        spriteRenderer.DOColor(new Color(255, 255, 255), 0.0f).SetDelay(delay);
    }
}
