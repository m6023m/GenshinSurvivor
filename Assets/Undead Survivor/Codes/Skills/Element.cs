using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Element
{
    public enum Type
    {
        Physics,
        Pyro,
        Hydro,
        Anemo,
        Dendro,
        Electro,
        Cyro,
        Geo,
        Immune,
        Fix
    }


    private static Color[] typeColors = {
        new Color (1,1,1,1), //Physics FFFFFF
        new Color (1,0.627f,0.4f,1), //Pyro FFA066
        new Color (0.341f,0.87f,1,1), //Hydro 57DEFF
        new Color (0.345f,0.968f,0.792f,1), //Anemo 58F7CA
        new Color (0.827f,0.96f,0.513f,1), //Dendro D3F583
        new Color (0.89f,0.654f,1,1), //Electro E3A7FF
        new Color (0.678f,0.988f,0.988f,1), //Cyro ADFCFC
        new Color (0.956f,0.85f,0.372f,1), //Geo FFC401
        new Color (0.443f,0.443f,0.443f,1), //Immune 717171
        new Color (0.443f,0.443f,0.443f,1)};//Fix 717171

    public static Color Color(Type type)
    {
        return typeColors[(int)type];
    }

}