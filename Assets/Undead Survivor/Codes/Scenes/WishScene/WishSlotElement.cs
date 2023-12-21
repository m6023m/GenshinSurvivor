using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WishSlotElement : MonoBehaviour
{
    Image _image;
    Image image
    {
        get
        {
            if (_image == null) _image = GetComponent<Image>();
            return _image;
        }
    }
    public void Init(Sprite sprite)
    {
        image.sprite = sprite;
    }
}