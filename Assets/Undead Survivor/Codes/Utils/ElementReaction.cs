using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElementReaction : MonoBehaviour
{
    public ElementReactionObject elementReactionObject;
    SkillData skillData;
    ElementAttach _elementAttach1;
    ElementAttach _elementAttach2;
    public ElementAttach elementAttach1
    {
        get
        {
            if (_elementAttach1 == null) _elementAttach1 = GetComponentsInChildren<ElementAttach>(true)[0];
            return _elementAttach1;
        }
    }
    public ElementAttach elementAttach2
    {
        get
        {
            if (_elementAttach1 == null) _elementAttach1 = GetComponentsInChildren<ElementAttach>(true)[1];
            return _elementAttach1;
        }
    }

    Enemy _parentEnemy;
    Enemy parentEnemy
    {
        get
        {
            if (_parentEnemy == null) _parentEnemy = GetComponentInParent<Enemy>(true);
            return _parentEnemy;
        }

    }
    Material _parentMaterial;
    Material parentMaterial
    {
        get
        {
            if (_parentMaterial == null) _parentMaterial = parentEnemy.GetComponent<SpriteRenderer>().material;
            return _parentMaterial;
        }
    }
    bool isFrozen = false;
    int frozenMaterialID;
    float frozenTime;
    float frozenTimeMax = 3.0f;
    bool isPetrification = false;
    int petrificationMaterialID;
    float petrificationTime;
    float petrificationTimeMax = 4.0f;

    bool isReaction = false;
    float reactionTime;
    float reactionTimeMax = 1.0f;
    SkillName reactionedSkillName;
    private void Update()
    {
        FrozenTimeCheck();
        ReactionTimeCheck();
    }
    void FrozenTimeCheck()
    {
        if (!isFrozen) return;
        frozenTime += Time.deltaTime;
        if (frozenTime > frozenTimeMax)
        {
            Frozen(0);
            frozenTime = 0;
        }
    }

    void PetrificationTimeCheck()
    {
        if (!isPetrification) return;
        petrificationTime += Time.deltaTime;
        if (petrificationTime > petrificationTimeMax)
        {
            Petrification(0);
            petrificationTime = 0;
        }
    }

    void ReactionTimeCheck()
    {
        if (!isReaction) return;
        reactionTime += Time.deltaTime;
        if (reactionTime > reactionTimeMax)
        {
            elementAttach1.ResetAttach();
            elementAttach2.ResetAttach();
            isReaction = false;
        }
    }


    private void Awake()
    {
        Init();

    }

    public void Init()
    {

        skillData = GameManager.instance.skillData;
        if (parentEnemy.type == Enemy.Type.Boss) return;
        frozenMaterialID = Shader.PropertyToID("_FrozenFade");
        frozenTime = 0;
        petrificationMaterialID = Shader.PropertyToID("_MetalFade");
        petrificationTime = 0;
    }

    public void AddElement(Element.Type elementType, float damage, bool infinty = false)
    {
        if (isReaction) return;
        if (elementAttach1 == null) return;

        if (elementAttach1.elementType == Element.Type.Physics)
        {
            AttachElement1(elementType, infinty);
        }
        else if (elementAttach2.elementType == Element.Type.Physics)
        {
            AttachElement2(elementType, infinty, damage);
        }
    }
    public void AddElement(SkillName skillName, float damage, Element.Type elementType, bool infinty = false)
    {
        if (isReaction) return;
        if (elementAttach1 == null) return;

        if (elementAttach1.elementType == Element.Type.Physics)
        {
            AttachElement1(elementType, infinty);
        }
        else if (elementAttach2.elementType == Element.Type.Physics)
        {
            AttachElement2(elementType, infinty, damage, () =>
            {
                SkillName skill = skillName;
                reactionedSkillName = skill;
            });
        }
    }
    public void AttachElement1(Element.Type elementType, bool infinty)
    {
        if (!CheckAttachCheck1(elementType)) return;

        elementAttach1.Infinity(infinty).AttachElement(elementType);
    }
    public bool CheckAttachCheck1(Element.Type elementType)
    {
        if (elementType == Element.Type.Physics ||
         elementType == Element.Type.Anemo ||
         elementType == Element.Type.Geo ||
         elementType == Element.Type.Immune ||
         elementType == Element.Type.Physics) return false;

        return true;
    }
    public void AttachElement2(Element.Type elementType, bool infinty, float damage, UnityAction action = null)
    {
        if (!CheckAttachCheck2(elementType)) return;

        if (elementType == elementAttach1.elementType) return;

        elementAttach2.Infinity(infinty).AttachElement(elementType);
        if (action != null)
        {
            action.Invoke();
        }
        isReaction = true;
        reactionTime = 0;
        Reaction(damage);
    }
    public bool CheckAttachCheck2(Element.Type elementType)
    {
        if (elementType == Element.Type.Physics ||
         elementType == Element.Type.Immune ||
         elementType == Element.Type.Physics) return false;

        return true;
    }

    public void ResetAttach()
    {
        elementAttach1.ResetAttach();
        elementAttach2.ResetAttach();
    }
    public void ResetAttach(Element.Type elemetType)
    {
        elementAttach1.ResetAttach();
        elementAttach2.ResetAttach();
        elementAttach1.AttachElement(elemetType);
        Frozen(0);
    }

    public void Reaction(float damage)
    {
        reactionTimeMax = 1.0f;
        switch (elementAttach2.elementType)
        {
            case Element.Type.Anemo:
                ReactionAnemo(damage);
                break;
            case Element.Type.Geo:
                DropCrystalize(elementAttach1);
                break;
            case Element.Type.Pyro:
                ReactionPyro(damage);
                break;
            case Element.Type.Hydro:
                ReactionHydro(damage);
                break;
            case Element.Type.Dendro:
                ReactionDendro(damage);
                break;
            case Element.Type.Electro:
                ReactionElectro(damage);
                break;
            case Element.Type.Cyro:
                ReactionCyro(damage);
                reactionTimeMax = 3.0f;
                break;
        }
    }

    void ReactionAnemo(float damage)
    {
        if (elementAttach1.elementType == Element.Type.Physics ||
        elementAttach1.elementType == Element.Type.Anemo ||
        elementAttach1.elementType == Element.Type.Geo ||
        elementAttach1.elementType == Element.Type.Dendro ||
        elementAttach1.elementType == Element.Type.Geo) return;

        ReactionSkill(SkillName.Swirl, damage, elementAttach1.elementType);
        SkillData.ParameterWithKey parameterWithKey = GameManager.instance.skillData.skills[reactionedSkillName];
        if (parameterWithKey.changeElementType == Element.Type.Anemo
        || parameterWithKey.changeElementType == Element.Type.Physics)
        {
            parameterWithKey.changeElementType = elementAttach1.elementType;
        }
    }
    void ReactionPyro(float damage)
    {
        switch (elementAttach1.elementType)
        {
            case Element.Type.Hydro:
                ReactionSkill(SkillName.Vaporize, damage, Element.Type.Pyro);
                break;
            case Element.Type.Electro:
                ReactionSkill(SkillName.Overloaded, damage, Element.Type.Pyro);
                break;
            case Element.Type.Cyro:
                ReactionSkill(SkillName.Melt, damage, Element.Type.Pyro);
                break;
            case Element.Type.Dendro:
                ReactionSkill(SkillName.Burning, damage, Element.Type.Pyro);
                break;
            case Element.Type.Geo:
                DropCrystalize(elementAttach2);
                break;
        }
    }

    void ReactionHydro(float damage)
    {
        switch (elementAttach1.elementType)
        {
            case Element.Type.Pyro:
                ReactionSkill(SkillName.Vaporize, damage, Element.Type.Hydro);
                break;
            case Element.Type.Electro:
                ReactionSkill(SkillName.ElectroCharged, damage, Element.Type.Electro);
                break;
            case Element.Type.Cyro:
                Frozen(1);
                break;
            case Element.Type.Geo:
                DropCrystalize(elementAttach2);
                break;
        }
    }

    void ReactionDendro(float damage)
    {
        switch (elementAttach1.elementType)
        {
            case Element.Type.Pyro:
                ReactionSkill(SkillName.Burning, damage, Element.Type.Pyro);
                break;
        }
    }

    void ReactionElectro(float damage)
    {
        switch (elementAttach1.elementType)
        {
            case Element.Type.Pyro:
                ReactionSkill(SkillName.Overloaded, damage, Element.Type.Electro);
                break;
            case Element.Type.Hydro:
                ReactionSkill(SkillName.ElectroCharged, damage, Element.Type.Electro);
                break;
            case Element.Type.Cyro:
                ReactionSkill(SkillName.Superconduct, damage, Element.Type.Cyro);
                break;
            case Element.Type.Geo:
                DropCrystalize(elementAttach2);
                break;
        }
    }

    void ReactionCyro(float damage)
    {
        switch (elementAttach1.elementType)
        {
            case Element.Type.Pyro:
                ReactionSkill(SkillName.Melt, damage, Element.Type.Cyro);
                break;
            case Element.Type.Hydro:
                Frozen(1);
                break;
            case Element.Type.Electro:
                ReactionSkill(SkillName.ElectroCharged, damage, Element.Type.Cyro);
                break;
            case Element.Type.Geo:
                DropCrystalize(elementAttach2);
                break;
        }
    }

    public void Frozen(float fade)
    {
        if (parentEnemy.type == Enemy.Type.Boss) return;
        if (!parentEnemy.isLive) return;
        parentMaterial.SetFloat(frozenMaterialID, fade);
        isFrozen = fade == 1;
        EnemyStop(isFrozen);
        if (isFrozen) GameManager.instance.damageAttach.WriteReaction(parentEnemy.transform, skillData.skills[SkillName.Frozen], Element.Type.Cyro);
    }

    public void Petrification(float fade)
    {
        if (parentEnemy.type == Enemy.Type.Boss) return;
        if (!parentEnemy.isLive) return;
        parentMaterial.SetFloat(petrificationMaterialID, fade);
        isPetrification = fade == 1;
        EnemyStop(isPetrification);
    }
    public void Petrification(float fade, float duration)
    {
        Petrification(fade);
        petrificationTimeMax = duration;
    }

    void DropCrystalize(ElementAttach elementAttach)
    {
        SkillData.ParameterWithKey parameterWithKey = skillData.skills[SkillName.Crystalize];
        GameManager.instance.damageAttach.WriteReaction(parentEnemy.transform, parameterWithKey, elementAttach.elementType);


        CrystalizeObject crystalizeObject = GameManager.instance.poolManager.GetObject<CrystalizeObject>();
        crystalizeObject.transform.position = parentEnemy.gameObject.transform.position;
    }
    void EnemyStop(bool isStop)
    {
        if (parentEnemy is EnemyNormal)
        {
            EnemyNormal enemyNormal = (EnemyNormal)parentEnemy;
            enemyNormal.Stop(isStop);
        }
    }

    void ReactionSkill(SkillName skillName, float damage, Element.Type elementType)
    {
        SkillData.ParameterWithKey parameterWithKey = skillData.skills[skillName];
        elementReactionObject.Init(parameterWithKey, damage, elementType);
        InvokeReaction(parameterWithKey);
    }

    void InvokeReaction(SkillData.ParameterWithKey parameterWithKey)
    {
        foreach (UnityAction<SkillName> action in parameterWithKey.reactionListener)
        {
            action.Invoke(parameterWithKey.name);
        }
    }
}
