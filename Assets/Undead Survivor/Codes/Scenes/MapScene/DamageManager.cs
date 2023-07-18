using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageManager : MonoBehaviour
{
    public GameObject prefab;

    IEnumerator Damage(Transform transform, float damage, Color color, float movePositionX = 0f, float movePositionY = 0f)
    {
        yield return null;
        GameObject damagaeText = Instantiate(prefab, transform);
        damagaeText.transform.rotation = Quaternion.Euler(0, 0, 0);
        float randomRangeX = Random.Range(0, 2);
        float randomRangeY = Random.Range(-1, 1);
        damagaeText.transform.localPosition = new Vector3(randomRangeX, randomRangeY);
        if (movePositionX != 0 || movePositionY != 0)
        {
            damagaeText.transform.localPosition = new Vector2(movePositionX, movePositionY);
        }
        TextMeshPro textGUI = damagaeText.GetComponent<TextMeshPro>();
        string damageText = Mathf.Round(damage).ToString();
        Color damageColor = color;
        Vector2 childScale = damagaeText.transform.localScale;
        Vector2 parentScale = transform.localScale;
        if (damage == 0)//면역
        {
            damageText = "Basic.Immune".Localize();
            damageColor = Element.Color(Element.Type.Immune);
        }
        damagaeText.transform.localScale = new Vector2(childScale.x / parentScale.x, childScale.y / parentScale.y);
        textGUI.color = damageColor;
        textGUI.text = damageText;
    }


    IEnumerator Reaction(Transform transform, SkillData.ParameterWithKey parameterWithKey, Element.Type elementType)
    {
        yield return null;
        GameObject damagaeText = Instantiate(prefab, transform);
        damagaeText.transform.rotation = Quaternion.Euler(0, 0, 0);
        damagaeText.transform.localPosition = new Vector2(-1, 0);
        TextMeshPro textGUI = damagaeText.GetComponent<TextMeshPro>();

        Color damageColor = Element.Color(elementType);
        Vector2 childScale = damagaeText.transform.localScale;
        Vector2 parentScale = transform.localScale;
        string text = "Basic.Reaction.".AddString(parameterWithKey.name.ToString()).Localize();
        damagaeText.transform.localScale = new Vector2(childScale.x / parentScale.x, childScale.y / parentScale.y);
        textGUI.color = damageColor;
        textGUI.text = text;

    }

    public void WriteDamage(Transform transform, float damage, Color color, float movePositionX = 0f, float movePositionY = 0f)
    {
        StartCoroutine(Damage(transform, damage, color, movePositionX, movePositionY));
    }

    public void WriteReaction(Transform transform, SkillData.ParameterWithKey parameterWithKey, Element.Type elementType)
    {
        StartCoroutine(Reaction(transform, parameterWithKey, elementType));
    }
}
