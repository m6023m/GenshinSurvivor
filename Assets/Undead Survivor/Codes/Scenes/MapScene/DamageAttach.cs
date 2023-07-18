using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageAttach : MonoBehaviour
{
    public TextMeshPro[] texts;
    int textNumber = 0;
    int maxTextCount = 0;
    private void Awake()
    {
        maxTextCount = texts.Length;
    }
    IEnumerator Damage(Transform parentTranform, float damage, Color color, float movePositionX = 0f, float movePositionY = 0f, int textNumber = 0)
    {
        yield return null;
        if (textNumber >= maxTextCount)
        {
            textNumber = 0;
        }
        TextMeshPro damageText = texts[textNumber];
        damageText.gameObject.SetActive(true);
        damageText.transform.rotation = Quaternion.Euler(0, 0, 0);
        float randomRangeX = Random.Range(0, 2);
        float randomRangeY = Random.Range(-1, 1);
        damageText.transform.localPosition = new Vector3(randomRangeX, randomRangeY);
        if (movePositionX != 0 || movePositionY != 0)
        {
            damageText.transform.localPosition = new Vector2(movePositionX, movePositionY);
        }
        string damageString = Mathf.Round(damage).ToString();
        Color damageColor = color;
        Vector2 childScale = damageText.transform.localScale;
        Vector2 parentScale = parentTranform.localScale;
        if (damage == 0)//면역
        {
            damageString = ("Basic.Immune").Localize();
            damageColor = Element.Color(Element.Type.Immune);
        }
        damageText.transform.localScale = new Vector2(1.0f / parentScale.x, 1.0f / parentScale.y);
        damageText.color = damageColor;
        damageText.text = damageString;
        Animator animator = damageText.GetComponent<Animator>();
        animator.Rebind();
    }


    IEnumerator Reaction(Transform parentTranform, SkillData.ParameterWithKey parameterWithKey, Element.Type elementType, int textNumber = 0)
    {
        yield return null;
        if (textNumber >= maxTextCount)
        {
            textNumber = 0;
        }
        TextMeshPro damageText = texts[textNumber];
        damageText.gameObject.SetActive(true);
        damageText.transform.rotation = Quaternion.Euler(0, 0, 0);
        damageText.transform.localPosition = new Vector2(-1, 0);

        Color damageColor = Element.Color(elementType);
        Vector2 childScale = damageText.transform.localScale;
        Vector2 parentScale = parentTranform.localScale;
        string text = "Basic.Reaction.".AddString(parameterWithKey.name.ToString()).Localize();
        damageText.transform.localScale = new Vector2(1.0f / parentScale.x, 1.0f / parentScale.y);
        damageText.color = damageColor;
        damageText.text = text;
        Animator animator = damageText.GetComponent<Animator>();
        animator.Rebind();
    }

    public void WriteDamage(Transform parentTranform, float damage, Color color, float movePositionX = 0f, float movePositionY = 0f)
    {
        StartCoroutine(Damage(parentTranform, damage, color, movePositionX, movePositionY, textNumber));
        AddTextNumber();
    }

    public void WriteReaction(Transform parentTranform, SkillData.ParameterWithKey parameterWithKey, Element.Type elementType)
    {
        StartCoroutine(Reaction(parentTranform, parameterWithKey, elementType, textNumber));
        AddTextNumber();
    }

    void AddTextNumber()
    {
        textNumber++;
        if (textNumber >= maxTextCount)
        {
            textNumber = 0;
        }
    }
}
