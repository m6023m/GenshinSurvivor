using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class ScrollWithRewired : MonoBehaviour
{
    ScrollRect scrollRect; // The ScrollRect you want to control
    public float scrollSpeed = 0.1f; // The speed at which the ScrollRect will scroll
    public string scrollButtonText = "Vertical";

    private Rewired.Player rewiredPlayer; // The Rewired Player

    void Awake()
    {
        // Get the Rewired Player
        scrollRect = GetComponent<ScrollRect>();
        rewiredPlayer = ReInput.players.GetPlayer(0);
        scrollRect.normalizedPosition = new Vector2(0, 1);
    }

    void Update()
    {
        // Get input from Rewired
        float verticalInput = rewiredPlayer.GetAxis(scrollButtonText);

        // Apply the input to the ScrollRect
        Vector2 newPosition = scrollRect.normalizedPosition;
        newPosition.y += verticalInput * scrollSpeed;
        newPosition.y = Mathf.Clamp(newPosition.y, 0f, 1f);
        scrollRect.normalizedPosition = newPosition;
    }
}
