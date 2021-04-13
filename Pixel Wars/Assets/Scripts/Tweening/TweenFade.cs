using UnityEngine;

public class TweenFade : MonoBehaviour
{
    public static float TweenTime = 0.7f;
    private RectTransform fadeRectTransform;
    
    public void OnEnable()
    {
        fadeRectTransform = GameObject.Find("FadeImage").GetComponent<RectTransform>();
        SceneHandler.fadeOutOnLevelSelect += TweenFadeSceneOut;
        TweenFadeSceneIn();
    }

    public void TweenFadeSceneIn()
    {
        Debug.Log("fading scene in");
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