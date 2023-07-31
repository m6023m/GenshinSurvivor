using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectFirstNavigable : MonoBehaviour
{
    private EventSystem eventSystem;

    void Start()
    {
        eventSystem = EventSystem.current;
    }

    void Update()
    {
        if (eventSystem.currentSelectedGameObject == null)
        {
            Selectable[] selectables = FindObjectsOfType<Selectable>();
            foreach (var selectable in selectables)
            {
                if (selectable.gameObject.activeSelf && selectable.IsInteractable() && selectable.navigation.mode != Navigation.Mode.None)
                {
                    eventSystem.SetSelectedGameObject(selectable.gameObject);
                    break;
                }
            }
        }
    }
}
