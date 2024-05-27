using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject _loadingScreen;
    [SerializeField] GameObject _mainMenu;

    [SerializeField] Slider _loadingSlider;

    public void Print()
    {
        print("clicled");
    }

    public void LoadScene(int sceneId)
    {
        _mainMenu.SetActive(false);
        _loadingScreen.SetActive(true);

        StartCoroutine(BeginSceneLoading(sceneId));
    }

    IEnumerator BeginSceneLoading(int sceneId)
    {
        var operation = SceneManager.LoadSceneAsync(sceneId);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            _loadingSlider.value = progress;
            yield return null;
        }
    }
}
