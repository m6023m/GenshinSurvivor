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
            GameManager.instance.damageAttach.WriteReaction(parent.transform, parameterWithKey, elementType);
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
                * GameManager.instance.statCalculator.Duration
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
        float area = parameterWithKey.parameter.area * GameManager.instance.statCalculator.Area * skillSequence.skillSize;
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
                skill.transform.SetParent(transform);
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


                switch (skillSequence.objectType)
                {
                    case Skill.ObjectType.Skill:
                    case Skill.ObjectType.Buff:
                        skill.ReactedDamage(reactedDamage).Init(parameterWithKey, skillSequence, transform.CopyTransformValue(), idx);
                        break;
                    case Skill.ObjectType.Sheild:
                    case Skill.ObjectType.Summon:
                    case Skill.ObjectType.SkillEffect:
                        skill.Init(parameterWithKey, skillSequence, transform.CopyTransformValue(), idx);
                        break;
                }
            }

        }
    }

}