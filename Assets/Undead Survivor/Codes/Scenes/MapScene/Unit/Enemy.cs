using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Type
    {
        Normal,
        Elite,
        Boss
    }
    public float speed;
    public float armor;
    public float health;
    public float maxHealth;
    public float damage;
    public float PhysicsRes;
    public float PyroRes;
    public float HydroRes;
    public float AnemoRes;
    public float DendroRes;
    public float ElectroRes;
    public float CyroRes;
    public float GeoRes;
    public float exp = 1;
    public Type type;
    public Element.Type elementType;
    public bool isLive;
    protected bool isPattern = false;
    protected bool isPatternCoolTime = false;
    protected float damageTime = 0;
    protected float damageTimeMax = 0.5f;
    protected bool isNodamage = false;
    DamageAttach _damageAttach;
    public DamageAttach damageAttach
    {
        get
        {
            if (_damageAttach == null)
            {
                _damageAttach = GameManager.instance.damageAttach;
            }
            return _damageAttach;
        }
    }
    Player player;
    Dictionary<int, Summon> summons = new Dictionary<int, Summon>();
    Dictionary<int, float> damageTimers = new Dictionary<int, float>();
    Dictionary<int, Skill> skills = new Dictionary<int, Skill>();
    EnemyDebuff _enemyDebuff;
    public EnemyDebuff enemyDebuff
    {
        get
        {
            if (_enemyDebuff == null)
            {
                _enemyDebuff = GetComponent<EnemyDebuff>();
            }
            return _enemyDebuff;
        }
    }
    public virtual void Init(SpawnData data)
    {
        float gameLevelCorrection = GameManager.instance.statCalcuator.GameLevelCorrection;
        UserData userData = GameDataManager.instance.saveData.userData;
        summons = new Dictionary<int, Summon>();
        damageTimers = new Dictionary<int, float>();
        skills = new Dictionary<int, Skill>();
        type = data.enemyType;
        elementType = data.enemyStat.elementType;
        exp = data.enemyStat.exp;
        speed = data.enemyStat.speed * gameLevelCorrection;
        maxHealth = data.enemyStat.health * gameLevelCorrection * userData.stageHP;
        health = data.enemyStat.health * gameLevelCorrection * userData.stageHP;
        damage = data.enemyStat.damage * gameLevelCorrection * userData.stageATK;
        armor = data.enemyStat.armor;
        PhysicsRes = data.enemyStat.PhysicsRes;
        PyroRes = data.enemyStat.PyroRes;
        HydroRes = data.enemyStat.HydroRes;
        AnemoRes = data.enemyStat.AnemoRes;
        DendroRes = data.enemyStat.DendroRes;
        ElectroRes = data.enemyStat.ElectroRes;
        CyroRes = data.enemyStat.CyroRes;
        GeoRes = data.enemyStat.GeoRes;
        damageTime = 0;
        isNodamage = false;
        player = null;
    }

    protected virtual void Update()
    {
        OnStayDamage();
        OnStaySkill();
    }

    protected virtual void CheckExtendDamage(SkillData.ParameterWithKey parameterWithKey)
    {
        if (parameterWithKey.parameter.extendDamageDictionary != null)
        {
            foreach (KeyValuePair<SkillName, float> extendDamage in parameterWithKey.parameter.extendDamageDictionary)
            {
                SkillData.ParameterWithKey extendParameter = GameManager.instance.skillData.skills[extendDamage.Key];
                Element.Type elementType = extendParameter.parameter.type;
                if (extendParameter.changeElementType != Element.Type.Physics)
                {
                    elementType = extendParameter.changeElementType;
                }
                float damageResult = GameManager.instance.statCalcuator.CalcDamage(this, extendParameter, extendDamage.Value, elementType);

                ReceiveDamage(extendDamage.Key, damageResult, elementType);
                if (damageAttach != null)
                {
                    damageAttach.WriteDamage(transform, damageResult, Element.Color(elementType));
                }
            }
        }
    }

    public void ReceiveDamage(Skill skill)
    {
        SkillData.ParameterWithKey parameterWithKey = skill.parameterWithKey;
        float skillDamage = GameManager.instance.statCalcuator.CalcDamage(this, parameterWithKey, skill.Damage, skill.elementType);

        Skill_Parameter parameter = parameterWithKey.parameter;
        if (damageAttach != null)
        {
            damageAttach.WriteDamage(transform, skillDamage, Element.Color(skill.elementType));
        }
        ReceiveDamage(parameterWithKey.name, skillDamage, skill.elementType);
        CheckExtendDamage(parameterWithKey);
    }

    public virtual void ReceiveDamage(SkillName skillName, float damage, Element.Type elementType)
    {
        GameManager.instance.battleResult.UpdateDamage(skillName, damage);
        if (GameManager.instance.skillData.skills[skillName].parameter.isDebuffable)
        {
            enemyDebuff.AddDebuff(skillName);
        }
        health -= damage;
        LiveCheck();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnterSkill(collision);
        OnEnterDamage(collision);
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
    }

    void OnEnterSkill(Collider2D collision)
    {
        if (!collision.CompareTag("Skill") || !isLive) return;
        Skill skill = collision.GetComponent<Skill>();
        int instanceId = collision.gameObject.GetInstanceID();
        ReceiveDamage(skill);

        if (skill.skillSequence.isContinueDamage)
        {
            damageTimers.AddOrUpdate(instanceId, 0f);
            skills.AddOrUpdate(instanceId, skill);
        }
    }

    void OnEnterDamage(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponentInParent<Player>();
            if (isNodamage) return;
            player.Hit(damage, 0.0f, this);
        }
        if (collision.CompareTag("Summon"))
        {
            int coliderId = collision.gameObject.GetInstanceID();
            Summon summon = collision.GetComponent<Summon>();
            summons.AddOrUpdate(coliderId, summon);
            if (isNodamage) return;
            summon.Hit(damage, 0.0f, this);
        }
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

    void OnExitSkill(Collider2D collision)
    {
        if (!collision.CompareTag("Skill") || !isLive) return;
        int instanceId = collision.gameObject.GetInstanceID();
        damageTimers.Remove(instanceId);
        skills.Remove(instanceId);
    }
    void OnStayDamage()
    {
        damageTime += Time.deltaTime;
        if (isNodamage) return;
        if (damageTime > damageTimeMax)
        {
            if (player != null)
            {
                player.Hit(damage, 0.0f, this);
            }
            foreach (KeyValuePair<int, Summon> summon in summons)
            {
                summon.Value.Hit(damage, 0.0f, this);
            }
            damageTime = 0;
        }
    }

    void OnStaySkill()
    {
        foreach (KeyValuePair<int, Skill> skill in skills)
        {
            damageTimers[skill.Key] += Time.deltaTime;
            if (damageTimers[skill.Key] >= skill.Value.parameterWithKey.parameter.skillTick * GameManager.instance.artifactData.Resolution_of_Sojourner())
            {
                ReceiveDamage(skill.Value);

                damageTimers[skill.Key] = 0.0f; // 타이머를 리셋합니다.
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        OnExitDamage(collision);
        OnExitSkill(collision);
    }

    protected virtual void LiveCheck()
    {
    }

    protected virtual void Dead()
    {
        gameObject.SetActive(false);
    }
}
