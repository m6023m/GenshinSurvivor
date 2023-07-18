using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class HorizontalLayout : MonoBehaviour
{
    public float spacing = 10f; // 오브젝트 사이의 간격

#if UNITY_EDITOR
    private void Update()
    {
        AlignHorizontally();
    }
#endif

    private void AlignHorizontally()
    {
        float totalWidth = 0f;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        foreach (Transform child in transform)
        {
            totalWidth += child.GetComponent<Renderer>().bounds.size.x;
        }

        totalWidth += spacing * (transform.childCount - 1); 

        float startX = transform.position.x - totalWidth / 2f; 

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Renderer renderer = child.GetComponent<Renderer>();

            float childWidth = renderer.bounds.size.x;
            float childX = startX + childWidth / 2f;

            child.position = new Vector3(childX, transform.position.y, transform.position.z);

            startX += childWidth + spacing;
        }
    }
}
