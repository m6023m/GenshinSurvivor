using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTester : MonoBehaviour
{
    public enum TransformMethodType {
        DOMove,
        DORotate,
        DOScale,
        DOLookAt,
        DOJump,
        DOPosition,
        DOPath,
        DOTimeScale,
    }
    public enum DOMoveType{
        Local,
        Punch,
        Shake,
    }
    public Transform mTransform;
    public Material mMaterial;
    public TrailRenderer mTrailRenderer;

    // Update is called once per frame
    void Update()
    {
        
    }
}
