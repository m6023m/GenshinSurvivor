using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UITweening : MonoBehaviour
{
    private RectTransform uiObject;
    [SerializeField] public Vector2 startPosition;
    [SerializeField] public Vector2 endPosition;
    [SerializeField] public float duration;
    Vector2 defaultPosition;

    private void Awake()
    {
        uiObject = GetComponent<RectTransform>();
    }
    private void Start()
    {
        uiObject.DOAnchorPos(endPosition, duration);
    }

    public void SetDefaultPosition()
    {
        uiObject.DOAnchorPos(startPosition, 0);
        uiObject.DOAnchorPos(endPosition, duration);
    }
    public void SetDefaultPositionWithSize()
    {
        Vector2 vector = Vector2.zero;
        if (uiObject != null)
        {
            float x = uiObject.rect.x / 2;
            vector = new Vector2(x, 0);
        }
        uiObject.DOAnchorPos(startPosition, 0);
        uiObject.DOAnchorPos(endPosition + vector, duration);
    }
}

