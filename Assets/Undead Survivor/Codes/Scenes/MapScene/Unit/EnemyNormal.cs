using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class EnemyNormal : Enemy
{
    public enum Name
    {
        Slime_Hydro,
        Slime_Pyro,
        Slime_Electro_P,
        Slime_Electro_Y,
        Slime_Cyro,
        Slime_Geo,
        Slime_Anemo,
        Slime_Dendro,
        Chuchu_Basic,
        Chuchu_Weapon,
        Chuchu_Weapon_Pyro,
        Chuchu_Big_Basic,
        Chuchu_Big_Shield,
        Chuchu_Arrow,
        Whopperflower_Pyro,
        Whopperflower_Cyro,
        Whopperflower_Electro,
        Chuchu_King_Cyro,
        Chuchu_King_Electro,
        Chuchu_King_Geo,
        Kairagi_Electro,
        Kairagi_Pyro,
        Nobushi,
        Specter_Anemo,
        Specter_Cyro,
        Specter_Dendro,
        Specter_Electro,
        Specter_Geo,
        Specter_Hydro,
        Specter_Pyro,
        Treasure_Hunter,
    }

    public enum Pattern
    {
        No_Pattern,
        Charge,
        Range,
        Warp,
        Jump,
        Suicide_Bomb
    }

    public float patternRange = 0;
    public float patternCoolTime = 5f;
    EnemyAttackData enemyAttackData;
    EnemyAttack enemyAttack;
    public Pattern pattern;
    public float size = 0.8f;
    public Rigidbody2D target;
    public RuntimeAnimatorController[] animCon;
    UnityAction OnDead;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Collider2D coll;
    Animator animator;
    Transform shadow;
    public ElementReaction elementReaction;
    Vector2 magnetVec;
    Vector2 knockBackVec;
    Vector2 pushVec;
    private WaitForFixedUpdate wait;
    float elementAttachTime = 0;
    float elementAttachTimeTerm = 3; //원소 속성이 있을 경우 갱신시간

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        shadow = transform.GetChild(0);
        elementReaction = GetComponentInChildren<ElementReaction>();
        wait = new WaitForFixedUpdate();
        magnetVec = Vector2.zero;
        enemyAttack = GetComponentInChildren<EnemyAttack>();
    }

    void FixedUpdate()
    {
        UpdateMovePosition();
        ElementAttachCheck();
    }

    void UpdateMovePosition()
    {
        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Stop") || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        Vector2 dirVec = target.position - rigid.position;
        float distanceToPlayer = dirVec.magnitude;

        if (!isPatternCoolTime && distanceToPlayer <= patternRange && patternRange != 0 && distanceToPlayer != 0)
        {
            PatternCheck();
        }

        if (!isPattern)
        {
            Vector2 nextVec = dirVec.normalized * speed * addSpeed * Time.fixedDeltaTime;
            nextVec += magnetVec * Time.fixedDeltaTime;
            nextVec += pushVec * Time.fixedDeltaTime;
            nextVec += knockBackVec * Time.fixedDeltaTime;

            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;
        }
    }

    void ElementAttachCheck()
    {
        elementAttachTime += 1 * Time.fixedDeltaTime;

        if (elementAttachTime >= elementAttachTimeTerm)
        {
            elementReaction.AddElement(elementType, 0, true);
            elementAttachTime = 0;
        }
    }
    void PatternCheck()
    {
        if (isPattern) return;
        switch (pattern)
        {
            case Pattern.Charge:
                ChargePattern();
                break;
            case Pattern.Range:
                RangePattern();
                break;
            case Pattern.Warp:
                WarpPattern();
                break;
            case Pattern.Jump:
                JumpPattern();
                break;
            case Pattern.Suicide_Bomb:
                Suicide_BombPattern();
                break;
        }
    }

    void LateUpdate()
    {
        if (!isLive || isPattern) return;
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        shadow.gameObject.SetActive(true);
        elementReaction.gameObject.SetActive(true);
        health = maxHealth;
    }

    public override void Init(SpawnData data)
    {
        base.Init(data);
        addSpeed = 1;
        animator.runtimeAnimatorController = animCon[(int)data.spriteName];

        InitPatternData(data);
        size = data.enemyStat.size;
        transform.localScale = Vector3.one * size;
        isPatternCoolTime = false;
        isNodamage = false;

        SetOnDead();
        rigid.velocity = Vector2.zero;
        magnetVec = Vector2.zero;
        knockBackVec = Vector2.zero;
        pushVec = Vector2.zero;
        elementReaction.ResetAttach(elementType);
        elementReaction.Frozen(0.0f);
        elementReaction.Petrification(0.0f);
        ResetReaction();
        animator.Rebind();
    }
    void SetOnDead()
    {
        foreach (Character character in GameDataManager.instance.saveData.userData.selectChars)
        {
            if (character == null) continue;
            if (character.charNum == CharacterData.Name.Tartaglia)
            {
                if (character.constellation[1])
                {
                    OnDead = () =>
                    {
                        GameManager.instance.skillData.skills[SkillName.EB_Tartaglia].parameter.elementGauge += 0.5f;
                    };
                }
            }
        }
    }

    void InitPatternData(SpawnData data)
    {
        pattern = data.enemyStat.pattern;
        patternCoolTime = data.enemyStat.patternCoolTime;
        patternRange = data.enemyStat.patternRange;
        enemyAttackData = data.enemyStat.enemyAttackData;
    }



    void ResetReaction()
    {
        foreach (SkillMoveSet skillMoveSet in GetComponentsInChildren<SkillMoveSet>())
        {
            skillMoveSet.gameObject.SetActive(false);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Skill") && isLive)
        {
            Skill skill = collision.GetComponent<Skill>();
            SkillData.ParameterWithKey parameterWithKey = skill.parameterWithKey;
            Skill_Parameter parameter = parameterWithKey.parameter;
            if (parameter.knockBack > 0)
            {
                StartCoroutine(KnockBack(parameter.knockBack));
            }
        }

    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);

        if (collision.CompareTag("SkillEffectArea"))
        {
            SkillMoveSet skill = collision.GetComponentInParent<SkillMoveSet>();

            if (skill.parameterWithKey.parameter.magnet != 0 && skill.skillSequence.isMagnet)
            {
                Magnet(collision.gameObject.transform.position, skill.parameterWithKey.parameter.magnetSpeed);
            }
        }

    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (collision.CompareTag("SkillEffectArea"))
        {
            magnetVec = Vector2.zero;
        }
    }

    public override void ReceiveDamage(SkillName skillName, float damage, Element.Type elementType)
    {
        if (pattern == Pattern.Warp && isPattern) return;//워프 패턴 중 무적

        base.ReceiveDamage(skillName, damage, elementType);
        Hit();
        elementReaction.AddElement(skillName, damage, elementType);
        CheckEffect(skillName, damage, elementType);
    }

    void CheckEffect(SkillName skillName, float damage, Element.Type elementType)
    {
        switch (skillName)
        {
            case SkillName.EB_Zhongli:
                if (GameManager.instance.skillData.skills[skillName].constellations.num3)
                {
                    elementReaction.Petrification(1.0f, 4.0f);
                }
                else
                {
                    elementReaction.Petrification(1.0f, 6.0f);
                }
                break;
        }
    }

    void Magnet(Vector3 target, float magnetSpeed)
    {
        Vector3 targetPosition = target;
        magnetVec = (targetPosition - transform.position).normalized * magnetSpeed * GameManager.instance.statCalculator.Magnet;
    }

    protected override void LiveCheck()
    {
        base.LiveCheck();
        if (!isLive) return;
        if (health <= 0)
        {//죽었으면
            isLive = false;
            elementReaction.Frozen(0.0f);
            elementReaction.Petrification(0.0f);
            animator.SetBool("Pattern", false);
            animator.SetTrigger("PatternStop");
            animator.SetBool("Stop", false);
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            GameManager.instance.gameInfoData.kill++;
            GameManager.instance.battleResult.kill++;
            if (type == Enemy.Type.Normal)
            {
                GameDataManager.instance.saveData.record.killCount++;
            }
            else if (type == Enemy.Type.Elite)
            {
                GameDataManager.instance.saveData.record.killEliteCount++;
            }
            GameManager.instance.GetExp(exp);
            if (OnDead != null)
            {
                OnDead.Invoke();
            }
            DropRandomItem();

            AudioManager.instance.PlaySFX(AudioManager.SFX.Dead);
            shadow.gameObject.SetActive(false);
            elementReaction.gameObject.SetActive(false);
            animator.SetTrigger("Dead");
        }
    }

    IEnumerator KnockBack(float knockBack)
    {
        yield return wait;
        Vector3 palyerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - palyerPos;
        Vector2 knockBackValue = dirVec.normalized * knockBack;
        if (knockBackValue.magnitude > knockBackVec.magnitude)
        {
            knockBackVec = knockBackValue;
        }
        yield return new WaitForSecondsRealtime(0.3f);
        knockBackVec = Vector2.zero;
    }
    void DropRandomItem()
    {
        GameObject elementalSphere = GameManager.instance.poolManager.Get(PoolManager.Type.ElementalSphere);
        elementalSphere.transform.position = gameObject.transform.position;

        RandomDrop();
    }

    void RandomDrop()
    {

        int randomNum = Random.Range(0, 300);
        int dropPer = 0;
        if (type == Type.Normal)
        {
            dropPer = 1 + (int)GameManager.instance.statCalculator.Luck;
            if (randomNum <= dropPer)
            {
                int randomBox = Random.Range(0, 10);
                GameObject dropItem = GameManager.instance.poolManager.Get(PoolManager.Type.DropItem);
                dropItem.transform.position = gameObject.transform.position;
                DropItem drop = dropItem.GetComponent<DropItem>();
                if (randomBox == 1)
                {
                    drop.Init(DropItem.Name.Box_Ex);
                }
                else
                {
                    drop.Init(DropItem.Name.Box_Normal);
                }
            }
        }
        else if (type == Type.Elite)
        {
            GameObject dropItem = GameManager.instance.poolManager.Get(PoolManager.Type.DropItem);
            dropItem.transform.position = gameObject.transform.position;
            DropItem drop = dropItem.GetComponent<DropItem>();
            drop.Init(DropItem.Name.Box_Ex);
        }
    }

    private void Hit()
    {
        if (!isLive) return;
        AudioManager.instance.PlaySFX(AudioManager.SFX.Hit);
        animator.SetTrigger("Hit");
    }


    void ChargePattern()
    {
        isPatternCoolTime = true;
        isPattern = true;

        PatternDelay(1.0f).OnComplete(() =>
        {
            animator.SetBool("Pattern", true);

            Vector2 dirVec = target.position - rigid.position;
            rigid.AddForce(dirVec.normalized * speed * enemyAttackData.speed, ForceMode2D.Impulse);

            PatternDelay(1.0f).OnComplete(() =>
            {
                // 돌진 종료 애니메이션 트리거
                animator.SetBool("Pattern", false);
                animator.SetTrigger("PatternStop");
                isPattern = false;

                PatternDelay(patternCoolTime).OnComplete(() =>
                {
                    isPatternCoolTime = false;
                });
            });
        });
    }

    void RangePattern()
    {
        isPatternCoolTime = true;
        isPattern = true;


        PatternDelay(1.0f).OnComplete(() =>
        {

            animator.SetBool("Pattern", true);
            enemyAttack.transform.parent = transform;
            enemyAttack.GetComponent<EnemyAttack>().Init(enemyAttackData);

            PatternDelay(1.0f).OnComplete(() =>
            {
                animator.SetBool("Pattern", false);
                animator.SetTrigger("PatternStop");
                isPattern = false;

                PatternDelay(patternCoolTime).OnComplete(() =>
                {
                    isPatternCoolTime = false;
                });
            });
        });
    }


    Tweener PatternDelay(float delay)
    {
        return spriter.DOColor(new Color(1, 1, 1, 1), delay);
    }
    void WarpPattern()
    {
        isPatternCoolTime = true;
        isPattern = true;
        isNodamage = true;


        rigid.isKinematic = true;
        Vector2 dirVec = target.position - rigid.position;
        Vector2 vecDestination = dirVec.normalized * speed * 3f;
        if (vecDestination.magnitude > dirVec.magnitude)
        {
            vecDestination = target.position;
        }

        enemyAttack.transform.parent = transform;
        enemyAttackData.targetDirection = vecDestination;
        enemyAttack.Init(enemyAttackData);



        animator.SetBool("Pattern", true);
        PatternDelay(enemyAttackData.patternDelay).OnComplete(() =>
        {
            transform.position = vecDestination;
            rigid.isKinematic = true;
            animator.SetBool("Pattern", false);

            PatternDelay(enemyAttackData.patternDelay).OnComplete(() =>
            {
                animator.SetTrigger("PatternStop");
                isPattern = false;
                isNodamage = false;
                PatternDelay(patternCoolTime).OnComplete(() =>
                {
                    isPatternCoolTime = false;
                });

            });
        });
    }

    void JumpPattern()
    {
        isPatternCoolTime = true;
        isPattern = true;
        isNodamage = true;


        rigid.isKinematic = true;
        Vector2 dirVec = target.position - rigid.position;
        Vector2 vecDestination = dirVec.normalized * speed * 3f;
        if (vecDestination.magnitude > dirVec.magnitude)
        {
            vecDestination = target.position;
        }

        enemyAttack.transform.parent = transform;
        enemyAttackData.targetDirection = vecDestination;
        enemyAttack.Init(enemyAttackData);



        animator.SetBool("Pattern", true);
        PatternDelay(enemyAttackData.patternDelay).OnComplete(() =>
        {
            rigid.isKinematic = true;
            animator.SetBool("Pattern", false);
            transform.DOJump(vecDestination, enemyAttackData.speed, 1, enemyAttackData.duration)
            .SetEase(Ease.OutQuad)
                        .OnStart(() =>
                        {
                            // 점프 시작시 실행할 로직
                        })
                        .OnComplete(() =>
                        {
                            animator.SetTrigger("PatternStop");
                            isPattern = false;
                            isNodamage = false;
                            PatternDelay(patternCoolTime).OnComplete(() =>
                            {
                                isPatternCoolTime = false;
                            });
                        });

        });
    }
    public void Suicide_BombPattern()
    {
        addSpeed = enemyAttack.attackData.speed;
        enemyAttack.transform.parent = transform;

        animator.SetBool("Pattern", true);

        enemyAttack.attackData.endListener = () =>
        {
            animator.SetBool("Pattern", false);
            health = -1;
            LiveCheck();
        };
        enemyAttack.Init(enemyAttackData);

        enemyAttack.ActivateEnemyPatternArea();
    }


    public void Stop(bool isStop)
    {
        animator.SetBool("Stop", isStop);
    }
}