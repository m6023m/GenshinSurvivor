using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public bool isPointerDown = false;
    public bool interactable = false;
    private float pointerDownTimer = 0f;
    private float onClickTimer = 0.5f;
    public float longClickDuration = 3f; // 롱클릭으로 인식할 최소 시간
    public UnityAction onClick;
    public UnityAction onLongClick;
    public UnityAction onLongClickStart;
    public UnityAction onLongClickCancel;
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!interactable) return;
        isPointerDown = true;
        if (onLongClickStart != null) onLongClickStart.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!interactable) return;
        if (onLongClickCancel != null) onLongClickCancel.Invoke();
        CheckClick();
        ResetButtonState();
    }
    public void CheckClick()
    {
        if (onClickTimer > pointerDownTimer)
        {
            if (onClick != null) onClick.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!interactable) return;
        if (onLongClickCancel != null) onLongClickCancel.Invoke();
        ResetButtonState();
    }

    private void Update()
    {
        if (!interactable) return;
        if (isPointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            image.fillAmount = pointerDownTimer / longClickDuration;
            if (pointerDownTimer >= longClickDuration)
            {
                if (onLongClick != null) onLongClick.Invoke();
                ResetButtonState();
            }
        }
    }

    public void ResetButtonState()
    {
        isPointerDown = false;
        pointerDownTimer = 0f;
        image.fillAmount = 0f;
    }
}
