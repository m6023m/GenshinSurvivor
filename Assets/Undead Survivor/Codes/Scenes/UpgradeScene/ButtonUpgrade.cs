using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonUpgrade : MonoBehaviour
{
    public GameObject panel;
    public Button btnUpgrade, btnBack;

    void Start()
    {
        btnUpgrade.onClick.AddListener(OnClickUpgrade);
        btnBack.onClick.AddListener(GoBack);
    }


    void OnClickUpgrade() {

    }

    void GoBack() {
        SceneManager.LoadScene("MainScene");
    }
}
