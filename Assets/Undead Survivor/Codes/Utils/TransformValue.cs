using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct TransformValue
{
    private Matrix4x4 transformMatrix;
    public Vector3 position
    {
        get
        {
            return transformMatrix.GetColumn(3);
        }
    }
    public Vector3 localPosition;
    public Quaternion rotation
    {
        get
        {
            return Quaternion.LookRotation(
            transformMatrix.GetColumn(2),
            transformMatrix.GetColumn(1)
            );
        }
    }
    public TransformValue(
        Transform transform
    )
    {
        transformMatrix = transform.localToWorldMatrix;
        localPosition = transform.localPosition; 
    }
}