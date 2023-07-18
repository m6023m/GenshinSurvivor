using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.SimpleLocalization;

public class Localization : MonoBehaviour
{
    public void Awake()
    {
        LocalizationManager.Read();

        switch (Application.systemLanguage)
        {
            case SystemLanguage.Korean:
                LocalizationManager.Language = "Korean";
                break;
            case SystemLanguage.English:
                LocalizationManager.Language = "English";
                break;
            default:
                LocalizationManager.Language = "English";
                break;
        }

        // This way you can insert values to localized strings.
        // FormattedText.text = LocalizationManager.Localize("Settings.PlayTime", TimeSpan.FromHours(10.5f).TotalHours);

        // This way you can subscribe to localization changed event.
        // LocalizationManager.LocalizationChanged += () => FormattedText.text = LocalizationManager.Localize("Settings.PlayTime", TimeSpan.FromHours(10.5f).TotalHours);
    }

    public void SetLocalization(string localization)
    {
        LocalizationManager.Language = localization;
    }

}