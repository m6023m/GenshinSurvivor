using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class ButtonClicker : MonoBehaviour
{

    [Header("# Rewiered")]
    Rewired.Player rewiredPlayer;
    Button button; // Assign your buttons in the inspector
    public KeyCode[] keys; // Assign the corresponding keys in the inspector
    public string[] keyCustoms; // Assign the corresponding keys in the inspector
    public bool interactable = true;
    void Awake()
    {
        rewiredPlayer = Rewired.ReInput.players.GetPlayer(0);
        button = GetComponent<Button>();
    }


    void Update()
    {
        if (!interactable) return;
        foreach (KeyCode key in keys)
        {
            if (rewiredPlayer.GetButtonDown(key.ToString()))
            {
                button.onClick.Invoke();
            }
        }
        if (keyCustoms == null) return;
        foreach (string key in keyCustoms)
        {
            if (rewiredPlayer.GetButtonDown(key))
            {
                button.onClick.Invoke();
            }
        }
    }
}
