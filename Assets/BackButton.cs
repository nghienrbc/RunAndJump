using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public Texture2D color_Texture;
    private bool allowScene;
    public string SceneName;

    bool enableClick = true;
    private void Start()
    { 
    }

    private void OnEnable()
    {
        allowScene = false;
    }

    public void ButtonClick()
    {
        if (enableClick)
        {
            enableClick = false;
            StartCoroutine(LoadNextAsyncScene(SceneName));
            StartCoroutine(TimeDelay(0.5f));
        }
    } 

    IEnumerator LoadNextAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;
        while (asyncLoad.progress < 0.9f || !allowScene)
        {
            yield return null;
            //  print(!);
        }
        if(sceneName == "LoginScene")
        { 
            PlayerPrefs.SetInt("IsLoadedData", 0);
        }
        asyncLoad.allowSceneActivation = true;
        enableClick = true;

    }

    IEnumerator TimeDelay(float _timedelay)
    {
        yield return new WaitForSeconds(_timedelay);
        allowScene = true;
    }
} 
