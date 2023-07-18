using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VersionText : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = "v".AddString(Application.version);
    }
}
