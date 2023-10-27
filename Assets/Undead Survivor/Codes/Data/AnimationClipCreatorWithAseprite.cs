#if UNITY_EDITOR
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;


using UnityEditor;

[CreateAssetMenu(fileName = "AnimationCreator", menuName = "GenshinSurvivor/AnimationClipCreatorWithAseprite", order = 2)]
public class AnimationClipCreatorWithAseprite : ScriptableObject
{
    public GameObject asepriteObject;
    [BoxGroup("Animation Settings"), Tooltip("Folder path to save generated animation clip"), FolderPath]
    public string saveFolderPath = "Assets";

    [Button]
    public void CreateAnimationClips()
    {
        Animator myAnimator = asepriteObject.GetComponent<Animator>();
        AnimationClip[] clips = GetAnimationClips(myAnimator);
        foreach (AnimationClip clip in clips)
        {
            CreateAnimationClipsFromKeyframes(clip);
        }

    }

    public void CreateAnimationClipsFromKeyframes(AnimationClip clip)
    {
        List<AnimationClip> newClips = new List<AnimationClip>();
        EditorCurveBinding[] editorCurves = AnimationUtility.GetObjectReferenceCurveBindings(clip);

        foreach (EditorCurveBinding editorCurve in editorCurves)
        {
            AnimationClip newClip = new AnimationClip();
            string name = $"{clip.name}_{editorCurve.path}";
            newClip.name = name;

            ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(clip, editorCurve);

            AnimationUtility.SetObjectReferenceCurve(newClip, editorCurve, keyframes);

            newClips.Add(newClip);

            AssetDatabase.CreateAsset(newClip, $"{saveFolderPath}/{name}.anim");
        }

    }
    public AnimationClip[] GetAnimationClips(Animator animator)
    {
        if (animator.runtimeAnimatorController == null)
        {
            return null; // Animator에 AnimatorController가 할당되지 않았으면 null 반환
        }

        return animator.runtimeAnimatorController.animationClips;
    }

}

#endif