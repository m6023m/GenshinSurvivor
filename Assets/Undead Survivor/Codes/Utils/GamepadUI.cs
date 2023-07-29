using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamepadUI : MonoBehaviour
{
    Image _image;
    Image image
    {
        get
        {
            if (_image == null) _image = GetComponentInChildren<Image>(true);
            return _image;
        }
    }
    TextMeshProUGUI _textMesh;
    TextMeshProUGUI textMesh
    { 
        get
        {
            if (_textMesh == null) _textMesh = GetComponentInChildren<TextMeshProUGUI>(true);
            return _textMesh;
        }
    }

    [HideInInspector]
    public Sprite sprite
    {
        get
        {
            return image.sprite;
        }
        set
        {
            image.sprite = value;
        }
    }
    [HideInInspector]
    public string text
    {
        get
        {
            return textMesh.text;
        }
        set
        {
            textMesh.text = value;
        }
    }
}
