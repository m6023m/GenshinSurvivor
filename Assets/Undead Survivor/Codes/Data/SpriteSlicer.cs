using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "SpriteSlicer", menuName = "GenshinSurvivor/SpriteSlicer", order = 1)]
public class SpriteSlicer : ScriptableObject
{
    [Required]
    public Texture2D textureToSlice;

    [BoxGroup("Sprite Size"), MinValue(1)]
    public int width = 32;

    [BoxGroup("Sprite Size"), MinValue(1)]
    public int height = 32;

    [BoxGroup("Sprite Settings")]
    public float pixelsPerUnit = 100.0f;  // 단위당 픽셀

    [BoxGroup("Sprite Settings")]
    public TextureImporterCompression textureCompression = TextureImporterCompression.CompressedLQ;  // 압축

    [BoxGroup("Sprite Settings")]
    public FilterMode filterMode = FilterMode.Bilinear;  // 필터 모드

    [BoxGroup("Sprite Settings"), Tooltip("Exclude frames that are fully transparent or a certain color")]
    public bool excludeEmptyFrames = true;


    [Button]
    private void SliceSprite()
    {
#if UNITY_EDITOR
        if (textureToSlice == null)
        {
            Debug.LogWarning("No texture specified!");
            return;
        }

        string path = AssetDatabase.GetAssetPath(textureToSlice);
        TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;

        ti.isReadable = true;
        ti.textureType = TextureImporterType.Sprite;
        ti.spriteImportMode = SpriteImportMode.Multiple;
        ti.spritePixelsPerUnit = pixelsPerUnit;
        ti.textureCompression = textureCompression;
        ti.filterMode = filterMode;

        // 변경 사항 적용
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

        List<SpriteMetaData> newData = new List<SpriteMetaData>();

        int spriteIndex = 0; // 새로 추가된 인덱스 변수

        for (int j = 0; j < textureToSlice.height; j += height) // <-- y 방향 먼저 반복
        {
            for (int i = 0; i < textureToSlice.width; i += width) // <-- x 방향으로 반복
            {
                if (excludeEmptyFrames && IsFrameEmpty(textureToSlice, i, j, width, height))
                    continue;

                SpriteMetaData smd = new SpriteMetaData
                {
                    alignment = 0,
                    border = new Vector4(0, 0, 0, 0),
                    name = string.Format("{0}_{1}", textureToSlice.name, spriteIndex),
                    pivot = new Vector2(0.5f, 0.5f),
                    rect = new Rect(i, j, width, height)
                };

                newData.Add(smd);

                spriteIndex++; // 인덱스 증가
            }
        }

        ti.spritesheet = newData.ToArray();
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
#endif
    }

#if UNITY_EDITOR
    private bool IsFrameEmpty(Texture2D texture, int xStart, int yStart, int width, int height)
    {
        for (int x = xStart; x < xStart + width; x++)
        {
            for (int y = yStart; y < yStart + height; y++)
            {
                if (texture.GetPixel(x, y).a != 0)
                    return false; // 투명하지 않은 픽셀이 하나라도 있으면 비어있지 않다고 판단
            }
        }
        return true;
    }
#endif
}