using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LegionUnitSelector : MonoBehaviour
{
    private Vector3 worldMousePos;
    private RaycastHit hit;

    [SerializeField] private Transform unitParent;

    public delegate void OnUnitsSelected();

    public static event OnUnitsSelected onArmyGathered;

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
                hit.transform.SetParent(unitParent);
            }
        }
    }
}