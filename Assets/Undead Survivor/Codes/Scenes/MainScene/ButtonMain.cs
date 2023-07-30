using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ButtonMain : MonoBehaviour
{
    public Button btnStart, btnUpgrade, btnGatcha, btnArchive, btnOption, btnCredit, btnExit, btnWeapon;
    public MainOption optionPannel;
    public GameObject saveBattlePanel;
    public Button saveBattleYes, saveBattleNo;
    void Start()
    {
        btnStart.onClick.AddListener(OnClickStart);
        btnUpgrade.onClick.AddListener(OnClickUpgrade);
        btnGatcha.onClick.AddListener(OnClickGatcha);
        btnArchive.onClick.AddListener(OnClickArchive);
        btnOption.onClick.AddListener(OnClickOption);
        btnCredit.onClick.AddListener(OnClickCredit);
        btnExit.onClick.AddListener(OnClickExit);
        btnWeapon.onClick.AddListener(OnClickWeapon);
        saveBattleYes.onClick.AddListener(OnClickBattleYes);
        saveBattleNo.onClick.AddListener(OnClickBattleNo);
        Time.timeScale = 1;
        AudioManager.instance.PlayBGM(AudioManager.BGM.Main, true);
        if (GameDataManager.instance.saveData.gameInfoData.isGameContinue)
        {
            saveBattlePanel.SetActive(true);
            saveBattleYes.gameObject.SelectObject();
        }
    }

    void OnClickBattleYes()
    {
        saveBattlePanel.SetActive(false);
        SceneManager.LoadScene(GameDataManager.instance.saveData.gameInfoData.currentScene);
    }
    void OnClickBattleNo()
    {
        GameDataManager.instance.saveData.gameInfoData.isGameContinue = false;
        btnStart.gameObject.SelectObject();
        saveBattlePanel.SetActive(false);
    }
    void OnClickStart()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        SceneManager.LoadScene("CharacterSelectScene");
    }

    void OnClickUpgrade()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        SceneManager.LoadScene("UpgradeScene");
    }

    void OnClickGatcha()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        SceneManager.LoadScene("WishScene");
    }

    void OnClickArchive()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        SceneManager.LoadScene("AchieveScene");
    }

    void OnClickWeapon()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        SceneManager.LoadScene("WeaponScene");
    }

    void OnClickOption()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        optionPannel.gameObject.SetActive(true);
        optionPannel.closeAction = () =>
        {
            btnStart.gameObject.SelectObject();
        };
        saveBattleYes.gameObject.SelectObject();
    }

    void OnClickCredit()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        SceneManager.LoadScene("CreditScene");
    }
    void OnClickExit()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        Application.Quit();
    }


    void MoveMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
