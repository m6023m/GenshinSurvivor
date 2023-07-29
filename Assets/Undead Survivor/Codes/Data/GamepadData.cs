using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "GamepadData", menuName = "GenshinSurvivor/GamepadData", order = 0)]
public class GamepadData : ScriptableObject
{

    [Serializable]
    public class GamepadTexture
    {
        public GamepadType gamepadType;
        public Sprite gamepadSprite;
    }
    [Searchable]
    public List<GamepadTexture> gamepadTextures;

    public enum GamepadType
    {
        None,
        Joy_Left_Horizontal,
        Joy_Left_Vertical,
        Joy_Right_Horizontal,
        Joy_Right_Vertical,
        D_Pad_All,
        Menu,
        TouchPad,
        Circle,
        Triangle,
        Cross,
        Square,
        L1,
        L2,
        R1,
        R2
    }
    public GamepadTexture Get(GamepadData.GamepadType name)
    {
        foreach (GamepadTexture texture in gamepadTextures)
        {
            if (texture.gamepadType.Equals(name))
            {
                return texture;
            }
        }
        return null;
    }
}
