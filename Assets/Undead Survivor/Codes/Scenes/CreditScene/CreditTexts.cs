using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreditTexts : MonoBehaviour
{
    public CreditText[] creditTexts;
    private void Awake()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        string textResult = "";
        foreach (CreditText creditText in creditTexts)
        {
            textResult = textResult.AddString(creditText.discription);
            if (creditText.link.Length > 0)
            {
                textResult = textResult.AddString(" ".AddString(creditText.link.FormatLink()));
            }
            textResult = textResult.AddString("\n");
        }
        text.text = textResult;
    }
}

[Serializable]
public class CreditText
{
    public string discription;
    public string link;
}