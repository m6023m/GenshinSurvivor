using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WishSlotOutline : MonoBehaviour
{
    Image image;
    public void Init(Material material)
    {
        image = GetComponent<Image>();
        image.material = material;
    }
}