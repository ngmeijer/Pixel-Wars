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
    
    
    private void Start()
    {
        originalMat = mat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyUnit") || other.gameObject.CompareTag("PlayerUnit"))
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
        if (other.gameObject.CompareTag("EnemyUnit") || other.gameObject.CompareTag("PlayerUnit"))
        {
            rend.material = originalMat;
        }
    }
}
