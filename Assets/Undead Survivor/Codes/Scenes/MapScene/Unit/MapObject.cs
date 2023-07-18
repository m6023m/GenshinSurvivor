using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public enum Name
    {
        Box0,
        Box1,
        Tree
    }
    Rigidbody2D rigid;
    Collider2D coll;
    SpriteRenderer spriter;
    Animator animator;
    IEnumerator countinousDamage;
    public Name objectName;
    float health = 3;
    bool isLive;
    private WaitForFixedUpdate wait;
    public RuntimeAnimatorController[] animCon;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Init();
    }
    void Init()
    {
        health = 3;
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 20; 
        float size = 1;
        if (objectName == Name.Tree)
        {
            size = 1.5f;
        }
        animator.runtimeAnimatorController = animCon[(int)objectName];
        transform.localScale = new Vector2(size, size);
        ShowObject();
        animator.Rebind();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Skill") && isLive)
        {
            SkillData.ParameterWithKey parameterWithKey = collision.GetComponent<Skill>().parameterWithKey;
            Skill_Parameter parameter = parameterWithKey.parameter;
            float skillDamage = 1;
            health -= skillDamage;
            if (parameter.skillTick > 0)
            {
                countinousDamage = ContinuousDamage(parameter.skillTick, skillDamage, parameterWithKey);
                StartCoroutine(countinousDamage);
            }

            LiveCheck();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Skill"))
        {
            Skill_Parameter parameter = collision.GetComponent<Skill>().parameterWithKey.parameter;
            if (parameter.skillTick > 0)
            {
                if (countinousDamage != null)
                {
                    StopCoroutine(countinousDamage);
                }
            }
        }
        if (collision.CompareTag("Area"))
        {
            Init();
        }
    }

    IEnumerator ContinuousDamage(float interval, float damage, SkillData.ParameterWithKey parameterWithKey)
    {
        Skill_Parameter parameter = parameterWithKey.parameter;
        while (true)
        {
            float skillDamage = 1.0f;
            if (isLive && !GameManager.instance.IsPause)
            {
                health -= skillDamage;
                LiveCheck();
            }
            else
            {
                yield break;
            }
            yield return new WaitForSecondsRealtime(interval);
        }
    }
    public void LiveCheck()
    {
        if (!isLive) return;
        if (health > 0)
        {
            Hit();
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            DropRandomItem();
            HideObject();
        }
    }

    public void HideObject()
    {
        spriter.enabled = false;
        coll.enabled = false;
        StartCoroutine(InitItem());
    }

    IEnumerator InitItem()
    {
        yield return new WaitForSecondsRealtime(60.0f);
        Init();
    }

    public void ShowObject()
    {
        spriter.enabled = true;
        coll.enabled = true;
    }
    void DropRandomItem()
    {
        GameObject elementalSphere = GameManager.instance.poolManager.Get(PoolManager.Type.ElementalSphere);
        elementalSphere.transform.position = gameObject.transform.position;

        RandomDrop();
    }

    void RandomDrop()
    {

        int randomNum = Random.Range(0, 100);
        int dropPer = 0;
        if (objectName == Name.Box0 || objectName == Name.Box1)
        {
            dropPer = 1 + (int)GameManager.instance.statCalcuator.Luck;
            if (randomNum <= dropPer)
            {
                int randomBox = Random.Range(0, 20);
                GameObject dropItem = GameManager.instance.poolManager.Get(PoolManager.Type.DropItem);
                dropItem.transform.position = gameObject.transform.position;
                DropItem drop = dropItem.GetComponent<DropItem>();
                if (randomBox > 0 && randomBox < 5)
                {
                    drop.Init(DropItem.Name.Box_Ex);
                }
                else if (randomBox == 5 && randomBox == 6)
                {
                    drop.Init(DropItem.Name.Heal);
                }
                else if (randomBox == 7)
                {
                    drop.Init(DropItem.Name.Regen);
                }
            }
        }
        else if (objectName == Name.Tree)
        {
            GameObject dropItem = GameManager.instance.poolManager.Get(PoolManager.Type.DropItem);
            dropItem.transform.position = gameObject.transform.position;
            DropItem drop = dropItem.GetComponent<DropItem>();
            drop.Init(DropItem.Name.Heal);
        }
    }

    private void Hit()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Hit);
        animator.SetTrigger("Hit");
    }

}
