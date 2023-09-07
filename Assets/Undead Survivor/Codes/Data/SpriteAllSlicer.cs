using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "SpriteAllSlicer", menuName = "GenshinSurvivor/SpriteAllSlicer", order = 1)]
public class SpriteAllSlicer : ScriptableObject
{
    [Required, Title("Textures to Slice")]
    public Texture2D[] texturesToSlice;

    [BoxGroup("Sprite Settings")]
    public TextureImporterCompression textureCompression = TextureImporterCompression.CompressedLQ;  // 압축

    [BoxGroup("Sprite Settings")]
    public FilterMode filterMode = FilterMode.Bilinear;  // 필터 모드

    [BoxGroup("Sprite Settings"), Tooltip("Exclude frames that are fully transparent or a certain color")]
    public bool excludeEmptyFrames = true;

    [Button]
    private void SliceAllTextures()
    {
#if UNITY_EDITOR
        foreach (var texture in texturesToSlice)
        {
            SliceTexture(texture);
        }
#endif
    }

#if UNITY_EDITOR
    private void SliceTexture(Texture2D textureToSlice)
    {
        if (textureToSlice == null)
        {
            Debug.LogWarning("No texture specified!");
            return;
        }

        string path = AssetDatabase.GetAssetPath(textureToSlice);

        TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
        // Save the original settings
        bool originalIsReadable = ti.isReadable;
        bool originalMipmapEnabled = ti.mipmapEnabled;
        bool originalCrunchedCompression = ti.crunchedCompression;
        int originalCompressionQuality = ti.compressionQuality;
        int originalWidth = 0;
        int originalHeight = 0;
        ti.GetSourceTextureWidthAndHeight(out originalWidth, out originalHeight);
        TextureImporterCompression originalCompression = ti.textureCompression;

        // Set new settings
        ti.isReadable = true;
        ti.mipmapEnabled = false;
        ti.crunchedCompression = false;
        ti.textureCompression = TextureImporterCompression.Uncompressed;

        // Set to non-Crunch compression
        ti.textureType = TextureImporterType.Sprite;
        ti.spriteImportMode = SpriteImportMode.Multiple;
        ti.textureCompression = textureCompression;
        ti.filterMode = filterMode;

        // 변경 사항 적용
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

        // Reload the texture after settings have changed
        textureToSlice = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

        // Extract width and height from texture name
        Match match = Regex.Match(textureToSlice.name, @"_(\d+)x(\d+)$");
        if (!match.Success)
        {
            Debug.LogError("Failed to extract width and height from texture name: " + textureToSlice.name);
            return;
        }
        int width = int.Parse(match.Groups[1].Value);
        int height = int.Parse(match.Groups[2].Value);
        ti.spritePixelsPerUnit = (width + height) / 2.0f;

        // 변경 사항 적용
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

        int textureX = originalWidth / width;
        int textureY = originalHeight / height;
        if (originalWidth % width > 0) textureX++;
        if (originalHeight % height > 0) textureY++;
        Debug.Log(textureToSlice.name + " Width " + originalWidth + " Height " + originalHeight);
        Debug.Log(textureToSlice.name + " SizeX " + textureX + " SizeY " + textureY);

        // Grid by Cell Size를 사용하여 슬라이스
        SpriteMetaData[] tiles = new SpriteMetaData[textureX * textureY];
        for (int y = 0; y < textureY; y++)
        {
            for (int x = 0; x < textureX; x++)
            {
                // 해당 프레임이 빈 프레임인지 확인

                if (excludeEmptyFrames && IsFrameEmpty(textureToSlice, x * width, (textureY - y - 1) * height, width, height))
                    continue;

                SpriteMetaData tile = new SpriteMetaData
                {
                    name = string.Format("{0}_{1}", textureToSlice.name, y * textureX + x),
                    rect = new Rect(x * width, (textureY - y - 1) * height, width, height),
                    alignment = (int)SpriteAlignment.Center
                };

                tiles[y * textureX + x] = tile;
            }
        }
        ti.spritesheet = tiles;
        // Reset to original Crunch compression
        // Restore original settings
        ti.isReadable = originalIsReadable;
        ti.mipmapEnabled = originalMipmapEnabled;
        ti.textureCompression = originalCompression;
        ti.crunchedCompression = originalCrunchedCompression;
        ti.compressionQuality = originalCompressionQuality;


        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
    }

    private bool IsFrameEmpty(Texture2D texture, int xStart, int yStart, int width, int height)
    {
        for (int x = xStart; x < xStart + width; x++)
        {
            for (int y = yStart; y < yStart + height; y++)
            {
                if (texture.GetPixel(x, y).a != 0)
                {
                    Debug.Log(texture.name + " IsFrameEmpty false");
                    return false; // 투명하지 않은 픽셀이 하나라도 있으면 비어있지 않다고 판단
                }
            }
        }
        Debug.Log(texture.name + " IsFrameEmpty true");
        return true;
    }
#endif
}
