using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System.Diagnostics.CodeAnalysis;
using System;

public class Buff : SkillMoveSet
{
    public Vector3 savePosition;
    public Vector3 playerPosition;
    float buffValue0 = 0;
    float buffValue1 = 0;
    float debuffValue0 = 0;
    bool buffTrigger = false;
    float buffTime;
    float reactionTime;
    float reactionTickTime;
    StatBuff statBuff;
    bool isPlayerInside;
    private float timeSinceLastCheck = 0f;
    private const float checkInterval = 1f; 

    Enemy enemy;
    float skillDamage;

    protected override void Update()
    {
        base.Update();
        CheckPlayerInside();
        if (parameterWithKey.type != Skill.Type.Reaction) return;
        reactionTime += (1 * Time.deltaTime);
        reactionTickTime += (1 * Time.deltaTime);
        if (skillSequence.duration == 0) return;
        if (reactionTime > skillSequence.duration) return;
        if (reactionTickTime >= skillSequence.coolTime)
        {
            enemy.ReceiveDamage(parameterWithKey.name, skillDamage, skillSequence.elementType);
            if (enemy.damageAttach != null)
            {
                enemy.damageAttach.WriteDamage(enemy.transform, skillDamage, Element.Color(skillSequence.elementType), 0, 10.0f);
            }

            reactionTickTime = 0;
        }
    }
    void CheckPlayerInside()
    {
        if (isPlayerInside)
        {
            timeSinceLastCheck += Time.deltaTime;
            if (timeSinceLastCheck >= checkInterval)
            {
                CheckPlayerEffect();
                timeSinceLastCheck = 0f;
            }
        }
    }
    void CheckPlayerEffect() {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        switch (parameterWithKey.name)
        {
            case SkillName.EB_Bennet:
                float healthPercentage = player.health / player.maxHealth * 100.0f;
                buffTrigger = healthPercentage > 70;
                if (parameterWithKey.constellations.num5)
                {
                    baseAttack.parameter.SetElementType(baseAttack, Element.Type.Pyro);
                }
                break;
            case SkillName.EB_Diluc:
                baseAttack.parameter.SetElementType(baseAttack, Element.Type.Pyro);
                break;
            case SkillName.E_Keqing:
                baseAttack.parameter.SetElementType(baseAttack, Element.Type.Electro);
                break;
            case SkillName.E_Chongyun:
                baseAttack.parameter.SetElementType(baseAttack, Element.Type.Cyro);
                break;
            case SkillName.EB_Noelle:
                baseAttack.parameter.SetElementType(baseAttack, Element.Type.Geo);
                baseAttack.parameter.isTypeFix = true;
                break;
            case SkillName.E_Tartaglia:
                baseAttack.parameter.SetElementType(baseAttack, Element.Type.Hydro);
                baseAttack.parameter.isTypeFix = true;
                break;
            case SkillName.E_Zhongli:
                statBuff.isResonance = true;
                break;
            case SkillName.EB_Xiao:
                baseAttack.parameter.SetElementType(baseAttack, Element.Type.Anemo);
                baseAttack.parameter.isTypeFix = true;
                if (parameterWithKey.constellations.num5)
                {
                    GameManager.instance.skillData.Get(SkillName.E_Xiao).parameter.coolTime = 1.0f;
                }
                break;
            case SkillName.E_Hutao:
                baseAttack.parameter.SetElementType(baseAttack, Element.Type.Pyro);
                baseAttack.parameter.isTypeFix = true;
                break;
            case SkillName.E_Kazuha:
                GameManager.instance.player.coll.isTrigger = true;
                break;
        }
    }

    void FixedUpdate()
    {
        if (GameManager.instance.IsPause) return;
        if (parameterWithKey.type == Skill.Type.Reaction) return;
        buffTime += (1 * Time.fixedDeltaTime);
        playerPosition = player.transform.position;
        float coolTime = skillSequence.coolTime;

        if (buffTime >= coolTime)
        {
            buffTime = 0;
            StartCoroutine(SkillCast());
        }
        savePosition = transform.position;
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (buffTrigger) return;
        switch (parameterWithKey.name)
        {
            case SkillName.ElectroCharged:
                scanner.GetTargets();
                if (scanner.targetNum == 0) return;
                buffTrigger = true;
                List<Collider2D> hydroTargets = new List<Collider2D>();
                for (int index = 0; index < scanner.targetNum; index++)
                {
                    Collider2D collider = scanner.targets[index];
                    ElementReaction elementReaction = collider.GetComponentInChildren<ElementReaction>();
                    if (elementReaction != null && elementReaction.elementAttach1.elementType == Element.Type.Hydro)
                    {
                        hydroTargets.Add(collider);
                    }
                }
                Transform nearestSecond = scanner.GetNearestSecond(hydroTargets.ToArray());
                if (nearestSecond != null)
                    nearestSecond.GetComponentInChildren<ElementReaction>().AddElement(Element.Type.Electro, 0f);
                break;
        }
    }

    IEnumerator SkillCast()
    {
        yield return null;

        switch (parameterWithKey.name)
        {
            case SkillName.EB_Bennet:
                if (!buffTrigger)
                {
                    player.HealHealth(player.maxHealth / 10.0f * parameterWithKey.parameter.healPer);
                }
                break;
            case SkillName.E_Barbara:
                player.HealHealth(player.maxHealth / 20.0f * parameterWithKey.parameter.healPer);
                break;
            case SkillName.EB_Jean:
                player.HealHealth(GameManager.instance.statCalcuator.Atk / 2.0f * parameterWithKey.parameter.healPer);
                break;
            case SkillName.E_Qiqi:
                player.HealHealth(GameManager.instance.statCalcuator.Atk * 1.0f * parameterWithKey.parameter.healPer);
                break;
            case SkillName.EB_Diona:
                player.HealHealth(GameManager.instance.statCalcuator.Health * 0.1f * parameterWithKey.parameter.healPer);
                break;
        }
    }
    public override SkillMoveSet Init(SkillData.ParameterWithKey parameterWithKey, SkillSet.SkillSequence skillSequence, TransformValue prevTransform, int index)
    {
        buffTrigger = false;
        buffValue0 = 0;
        statBuff = GameManager.instance.statBuff;
        this.skillSequence = skillSequence;
        base.Init(parameterWithKey, skillSequence, prevTransform, index);
        if (parameterWithKey.type == Skill.Type.Reaction)
        {
            InitReaction();
        }
        else
        {
            InitBuff();
        }

        return this;
    }

    void InitReaction()
    {
        enemy = GetComponentInParent<Enemy>();
        if (enemy == null || !enemy.isLive) return;
        float reactionDamage = GameManager.instance.statCalcuator.ReactionDamage(parameterWithKey, reactedDamage, skillSequence.elementType);
        skillDamage = GameManager.instance.statCalcuator.CalcDamage(enemy, parameterWithKey, reactionDamage, skillSequence.elementType);
        switch (parameterWithKey.name)
        {
            case SkillName.Vaporize:
            case SkillName.Melt:
                enemy.ReceiveDamage(parameterWithKey.name, skillDamage, skillSequence.elementType);
                if (enemy.damageAttach != null)
                {
                    enemy.damageAttach.WriteDamage(enemy.transform, skillDamage, Element.Color(skillSequence.elementType), 0, 10.0f);
                }
                break;
        }

    }

    void InitBuff()
    {
        OnSkillStart();
    }


    void Disable()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }


    void OnSkillStart()
    {
        float buffValue0 = 0;
        float buffValue1 = 0;
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        switch (parameterWithKey.name)
        {
            case SkillName.E_Barbara:
                if (parameterWithKey.constellations.num1)
                {
                    buffValue1 = 0.15f;
                    this.buffValue1 = buffValue1;
                    statBuff.hydroDmg += buffValue1;
                }
                break;
            case SkillName.EB_Barbara:
                player.HealHealth(player.maxHealth / 5.0f * parameterWithKey.parameter.healPer);
                break;
            case SkillName.EB_Razor:
                buffValue0 = baseAttack.parameter.coolTime * 0.4f;
                this.buffValue0 = buffValue0;//일반공격 쿨타임 40프로 감소
                baseAttack.parameter.coolTime -= this.buffValue0;
                break;
            case SkillName.EB_Qiqi:
                player.HealHealth(GameManager.instance.statCalcuator.Atk * 3.0f * parameterWithKey.parameter.healPer);
                break;
            case SkillName.EB_Noelle:
                buffValue0 = GameManager.instance.statCalcuator.Armor * 0.7f;
                this.buffValue0 = buffValue0;//방어력의 70%만큼 공격력 증가
                statBuff.atk += this.buffValue0;
                baseAttack.parameter.area += 0.5f;
                break;
            case SkillName.EB_Beidou:
                baseAttack.parameter.AddExtendDamage(SkillName.EB_Beidou, parameterWithKey.parameter.damage);
                break;
            case SkillName.E_Tartaglia:
                buffValue0 = baseAttack.parameter.coolTime * 0.05f * parameterWithKey.level;
                this.buffValue0 = buffValue0;//일반공격 쿨타임 스킬 레벨 X %5 프로 감소
                baseAttack.parameter.coolTime -= buffValue0;
                break;
            case SkillName.E_Xingqiu:
                player.AddHitQueue(() =>
                {
                    Disable();
                    return 0.8f;
                });
                break;
            case SkillName.EB_Xingqiu:
                player.AddHitQueue(() =>
                {
                    return 0.8f;
                });
                break;
            case SkillName.E_Hutao:
                float loseHp = GameManager.instance.player.health * 0.3f;
                GameManager.instance.player.health -= loseHp;
                buffValue0 = GameManager.instance.statCalcuator.Health * 0.02f;
                if (GameManager.instance.player.health / GameManager.instance.statCalcuator.Health <= 0.5f)
                {
                    buffValue0 *= 1.5f;
                }
                this.buffValue0 = buffValue0;//체력의 5% 만큼 공격력 증가
                statBuff.atk += this.buffValue0;
                break;
            case SkillName.EB_Rosaria:
                statBuff.luck += 3.0f;
                break;
            case SkillName.E_Yanfei:
                float damageResult = baseAttack.parameter.damage / 3.0f;
                if (parameterWithKey.constellations.num5)
                {
                    damageResult *= 1.5f;
                }
                baseAttack.parameter.AddExtendDamage(SkillName.E_Yanfei, damageResult);
                break;
            case SkillName.EB_Yanfei:
                baseAttack.parameter.damage += 0.2f;
                break;
            case SkillName.EB_Eula:
                statBuff.eulaStack = 0;
                if (parameterWithKey.constellations.num5)
                {
                    statBuff.eulaStack = 5;
                }
                break;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
        OnTriggerEnterPlayer(collider);
        OnTriggerEnterEnemy(collider);
    }
    void OnTriggerEnterPlayer(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;

        isPlayerInside = true;
        float buffValue0 = 0;
        float buffValue1 = 0;
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        switch (parameterWithKey.name)
        {
            case SkillName.EB_Bennet:
                float healthPercentage = player.health / player.maxHealth * 100.0f;
                buffValue0 += player.stat.atk;
                this.buffValue0 = buffValue0;
                statBuff.atk += this.buffValue0;
                if (parameterWithKey.constellations.num5)
                {
                    buffValue1 = 0.15f;
                    this.buffValue1 = buffValue1;
                    statBuff.pyroDmg += buffValue1;
                }
                break;
            case SkillName.E_Chongyun:
                buffValue0 += baseAttack.parameter.coolTime * 0.08f;
                this.buffValue0 = buffValue0;//일반공격 쿨타임 8프로 감소
                baseAttack.parameter.coolTime -= this.buffValue0;
                break;
            case SkillName.EB_Jean:
                if (parameterWithKey.constellations.num5)
                {
                    buffValue0 = 3.0f;
                    statBuff.armor += buffValue0;
                }
                break;
            case SkillName.EB_Diona:
                if (parameterWithKey.constellations.num3)
                {
                    buffValue0 += baseAttack.parameter.coolTime * 0.2f;
                    this.buffValue0 = buffValue0;//일반공격 쿨타임 20프로 감소
                    baseAttack.parameter.coolTime -= this.buffValue0;
                }
                if (parameterWithKey.constellations.num5)
                {
                    statBuff.heal += 0.3f;
                    statBuff.elementMastery += 200;
                }
                break;
            case SkillName.E_Albedo:
                if (parameterWithKey.constellations.num1)
                {
                    statBuff.armor += 3.0f;
                }
                if (parameterWithKey.constellations.num5)
                {
                    statBuff.cooltime += 0.2f;
                }
                break;
            case SkillName.EB_Travler_Geo:
                statBuff.luck += 3;
                break;
        }
    }
    void OnTriggerEnterEnemy(Collider2D collider)
    {
        float debuffValue0 = 0;
        if (!collider.CompareTag("Enemy")) return;
        Enemy enemy = collider.GetComponent<Enemy>();
        switch (parameterWithKey.name)
        {
            case SkillName.EB_Jean:
                if (parameterWithKey.constellations.num3)
                {
                    debuffValue0 = 0.4f;
                    enemy.AnemoRes -= debuffValue0;
                }
                break;
            case SkillName.E_Zhongli:
                debuffValue0 = 0.2f;
                enemy.PhysicsRes -= debuffValue0;
                enemy.PyroRes -= debuffValue0;
                enemy.HydroRes -= debuffValue0;
                enemy.AnemoRes -= debuffValue0;
                enemy.DendroRes -= debuffValue0;
                enemy.ElectroRes -= debuffValue0;
                enemy.CyroRes -= debuffValue0;
                enemy.GeoRes -= debuffValue0;
                break;
        }
    }

    protected override void OnTriggerExit2D(Collider2D collider)
    {
        base.OnTriggerExit2D(collider);
        OnTriggerExitPlayer(collider);
        OnTriggerExitEnemy(collider);
    }

    void OnTriggerExitPlayer(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;
        isPlayerInside = false;
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        switch (parameterWithKey.name)
        {
            case SkillName.EB_Bennet:
                float healthPercentage = player.health / player.maxHealth * 100.0f;
                statBuff.atk -= buffValue0;

                if (parameterWithKey.constellations.num5)
                {
                    statBuff.pyroDmg -= buffValue1;
                    baseAttack.parameter.SetElementType(baseAttack, Element.Type.Physics);
                }
                break;
            case SkillName.E_Chongyun:
                baseAttack.parameter.SetElementType(baseAttack, Element.Type.Physics);
                baseAttack.parameter.coolTime += buffValue0;
                break;
            case SkillName.EB_Jean:
                if (parameterWithKey.constellations.num5)
                {
                    statBuff.armor -= buffValue0;
                }
                break;
            case SkillName.EB_Diona:
                if (parameterWithKey.constellations.num3)
                {
                    baseAttack.parameter.coolTime += buffValue0;
                }
                if (parameterWithKey.constellations.num5)
                {
                    statBuff.heal -= 0.3f;
                    statBuff.elementMastery -= 200;
                }
                break;
            case SkillName.E_Albedo:
                if (parameterWithKey.constellations.num1)
                {
                    statBuff.armor -= 3.0f;
                }
                if (parameterWithKey.constellations.num5)
                {
                    statBuff.cooltime -= 0.2f;
                }
                break;
            case SkillName.E_Zhongli:
                statBuff.isResonance = false;
                break;
            case SkillName.EB_Travler_Geo:
                statBuff.luck -= 3;
                break;
        }
    }

    void OnTriggerExitEnemy(Collider2D collider)
    {
        if (!collider.CompareTag("Enemy")) return;
        Enemy enemy = collider.GetComponent<Enemy>();
        switch (parameterWithKey.name)
        {
            case SkillName.EB_Jean:
                if (parameterWithKey.constellations.num3)
                {
                    enemy.AnemoRes += debuffValue0;
                }
                break;
            case SkillName.E_Zhongli:
                enemy.PhysicsRes += debuffValue0;
                enemy.PyroRes += debuffValue0;
                enemy.HydroRes += debuffValue0;
                enemy.AnemoRes += debuffValue0;
                enemy.DendroRes += debuffValue0;
                enemy.ElectroRes += debuffValue0;
                enemy.CyroRes += debuffValue0;
                enemy.GeoRes += debuffValue0;
                break;
        }
    }

    void OnSkillEnd()
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        switch (parameterWithKey.name)
        {
            case SkillName.EB_Diluc:
                baseAttack.parameter.SetElementType(baseAttack, Element.Type.Physics);
                break;
            case SkillName.E_Barbara:
                if (parameterWithKey.constellations.num1)
                {
                    statBuff.hydroDmg -= buffValue1;
                }
                break;
            case SkillName.E_Keqing:
                baseAttack.parameter.SetElementType(baseAttack, Element.Type.Physics);
                player.transform.position = savePosition;
                break;
            case SkillName.EB_Razor:
                baseAttack.parameter.coolTime += buffValue0;
                break;
            case SkillName.EB_Noelle:
                baseAttack.parameter.isTypeFix = false;
                baseAttack.parameter.SetElementType(baseAttack, Element.Type.Physics);
                statBuff.atk -= buffValue0;
                baseAttack.parameter.area -= 0.5f;
                break;
            case SkillName.E_Tartaglia:
                baseAttack.parameter.isTypeFix = false;
                baseAttack.parameter.SetElementType(baseAttack, Element.Type.Physics);
                baseAttack.parameter.coolTime += buffValue0;
                break;
            case SkillName.EB_Beidou:
                baseAttack.parameter.RemoveExtendDamage(SkillName.EB_Beidou);
                break;
            case SkillName.E_Xingqiu:
                player.HealHealth(player.maxHealth * 3.0f / 100.0f * parameterWithKey.parameter.healPer);
                break;
            case SkillName.EB_Xingqiu:
                player.HealHealth(player.maxHealth * 3.0f / 100.0f * parameterWithKey.parameter.healPer);
                break;
            case SkillName.EB_Xiao:
                baseAttack.parameter.SetElementType(baseAttack, Element.Type.Anemo);
                baseAttack.parameter.isTypeFix = true;
                if (parameterWithKey.constellations.num5)
                {
                    GameManager.instance.skillData.Get(SkillName.E_Xiao).parameter.coolTime = 6.4f;
                }
                break;
            case SkillName.E_Hutao:
                baseAttack.parameter.isTypeFix = false;
                baseAttack.parameter.SetElementType(baseAttack, Element.Type.Physics);
                statBuff.atk -= buffValue0;
                break;
            case SkillName.EB_Rosaria:
                statBuff.luck -= 3.0f;
                break;
            case SkillName.E_Yanfei:
                baseAttack.parameter.RemoveExtendDamage(SkillName.E_Yanfei);
                break;
            case SkillName.EB_Yanfei:
                baseAttack.parameter.damage -= 0.2f;
                break;
            case SkillName.E_Kazuha:
                GameManager.instance.player.coll.isTrigger = false;
                break;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnSkillEnd();
    }
}