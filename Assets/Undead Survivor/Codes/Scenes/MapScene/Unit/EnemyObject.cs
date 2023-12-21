using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : Enemy
{
    ElementReaction elementReaction
    {
        get
        {
            return GetComponentInChildren<ElementReaction>(true);
        }
    }
    float elementAttachTime = 0;
    float elementAttachTimeTerm = 3; //원소 속성이 있을 경우 갱신시간

    void FixedUpdate()
    {
        ElementAttachCheck();
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

    void OnEnable()
    {
        isLive = true;
        elementReaction.gameObject.SetActive(true);
        health = maxHealth;
    }

    public override void Init(SpawnData data)
    {
        base.Init(data);
        elementReaction.Init();
        elementReaction.ResetAttach(elementType);
        ResetReaction();
    }




    void ResetReaction()
    {
        foreach (SkillMoveSet skillMoveSet in GetComponentsInChildren<SkillMoveSet>())
        {
            skillMoveSet.gameObject.SetActive(false);
        }
    }


    public override void ReceiveDamage(SkillName skillName, float damage, Element.Type elementType)
    {
        base.ReceiveDamage(skillName, damage, elementType);

        elementReaction.AddElement(elementType, damage);
    }

    protected override void LiveCheck()
    {
        base.LiveCheck();
        if (!isLive) return;
        if (health > 0)
        {// 아직 살아있으면
            Hit();
        }
        else
        {//죽었으면
            isLive = false;
            gameObject.SetActive(false);
            elementReaction.gameObject.SetActive(false);
            GameManager.instance.gameInfoData.kill++;
            GameManager.instance.battleResult.kill++;
            GameManager.instance.GetExp(exp);
            DropRandomItem();

            AudioManager.instance.PlaySFX(AudioManager.SFX.Dead);
        }
    }
    void DropRandomItem()
    {
        ElementalSphere elementalSphere = GameManager.instance.poolManager.GetElementalSphere();
        elementalSphere.transform.position = gameObject.transform.position;

        RandomDrop();
    }

    void RandomDrop()
    {

        int randomNum = Random.Range(0, 100);
        int dropPer = 0;
        if (type == Type.Normal)
        {
            dropPer = 1 + (int)GameManager.instance.statCalculator.Luck;
            if (randomNum <= dropPer)
            {
                int randomBox = Random.Range(0, 6);
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
            drop.Init(DropItem.Name.Box_Unique);
        }
    }

    private void Hit()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Hit);
    }


}