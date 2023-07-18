using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WishSlotElement : MonoBehaviour
{
    Image image;
    public void Init(Sprite sprite)
    {
        image = GetComponent<Image>();
        image.sprite = sprite;
    }
}