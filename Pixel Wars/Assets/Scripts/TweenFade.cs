using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TweenFade : MonoBehaviour
{
    public static float TweenTime = 1f;
    private RectTransform fadeRectTransform;
    
    private static TweenFade instance;
    public static TweenFade Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else instance = this;
    }
    
    public void Start()
    {
        resetScript(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        SceneManager.sceneLoaded += resetScript;
        SceneHandler.fadeOutOnLevelSelect += TweenFadeSceneOut;
        
        DontDestroyOnLoad(this.gameObject);
    }

    public void TweenFadeSceneIn()
    {
        LeanTween.alpha(fadeRectTransform, 0f, TweenTime);
    }
    
    public void TweenFadeSceneOut()
    {
        Debug.Log(fadeRectTransform);
        LeanTween.alpha(fadeRectTransform, 1f, TweenTime);
    }

    private void resetScript(Scene pScene, LoadSceneMode pMode)
    {
        fadeRectTransform = GameObject.Find("FadeImage").GetComponent<RectTransform>();
        TweenFadeSceneIn();
    }
}