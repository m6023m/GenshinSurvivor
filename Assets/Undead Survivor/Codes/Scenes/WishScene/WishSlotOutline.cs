using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WishSlotOutline : MonoBehaviour
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
    public void Init(Material material)
    {
        image.material = material;
    }
}