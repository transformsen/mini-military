using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider slider;

    AsyncOperation async;

    public void PlayGame()
    {
        StartCoroutine(Load("GameScene"));
    }

    private IEnumerator Load(string senceName)
    {
        loadingScreen.SetActive(true);
        async = SceneManager.LoadSceneAsync(senceName);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            Debug.Log("async.progress=" + async.progress);
            slider.value = async.progress;
            if (async.progress == 0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }

    }

    public void OpenAvatarSelection()
    {
        StartCoroutine(Load("AvatarSelectionScene")); 
    }

    public void ExitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
