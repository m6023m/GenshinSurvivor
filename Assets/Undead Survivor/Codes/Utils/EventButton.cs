using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class EventButton : Button
{
    public UnityAction onSelect;
    public UnityAction onDeselect;
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        if (onSelect != null)
        {
            onSelect.Invoke();
        }
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        if (onDeselect != null)
        {
            onDeselect.Invoke();
        }
    }
}

