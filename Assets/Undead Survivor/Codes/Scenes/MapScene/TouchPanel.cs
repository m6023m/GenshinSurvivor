using UnityEngine;

public class TouchPanel : MonoBehaviour
{

    CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void LateUpdate()
    {

        if (GameDataManager.instance.saveData.option.isVisibleJoystick)
        {
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.alpha = 0;
        }
    }
}
