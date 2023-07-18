using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownElement : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public Sprite[] sprite;
    public List<Element.Type> availableElements;
    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        availableElements = new List<Element.Type>();
        ResetDropdown(availableElements);
    }

    public void ResetDropdown(List<Element.Type> elements)
    {
        availableElements = elements;
        dropdown.ClearOptions();
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        foreach (Element.Type elementType in availableElements)
        {
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(sprite[(int)elementType]);
            options.Add(optionData);
        }
        dropdown.AddOptions(options);
    }

}
