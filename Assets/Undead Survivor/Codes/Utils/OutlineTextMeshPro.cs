using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutlineTextMeshPro : MonoBehaviour
{
    TextMeshPro textMesh;
    public Color color;

    [Range(0.0f, 2.0f)]
    public float outlineWidth;

    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        textMesh.outlineColor = color;
        textMesh.outlineWidth = outlineWidth;
    }
}
