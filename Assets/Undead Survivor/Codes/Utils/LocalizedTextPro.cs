using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.SimpleLocalization
{
    /// <summary>
    /// Localize text component.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedTextPro : MonoBehaviour
    {
        public string LocalizationKey;
        TextMeshProUGUI _textMesh;
        TextMeshProUGUI textMesh
        {
            get
            {
                if (_textMesh == null) _textMesh = GetComponent<TextMeshProUGUI>();
                return _textMesh;
            }
        }

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
            textMesh.text = LocalizationKey.Localize();
        }
    }
}