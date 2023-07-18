using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public enum AttackName
    {
        None,
        Arrow,
        Breath,
        Meteor,
        Melee,
        Charge

    }
    EnemyAttackData attackData;
    RuntimeAnimatorController animatorController;
    float damageTime = 0;
    float damageTimeMax = 0.5f;

    public AnimationClip[] animationClips;
    bool isInit = false;
    Vector3 originalPosition = Vector3.zero;
    Vector3 targetDir;
    Enemy enemy;
    SpriteRenderer spriteRenderer;
    string shaderOutline = "_OuterOutlineFade";
    int shaderOutlineID;
    AttackName attackName;

    EnemyAttack parentEnemyAttack;
    Player player;
    Dictionary<int, Summon> summons = new Dictionary<int, Summon>();


    private void FixedUpdate()
    {
        if (!isInit) return;
        if (attackData.patternType != EnemyAttack.PatternType.Range &&
        attackData.patternType != EnemyAttack.PatternType.Wave) return;
        if (!enemy.isLive)
        {
            DisableObject();
            return;
        }
        Vector2 nextVec = originalPosition + (targetDir.normalized * attackData.speed * Time.fixedDeltaTime);
        originalPosition = nextVec;
        transform.position = originalPosition;
    }

    protected virtual void Update()
    {
        OnStayDamage();
    }

    public void Init(EnemyAttackData data)
    {
        summons = new Dictionary<int, Summon>();
        player = null;
        isInit = false;
        attackData = data;
        transform.localPosition = Vector3.zero;
        originalPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        shaderOutlineID = Shader.PropertyToID(shaderOutline);
        parentEnemyAttack = GetComponentInParent<EnemyAttack>();
        animatorController = GetComponent<Animator>().runtimeAnimatorController;
        SetAnimation(AttackName.None);
        enemy = GetComponentInParent<Enemy>();
        transform.localScale = new Vector3(1f, 1f, 1f);
        targetDir = attackData.targetDirection;
        if (attackData.patternType == EnemyAttack.PatternType.Wave)
        {
            targetDir = transform.up;
        }
        damageTime = 0;
        InitAttack();
    }

    void InitAttack()
    {
        if (attackData.patternType == EnemyAttack.PatternType.None)
        {
            DisableObject();
            return;
        }
        if (isInit) return;
        switch (attackData.patternType)
        {
            case EnemyAttack.PatternType.Range:
                attackName = AttackName.Arrow;
                spriteRenderer.material.SetFloat(shaderOutlineID, 1.0f);
                break;
            case EnemyAttack.PatternType.Breath:
                attackName = AttackName.Breath;
                spriteRenderer.material.SetFloat(shaderOutlineID, 0.0f);
                break;
            case EnemyAttack.PatternType.Meteor:
                attackName = AttackName.Meteor;
                transform.RotationFix(Vector3.up);
                spriteRenderer.material.SetFloat(shaderOutlineID, 0.0f);
                break;
            case EnemyAttack.PatternType.Warp:
                return;
            case EnemyAttack.PatternType.Howling:
                transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
                spriteRenderer.material.SetFloat(shaderOutlineID, 0.0f);
                return;
            case EnemyAttack.PatternType.Wave:
                attackName = AttackName.Arrow;
                spriteRenderer.material.SetFloat(shaderOutlineID, 0.0f);
                break;
            case EnemyAttack.PatternType.Melee:
                attackName = AttackName.Melee;
                spriteRenderer.material.SetFloat(shaderOutlineID, 0.0f);
                break;
            case EnemyAttack.PatternType.Charge:
                attackName = AttackName.Charge;
                spriteRenderer.material.SetFloat(shaderOutlineID, 0.0f);
                break;

        }
        SetAnimation(attackName);
        CalcAnimationEndDuration();

        isInit = true;
    }


    void CalcAnimationEndDuration()
    {
        AnimationClip animationClip = animationClips[(int)attackName];
        float duration = animationClip.length;
        if (attackData.patternType == EnemyAttack.PatternType.Range)
        {
            duration = 3.0f; //원거리 공격이면 3초만 활성화 됨
        }
        if (attackData.duration > 0)
        {
            duration = attackData.duration;
        }
        StartCoroutine(AnimationEnd(duration));
    }


    IEnumerator AnimationEnd(float duration)
    {
        yield return new WaitForSeconds(duration);
        DisableObject();
    }




    void SetAnimation(AttackName attackName)
    {
        AnimatorOverrideController aoc = new AnimatorOverrideController(animatorController);
        aoc["Skill_None"] = animationClips[(int)attackName];
        GetComponent<Animator>().runtimeAnimatorController = aoc;
    }

    void OnStayDamage()
    {
        if (!isInit) return;
        damageTime += Time.deltaTime;
        if (damageTime > damageTimeMax)
        {
            if (player != null)
            {
                player.Hit(attackData.damage, 0.0f, enemy);
            }
            foreach (KeyValuePair<int, Summon> summon in summons)
            {
                summon.Value.Hit(attackData.damage, 0.0f, enemy);
            }
            damageTime = 0;
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnterDamage(collision);
    }

    void OnEnterDamage(Collider2D collision)
    {
        if (!isInit) return;
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponentInParent<Player>();
            player.Hit(attackData.damage, 0.0f, enemy);
        }
        if (collision.CompareTag("Summon"))
        {
            int coliderId = collision.gameObject.GetInstanceID();
            Summon summon = collision.GetComponent<Summon>();
            summons.AddOrUpdate(coliderId, summon);
            summon.Hit(attackData.damage, 0.0f, enemy);
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
        if (attackData.patternType != EnemyAttack.PatternType.Range) return;
        DisableObject();
    }
    private void DisableObject()
    {
        gameObject!.SetActive(false);
    }
}

