using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenFade : MonoBehaviour
{
    public static float TweenTime = 1f;
    [SerializeField] private RectTransform rect;

    public void Start()
    {
        TweenFadeSceneIn();

        SceneHandler.fadeOutOnLevelSelect += TweenFadeSceneOut;
    }

    public void TweenFadeSceneIn()
    {
        LeanTween.alpha(rect, 0f, TweenTime);
    }
    
    public void TweenFadeSceneOut(int pIndex)
    {
        LeanTween.alpha(rect, 1f, TweenTime);
    }
}