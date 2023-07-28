using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipePanel : MonoBehaviour
{
    private RectTransform touchArea;
    private Vector2 startPos;
    private bool isSwipe;

    [Header("# Rewiered")]
    Rewired.Player rewiredPlayer;

    List<UnityAction<Swipe>> _swipeListener;
    List<UnityAction<Swipe>> swipeListeners
    {
        get
        {
            if (_swipeListener == null)
            {
                _swipeListener = new List<UnityAction<Swipe>>();
            }
            return _swipeListener;
        }
    }

    private void Awake()
    {
        touchArea = GetComponent<RectTransform>();
        rewiredPlayer = Rewired.ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            isSwipe = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isSwipe)
            {
                Vector2 endPos = Input.mousePosition;
                HandleSwipe(startPos, endPos);
            }
        }
        if (rewiredPlayer.GetButtonDown("R1"))
        {
            InvokeSwipeListener(Swipe.LEFT);
        }
        else if (rewiredPlayer.GetButtonDown("L1"))
        {
            InvokeSwipeListener(Swipe.RIGHT);
        }
    }

    public void AddSwipeListener(UnityAction<Swipe> action)
    {
        swipeListeners.Add(action);
    }

    private void HandleSwipe(Vector2 start, Vector2 end)
    {
        if (!RectTransformUtility.RectangleContainsScreenPoint(touchArea, start))
        {
            return;
        }

        Vector2 swipeDirection = end - start;

        if (swipeDirection.x > 0)
        {
            InvokeSwipeListener(Swipe.RIGHT);
        }
        else if (swipeDirection.x < 0)
        {
            InvokeSwipeListener(Swipe.LEFT);
        }
    }

    private void InvokeSwipeListener(Swipe swipe)
    {
        foreach (UnityAction<Swipe> swipeAction in swipeListeners)
        {
            swipeAction.Invoke(swipe);
        }
    }
}

public enum Swipe
{
    LEFT,
    RIGHT
}
