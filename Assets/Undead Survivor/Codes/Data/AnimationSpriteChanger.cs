using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "AnimationSpriteChanger", menuName = "GenshinSurvivor/AnimationSpriteChanger", order = 1)]
public class AnimationSpriteChanger : ScriptableObject
{
    public AnimationClip[] animationClips;
    public int[] spritesPerClip;
    public Sprite[] newSprites;

    [Button]
    public void ChangeSprites()
    {
        // 에디터에서만 실행
#if UNITY_EDITOR
        // 스프라이트 인덱스 초기화
        int spriteIndex = 0;

        // 각 애니메이션 클립에 대해
        for (int clipIndex = 0; clipIndex < animationClips.Length; clipIndex++)
        {
            // 애니메이션 클립의 에디터 커브 바인딩 가져오기
            EditorCurveBinding[] curveBindings = AnimationUtility.GetObjectReferenceCurveBindings(animationClips[clipIndex]);

            ObjectReferenceKeyframe[] keyframes = null;
            // 각 커브 바인딩에 대해
            for (int curveIndex = 0; curveIndex < curveBindings.Length; curveIndex++)
            {
                // 에디터 커브 바인딩의 오브젝트 참조 키 프레임 가져오기
                keyframes = AnimationUtility.GetObjectReferenceCurve(animationClips[clipIndex], curveBindings[curveIndex]);

                // 각 키 프레임에 대해
                for (int keyFrameIndex = 0; keyFrameIndex < keyframes.Length; keyFrameIndex++)
                {
                    // 키 프레임의 스프라이트를 새로운 스프라이트로 변경
                    keyframes[keyFrameIndex].value = newSprites[spriteIndex % newSprites.Length];
                    spriteIndex++;
                }

                // 애니메이션 클립의 오브젝트 참조 커브 설정
                AnimationUtility.SetObjectReferenceCurve(animationClips[clipIndex], curveBindings[curveIndex], keyframes);
            }

            // 스프라이트 인덱스 갱신
            spriteIndex += spritesPerClip[clipIndex] - keyframes.Length;
        }
#endif
    }

    [Button]
    public void EmptySprites()
    {
        newSprites = null;
    }
}
