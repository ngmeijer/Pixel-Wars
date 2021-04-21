using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    protected List<Light> listOfActiveUnitLights = new List<Light>();
    [SerializeField] protected List<GameObject> listOfActiveUnits = new List<GameObject>();
    
    [SerializeField] protected Transform selectedUnitParent;
    [SerializeField] protected Transform unselectedUnitParent;
}
