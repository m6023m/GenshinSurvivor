using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementAttach : MonoBehaviour
{
    public Sprite[] elementSprite;
    public Element.Type elementType;
    SpriteRenderer spriteRenderer;
    bool isInfinity;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (elementType == Element.Type.Physics || elementType == Element.Type.Immune)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0);
            return;
        }
        spriteRenderer.color = new Color(1, 1, 1, 1);
        spriteRenderer.sprite = elementSprite[(int)elementType];
    }
    public ElementAttach Infinity(bool infinity)
    {
        isInfinity = infinity;
        return this;
    }
    public void AttachElement(Element.Type elementType)
    {
        if (this.elementType == elementType) return;
        this.elementType = elementType;
        if (!isInfinity) DetachElement(3.0f);
    }
    public void DetachElement(float time)
    {
        if (!gameObject.activeInHierarchy) return;
        StartCoroutine(_DettachElement(time));
    }

    IEnumerator _DettachElement(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        ResetAttach();
    }

    public void ResetAttach()
    {
        elementType = Element.Type.Physics;
    }
}
