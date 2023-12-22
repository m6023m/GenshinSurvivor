using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class SkillObject : PoolingObject
{
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
    public SkillData.ParameterWithKey parameterWithKey;
    List<TransformValue> currentTransforms;
    bool isSkillActivated = false;

    private void Update()
    {
        if (parameterWithKey == null || GameManager.instance.IsPause || isSkillActivated) return;
        Skill_Parameter skillParam = parameterWithKey.parameter;
        float elementGaugeMax = skillParam.elementGaugeMax;

        if (GameManager.instance.artifactData.Reminiscence_of_Shime())
        {
            elementGaugeMax += 15;
        }

        skillTime += (1 * Time.deltaTime);
        if (skillParam.elementGauge > 9999)
        {
            skillTime = 9999;
        }
        float coolTime = skillParam.coolTime;
        coolTime -= skillParam.coolTime * GameManager.instance.statCalculator.CooltimeWithArtifact(parameterWithKey.type);
        if (coolTime <= 0) coolTime = 0.02f;
        if (skillTime >= coolTime)
        {
            if (parameterWithKey.type == Skill.Type.Burst && skillParam.elementGauge < elementGaugeMax) return;
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
                * GameManager.instance.statCalculator.Duration
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
        SkillData.ParameterWithKey skill = GameManager.instance.skillData.skills[parameterWithKey.name];
        foreach (UnityAction action in skill.skillStartListener)
        {
            action.Invoke();
        }
        if (skill.type == Skill.Type.Basic)
        {
            SkillData.ParameterWithKey skillEulaBurst = GameManager.instance.skillData.skills[SkillName.EB_Eula];
            GameManager.instance.statBuff.eulaStack++;
            if (skillEulaBurst.constellations.num5)
            {
                GameManager.instance.statBuff.eulaStack++;
            }
        }
        switch (parameterWithKey.name)
        {
            case SkillName.E_Miko:
                GameManager.instance.statBuff.EB_Miko_Stack++;
                break;
            case SkillName.E_Shenhe:
                GameManager.instance.statBuff.E_Shenhe_Stack = 7;
                break;
        }
    }

    void InvokeEndListeners()
    {
        SkillData.ParameterWithKey skill = GameManager.instance.skillData.skills[parameterWithKey.name];
        foreach (UnityAction action in skill.skillEndListener)
        {
            action.Invoke();
        }
        switch (parameterWithKey.name)
        {
            case SkillName.EB_Miko:
                GameManager.instance.statBuff.EB_Miko_Stack = 0;
                break;
            case SkillName.E_Heizo:
                GameManager.instance.statBuff.E_Heizo_Stack = 0;
                break;
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
                SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
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
            area *= parameterWithKey.parameter.area * GameManager.instance.statCalculator.Area;
        }
        if (parameterWithKey.name == SkillName.EB_Eula && skillSequence.isConditionChange)
        {
            float areaPer = 1.0f + (GameManager.instance.statBuff.eulaStack * 0.05f);
            area *= areaPer;
        }
        float magentArea = parameterWithKey.parameter.magnet * GameManager.instance.statCalculator.Magnet;

        int skillCount = skillSequence.skillCount;
        if (skillCount <= 0) skillCount = 1;
        if (skillSequence.isSkillAdd)
        {
            skillCount += parameterWithKey.parameter.count;
            skillCount += (int)GameManager.instance.statCalculator.Amount;
        }

        for (int i = 0; i < skillCount; i++)
        {
            int idx = i;

            SkillMoveSet skill = null;

            switch (skillSequence.objectType)
            {
                case Skill.ObjectType.Skill:
                    skill = GameManager.instance.poolManager.GetObject<Skill>();
                    break;
                case Skill.ObjectType.Buff:
                    skill = GameManager.instance.poolManager.GetObject<Buff>();
                    break;
                case Skill.ObjectType.Sheild:
                    skill = GameManager.instance.poolManager.GetObject<Sheild>();
                    break;
                case Skill.ObjectType.Summon:
                    skill = GameManager.instance.poolManager.GetObject<Summon>();
                    break;
                case Skill.ObjectType.SkillEffect:
                    skill = GameManager.instance.poolManager.GetObject<SkillEffect>();
                    break;
            }


            if (skill != null)
            {

                CapsuleCollider2D skillEffectArea = skill.GetComponentInChildren<CapsuleCollider2D>();
                skill.gameObject.SetActive(true);
                skill.transform.parent = transform;
                if (skillSequence.layerOrder != 0)
                {
                    skill.spriteRenderer.sortingOrder = skillSequence.layerOrder;
                }
                else
                {
                    skill.spriteRenderer.sortingOrder = 800;
                }


                skillEffectArea.size = new Vector2(magentArea, magentArea);
                skill.transform.localScale = new Vector2(area, area);

                TransformValue currentTransform = new TransformValue();
                if (currentTransforms.Count > idx)
                {
                    currentTransform = currentTransforms[idx];
                }


                if (!gameObject.activeInHierarchy) return;
                skill.Init(parameterWithKey, skillSequence, currentTransform, idx)
                .AddEndListener(() => AddCurrentTransform(skill.transform, skillSequence, idx));

                AddCurrentTransform(skill.transform, skillSequence, idx);
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

    public void AddSkillTime(float time)
    {
        skillTime += time;
    }
}