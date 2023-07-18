using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalSphere : MonoBehaviour
{
    float speed = 2.0f;
    Rigidbody2D rigid;
    Collider2D coll;
    IEnumerator countinousDamage;
    Vector2 magnetVec;
    float removeTime = 10.0f;
    float time = 0;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        magnetVec = Vector2.zero;
    }

    private void OnEnable()
    {
        time = 0;
    }
    private void FixedUpdate()
    {
        FixedMove();
        DisableCheck();
    }

    private void FixedMove()
    {
        Vector2 nextVec = magnetVec * speed * Time.fixedDeltaTime;
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("DropArea"))
        {
            Magnet(collision.gameObject.transform.position, speed * 3);
        }
        if (collision.CompareTag("SkillEffectArea"))
        {
            SkillMoveSet skill = collision.GetComponentInParent<SkillMoveSet>();
            if (skill.parameterWithKey.parameter.magnet != 0 && skill.skillSequence.isMagnet)
            {
                Magnet(collision.gameObject.transform.position, skill.parameterWithKey.parameter.magnetSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (SkillData.ParameterWithKey param in GameManager.instance.ownBursts)
            {
                float result = 1 * GameManager.instance.statCalcuator.Regen;
                result += GameManager.instance.artifactData.Scholar();
                param.parameter.elementGauge += result;
            }

            AudioManager.instance.PlaySFX(AudioManager.SFX.Regen);
            gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("SkillEffectArea"))
        {
            magnetVec = new Vector2(0, 0);
        }
    }

    void Magnet(Vector3 target, float magnetSpeed)
    {
        Vector3 targetPosition = target;
        magnetVec = (targetPosition - transform.position) * magnetSpeed * 0.3f;
    }
}
