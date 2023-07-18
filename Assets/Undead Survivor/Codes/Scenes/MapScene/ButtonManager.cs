using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [Header("# Buttons")]
    public Button btnPaimon;
    public Button btnContinue;
    public Button btnOption;
    public Button btnCloseOption;
    public Button btnSurrender;
    public Button btnSurrenderOk;
    public Button btnSurrenderCancel;
    public Button btnNext;

    [Header("# Popup")]
    public GameObject pauseWindow;
    public GameObject optionWindow;
    public GameObject surrenderConfirmWindow;
    public GameObject nextWindow;
    public GameObject levelUpWindow;
    void Awake()
    {
        btnPaimon.onClick.AddListener(OnClickPaimon);
        btnContinue.onClick.AddListener(OnClickContinue);
        btnOption.onClick.AddListener(OnClickOption);
        btnCloseOption.onClick.AddListener(OnClickCloseOption);
        btnSurrender.onClick.AddListener(OnClickSurrender);
        btnSurrenderOk.onClick.AddListener(OnClickSurrenderOk);
        btnSurrenderCancel.onClick.AddListener(OnClickSurrenderCancel);
        btnNext.onClick.AddListener(OnClickNext);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Event.current.Use();
            if (levelUpWindow.activeSelf) return;
            if (GameManager.instance.IsPause)
            {
                pauseWindow.SetActive(false);
                optionWindow.SetActive(false);
                GameManager.instance.Pause(false);
            }
            else
            {
                OnClickPaimon();
            }
        }
    }

    void OnClickPaimon()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Paimon);
        if (GameManager.instance.IsPause == false)
        {
            GameManager.instance.Pause(true);
            pauseWindow.SetActive(true);
            GameManager.instance.infoManager.Init();
            GameDataManager.instance.saveData.record.clickPaimonCount++;
        }
    }

    void OnClickContinue()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.PaimonBack);
        GameManager.instance.Pause(false);
        pauseWindow.SetActive(false);
    }

    void OnClickCloseOption()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        pauseWindow.SetActive(true);
        optionWindow.SetActive(false);
    }

    void OnClickOption()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        pauseWindow.SetActive(false);
        optionWindow.SetActive(true);
    }

    void OnClickSurrender()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        surrenderConfirmWindow.SetActive(true);
        pauseWindow.SetActive(false);
    }

    void OnClickSurrenderOk()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        surrenderConfirmWindow.SetActive(false);
        GameManager.instance.player.Surrender();
        GameManager.instance.Pause(false);
    }
    void OnClickSurrenderCancel()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        surrenderConfirmWindow.SetActive(false);
        pauseWindow.SetActive(true);
    }
    public void PopNextDefeat()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Lose);
        AudioManager.instance.PlayBGM(AudioManager.BGM.Battle0, false);
        GameDataManager.instance.saveData.record.gameOverCount++;
        nextWindow.SetActive(true);
    }
    public void PopNextVictory()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Victory);
        AudioManager.instance.PlayBGM(AudioManager.BGM.Battle0, false);
        GameDataManager.instance.saveData.record.gameClearCount++;
        nextWindow.SetActive(true);
    }
    void OnClickNext()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        GameDataManager.instance.saveData.userData.mora += GameManager.instance.battleResult.gainMora;
        GameDataManager.instance.saveData.userData.primoGem += GameManager.instance.battleResult.gainPrimoGem;
        GameManager.instance.gameInfoData.isGameContinue = false;
        GameDataManager.instance.SaveInstance();
        SceneManager.LoadScene("ResultScene");
    }
}
