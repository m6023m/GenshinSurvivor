using UnityEngine;
using UnityEngine.UI;

public class ButtonClicker : MonoBehaviour
{
    Button button; // Assign your buttons in the inspector
    public KeyCode[] keys; // Assign the corresponding keys in the inspector
    void Awake()
    {
        button = GetComponent<Button>();
    }


    void Update()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]))
            {
                button.onClick.Invoke();
            }
        }
    }
}
