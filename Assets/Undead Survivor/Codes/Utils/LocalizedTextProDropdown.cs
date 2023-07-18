using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.SimpleLocalization
{
    /// <summary>
    /// Localize text component.
    /// </summary>
    [RequireComponent(typeof(TMP_Dropdown))]
    public class LocalizedTextProDropdown : MonoBehaviour
    {
        public void Start()
        {
            Localize();
            LocalizationManager.LocalizationChanged += Localize;
        }

        public void OnDestroy()
        {
            LocalizationManager.LocalizationChanged -= Localize;
        }

        private void Localize()
        {
            LocalizeItem();
            LocalizeCaption();
        }

        private void LocalizeItem()
        {
            foreach (TMP_Dropdown.OptionData optionData in GetComponent<TMP_Dropdown>().options)
            {
                string optionText = optionData.text;
                optionData.text = optionText.Localize();
            }
        }

        private void LocalizeCaption()
        {
            TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();
            string captionText = dropdown.captionText.text;
            dropdown.captionText.text = captionText.Localize();
        }

    }
}