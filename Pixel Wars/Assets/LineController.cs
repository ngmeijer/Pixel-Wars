using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LineController : MonoBehaviour
{
    private LineRenderer renderer;
    [SerializeField] private List<Vector3> points = new List<Vector3>();
    [SerializeField] private float _updateInterval = 0.2f;
    [SerializeField] private Transform Player;
    private float _timer;
    private Camera _cam;
    public Vector3 Offset;

    public delegate void OnPathDrawn(List<Vector3> pPosition);

    public static OnPathDrawn onPathDrawn;

    private void Awake()
    {
        renderer = GetComponent<LineRenderer>();
        _cam = Camera.main;
        PlayerMovement.onArrive += clearWaypointList;

        renderer.SetPosition(0, Player.position);
        points.Add(renderer.GetPosition(0));
    }

    private void clearWaypointList()
    {
        points.Clear();
        renderer.positionCount = 1;
        points.Add(renderer.GetPosition(0));
    }

    private void Update()
    {
        handleLineDrawingInput();
    }

    private void handleLineDrawingInput()
    {
        _timer += Time.deltaTime;

        if ((Input.GetMouseButton(1)) && (_timer >= _updateInterval) && (renderer.positionCount >= 1))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldMousePos = _cam.ScreenToWorldPoint(mousePosition);

            Vector3 correctPosition = new Vector3(worldMousePos.x, worldMousePos.y, 0f);
            points.Add(correctPosition);

            renderer.positionCount += 1;

            renderer.SetPosition(0, Player.position);
            renderer.SetPosition(renderer.positionCount - 1, correctPosition);
            _timer = 0f;
        }

        if (Input.GetMouseButtonUp(1) && points.Count >= 1)
        {
            onPathDrawn?.Invoke(points);
        }
    }

    private void OnDrawGizmos()
    {
        foreach (Vector3 point in points)
        {
            Vector3 newPoint = new Vector3(point.x, point.y, 0f);
            Gizmos.DrawCube(newPoint, new Vector3(0.5f, 0.5f, 0.5f));
        }
    }
}