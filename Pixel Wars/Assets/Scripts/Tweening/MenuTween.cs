using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTween : MonoBehaviour
{
    [SerializeField] private GameObject tipsAndTricks;
    
    public void EnableTipsAndTricks()
    {
        tipsAndTricks.SetActive(true);
        LeanTween.scaleY(tipsAndTricks, 1f, 0.15f);
    }
    
    public void DisableTipsAndTricksWrapper()
    {
        StartCoroutine(disableTipsAndTricks());
    }

    private IEnumerator disableTipsAndTricks()
    {
        LeanTween.scaleY(tipsAndTricks, 0f, 0.15f);
        yield return new WaitForSeconds(0.15f);
        tipsAndTricks.SetActive(false);
    }
    
}
