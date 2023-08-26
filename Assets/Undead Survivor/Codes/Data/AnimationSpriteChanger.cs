using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "AnimationSpriteChanger", menuName = "GenshinSurvivor/AnimationSpriteChanger", order = 1)]
public class AnimationSpriteChanger : ScriptableObject
{
    public AnimationClip animationClip;
    public Sprite[] newSprites;

    [Button]
    public void ChangeSprites()
    {
        // 에디터에서만 실행
        #if UNITY_EDITOR
        // 애니메이션 클립의 에디터 커브 바인딩 가져오기
        EditorCurveBinding[] curveBindings = AnimationUtility.GetObjectReferenceCurveBindings(animationClip);

        // 각 커브 바인딩에 대해
        for (int i = 0; i < curveBindings.Length; i++)
        {
            // 에디터 커브 바인딩의 오브젝트 참조 키 프레임 가져오기
            ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(animationClip, curveBindings[i]);

            // 각 키 프레임에 대해
            for (int j = 0; j < keyframes.Length; j++)
            {
                // 키 프레임의 스프라이트를 새로운 스프라이트로 변경
                keyframes[j].value = newSprites[j % newSprites.Length];
            }

            // 애니메이션 클립의 오브젝트 참조 커브 설정
            AnimationUtility.SetObjectReferenceCurve(animationClip, curveBindings[i], keyframes);
        }
        #endif
    }
}
