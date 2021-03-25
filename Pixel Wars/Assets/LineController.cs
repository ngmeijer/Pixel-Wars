using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LineController : MonoBehaviour
{
    private LineRenderer renderer;
    [SerializeField] private List<Vector3> points = new List<Vector3>();
    [SerializeField] private float _updateInterval = 0.1f;
    [SerializeField] private Transform Player;
    private float timer;
    private Camera cam;
    public Vector3 Offset;

    public delegate void OnPathDrawn(List<Vector3> pPosition);

    public static OnPathDrawn onPathDrawn;
    private bool selectedUnits;

    private void Awake()
    {
        renderer = GetComponent<LineRenderer>();
        cam = Camera.main;
        LegionUnitSelector.onArmyGathered += handleLineDrawingInput;
        PlayerMovement.onArrive += clearWaypointList;
    }

    private void clearWaypointList()
    {
        points.Clear();
        renderer.positionCount = 1;
    }

    private void Update()
    {
        handleLineDrawingInput();
    }

    private void handleLineDrawingInput()
    {
        timer += Time.deltaTime;

        if ((Input.GetMouseButton(1)) && (timer >= _updateInterval) && (renderer.positionCount >= 1))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;

            Vector3 worldMousePos = cam.ScreenToWorldPoint(mousePos);
            Vector3 correctPos = new Vector3(worldMousePos.x, 0f, worldMousePos.z);
            points.Add(correctPos);

            renderer.positionCount += 1;

            renderer.SetPosition(0, new Vector3(Player.position.x, 0f, Player.position.z));
            renderer.SetPosition(renderer.positionCount - 1, correctPos);
            timer = 0f;
        }

        if (Input.GetMouseButtonUp(1) && points.Count >= 1)
        {
            onPathDrawn?.Invoke(points);
            selectedUnits = false;
        }
    }
}