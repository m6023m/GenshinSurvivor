using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    public Slider progressBar;
    public CanvasGroup canvasGroup;

    // Instance를 가져옵니다.
    public static LoadingScreenController instance;

    private void Awake()
    {
        // 이 오브젝트를 파괴하지 않고 유지합니다.
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    // 씬을 비동기적으로 로드하는 코루틴을 시작하는 메서드입니다.
    public void LoadScene(string sceneName)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            progressBar.value = progress;
            yield return null;
        }

        canvasGroup.alpha = 0.0f;
        canvasGroup.blocksRaycasts = false;
    }
}
