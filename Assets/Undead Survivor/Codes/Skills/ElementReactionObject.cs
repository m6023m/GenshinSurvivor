using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class ElementReactionObject : MonoBehaviour
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

    float skillTime = 0;
    float sequenceTime = 0;
    SkillData.ParameterWithKey parameterWithKey;
    Vector3 currentSkillPosition = Vector3.zero;
    bool isSkillActivated = false;
    float reactedDamage;
    Enemy parent;


    IEnumerator SkillCast(Element.Type elementType)
    {
        if (parent != null && parent.isLive)
        {
            GameManager.instance.damageManager.WriteReaction(parent.transform, parameterWithKey, elementType);
        }
        AudioManager.instance.PlaySFX(AudioManager.SFX.Melee);
        GameDataManager.instance.saveData.record.useElementReactionCount++;
        Vector2 targetDir = Vector2.zero;

        yield return null;
        if (parameterWithKey.skillSet == null) yield break;
        for (int i = 0; i < parameterWithKey.skillSet.sequences.Count; i++)
        {
            SkillSet.SkillSequence skillSequence = parameterWithKey.skillSet.sequences[i];
            skillSequence.elementType = elementType;
            if (skillSequence.duration == -1)
            {
                isSkillActivated = true;
            }
            if (skillSequence.objectType == Skill.ObjectType.Summon)
            {
                SkillSet.SkillSequence subSkillSequence = parameterWithKey.skillSet.sequences[i + 1];
                StartSkillSequence(skillSequence, subSkillSequence);
            }
            else
            {
                if (!skillSequence.isSummonAttack)
                {
                    StartSkillSequence(skillSequence, null);
                }
            }
            AnimationClip animationClip = skillSequence.animation;
            float duration = animationClip.length;
            duration = duration / skillSequence.animationSpeed;
            if (skillSequence.duration > 0)
            {
                duration = skillSequence.duration
                * GameManager.instance.statCalcuator.Duration
                * parameterWithKey.parameter.duration;
            }
            if (skillSequence.delay > 0)
            {
                duration += skillSequence.delay;
            }
            sequenceTime = 0;
            while (true)
            {
                if (skillSequence.isContinue)
                {
                    break;
                }
                else if (sequenceTime > duration)
                {
                    break;
                }
                yield return null;
            }
        }
    }


    public void Init(SkillData.ParameterWithKey parameterWithKey, float damage, Element.Type elementType)
    {
        this.parameterWithKey = parameterWithKey;
        parent = GetComponentInParent<Enemy>();
        this.reactedDamage = damage;
        CheckElementalReraction(elementType);
    }

    void CheckElementalReraction(Element.Type elementType)
    {
        if (parameterWithKey.type != Skill.Type.Reaction) return;
        if (!gameObject.activeInHierarchy) return;
        StartCoroutine(SkillCast(elementType));
    }

    void StartSkillSequence(SkillSet.SkillSequence skillSequence, SkillSet.SkillSequence subSkillSequence)
    {
        CreateObjects(skillSequence, subSkillSequence);
    }


    private void CreateObjects(SkillSet.SkillSequence skillSequence, SkillSet.SkillSequence subSkillSequence)
    {
        float area = parameterWithKey.parameter.area * GameManager.instance.statCalcuator.Area * skillSequence.skillSize;
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

            Transform skill = GetSkillObject(idx, objectType);

            if (skill != null)
            {
                CapsuleCollider2D skillEffectArea = skill.GetComponentInChildren<CapsuleCollider2D>();
                skill.gameObject.SetActive(true);
                skill.SetParent(transform);
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
                        skill.GetComponent<Skill>().ReactedDamage(reactedDamage).Init(parameterWithKey, skillSequence, transform.CopyTransformValue(), idx);
                        break;
                    case Skill.ObjectType.Buff:
                        skill.GetComponent<Buff>().ReactedDamage(reactedDamage).Init(parameterWithKey, skillSequence, transform.CopyTransformValue(), idx);
                        break;
                    case Skill.ObjectType.Sheild:
                        skill.GetComponent<Sheild>().Init(parameterWithKey, skillSequence, transform.CopyTransformValue(), idx);
                        break;
                    case Skill.ObjectType.Summon:
                        skill.GetComponent<Summon>().SetSubSkill(subSkillSequence).Init(parameterWithKey, skillSequence, transform.CopyTransformValue(), idx);
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