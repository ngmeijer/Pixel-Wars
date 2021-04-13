using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class LegionUnitSelector : MonoBehaviour
{
    private RaycastHit hit;

    [SerializeField] private Transform selectedUnitParent;
    [SerializeField] private Transform unselectedUnitParent;
    private List<GameObject> listOfExistingUnits = new List<GameObject>();
    [SerializeField] private List<GameObject> listOfActiveUnits = new List<GameObject>();
    
    private List<Light> listOfExistingUnitLights = new List<Light>();
    private List<Light> listOfActiveUnitLights = new List<Light>();

    public delegate void SelectedUnit(GameObject pUnit);
    public static event SelectedUnit onUnitSelect;

    private void Start()
    {
        PlayerMovement.onArrive += clearUnitSelection;
        
        onUnitSelect += enableUnitHighlight;
        onUnitSelect += transferUnitToSelectionParent;
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
                    onUnitSelect?.Invoke(hit.transform.gameObject);
                }
            }
        }
    }

    private IEnumerator clearUnitSelection()
    {
        StartCoroutine(disableUnitHighlight());
        for (int index = 0; index < listOfActiveUnits.Count; index++)
        {
            listOfActiveUnits[index].transform.SetParent(unselectedUnitParent);
        }
        listOfActiveUnits.Clear();

        yield break;
    }

    private void handleComponentCaching(GameObject pUnit)
    {
        listOfExistingUnits.Add(pUnit);
    }

    private void enableUnitHighlight(GameObject pUnit)
    {
        Light light = pUnit.GetComponentInChildren<Light>();
        light.enabled = true;
    }

    private void transferUnitToSelectionParent(GameObject pUnit)
    {
        hit.transform.SetParent(selectedUnitParent);
        listOfActiveUnits.Add(hit.transform.gameObject);
        listOfActiveUnitLights.Add(hit.transform.gameObject.GetComponentInChildren<Light>());
    }

    private IEnumerator disableUnitHighlight()
    {
        yield return new WaitForSeconds(1f);
        foreach (Light light in listOfActiveUnitLights)
        {
            light.enabled = false;
        }
        
        listOfActiveUnitLights.Clear();
        yield break;
    }
}