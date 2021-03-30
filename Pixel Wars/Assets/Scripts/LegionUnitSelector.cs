using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class LegionUnitSelector : MonoBehaviour
{
    private RaycastHit hit;
    private LineRenderer highlightAreaRenderer;
    private int highlightIndex = 0;

    [SerializeField] private Transform selectedUnitParent;
    [SerializeField] private Transform unselectedUnitParent;
    private List<GameObject> listOfExistingUnits = new List<GameObject>();
    [SerializeField] private List<GameObject> listOfActiveUnits = new List<GameObject>();
    
    private List<Light> listOfExistingUnitLights = new List<Light>();
    private List<Light> listOfActiveUnitLights = new List<Light>();

    private delegate void SelectedUnit();

    private event SelectedUnit onUnitSelect;
    
    public delegate void OnUnitsSelected();

    public static event OnUnitsSelected onArmyGathered;

    private void Start()
    {
        PlayerMovement.onArrive += clearUnitSelection;
        PlayerMovement.onArrive += disableUnitHighlight;
        
        LegionSpawner.onLegionUnitSpawned += handleComponentCaching;
        
        highlightAreaRenderer = GetComponent<LineRenderer>();

        onUnitSelect += enableUnitHighlight;
    }

    private void Update()
    {
        checkUnitSelection();
    }

    private void checkUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100) && hit.transform.CompareTag("PlayerUnit"))
            {
                if (!listOfActiveUnits.Contains(hit.transform.gameObject))
                {
                    hit.transform.SetParent(selectedUnitParent);
                    listOfActiveUnits.Add(hit.transform.gameObject);
                    listOfActiveUnitLights.Add(hit.transform.gameObject.GetComponentInChildren<Light>());
                    
                    enableUnitHighlight();
                }
            }
        }
    }

    private void clearUnitSelection()
    {
        disableUnitHighlight();
        for (int index = 0; index < listOfActiveUnits.Count; index++)
        {
            listOfActiveUnits[index].transform.SetParent(unselectedUnitParent);
        }

        listOfActiveUnits.Clear();
    }

    private void handleComponentCaching(GameObject pUnit)
    {
        listOfExistingUnits.Add(pUnit);
    }

    private void enableUnitHighlight()
    {
        foreach (Light light in listOfActiveUnitLights)
        {
            light.enabled = true;
        }
    }

    private void disableUnitHighlight()
    {
        foreach (Light light in listOfActiveUnitLights)
        {
            light.enabled = false;
        }
        
        listOfActiveUnitLights.Clear();
    }
}