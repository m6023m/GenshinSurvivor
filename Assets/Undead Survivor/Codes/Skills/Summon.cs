using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Summon : SkillMoveSet
{
    float health = 9999;
    bool noDamage = false;
    float skillTime = 0;
    float skillCoolTime = 0;

    protected override void Update()
    {
        base.Update();
 
        CheckLive();
        if (parameterWithKey == null || GameManager.instance.IsPause) return;
        Skill_Parameter skillParam = parameterWithKey.parameter;

        skillTime += (1 * Time.deltaTime);
        if (subSkillSequence != null)
        {
            if (skillTime > skillCoolTime)
            {
                skillTime = 0;
                StartSkillSequence(subSkillSequence);
            }
        }
    }


    public override SkillMoveSet Init(SkillData.ParameterWithKey parameterWithKey, SkillSet.SkillSequence skillSequence, TransformValue prevTransform, int index)
    {
        this.skillSequence = skillSequence;
        InitMass(skillSequence);
        base.Init(parameterWithKey, skillSequence, prevTransform, index);
        InitHealth(parameterWithKey, skillSequence);

        return this;
    }

    public override SkillMoveSet SetSubSkill(SkillSet.SkillSequence subSkillSequence)
    {
        base.SetSubSkill(subSkillSequence);
        skillTime = 0;
        skillCoolTime = subSkillSequence.coolTime;
        InitSkillSetting();
        return this;
    }
    public void InitSkillSetting()
    {
        switch (parameterWithKey.name)
        {
            case SkillName.EB_Razor:
                SkillName baseAttackName = GameManager.instance.ownSkills[0].name;
                SkillData.ParameterWithKey baseAttack = GameManager.instance.skillData.Get(baseAttackName);
                skillCoolTime = baseAttack.parameter.coolTime;
                break;
        }
    }

    void InitMass(SkillSet.SkillSequence skillSequence)
    {
        GetComponent<Collider2D>().isTrigger = !skillSequence.isMass; //질량을 가지는 소환물이면 트리거를 Off 함
        GetComponent<Rigidbody2D>().bodyType = skillSequence.isMass ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;//질량을 가지는 소환물이면 고정, 아니면 플레이어를 따라다님
    }

    void InitHealth(SkillData.ParameterWithKey parameterWithKey, SkillSet.SkillSequence skillSequence)
    {
        health = 9999;
        switch (parameterWithKey.name)
        {
            case SkillName.E_Ninguang:
                health = player.stat.hp * 0.8f;
                break;
        }
    }

    IEnumerator NoDamage(float noDamageTime)
    {
        yield return new WaitForSecondsRealtime(noDamageTime);
        noDamage = false;
    }

    public void Hit(float damage, float noDamageTime, Enemy enemy)
    {
        if (skillSequence.isImmune || noDamage) return;
        ReceiveDamage(damage, noDamageTime);
    }

    void ReceiveDamage(float damage, float noDamageTime)
    {
        health -= damage;
        noDamage = true;
        StartCoroutine(NoDamage(noDamageTime));
    }

    void CheckLive()
    {
        if (health < 0)
        {
            gameObject.SetActive(false);
        }
    }
}