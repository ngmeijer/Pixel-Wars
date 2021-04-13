using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public delegate void FadeSceneOutOnLevelSelect();

    public static event FadeSceneOutOnLevelSelect fadeOutOnLevelSelect;
    
    private void Start()
    {
        LevelSelector.onLevelClicked += LoadScene;
    }

    public void LoadScene(string pSceneName)
    {
        fadeOutOnLevelSelect?.Invoke();
        StartCoroutine(loadWithDelay(pSceneName));
    }

    public void LoadScene(int pLevelIndex)
    {
        fadeOutOnLevelSelect?.Invoke();
        StartCoroutine(loadWithDelay(pLevelIndex));
    }

    private IEnumerator loadWithDelay(string pSceneName)
    {
        yield return new WaitForSeconds(TweenFade.TweenTime);
        SceneManager.LoadScene(pSceneName);
    }

    private IEnumerator loadWithDelay(int pLevelIndex)
    {
        yield return new WaitForSeconds(TweenFade.TweenTime);
        SceneManager.LoadScene("Level " + pLevelIndex.ToString());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        LevelSelector.onLevelClicked -= LoadScene;
    }
}