using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AutoScrollToSelected : MonoBehaviour
{
    ScrollRect scrollRect;
    void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Update()
    {
        // Get the currently selected UI element from the EventSystem
        GameObject selected = EventSystem.current.currentSelectedGameObject;

        // If nothing is selected, exit
        if (selected == null)
        {
            return;
        }

        // If the selected game object is not inside the scroll rect, exit
        Transform parent = selected.transform.parent;
        while (parent != null)
        {
            if (parent == scrollRect.content.transform)
            {
                break;
            }
            parent = parent.parent;
        }

        // If we didn't find a valid parent, exit
        if (parent == null)
        {
            return;
        }

        float selectedPosition = selected.transform.position.y;

        // Scroll the scroll rect to the selected position
        scrollRect.verticalNormalizedPosition = Mathf.Lerp(scrollRect.verticalNormalizedPosition, selectedPosition, Time.deltaTime * 10);
    }
}
