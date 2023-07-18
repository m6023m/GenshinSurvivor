using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingTextController : MonoBehaviour
{
    public float scrollSpeed = 30f;
    private RectTransform rectTransform;
    private float textWidth;
    private TextMeshProUGUI textMeshPro;
    private bool isOverflowing;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        textWidth = rectTransform.rect.width;
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        isOverflowing = IsTextOverflowing();

        if (isOverflowing)
        {
            rectTransform.localPosition += new Vector3(-scrollSpeed * Time.deltaTime, 0, 0);

            if (rectTransform.localPosition.x <= -textWidth)
            {
                rectTransform.localPosition = new Vector3(textWidth, 0, 0);
            }
        }
    }

    bool IsTextOverflowing()
    {
        float preferredWidth = textMeshPro.GetPreferredValues(textMeshPro.text).x;
        return preferredWidth > rectTransform.rect.width;
    }
}
