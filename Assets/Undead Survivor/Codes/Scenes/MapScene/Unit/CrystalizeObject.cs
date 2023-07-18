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

    private void Awake()
    {
        InitValues();
    }
    private void Update()
    {
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
    }
    private void FixedUpdate()
    {
        Vector2 nextVec = magnetVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("DropArea"))
        {
            Magnet(collision.gameObject.transform.position, speed);
        }
        if (collision.CompareTag("SkillEffectArea"))
        {
            if (collision.GetComponentInParent<SkillMoveSet>().parameterWithKey.parameter.magnet != 0)
            {
                Magnet(collision.gameObject.transform.position, collision.GetComponentInParent<SkillMoveSet>().parameterWithKey.parameter.magnetSpeed);
            }
        }
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
        magnetVec = (targetPosition - transform.position) * magnetSpeed;
    }
}
