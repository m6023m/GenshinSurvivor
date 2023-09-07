using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Animations;
#endif

[CreateAssetMenu(fileName = "AnimationCreator", menuName = "GenshinSurvivor/AnimationCreator", order = 2)]
public class AnimationCreator : ScriptableObject
{
    [Required, Title("Textures Containing Sprites")]
    public Texture2D[] spriteTextures;

    [BoxGroup("Animation Settings"), Tooltip("Frames per second for the animation")]
    public int framesPerSecond = 24;

    [BoxGroup("Animation Settings"), Tooltip("Folder path to save generated animation clip"), FolderPath]
    public string saveFolderPath = "Assets/";

    [Button]
    public void CreateAnimationClip()
    {
#if UNITY_EDITOR
        foreach (var texture in spriteTextures)
        {
            var sprites = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(texture));
            AnimationClip clip = new AnimationClip();
            EditorCurveBinding spriteBinding = new EditorCurveBinding();
            spriteBinding.type = typeof(SpriteRenderer);
            spriteBinding.path = "";
            spriteBinding.propertyName = "m_Sprite";

            ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[sprites.Length];
            for (int i = 0; i < (sprites.Length - 1); i++)
            {
                spriteKeyFrames[i] = new ObjectReferenceKeyframe();
                spriteKeyFrames[i].time = i * (1.0f / framesPerSecond);
                spriteKeyFrames[i].value = sprites[i + 1];
            }
            AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, spriteKeyFrames);

            AssetDatabase.CreateAsset(clip, saveFolderPath +  "/" + RemoveSizeSuffix(texture.name) + ".anim");
        }
#endif
    }
    private string RemoveSizeSuffix(string originalName)
    {
        return System.Text.RegularExpressions.Regex.Replace(originalName, @"_\d+x\d+$", "");
    }
}
