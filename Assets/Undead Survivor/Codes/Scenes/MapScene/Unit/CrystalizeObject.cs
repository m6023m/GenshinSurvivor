using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalizeObject : MonoBehaviour
{
    float speed = 2.0f;
    Rigidbody2D rigid;
    Collider2D coll;
    SpriteRenderer spriteRenderer;
    IEnumerator countinousDamage;
    Vector2 magnetVec;
    Element.Type elementType;
    float disableTime = 3.0f;
    float gameTime = 0;
    private HashSet<Collider2D> dropColliders = new HashSet<Collider2D>();
    private HashSet<SkillMoveSet> skillMoveSets = new HashSet<SkillMoveSet>();

    private void Awake()
    {
        InitValues();
    }
    private void LateUpdate()
    {
        magnetVec = new Vector2(0, 0);
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

    void CheckDisable() {
        gameTime += Time.deltaTime;
        if (gameTime > disableTime)
        {
            gameTime = 0;
            gameObject.SetActive(false);
        }
    }

    void InitValues()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        magnetVec = Vector2.zero;
    }


    public void Init(Element.Type elementType)
    {
        InitValues();
        spriteRenderer.color = Element.Color(elementType);
        dropColliders = new HashSet<Collider2D>();
        skillMoveSets = new HashSet<SkillMoveSet>();
    }
    private void Move()
    {
        Vector2 nextVec = magnetVec * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SkillData.ParameterWithKey parameterWithKey = GameManager.instance.skillData.Get(SkillName.Crystalize);
            GameManager.instance.player.elementReactionObject.Init(parameterWithKey, 0, elementType);

            AudioManager.instance.PlaySFX(AudioManager.SFX.Regen);
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


    void Magnet(Vector3 target, float magnetSpeed)
    {
        Vector3 targetPosition = target;
        magnetVec = (targetPosition - transform.position).normalized * magnetSpeed;
    }
}
