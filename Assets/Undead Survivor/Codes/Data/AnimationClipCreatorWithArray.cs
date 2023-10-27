using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;


#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "AnimationCreator", menuName = "GenshinSurvivor/AnimationCreatorWithArray", order = 2)]
public class AnimationClipCreatorWithArray : ScriptableObject
{
    [Required, Title("Sprites for Animation")]
    public List<SpriteData> spriteArrays; // Array of sprite arrays

    [BoxGroup("Animation Settings"), Tooltip("Frames per second for the animation")]
    public int framesPerSecond = 24;

    [BoxGroup("Animation Settings"), Tooltip("Folder path to save generated animation clip"), FolderPath]
    public string saveFolderPath = "Assets/";

    [BoxGroup("Animation Settings"), Tooltip("Names for the animation clips")]
    public string[] clipNames; // Names of the animation clips

    [BoxGroup("Animation Settings"), Tooltip("Number of slices or frames for each animation")]
    public int[] spriteSlices; // Number of slices for each animation

    [Serializable]
    public class SpriteData
    {
        public List<Sprite> sprites; // Array of sprite arrays

    }

    [Button]
    public void CreateAnimationClips()
    {
#if UNITY_EDITOR
        for (int i = 0; i < spriteArrays.Count; i++)
        {
            SpriteData spriteData = spriteArrays[i];
            string originalClipName = clipNames[i];
            int slices = spriteSlices[i]; // Number of slices for the current animation

            string spriteName = spriteData.sprites.Count > 0 ? spriteData.sprites[0].name : "Unnamed";

            string cleanedSpriteName = RemoveLast(spriteName);

            string finalClipName = $"{originalClipName}_{cleanedSpriteName}";

            AnimationClip clip = new AnimationClip
            {
                frameRate = framesPerSecond // or slices to match the number of slices
            };

            EditorCurveBinding spriteBinding = new EditorCurveBinding
            {
                type = typeof(SpriteRenderer),
                path = "",
                propertyName = "m_Sprite"
            };

            slices = Mathf.Min(slices, spriteData.sprites.Count);

            ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[slices];
            for (int j = 0; j < slices; j++)
            {
                spriteKeyFrames[j] = new ObjectReferenceKeyframe
                {
                    time = j / (float)framesPerSecond,
                    value = spriteData.sprites[j]
                };
            }

            AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, spriteKeyFrames);

            string assetPath = $"{saveFolderPath}/{finalClipName}.anim";
            AssetDatabase.CreateAsset(clip, assetPath);
        }
#endif
    }

    private string RemoveSizeSuffix(string originalName)
    {
        return System.Text.RegularExpressions.Regex.Replace(originalName, @"_\d+x\d+$", "");
    }
    private string RemoveLast(string originalName)
    {
        return System.Text.RegularExpressions.Regex.Replace(originalName, @"_\d+$", "");
    }

}
