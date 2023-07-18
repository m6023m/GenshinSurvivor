using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ImageText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string mouseOverText;
    public Image _image;
    public Image image
    {
        get
        {
            if (_image == null) _image = GetComponentInChildren<Image>();
            return _image;
        }
    }
    public TextMeshProUGUI _textMesh;
    public TextMeshProUGUI textMesh
    {
        get
        {
            if (_textMesh == null) _textMesh = GetComponentInChildren<TextMeshProUGUI>();
            return _textMesh;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (mouseOverText == null) return;
        if (mouseOverText.Length <= 0) return;
        TooltipPanel.instance.ChangeTooltip(mouseOverText, transform.position.x, transform.position.y);
    
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (mouseOverText == null) return;
        if (mouseOverText.Length <= 0) return;
        TooltipPanel.instance.DisableTooltipWindow();
    }
}
