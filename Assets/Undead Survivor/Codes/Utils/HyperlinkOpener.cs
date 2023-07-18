using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class HyperlinkOpener : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler
{
    ScrollRect parentScroll;
    TMP_Text pTextMeshPro;

    void Awake()
    {
        pTextMeshPro = GetComponent<TMP_Text>();
        parentScroll = GetComponentInParent<ScrollRect>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(pTextMeshPro, Input.mousePosition, null);
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = pTextMeshPro.textInfo.linkInfo[linkIndex];
            string linkText = linkInfo.GetLinkID();
            Application.OpenURL(linkText);
        }
    }

    public void OnBeginDrag(PointerEventData e)
    {
        if (parentScroll == null) return;
        parentScroll.OnBeginDrag(e);
    }
    public void OnDrag(PointerEventData e)
    {
        if (parentScroll == null) return;
        parentScroll.OnDrag(e);
    }
    public void OnEndDrag(PointerEventData e)
    {
        if (parentScroll == null) return;
        parentScroll.OnEndDrag(e);
    }

    public void OnScroll(PointerEventData e)
    {
        if (parentScroll == null) return;
        parentScroll.OnScroll(e);
    }

}
