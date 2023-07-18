using System;
using System.Text;
using System.Collections.Generic;
using Assets.SimpleLocalization;
using UnityEngine;

public static class FormatExtension
{

    public static string FormatDecimalPlaces(this float value)
    {
        int intValue = (int)value;
        if (Math.Abs(value - intValue) > 0.01f)
        {
            // 소수점 아래에 값이 있을 경우, 최대 두 자리까지 표시
            return string.Format("{0:0.00}", value);
        }
        else
        {
            // 소수점 아래에 값이 없을 경우, 소수점을 표시하지 않음
            return string.Format("{0:0}", value);
        }
    }

    public static float GetPercetage(this float value)
    {
        return value * 100.0f;
    }

    public static string Localize(this string value)
    {
        return LocalizationManager.Localize(value);
    }
    public static string Localize(this string value, params object[] args)
    {
        if (args == null || args.Length == 0)
        {
            return LocalizationManager.Localize(value);
        }
        return LocalizationManager.Localize(value, args);
    }
    public static string LocalizeFloatArray(this string value, params float[] args)
    {
        switch (args.Length)
        {
            case 0:
                return LocalizationManager.Localize(value);
            case 1:
                return LocalizationManager.Localize(value, args[0]);
            case 2:
                return LocalizationManager.Localize(value, args[0], args[1]);
            case 3:
                return LocalizationManager.Localize(value, args[0], args[1], args[2]);
            case 4:
                return LocalizationManager.Localize(value, args[0], args[1], args[2], args[3]);
            case 5:
                return LocalizationManager.Localize(value, args[0], args[1], args[2], args[3], args[4]);
        }
        return LocalizationManager.Localize(value);
    }

    public static void OpenURL(this string url)
    {
        Application.OpenURL(url);
    }

    public static int ParseInt(this string value)
    {
        return int.Parse(value);
    }

    public static string FormatLink(this string value)
    {
        return "<color=blue><u><link=".AddString(value, ">Link</link></u></color>");
    }
    public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] = value;
        }
        else
        {
            dictionary.Add(key, value);
        }
    }

    public static void AddOrNotthing<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (!dictionary.ContainsKey(key))
        {
            dictionary.Add(key, value);
        }
    }

    public static Dictionary<TKey, TValue> DeepCopy<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
    {
        Dictionary<TKey, TValue> newDictionary = new Dictionary<TKey, TValue>(dictionary.Count);

        foreach (KeyValuePair<TKey, TValue> kvp in dictionary)
        {
            newDictionary.Add(kvp.Key, kvp.Value);
        }

        return newDictionary;
    }
    public static float CalcUpgradeWeapon(this float initValue, float addValue, int currentLevel, int maxLevel, float maxValue)
    {
        float result = initValue;
        float previousResult = initValue;

        for (int level = currentLevel; level <= maxLevel; level++)
        {
            result += addValue * level;

            if (result > maxValue)
            {
                return previousResult;
            }

            previousResult = result;
        }

        return result;
    }
    public static int CalcUpgradeWeaponLevel(this float initValue, float addValue, int currentLevel, int maxLevel, float maxValue)
    {
        float result = initValue;
        float previousResult = initValue;

        for (int level = currentLevel; level <= maxLevel; level++)
        {
            result += addValue * level;

            if (result > maxValue)
            {
                return level;
            }

            previousResult = result;
        }

        return maxLevel;
    }

    public static object[] ConvertToObjectArray(params object[] args)
    {
        return args;
    }
    static StringBuilder stringBuilder;

    public static string AddString(this string value, params string[] addText)
    {
        if (stringBuilder == null)
        {
            stringBuilder = new StringBuilder();
        }
        else
        {
            stringBuilder.Clear();
        }

        stringBuilder.Append(value);
        stringBuilder.Append(string.Join("", addText));

        return stringBuilder.ToString();
    }


}