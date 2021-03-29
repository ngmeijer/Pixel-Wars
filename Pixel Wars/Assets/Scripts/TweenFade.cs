using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TweenFade : MonoBehaviour
{
    public static float TweenTime = 1f;
    private RectTransform fadeRectTransform;
    
    public void Start()
    {
        fadeRectTransform = GameObject.Find("FadeImage").GetComponent<RectTransform>();
        SceneHandler.fadeOutOnLevelSelect += TweenFadeSceneOut;
        TweenFadeSceneIn();
    }

    public void TweenFadeSceneIn()
    {
        LeanTween.alpha(fadeRectTransform, 0f, TweenTime);
    }
    
    public void TweenFadeSceneOut()
    {
        LeanTween.alpha(fadeRectTransform, 1f, TweenTime);
    }

    private void OnDestroy()
    {
        SceneHandler.fadeOutOnLevelSelect -= TweenFadeSceneOut;
    }
}