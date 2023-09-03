using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTracker : MonoBehaviour
{
    public GameObject trackedObject;
    public Sprite objectIcon;
    CanvasGroup canvasGroup;
    Image icon;
    RectTransform canvasRect;
    RectTransform iconRect;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        icon = GetComponentsInChildren<Image>()[2];
        icon.sprite = objectIcon;
        canvasRect = icon.canvas.GetComponent<RectTransform>();
        iconRect = icon.GetComponent<RectTransform>();

        canvasGroup.alpha = 0; // Default alpha to visible
    }

    private void Update()
    {
        Vector3 playerPosition = GameManager.instance.player.transform.position;
        Vector3 targetPosition = trackedObject.transform.position;

        // Calculate direction from player to tracked object
        Vector3 direction = (targetPosition - playerPosition).normalized;

        // Calculate position on canvas at the edge in the direction of the target object
        // Separately handle x and y directions to fit within rectangular canvas
        float canvasRatio = (canvasRect.rect.width - iconRect.rect.width) / (canvasRect.rect.height - iconRect.rect.height);
        float directionRatio = Mathf.Abs(direction.x / direction.y);
        Vector3 canvasPosition;

        if (directionRatio > canvasRatio)
        {
            // Use full width of the canvas, calculate height based on direction
            canvasPosition = new Vector3(
                Mathf.Sign(direction.x) * (canvasRect.rect.width - iconRect.rect.width) / 2,
                Mathf.Sign(direction.y) * Mathf.Abs(((canvasRect.rect.width - iconRect.rect.width) / 2) / directionRatio),
                0
            );
        }
        else
        {
            // Use full height of the canvas, calculate width based on direction
            canvasPosition = new Vector3(
                Mathf.Sign(direction.x) * Mathf.Abs(((canvasRect.rect.height - iconRect.rect.height) / 2) * directionRatio),
                Mathf.Sign(direction.y) * (canvasRect.rect.height - iconRect.rect.height) / 2,
                0
            );
        }

        if (!float.IsNaN(canvasPosition.x) && !float.IsNaN(canvasPosition.y))
        {
            // Apply position to canvasGroup
            canvasGroup.transform.localPosition = canvasPosition;
        }

        // Check if target is visible on screen
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(targetPosition);
        bool isScreenInVisible = Camera.main.pixelRect.Contains(targetScreenPos);
        bool isObjectActive = trackedObject.activeInHierarchy;
        canvasGroup.alpha = !isScreenInVisible && isObjectActive ? 1 : 0;
    }
}
