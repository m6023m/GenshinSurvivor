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
    public List<AnimationEventer> animationStartEvents = new List<AnimationEventer>();
    public List<AnimationEventer> animationEndEvents = new List<AnimationEventer>();

    [Serializable]
    public class AnimationEventer
    {
        public string name;
        public string eventName;
    }

    [Button]
    public void CreateAnimationClips()
    {
        Debug.Log("Start CreateAnimationClips");
        Animator myAnimator = asepriteObject.GetComponent<Animator>();
        AnimationClip[] clips = GetAnimationClips(myAnimator);
        foreach (AnimationClip clip in clips)
        {
            CreateAnimationClipsFromKeyframes(clip);
        }
        Debug.Log("End CreateAnimationClips");
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


            AddEvent(newClip, animationStartEvents, 0);
            AddEvent(newClip, animationEndEvents, newClip.length);



            newClips.Add(newClip);

            AssetDatabase.CreateAsset(newClip, $"{saveFolderPath}/{name}.anim");

        }

    }
    public void AddEvent(AnimationClip newClip, List<AnimationEventer> animationEventers, float time)
    {
        List<AnimationEvent> eventsToAdd = new List<AnimationEvent>();
        foreach (AnimationEventer animationEventer in animationEventers)
        {
            if (newClip.name.Contains(animationEventer.name))
            {
                AnimationEvent animationEvent = new AnimationEvent
                {
                    time = time,
                    functionName = animationEventer.eventName
                };
                eventsToAdd.Add(animationEvent);
            }
        }
        AnimationEvent[] currentEvents = AnimationUtility.GetAnimationEvents(newClip);
        List<AnimationEvent> allEvents = new List<AnimationEvent>(currentEvents);
        allEvents.AddRange(eventsToAdd);

        AnimationUtility.SetAnimationEvents(newClip, allEvents.ToArray());
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