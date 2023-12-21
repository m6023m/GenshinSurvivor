using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutlineTextMeshPro : MonoBehaviour
{
    TextMeshPro _textMesh;
    TextMeshPro textMesh
    {

        get
        {
            if (_textMesh == null) _textMesh = GetComponent<TextMeshPro>();
            return _textMesh;
        }
    }
    public Color color;

    [Range(0.0f, 2.0f)]
    public float outlineWidth;

    void Awake()
    {
        textMesh.outlineColor = color;
        textMesh.outlineWidth = outlineWidth;
    }
}
