using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class ButtonClicker : MonoBehaviour
{

    [Header("# Rewiered")]
    Rewired.Player rewiredPlayer;
    Button button; // Assign your buttons in the inspector
    public KeyCode[] keys; // Assign the corresponding keys in the inspector
    void Awake()
    {
        rewiredPlayer = Rewired.ReInput.players.GetPlayer(0);
        button = GetComponent<Button>();
    }


    void Update()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (rewiredPlayer.GetButtonDown(keys[i].ToString()))
            {
                button.onClick.Invoke();
            }
        }
    }
}
