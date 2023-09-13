using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class SkillObject : MonoBehaviour
{
    SkillOwner skillOwner;
    private Transform nearestTarget;
    List<GameObject> _skillObjects;
    List<GameObject> skillObjects
    {
        get
        {
            if (_skillObjects == null) _skillObjects = new List<GameObject>();
            return _skillObjects;
        }
    }

    public float skillTime = 0;
    SkillData.ParameterWithKey parameterWithKey;
    List<TransformValue> currentTransforms;
    bool isSkillActivated = false;

    private void Update()
    {
        if (parameterWithKey == null || GameManager.instance.IsPause || isSkillActivated) return;
        Skill_Parameter skillParam = parameterWithKey.parameter;

        skillTime += (1 * Time.deltaTime);
        if (skillParam.elementGauge > 9999)
        {
            skillTime = 9999;
        }
        float coolTime = skillParam.coolTime;
        coolTime -= skillParam.coolTime * GameManager.instance.statCalcuator.CooltimeWithArtifact(parameterWithKey.type);
        if (coolTime <= 0) coolTime = 0.02f;
        if (skillTime >= coolTime)
        {
            if (parameterWithKey.type == Skill.Type.Burst && skillParam.elementGauge < skillParam.elementGaugeMax) return;
            skillTime = 0;
            skillParam.elementGauge = 0;
            StartCoroutine(SkillCast());
        }
    }

    public void SkillCastAbsolute()
    {
        skillTime = 0;
        StartCoroutine(SkillCast());
    }

    IEnumerator SkillCast()
    {
        float sequenceTime = 0;
        parameterWithKey.changeElementType = Element.Type.Physics;
        if (parameterWithKey.type != Skill.Type.Weapon)
        {
            AudioManager.instance.PlaySFX(AudioManager.SFX.Melee);
        }
        InvokeStartListeners();
        StartCoroutine(CheckArtifact());
        StartCoroutine(CheckWeapon());

        if (parameterWithKey.type == Skill.Type.Skill)
        {
            GameDataManager.instance.saveData.record.useSkillCount++;
        }
        else if (parameterWithKey.type == Skill.Type.Burst)
        {
            GameDataManager.instance.saveData.record.useBurstCount++;
        }

        Vector2 targetDir = Vector2.zero;
        if (skillOwner == null)
        {
            skillOwner = GetComponentInParent<SkillOwner>();
        }

        if (skillOwner.scanner.nearestTarget != null)
        {
            Vector3 targetPos = skillOwner.scanner.nearestTarget.position;
            targetDir = targetPos - skillOwner.transform.position;
            targetDir = targetDir.normalized;
        }
        yield return null;
        if (parameterWithKey.skillSet == null) yield break;
        for (int i = 0; i < parameterWithKey.skillSet.sequences.Count; i++)
        {
            SkillSet.SkillSequence skillSequence = parameterWithKey.skillSet.sequences[i];
            if (skillSequence.duration == -1)
            {
                isSkillActivated = true;
            }
            StartSkillSequence(skillSequence, null);

            AnimationClip animationClip = skillSequence.animation;
            float duration = animationClip.length;
            duration = duration / skillSequence.animationSpeed;
            if (skillSequence.duration > 0)
            {
                duration = skillSequence.duration
                * GameManager.instance.statCalcuator.Duration
                * parameterWithKey.parameter.duration;
            }
            if (skillSequence.isContinue)
            {
                duration = 0;
            }
            if (skillSequence.delay > 0)
            {
                duration += skillSequence.delay;
            }
            sequenceTime = 0;
            while (true)
            {
                if (sequenceTime > duration) break;
                if (!GameManager.instance.IsPause)
                {
                    sequenceTime += (1 * Time.deltaTime);
                }
                yield return null;
            }
        }
        RemoveElementalTrans();
        InvokeEndListeners();
    }

    void RemoveElementalTrans()
    {
        parameterWithKey.changeElementType = Element.Type.Anemo;
        parameterWithKey.parameter.RemoveExtendDamage(parameterWithKey.name);
    }

    IEnumerator CheckArtifact()
    {
        if (parameterWithKey.type != Skill.Type.Skill) yield break;
        yield return new WaitForSecondsRealtime(1.0f);
        if (GameManager.instance.artifactData.Gambler())
        {
            skillTime = 9999;
        }
    }
    IEnumerator CheckWeapon()
    {
        if (parameterWithKey.type != Skill.Type.Skill) yield break;
        yield return new WaitForSecondsRealtime(2.0f);
        Player player = GameManager.instance.player;

        foreach (Weapon weapon in player.weapons)
        {
            if (weapon.weaponAction != null)
            {
                if (weapon.weaponAction())
                {
                    skillTime = 9999;
                }
            }
        }
    }
    void InvokeStartListeners()
    {
        SkillData.ParameterWithKey skill = GameManager.instance.skillData.Get(parameterWithKey.name);
        foreach (UnityAction action in skill.skillStartListener)
        {
            action.Invoke();
        }
        if (skill.type == Skill.Type.Basic)
        {
            SkillData.ParameterWithKey skillEulaBurst = GameManager.instance.skillData.Get(SkillName.EB_Eula);
            GameManager.instance.statBuff.eulaStack++;
            if (skillEulaBurst.constellations.num5)
            {
                GameManager.instance.statBuff.eulaStack++;
            }
        }
    }

    void InvokeEndListeners()
    {
        SkillData.ParameterWithKey skill = GameManager.instance.skillData.Get(parameterWithKey.name);
        foreach (UnityAction action in skill.skillEndListener)
        {
            action.Invoke();
        }
    }

    public void Init(SkillData.ParameterWithKey parameterWithKey)
    {
        this.parameterWithKey = parameterWithKey;
        currentTransforms = new List<TransformValue>();
    }

    void StartSkillSequence(SkillSet.SkillSequence skillSequence, SkillSet.SkillSequence subSkillSequence)
    {
        if (!CheckSkillCondition(skillSequence)) return;
        CreateObjects(skillSequence, subSkillSequence);
    }

    bool CheckSkillCondition(SkillSet.SkillSequence skillSequence)
    {
        bool condition = !skillSequence.isConditionChange;
        switch (parameterWithKey.name)
        {
            case SkillName.EB_Tartaglia:
                SkillData.ParameterWithKey baseAttack = GameManager.instance.ownSkills[0];
                bool isMelee =
                baseAttack.name == SkillName.Basic_Sword
                 || baseAttack.name == SkillName.Basic_Claymore
                 || baseAttack.name == SkillName.Basic_Spear;
                condition = isMelee == skillSequence.isConditionChange;
                break;
            case SkillName.E_Eula:
                if (skillSequence.isConditionChange)
                {
                    if (GameManager.instance.statBuff.eulaSkillStack >= 5)
                    {
                        GameManager.instance.statBuff.eulaSkillStack = 0;
                        condition = true;
                    }
                    else if (GameManager.instance.statBuff.eulaSkillStack >= 4)
                    {
                        GameManager.instance.statBuff.eulaSkillStack++;
                        condition = true;
                    }
                    else
                    {
                        GameManager.instance.statBuff.eulaSkillStack++;
                    }
                }
                break;
            case SkillName.EB_Eula:
                condition = true;
                break;
            case SkillName.EB_Yanfei:
                if (parameterWithKey.constellations.num3)
                {
                    condition = true;
                }
                break;
            case SkillName.Weapon_Claymore_Skyward_Pride:
                condition = GameManager.instance.statBuff.Claymore_Skyward_PrideStack > 0;
                break;
        }
        return condition;
    }


    private void CreateObjects(SkillSet.SkillSequence skillSequence, SkillSet.SkillSequence subSkillSequence)
    {
        float area = skillSequence.skillSize;
        if (skillSequence.objectType != Skill.ObjectType.Sheild &&
         skillSequence.objectType != Skill.ObjectType.Summon)
        {
            area *= parameterWithKey.parameter.area * GameManager.instance.statCalcuator.Area;
        }
        if (parameterWithKey.name == SkillName.EB_Eula && skillSequence.isConditionChange)
        {
            float areaPer = 1.0f + (GameManager.instance.statBuff.eulaStack * 0.05f);
            area *= areaPer;
        }
        float magentArea = parameterWithKey.parameter.magnet * GameManager.instance.statCalcuator.Magnet;

        int skillCount = skillSequence.skillCount;
        if (skillCount <= 0) skillCount = 1;
        if (skillSequence.isSkillAdd)
        {
            skillCount += parameterWithKey.parameter.count;
            skillCount += (int)GameManager.instance.statCalcuator.Amount;
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

            Transform skill = GetSkillObject(objectType);

            if (skill != null)
            {

                CapsuleCollider2D skillEffectArea = skill.GetComponentInChildren<CapsuleCollider2D>();
                skill.gameObject.SetActive(true);
                skill.parent = transform;
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

                TransformValue currentTransform = new TransformValue();
                if (currentTransforms.Count > idx)
                {
                    currentTransform = currentTransforms[idx];
                }


                switch (skillSequence.objectType)
                {
                    case Skill.ObjectType.Skill:
                        if (!gameObject.activeInHierarchy) return;
                        skill.GetComponent<Skill>()
                        .Init(parameterWithKey, skillSequence, currentTransform, idx)
                        .AddEndListener(() => AddCurrentTransform(skill, skillSequence, idx));
                        break;
                    case Skill.ObjectType.Buff:
                        if (!gameObject.activeInHierarchy) return;
                        skill.GetComponent<Buff>()
                        .Init(parameterWithKey, skillSequence, currentTransform, idx)
                        .AddEndListener(() => AddCurrentTransform(skill, skillSequence, idx));
                        break;
                    case Skill.ObjectType.Sheild:
                        if (!gameObject.activeInHierarchy) return;
                        skill.GetComponent<Sheild>()
                        .Init(parameterWithKey, skillSequence, currentTransform, idx)
                        .AddEndListener(() => AddCurrentTransform(skill, skillSequence, idx));
                        break;
                    case Skill.ObjectType.Summon:
                        if (!gameObject.activeInHierarchy) return;
                        skill.GetComponent<Summon>()
                        .Init(parameterWithKey, skillSequence, currentTransform, idx)
                        .AddEndListener(() => AddCurrentTransform(skill, skillSequence, idx));
                        break;
                    case Skill.ObjectType.SkillEffect:
                        skill.GetComponent<SkillEffect>()
                        .Init(parameterWithKey, skillSequence, currentTransform, idx)
                        .AddEndListener(() => AddCurrentTransform(skill, skillSequence, idx));
                        break;
                }

                AddCurrentTransform(skill, skillSequence, idx);
            }

        }
    }

    void AddCurrentTransform(Transform skill, SkillSet.SkillSequence skillSequence, int idx)
    {
        if (!skillSequence.isSequence)
        {
            if (currentTransforms.Count <= idx)
            {
                currentTransforms.Add(skill.CopyTransformValue());
            }
            else
            {
                currentTransforms[idx] = skill.CopyTransformValue();
            }
        }
    }


    private Transform GetSkillObject(PoolManager.Type type)
    {
        Transform skillObject = GameManager.instance.poolManager.Get(type, skillObjects).transform;
        skillObjects.Add(skillObject.gameObject);
        return skillObject;
    }


}