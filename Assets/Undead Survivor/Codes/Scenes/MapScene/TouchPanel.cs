using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TouchPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public bl_Joystick joy;
    public Image outLineCircle;
    public Image stick;
    public PointerEventData currentPosition;
    public TextMeshProUGUI text;

    public Vector2 GetDir()
    {
        return new Vector2(joy.Horizontal, joy.Vertical);
    }

    public void OnPointerDown(PointerEventData data)
    {
        text.text = string.Format("PointerDown{0}", data.position);

        joy.OnPointerDown(data);

        if (GameDataManager.instance.saveData.option.isVisibleJoystick)
        {
            outLineCircle.color = new Color(1, 1, 1, 0.5f);
            stick.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            outLineCircle.color = new Color(1, 1, 1, 0.0f);
            stick.color = new Color(1, 1, 1, 0.0f);
        }
    }

    public void OnDrag(PointerEventData data)
    {
        text.text = string.Format("Drag{0}", data.position);
        currentPosition = data;
        joy.OnDrag(data);
    }
    public void OnPointerUp(PointerEventData data)
    {
        text.text = string.Format("PointerUp{0}", data.position);
        joy.OnPointerUp(data);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    private void OnEnable()
    {
        outLineCircle.color = new Color(1, 1, 1, 0);
        stick.color = new Color(1, 1, 1, 0);
        if (currentPosition != null)
        {
            joy.OnPointerUpFix();
        }
    }

}
