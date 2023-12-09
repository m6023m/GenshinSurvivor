using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class SkillMoveSet : MonoBehaviour
{
    public SkillData.ParameterWithKey parameterWithKey;
    public SkillSet.SkillSequence skillSequence;
    protected Vector3 targetDir;
    protected int penetrate = -1;
    protected Vector3 originalPosition;
    protected TransformValue prevTransform;
    protected SkillObject skillParent;
    protected Transform nearest = null;
    protected bool isSetTarget = false;
    protected bool isInit = false;
    protected float attackSpeed = 1.0f;
    protected Player player;
    protected float currentSize = 1;
    protected float reactedDamage = 0;
    protected SkillScanner scanner;
    protected BoxCollider2D collision;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rigid;
    protected UnityAction endListener;
    public bool isMove = false;

    float animationEndTime = 10.0f;
    float animationTime = 0.0f;

    //SubSkill
    protected List<Vector3> currentSkillPositions;
    protected List<GameObject> skillObjects;
    protected virtual void Update()
    {
        if (isInit == false ||
        skillSequence == null ||
        GameManager.instance.IsPause) return;
        CheckAnimation();
        if (parameterWithKey.type == Skill.Type.Reaction) return;

        if (skillSequence.isPositionFix && !isMove)
        {
            transform.position = originalPosition;
        }
        CalcTargetPosition();
        Rotation();
    }

    protected virtual void LateUpdate()
    {
        if (isInit == false ||
        skillSequence == null ||
        GameManager.instance.IsPause) return;
        if (parameterWithKey.type == Skill.Type.Reaction) return;

        float area = skillSequence.skillSize;
        if (skillSequence.objectType != Skill.ObjectType.Sheild &&
         skillSequence.objectType != Skill.ObjectType.Summon)
        {
            area *= parameterWithKey.parameter.area * GameManager.instance.statCalculator.Area;
        }
        if (parameterWithKey.name == SkillName.EB_Eula && skillSequence.isConditionChange)
        {
            float areaPer = 1.0f + (GameManager.instance.statBuff.eulaStack * 0.05f);
            area *= areaPer;
        }
        if (area != currentSize)
        {
            currentSize = area;
            transform.localScale = new Vector2(currentSize, currentSize);
        }
        Projectile();
        ResizeCollision();
    }
    void CheckAnimation()
    {
        animationTime += Time.deltaTime;
        if (animationTime >= animationEndTime)
        {
            AnimationEnd();
        }
    }
    void Projectile()
    {
        if (targetDir != null && skillSequence.isProjectile)
        {
            if (targetDir == Vector3.zero) targetDir = Vector3.up;
            originalPosition = transform.MoveTargetDirectionLinear(originalPosition, targetDir, attackSpeed);
            transform.rotation = Quaternion.FromToRotation(Vector3.up, targetDir);
        }
    }


    void Rotation()
    {
        if (skillSequence.isRotate)
        {
            GetComponentInParent<SkillObject>().transform.Rotate(Vector3.back * attackSpeed * 30 * Time.deltaTime);
        }
    }

    void CalcTargetPosition()
    {
        if (skillSequence.isTraking && isSetTarget)
        {
            ResetTargetDirectionTraking();
        }
    }

    public virtual SkillMoveSet Init(SkillData.ParameterWithKey parameterWithKey, SkillSet.SkillSequence skillSequence, TransformValue prevTransform, int index)
    {
        isInit = false;
        collision = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSkillPositions = new List<Vector3>();
        skillObjects = new List<GameObject>();
        this.parameterWithKey = parameterWithKey;
        this.skillSequence = skillSequence;
        this.penetrate = (int)(parameterWithKey.parameter.penetrate + GameManager.instance.statCalculator.Penetrate);
        this.prevTransform = prevTransform;
        player = GameManager.instance.player;
        isSetTarget = false;
        isMove = false;
        attackSpeed = parameterWithKey.parameter.speed * GameManager.instance.statCalculator.Aspeed;
        skillParent = GetComponentInParent<SkillObject>();
        scanner = GetComponent<SkillScanner>();
        scanner.Init(skillSequence.scanRange);

        if (skillSequence.isTransform)
        {
            GetComponent<SpriteRenderer>().color = Element.Color(skillSequence.elementType);
        }



        InitTarget();
        InitPosition(index);
        InitAnimation();
        PositionFix();
        InitMoveSet();
        originalPosition = new Vector3(transform.position.x, transform.position.y);

        isInit = true;

        return this;
    }
    public virtual SkillMoveSet AddEndListener(UnityAction action)
    {
        endListener = action;
        return this;
    }
    void ResizeCollision()
    {
        if (!skillSequence.isMass) return;
        collision.size = spriteRenderer.sprite.bounds.size;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (player == null) return;
        Vector3 startPoint = player.transform.position;
        Vector3 endPoint = targetDir * 20;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(startPoint, endPoint);


        if (nearest == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(nearest.position, 2.0f);

        Vector3 startPointSkill = transform.position;
        Vector3 endPointSkill = transform.CalcTarget(nearest) * 20;
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(startPoint, endPoint);
    }
#endif

    void InitPosition(int index)
    {
        transform.localPosition = Vector3.zero;
        transform.localPosition = skillSequence.defalutPosition;
        transform.localRotation = Quaternion.identity;
        if (skillParent != null)
        {
            skillParent.transform.localRotation = Quaternion.identity;
        }

        InitSequence();

        int skillCount = skillSequence.skillCount;
        if (skillCount <= 0) skillCount = 1;
        if (skillSequence.isSkillAdd)
        {
            skillCount += parameterWithKey.parameter.count;
            skillCount += (int)GameManager.instance.statCalculator.Amount;
        }

        float rotationAngle = 360 / skillCount * index;
        if (targetDir != null)
        {
            float targetAngle = Mathf.Atan2(targetDir.x, targetDir.y) * Mathf.Rad2Deg;
            rotationAngle -= targetAngle;
        }

        if (!skillSequence.isSequence)
        {
            float playerX = player.transform.position.x;
            float playerY = player.transform.position.y;
            float radian = rotationAngle * Mathf.Deg2Rad;

            float x = (Mathf.Sin(radian) * skillSequence.createRange * GameManager.instance.statCalculator.Area);
            float y = (Mathf.Cos(radian) * skillSequence.createRange * GameManager.instance.statCalculator.Area);

            transform.localPosition = new Vector3(-x, y, transform.position.z);

            if (skillSequence.aim == Skill.Aim.Basic)
            {
                this.targetDir = new Vector3(-x, y, transform.position.z);

            }
            transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }

        if (skillSequence.randomRange > 0)
        {
            float randomRange = Random.Range(-skillSequence.randomRange, skillSequence.randomRange);
            transform.localPosition = new Vector2(transform.localPosition.x + randomRange, transform.localPosition.y + randomRange);
        }
    }
    void InitSequence()
    {
        if (!skillSequence.isSequence) return;
        if (skillSequence.isResetPosition) return;
        if (skillSequence.isPositionFix)
        {
            transform.position = prevTransform.position;
        }
        else
        {
            transform.localPosition = prevTransform.localPosition;
        }
    }

    void InitTarget()
    {
        if (isSetTarget || skillSequence.isSequence) return;

        ResetTarget();
        if (skillSequence.aim == Skill.Aim.Player)
        {
            this.targetDir = GameManager.instance.player.playerVec;
            isSetTarget = true;
        }

    }

    void ResetTarget()
    {
        if (skillSequence.aim == Skill.Aim.SkillToTarget)
        {
            nearest = scanner.GetNearest();
        }

        if (skillSequence.aim == Skill.Aim.Target)
        {
            nearest = GameManager.instance.player.scanner.nearestTarget;
        }
        ResetTargetDirection();
    }
    void ResetTargetDirection()
    {
        if (nearest != null)
        {
            Vector3 dir = player.transform.CalcTarget(nearest);
            targetDir = dir;
            isSetTarget = true;
        }
    }
    void ResetTargetDirectionTraking()
    {
        if (nearest != null)
        {
            Vector3 dir = transform.CalcTarget(nearest);
            targetDir = dir;
            isSetTarget = true;
        }
    }

    void PositionFix()
    {
        if (skillSequence.isPositionFix && skillSequence.isRotationFix)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            if (skillSequence.defalutRotation != Vector2.zero)
            {
                transform.rotation = Quaternion.FromToRotation(Vector3.up, skillSequence.defalutRotation);
            }
        }
        else if (skillSequence.isPositionFix)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else if (skillSequence.isRotationFix)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, skillSequence.defalutRotation);
        }
    }

    void InitAnimation()
    {
        RuntimeAnimatorController ac = GetComponent<Animator>().runtimeAnimatorController;
        AnimatorOverrideController aoc = new AnimatorOverrideController(ac);
        aoc["Skill_None"] = skillSequence.animation;
        GetComponent<Animator>().speed = skillSequence.animationSpeed;
        if (skillSequence.isAnimationSpeedMatchDuration)
        {
            float duration = skillSequence.duration
                * GameManager.instance.statCalculator.Duration
                * parameterWithKey.parameter.duration;
            GetComponent<Animator>().speed = skillSequence.animation.length / duration;
        }
        GetComponent<Animator>().runtimeAnimatorController = aoc;
        CalcAnimationEndDuration();
    }

    void CalcAnimationEndDuration()
    {
        if (skillSequence.duration == -1) return;
        float duration = skillSequence.animation.length;
        duration = duration / skillSequence.animationSpeed;
        if (skillSequence.duration > 0 && !skillSequence.isSummonAttack)
        {
            duration = skillSequence.duration
                * GameManager.instance.statCalculator.Duration
                * parameterWithKey.parameter.duration;
        }
        if (skillSequence.isProjectile)
        {
            animationEndTime = 3.0f;
        }
        else
        {
            animationEndTime = duration;
        }

        if (skillSequence.moveTimeInDuration)
        {
            animationEndTime = skillSequence.moveTime;
        }
        animationTime = 0.0f;
    }

    void AnimationEnd()
    {
        if (skillSequence.duration == -1) return;
        if (!gameObject.activeInHierarchy) return;
        gameObject.SetActive(false);
        animationTime = 0.0f;
    }

    void InitMoveSet()
    {
        if (!gameObject.activeInHierarchy) return;
        StartCoroutine(MoveFromTo());
        StartCoroutine(MoveReset());
        MovePlayerToTarget();
    }

    void MovePlayerToTarget()
    {
        if (!skillSequence.isPlayerMove) return;
        Vector3 movePosition = new Vector3(player.transform.position.x, player.transform.position.y) + (targetDir * skillSequence.moveRange);
        player.transform.DOMove(movePosition, skillSequence.moveTime);
    }

    IEnumerator MoveFromTo()
    {
        yield return null;
        if (skillSequence.moveRange != 0 && skillSequence.isSkillMove)
        {
            Vector3 dir = targetDir.normalized * skillSequence.moveRange;
            Vector3 movePosition = new Vector3(transform.position.x, transform.position.y) + dir;
            rigid.constraints = RigidbodyConstraints2D.None;
            isMove = true;
            transform.DOMove(movePosition, skillSequence.moveTime).OnComplete(() =>
            {
                originalPosition = new Vector3(transform.position.x, transform.position.y);
                PositionFix();
                isMove = false;
            });
        }
    }
    IEnumerator MoveReset()
    {
        yield return null;
        if (skillSequence.isResetPosition)
        {
            float directionX = transform.localPosition.x > 0 ? 1 : -1;
            float directionY = transform.localPosition.y > 0 ? 1 : -1;
            float offsetX = skillSequence.centerOffset.x * GameManager.instance.statCalculator.Area;
            float offsetY = skillSequence.centerOffset.y * GameManager.instance.statCalculator.Area;
            float positionX = directionX * transform.localScale.x / 2;
            float positionY = directionY * transform.localScale.y / 2;
            Vector3 position = new Vector3(positionX + prevTransform.position.x + offsetX, positionY + prevTransform.position.y + offsetY);

            rigid.constraints = RigidbodyConstraints2D.None;
            isMove = true;
            transform.DOMove(position, skillSequence.moveTime).OnComplete(() =>
            {
                originalPosition = new Vector3(transform.position.x, transform.position.y);
                PositionFix();
                isMove = false;
            }).SetEase(Ease.Linear);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") && !collision.CompareTag("MapObject") || !skillSequence.isProjectile) return;

        OnTrigger();

        if (!skillSequence.isProjectile) return;
        penetrate--;

        if (penetrate == -1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            CalcTargetPosition();
        }
    }

    protected void OnTrigger()
    {
        if (skillSequence.isTrigger)
        {
            GameManager.instance.player.StartCoroutine(TriggerSkillSequence(skillSequence.triggerSkillSequence));
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {

    }

    protected virtual void OnDisable()
    {
        if (endListener == null) return;
        endListener.Invoke();
        foreach (SkillMoveSet skillMoveSet in GetComponentsInChildren<SkillMoveSet>())
        {
            skillMoveSet.gameObject.SetActive(false);
        }
    }

    public SkillMoveSet ReactedDamage(float damage)
    {
        reactedDamage = damage;
        return this;
    }


    protected IEnumerator TriggerSkillSequence(SkillSet.SkillSequence skillSequence)
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Range);
        CreateObjects(skillSequence);
        yield return null;
    }

    protected void StartSkillSequence(SkillSet.SkillSequence skillSequence)
    {
        if (!CheckSkillCondition(skillSequence)) return;
        AudioManager.instance.PlaySFX(AudioManager.SFX.Range);
        CreateObjects(skillSequence);
    }

    bool CheckSkillCondition(SkillSet.SkillSequence skillSequence)
    {
        bool condition = !skillSequence.isConditionChange;
        switch (parameterWithKey.name)
        {
            case SkillName.E_Ninguang:
            case SkillName.E_Albedo:
            case SkillName.E_Zhongli:
            case SkillName.E_Travler_Geo:
            case SkillName.EB_Travler_Geo:
                condition = GameManager.instance.statBuff.isResonance;//바위 창조물 공명
                break;
        }
        return condition;
    }

    private void CreateObjects(SkillSet.SkillSequence skillSequence)
    {
        float area = skillSequence.skillSize;
        if (skillSequence.objectType != Skill.ObjectType.Sheild &&
         skillSequence.objectType != Skill.ObjectType.Summon)
        {
            area *= parameterWithKey.parameter.area * GameManager.instance.statCalculator.Area;
        }
        float magentArea = parameterWithKey.parameter.magnet * GameManager.instance.statCalculator.Magnet;

        int skillCount = skillSequence.skillCount;
        if (skillCount <= 0) skillCount = 1;
        if (skillSequence.isSkillAdd)
        {
            skillCount += parameterWithKey.parameter.count;
            skillCount += (int)GameManager.instance.statCalculator.Amount;
        }

        PoolManager.Type objectType = PoolManager.Type.Skill;

        switch (skillSequence.objectType)
        {
            case Skill.ObjectType.Skill:
                objectType = PoolManager.Type.Skill;
                break;
            case Skill.ObjectType.Buff:
                objectType = PoolManager.Type.Buff;
                break;
            case Skill.ObjectType.Sheild:
                objectType = PoolManager.Type.Sheild;
                break;
            case Skill.ObjectType.Summon:
                objectType = PoolManager.Type.Summon;
                break;
            case Skill.ObjectType.SkillEffect:
                objectType = PoolManager.Type.SkillEffect;
                break;
        }

        for (int i = 0; i < skillCount; i++)
        {
            int idx = i;

            Transform skill = GetSkillObject(idx, objectType);

            if (skill != null)
            {
                CapsuleCollider2D skillEffectArea = skill.GetComponentInChildren<CapsuleCollider2D>();
                skill.gameObject.SetActive(true);
                if (!skillSequence.isTriggerAttack)
                {
                    skill.parent = transform;
                }
                if (skillSequence.layerOrder != 0)
                {
                    skill.GetComponent<SpriteRenderer>().sortingOrder = skillSequence.layerOrder;
                }
                else
                {
                    skill.GetComponent<SpriteRenderer>().sortingOrder = 800;
                }

                skillEffectArea.size = new Vector2(magentArea, magentArea);
                skill.localScale = new Vector2(area, area);

                switch (skillSequence.objectType)
                {
                    case Skill.ObjectType.Skill:
                        skill.GetComponent<Skill>().Init(parameterWithKey, skillSequence, transform.CopyTransformValue(), idx);
                        break;
                    case Skill.ObjectType.Buff:
                        skill.GetComponent<Buff>().Init(parameterWithKey, skillSequence, transform.CopyTransformValue(), idx);
                        break;
                    case Skill.ObjectType.Sheild:
                        skill.GetComponent<Sheild>().Init(parameterWithKey, skillSequence, transform.CopyTransformValue(), idx);
                        break;
                    case Skill.ObjectType.Summon:
                        skill.GetComponent<Summon>().Init(parameterWithKey, skillSequence, transform.CopyTransformValue(), idx);
                        break;
                    case Skill.ObjectType.SkillEffect:
                        skill.GetComponent<SkillEffect>().Init(parameterWithKey, skillSequence, transform.CopyTransformValue(), idx);
                        break;
                }
            }

        }
    }

    private Transform GetSkillObject(int idx, PoolManager.Type type)
    {
        Transform skillObject = null;
        skillObject = GameManager.instance.poolManager.Get(type, skillObjects).transform;
        skillObjects.Add(skillObject.gameObject);
        return skillObject;
    }


}
