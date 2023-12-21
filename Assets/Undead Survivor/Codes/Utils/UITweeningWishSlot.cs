using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UITweeningWishSlot : MonoBehaviour
{
    private RectTransform uiObject;
    Vector2 defaultPosition;
    bool isFlipped;
    int index;
    int maxCount;
    RectTransform _parentRect;
    RectTransform parentRect
    {
        get
        {
            if (_parentRect == null) uiObject.transform.parent.GetComponent<RectTransform>();
            return _parentRect;
        }
    }
    void Start()
    {
        uiObject = GetComponent<RectTransform>();
    }
    public void Tweening(int index, int maxCount)
    {
        this.index = index;
        this.maxCount = maxCount;
        isFlipped = true;
    }

    private void LateUpdate()
    {
        defaultPosition = uiObject.transform.position;


        float scale = uiObject.rect.size.x / 2;
        if (!isFlipped) return;
        isFlipped = false;
        float fromPosition = defaultPosition.x + 2400.0f;
        float divideSize = parentRect.rect.size.x / maxCount;

        uiObject.DOAnchorPosX(fromPosition, 0);
        uiObject.DOAnchorPosX(scale * 1.3f + index * divideSize, index * 0.2f);
    }

}

