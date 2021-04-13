using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private Material mat;
    private Material originalMat;
    [SerializeField]private Renderer rend;
    [SerializeField] private Material[] colourMaterials = new Material[2];
    private int amountOfPlayerUnits;
    private int amountOfEnemyUnits;
    
    private void Start()
    {
        originalMat = mat;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool enemyHasEntered = false;
        if (other.gameObject.CompareTag("EnemyUnit"))
        {
            amountOfEnemyUnits++;
            enemyHasEntered = true;
        }

        bool playerHasEntered = false;
        if (other.gameObject.CompareTag("PlayerUnit"))
        {
            amountOfPlayerUnits++;
            playerHasEntered = true;
        }
        
        if (playerHasEntered || enemyHasEntered)
        {
            ArmyColour colour = other.gameObject.GetComponent<ArmyColour>();

            switch (colour.usedColour)
            {
                case ArmyColourEnum.GREEN:
                    rend.material = colourMaterials[0];
                    break;
                case ArmyColourEnum.BLUE:
                    rend.material = colourMaterials[1];
                    break;
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        bool enemyHasExited = false;
        if (other.gameObject.CompareTag("EnemyUnit"))
        {
            amountOfEnemyUnits--;
            enemyHasExited = true;
        }

        bool playerHasExited = false;
        if (other.gameObject.CompareTag("PlayerUnit"))
        {
            amountOfPlayerUnits--;
            playerHasExited = true;
        }
        
        if (playerHasExited || enemyHasExited)
        {
            if(amountOfPlayerUnits == 0 && amountOfEnemyUnits == 0)
                rend.material = originalMat;
        }
    }
}