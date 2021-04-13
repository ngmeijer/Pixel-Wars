using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTween : MonoBehaviour
{
    public void OnButtonClick(Button pButton)
    {
        Vector3 buttonScale = pButton.gameObject.transform.localScale;
        LeanTween.scaleX(pButton.gameObject, buttonScale.x - 1f, 1f);
        LeanTween.scaleY(pButton.gameObject, buttonScale.y - 1f, 1f);
    }
}
