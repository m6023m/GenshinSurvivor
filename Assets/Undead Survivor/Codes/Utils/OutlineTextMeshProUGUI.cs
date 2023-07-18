using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutlineTextMeshProUGUI : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    public Color color;

    [Range(0.0f, 2.0f)]
    public float outlineWidth;

    void OnValidate()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.outlineColor = color;
        textMesh.outlineWidth = outlineWidth;
    }
}
