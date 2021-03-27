using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private bool stillLoading;
    
    public delegate void FadeSceneOutOnLevelSelect(int pIndex);
    public static event FadeSceneOutOnLevelSelect fadeOutOnLevelSelect;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "LevelSelector")
        {
            LevelSelector.onLevelClicked += LoadScene;
        }
    }
    
    public void LoadScene(string pSceneName)
    {
        StartCoroutine(loadWithDelay(pSceneName));
    }

    public void LoadScene(int pLevelIndex)
    {
        fadeOutOnLevelSelect.Invoke(pLevelIndex);
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
}