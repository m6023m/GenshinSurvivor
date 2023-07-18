using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotButton : MonoBehaviour
{
    protected Image image
    {
        get
        {
            if (_image == null)
            {
                _image = GetComponentsInChildren<Image>()[2];
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
                _rarityBackgroundImage = GetComponentsInChildren<Image>()[1];
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
    protected Button button
    {
        get
        {
            if (_button == null)
            {
                _button = GetComponentInChildren<Button>();
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
        rarityBackgroundImage.color =  new Color(1f, 1f, 1f, 1);
    }
    private Image _image;
    private Image _rarityBackgroundImage;
    private TextMeshProUGUI _title;
    private Button _button;
    public Sprite[] raritySprite;
}

