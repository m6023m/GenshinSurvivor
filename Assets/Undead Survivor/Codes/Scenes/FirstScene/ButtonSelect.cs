using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonSelect : MonoBehaviour
{
    public Button btnAether, btnLumine, btnYesNick, btnNoNick, btnYesConfirm, btnNoConfirm;
    public RectTransform transformNick, transformConfirm;
    public TextMeshProUGUI txtName;

    void Start()
    {
        btnAether.onClick.AddListener(OnClickAether);
        btnLumine.onClick.AddListener(OnClickLumine);
        btnYesNick.onClick.AddListener(OnClickYesNick);
        btnNoNick.onClick.AddListener(OnClickNoNick);
        btnYesConfirm.onClick.AddListener(OnclickYesConfirm);
        btnNoConfirm.onClick.AddListener(OnClickNoConfirm);
    }

    void OnClickAether()
    {
        GameDataManager.instance.saveData.userData.defaultChar = UserData.AETHER;
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        OpenCloseDialog(transformNick, true);
    }

    void OnClickLumine()
    {
        GameDataManager.instance.saveData.userData.defaultChar = UserData.LUMINE;
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        OpenCloseDialog(transformNick, true);
    }

    void OnClickYesNick()
    {
        OpenCloseDialog(transformConfirm, true);
        OpenCloseDialog(transformNick, false);
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
    }

    void OnClickNoNick()
    {
        OpenCloseDialog(transformNick, false);
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
    }

    void OnclickYesConfirm()
    {
        GameDataManager.instance.saveData.userData.name = txtName.text;
        GameDataManager.instance.saveData.charactors[GameDataManager.instance.saveData.userData.defaultChar].isMine = true;
        GameDataManager.instance.SaveInstance();
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        MoveMain();
    }

    void OnClickNoConfirm()
    {
        OpenCloseDialog(transformConfirm, false);
        OpenCloseDialog(transformNick, true);
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
    }

    void OpenCloseDialog(Transform transform, bool isOpen)
    {
        transform.gameObject.SetActive(isOpen);
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
    }


    void MoveMain()
    {
        LoadingScreenController.instance.LoadScene("MainScene");
    }
}
