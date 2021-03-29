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
    
    [Header("Desired UI positions")]
    [SerializeField] private Transform levelTitleDesiredPosition1;
    [SerializeField] private Transform levelTitleDesiredPosition2;

    private const float animTime = 0.45f;

    private void Start()
    {
        StartCoroutine(handleBanners());
        StartCoroutine(handleText());
    }
    
    private IEnumerator handleBanners()
    {
        LeanTween.moveX(upperBanner, upperBanner.transform.position.x - (Screen.width * 0.3f), animTime);
        LeanTween.moveX(lowerBanner, lowerBanner.transform.position.x + (Screen.width * 0.3f), animTime);
        
        yield return new WaitForSeconds(animTime);
        
        LeanTween.moveX(upperBanner, upperBanner.transform.position.x - (Screen.width * 1.5f), 1f);
        LeanTween.moveX(lowerBanner, lowerBanner.transform.position.x + (Screen.width * 1.2f), 1f);
    }

    private IEnumerator handleText()
    {
        LeanTween.moveY(levelTitle,
            levelTitleDesiredPosition1.position.y, 
            0.5f);
        yield return new WaitForSeconds(animTime + 0.58f);
        
        LeanTween.moveY(levelTitle,
            levelTitleDesiredPosition2.position.y,
            0.5f);
    }
}