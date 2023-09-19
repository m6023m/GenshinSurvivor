using System;
using System.Text;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using UnityEngine;
using UnityEngine.EventSystems;

public static class UtilExtension
{

    public static bool CheckSuccessByRate(this float per)
    {
        float random = UnityEngine.Random.Range(0.0f, 1.0f);
        return random < per;
    }


}