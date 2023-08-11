using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public Name dropName;
    public Sprite[] sprites;
    float speed = 2.0f;
    Rigidbody2D rigid;
    Collider2D coll;
    IEnumerator countinousDamage;
    Vector2 magnetVec;
    SpriteRenderer spriteRenderer;
    float createTime = 0;
    float maxLiveTime = 10;
    private Camera mainCamera;
    private Renderer objectRenderer;
    float disableTime = 30.0f;
    float gameTime = 0;
    private HashSet<Collider2D> dropColliders = new HashSet<Collider2D>();
    private HashSet<SkillMoveSet> skillMoveSets = new HashSet<SkillMoveSet>();

    public enum Name
    {
        Box_Normal,
        Box_Ex,
        Box_Rare,
        Box_Unique,
        Heal,
        Regen
    }

    public void Init(Name name)
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        magnetVec = Vector2.zero;
        spriteRenderer = GetComponent<SpriteRenderer>();
        dropName = name;
        spriteRenderer.sprite = sprites[(int)dropName];
        createTime = 0;
        mainCamera = Camera.main;
        objectRenderer = GetComponent<Renderer>();
        dropColliders = new HashSet<Collider2D>();
        skillMoveSets = new HashSet<SkillMoveSet>();
    }

    private void LateUpdate()
    {
        magnetVec = new Vector2(0, 0);
        if (createTime > maxLiveTime)
        {
            gameObject.SetActive(false);
        }
        if (rigid == null) return;
        foreach (var collider in dropColliders)
        {
            Magnet(collider.gameObject.transform.position, speed * 3);
        }
        foreach (var skill in skillMoveSets)
        {
            if (skill.gameObject.activeInHierarchy)
            {
                Magnet(skill.transform.position, skill.parameterWithKey.parameter.magnetSpeed);
            }
        }

        Move();
        CheckDisable();
    }
    private void Move()
    {
        Vector2 nextVec = magnetVec * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }
    void CheckDisable()
    {
        if (dropName == Name.Box_Unique) return;

        gameTime += Time.deltaTime;
        if (gameTime > disableTime)
        {
            gameTime = 0;
            gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DropPlayer();
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("DropArea"))
        {
            dropColliders.Add(collision);
        }

        if (collision.CompareTag("SkillEffectArea"))
        {
            SkillMoveSet skill = collision.GetComponentInParent<SkillMoveSet>();
            if (skill.parameterWithKey.parameter.magnet != 0 && skill.skillSequence.isMagnet)
            {
                skillMoveSets.Add(skill);
            }
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (dropColliders.Contains(collision))
        {
            dropColliders.Remove(collision);
        }

        SkillMoveSet skill = collision.GetComponentInParent<SkillMoveSet>();
        if (skillMoveSets.Contains(skill))
        {
            skillMoveSets.Remove(skill);
        }
    }


    void DropPlayer()
    {
        int mora = 0;
        int primoGem = 0;

        switch (dropName)
        {
            case Name.Box_Normal:
                mora = Random.Range(5, 20);
                primoGem = 5;
                break;
            case Name.Box_Ex:
                mora = Random.Range(10, 100);
                primoGem = 10;
                break;
            case Name.Box_Rare:
                mora = Random.Range(100, 500);
                primoGem = 160;
                break;
            case Name.Box_Unique:
                mora = Random.Range(1000, 5000);
                primoGem = 800;
                GameManager.instance.Victory();
                break;
            case Name.Heal:
                float healDefalut = 30;
                float healResult = healDefalut;
                GameDataManager.instance.saveData.record.healItemCount++;
                GameManager.instance.player.HealHealth(healResult);
                break;
            case Name.Regen:
                foreach (SkillData.ParameterWithKey param in GameManager.instance.ownBursts)
                {
                    param.parameter.elementGauge += 1000;
                }
                break;
        }

        GameManager.instance.GainMora(mora);
        GameManager.instance.GainPrimoGem(primoGem);
    }


    void Magnet(Vector3 target, float magnetSpeed)
    {
        Vector3 targetPosition = target;
        magnetVec = (targetPosition - transform.position).normalized * magnetSpeed;
    }
}
