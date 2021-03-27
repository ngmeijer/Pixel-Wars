using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public delegate void OnLevelButtonClicked(int pIndex);

    public static OnLevelButtonClicked onLevelClicked;

    public void SelectLevel(int pIndex)
    {
        if (onLevelClicked != null)
            onLevelClicked.Invoke(pIndex);
    }
}