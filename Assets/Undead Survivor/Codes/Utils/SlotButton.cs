using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotButton : MonoBehaviour
{
    public Material highlightMaterial;
    protected Image image
    {
        get
        {
            if (_image == null)
            {
                _image = GetComponentsInChildren<Image>()[1];
            }
            return _image;
        }
    }
    protected Image rarityBackgroundImage
    {
        get
        {
            if (_rarityBackgroundImage == null)
            {
                _rarityBackgroundImage = GetComponent<Image>();
            }
            return _rarityBackgroundImage;
        }
    }
    protected TextMeshProUGUI title
    {
        get
        {
            if (_title == null)
            {
                _title = GetComponentInChildren<TextMeshProUGUI>();
            }
            return _title;
        }
    }
    protected EventButton button
    {
        get
        {
            if (_button == null)
            {
                _button = GetComponentInChildren<EventButton>();
                _button.onSelect = () => { _button.image.material = highlightMaterial; };
                _button.onDeselect = () => { _button.image.material = null; };
            }
            return _button;
        }
    }

    protected void SetDisable()
    {
        image.color = new Color(0.5f, 0.5f, 0.5f, 1);
        rarityBackgroundImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
    }
    protected void SetEnable()
    {
        image.color = new Color(1f, 1f, 1f, 1);
        rarityBackgroundImage.color = new Color(1f, 1f, 1f, 1);
    }
    private Image _image;
    private Image _rarityBackgroundImage;
    private TextMeshProUGUI _title;
    private EventButton _button;
    public Sprite[] raritySprite;
}

