using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutlineTextMeshProUGUI : MonoBehaviour
{
    TextMeshProUGUI _textMesh;
    TextMeshProUGUI textMesh
    {

        get
        {
            if (_textMesh == null) _textMesh = GetComponent<TextMeshProUGUI>();
            return _textMesh;
        }
    }
    public Color color;

    [Range(0.0f, 2.0f)]
    public float outlineWidth;

    void OnValidate()
    {
        textMesh.outlineColor = color;
        textMesh.outlineWidth = outlineWidth;
    }
}
