using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipPanel : MonoBehaviour
{
    public static TooltipPanel instance;
    public RectTransform canvasTransform;
    RectTransform rectTransform;
    private TextMeshProUGUI textMeshPro;
    Button buttonClose;
    CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();

        buttonClose = GetComponentInChildren<Button>(true);
        buttonClose.onClick.AddListener(() =>
        {
            canvasGroup.alpha = 0.0f;
            buttonClose.image.raycastTarget = false;
            buttonClose.gameObject.SetActive(false);
            canvasGroup.blocksRaycasts = false;
        });
        if (!GameDataManager.instance.isMobile) buttonClose.gameObject.SetActive(false);
        instance = this;
    }

    public void ChangeTooltip(string text, float x, float y)
    {
        textMeshPro.text = text;
        buttonClose.image.raycastTarget = true;
        canvasGroup.blocksRaycasts = true;
        if (GameDataManager.instance.isMobile) buttonClose.gameObject.SetActive(true);
        canvasGroup.alpha = 1.0f;
        float canvasX = canvasTransform.rect.width / 2;
        transform.position = new Vector2(canvasX, y - transform.localScale.y / 2 + 100.0f);
    }

    public void DisableTooltipWindow()
    {
        if (GameDataManager.instance.isMobile) return;
        canvasGroup.alpha = 0.0f;
        buttonClose.image.raycastTarget = false;
        canvasGroup.blocksRaycasts = false;
        buttonClose.gameObject.SetActive(false);
    }
}
