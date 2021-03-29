using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class IntroSceneTween : MonoBehaviour
{
    [Header("UI objects to animate")]
    [SerializeField] private GameObject upperBanner;
    [SerializeField] private GameObject lowerBanner;

    [SerializeField] private GameObject levelTitle;
    [SerializeField] private RectTransform background;
    
    [Header("Desired UI positions")]
    [SerializeField] private Transform levelTitleDesiredPosition1;
    [SerializeField] private Transform levelTitleDesiredPosition2;

    private const float animTime = 1f;
    private const float textMoveSpeed = 1f;
    private const float bannerMoveSpeed = 1f;

    private void Start()
    {
        StartCoroutine(handleBanners());
        StartCoroutine(handleText());
        StartCoroutine(handleBackground());
    }
    
    private IEnumerator handleBanners()
    {
        LeanTween.moveX(upperBanner, upperBanner.transform.position.x - (Screen.width * 0.3f), bannerMoveSpeed);
        LeanTween.moveX(lowerBanner, lowerBanner.transform.position.x + (Screen.width * 0.3f), bannerMoveSpeed);
        
        yield return new WaitForSeconds(animTime);
        
        LeanTween.moveX(upperBanner, upperBanner.transform.position.x - (Screen.width * 1.5f), bannerMoveSpeed);
        LeanTween.moveX(lowerBanner, lowerBanner.transform.position.x + (Screen.width * 1.2f), bannerMoveSpeed);
    }

    private IEnumerator handleText()
    {
        LeanTween.moveY(levelTitle,
            levelTitleDesiredPosition1.position.y, 
            textMoveSpeed);
        yield return new WaitForSeconds(animTime + 1f);
        
        LeanTween.moveY(levelTitle,
            levelTitleDesiredPosition2.position.y,
            textMoveSpeed);
    }

    private IEnumerator handleBackground()
    {
        LeanTween.alpha(background, 1f, 0.5f);

        yield return new WaitForSeconds(1f);

        LeanTween.alpha(background, 0f, 0.5f);
    }
}