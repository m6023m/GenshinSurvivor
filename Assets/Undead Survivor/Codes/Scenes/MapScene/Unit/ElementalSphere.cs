using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalSphere : MonoBehaviour
{
    float speed = 2.0f;
    Rigidbody2D rigid;
    Collider2D coll;
    Vector2 magnetVec;
    float removeTime = 10.0f;
    float time = 0;
    private HashSet<Collider2D> dropColliders = new HashSet<Collider2D>();
    private HashSet<SkillMoveSet> skillMoveSets = new HashSet<SkillMoveSet>();

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        magnetVec = Vector2.zero;
    }

    private void OnEnable()
    {
        time = 0;
        dropColliders = new HashSet<Collider2D>();
        skillMoveSets = new HashSet<SkillMoveSet>();
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
        DisableCheck();
    }

    private void Move()
    {
        Vector2 nextVec = magnetVec * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void DisableCheck()
    {
        time += Time.fixedDeltaTime;
        if (time > removeTime)
        {
            gameObject.SetActive(false);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.AddElementGauge(1 + GameManager.instance.artifactData.Scholar());

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
