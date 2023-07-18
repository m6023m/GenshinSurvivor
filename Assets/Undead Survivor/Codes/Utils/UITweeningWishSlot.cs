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
        RectTransform parentRect = uiObject.transform.parent.GetComponent<RectTransform>();

        float scale = uiObject.rect.size.x / 2;
        if (!isFlipped) return;
        isFlipped = false;
        float fromPosition = defaultPosition.x + 2400.0f;
        float divideSize = parentRect.rect.size.x / maxCount;

        uiObject.DOAnchorPosX(fromPosition, 0);
        uiObject.DOAnchorPosX(scale * 1.3f + index * divideSize, index * 0.2f);
    }

}

