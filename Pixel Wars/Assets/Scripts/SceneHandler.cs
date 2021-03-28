using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public delegate void FadeSceneOutOnLevelSelect();

    public static event FadeSceneOutOnLevelSelect fadeOutOnLevelSelect;

    private static SceneHandler instance;

    public static SceneHandler Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else instance = this;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += resetScript;
        
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(string pSceneName)
    {
        fadeOutOnLevelSelect?.Invoke();
        StartCoroutine(loadWithDelay(pSceneName));
    }

    public void LoadScene(int pLevelIndex)
    {
        Debug.Log("waiting to load level [ METHOD ] before invoking [ COROUTINE ]...");
        fadeOutOnLevelSelect?.Invoke();
        StartCoroutine(loadWithDelay(pLevelIndex));
        Debug.Log("waiting to load level [ METHOD ] after invoking [ COROUTINE ]...");
    }

    private IEnumerator loadWithDelay(string pSceneName)
    {
        yield return new WaitForSeconds(TweenFade.TweenTime);
        SceneManager.LoadScene(pSceneName);
    }

    private IEnumerator loadWithDelay(int pLevelIndex)
    {
        Debug.Log("waiting to load level [ COROUTINE ]...");
        yield return new WaitForSeconds(TweenFade.TweenTime);
        SceneManager.LoadScene("Level " + pLevelIndex.ToString());
    }


    private void resetScript(Scene pScene, LoadSceneMode pMode)
    {
        Debug.Log("Subscribing to LevelSelector");
        LevelSelector.onLevelClicked += LoadScene;
    }
}