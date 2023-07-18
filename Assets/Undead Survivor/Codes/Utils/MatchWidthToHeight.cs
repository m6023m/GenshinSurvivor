using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class MatchWidthToHeight : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.y, rectTransform.sizeDelta.y);
    }
}