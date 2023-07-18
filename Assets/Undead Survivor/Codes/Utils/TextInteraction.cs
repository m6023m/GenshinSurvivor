using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TextInteraction : MonoBehaviour
{
    private TMP_Text textMeshPro;
    public string targetString = "";
    public UnityAction<float, float> onMouseOver;
    public UnityAction<float, float> onMouseOut;
    float mousePositionX = 0;
    float mousePositionY = 0;
    bool isTooltip = false;

    private void Start()
    {
        // TextMeshPro 컴포넌트 가져오기
        textMeshPro = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (targetString == "") return;
        CheckTargetString();
    }

    private void CheckTargetString()
    {
        // 텍스트 정보 가져오기
        TMP_TextInfo textInfo = textMeshPro.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            // 문자열 체크
            if (IsMouseOverCharacter(charInfo))
            {
                if (IsTargetString(charInfo))
                {
                    if (onMouseOver != null)
                    {
                        onMouseOver.Invoke(mousePositionX, mousePositionY);
                        isTooltip = true;
                    }
                }
                else
                {
                    if (isTooltip && onMouseOut != null)
                    {
                        onMouseOut.Invoke(mousePositionX, mousePositionY);
                        isTooltip = false;
                    }
                }
            }

        }
    }

    private bool IsTargetString(TMP_CharacterInfo charInfo)
    {
        // 문자열 체크
        int charIndex = charInfo.strikethroughVertexIndex;
        int stringLength = targetString.Length;

        // 현재 인덱스와 문자열 길이를 기준으로 대상 문자열 체크
        if (charIndex + stringLength <= textMeshPro.text.Length)
        {
            string currentString = textMeshPro.text.Substring(charIndex, stringLength);
            return currentString == targetString;
        }

        return false;
    }

    private bool IsMouseOverCharacter(TMP_CharacterInfo charInfo)
    {
        // 마우스 위치 가져오기
        Vector3 mousePosition = Input.mousePosition;

        // 글자의 범위 계산
        Vector3 charMin = textMeshPro.transform.TransformPoint(charInfo.bottomLeft);
        Vector3 charMax = textMeshPro.transform.TransformPoint(charInfo.topRight);

        // 마우스가 글자 위에 있는지 확인
        if (mousePosition.x >= charMin.x && mousePosition.x <= charMax.x &&
            mousePosition.y >= charMin.y && mousePosition.y <= charMax.y)
        {
            mousePositionX = mousePosition.x;
            mousePositionY = mousePosition.y;
            return true;
        }

        return false;
    }
}
