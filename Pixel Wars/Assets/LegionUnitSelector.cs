using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LegionUnitSelector : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Vector3 worldMousePos;
    private RaycastHit hit;

    private void Start()
    {
        cam = Camera.main;
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
            if (Physics.Raycast(ray, out hit, 100))
            {
                hit.transform.localScale = new Vector3(2, 2, 2);
            }
        }
    }
}