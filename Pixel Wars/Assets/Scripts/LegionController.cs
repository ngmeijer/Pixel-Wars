using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionController : MonoBehaviour
{ 
    public PixelUnit pixelProperties;
    [SerializeField] private GameObject[] arrayOfPixels;
    private List<PixelUnit> listOfEnemyUnits = new List<PixelUnit>();
    private float timer;
    private int index = 0;
    
    private void OnEnable()
    {
        switch (pixelProperties.colourEnum)
        {
            case ArmyColourEnum.GREEN:
                break;
            case ArmyColourEnum.RED:
                break;
            case ArmyColourEnum.BLUE:
                break;
            case ArmyColourEnum.ORANGE:
                break;
            case ArmyColourEnum.PURPLE:
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyUnit") || other.gameObject.CompareTag("PlayerUnit"))
        {
            if (index > arrayOfPixels.Length)
            {
                gameObject.SetActive(false);
                return;
            }
            
            // PixelUnit properties= other.GetComponent<LegionController>().pixelProperties;
            //
            // timer++;
            // if (timer >= properties.AttackSpeed)
            // {
            //     arrayOfPixels[index].gameObject.SetActive(false);
            //     index++;
            //     timer = 0;
            // }
        }
    }
}