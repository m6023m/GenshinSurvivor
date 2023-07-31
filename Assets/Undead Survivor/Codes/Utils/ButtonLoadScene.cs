using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonLoadScene : MonoBehaviour
{
    public string SceneName;
    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(LoadScene);
    }

    void LoadScene()
    {
        LoadingScreenController.instance.LoadScene(SceneName);
    }
}