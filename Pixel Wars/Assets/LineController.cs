using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LineController : MonoBehaviour
{
    private LineRenderer renderer;
    private List<Vector3> points = new List<Vector3>();
    [SerializeField] private float _updateInterval = 0.2f;
    private float _timer;
    private Camera _cam;
    public Vector3 Offset;

    public delegate IEnumerator OnPathDrawn(List<Vector3> pPosition);

    public static OnPathDrawn onPathDrawn;

    private void Awake()
    {
        renderer = GetComponent<LineRenderer>();
        _cam = Camera.main;
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

            Vector3 correctPosition = new Vector3(worldMousePos.x, 0, worldMousePos.y);
            points.Add(correctPosition);

            renderer.positionCount += 1;

            renderer.SetPosition(renderer.positionCount - 1, correctPosition + Offset);
            _timer = 0f;
        }
        else
        {
        }

        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("released mouse");
            onPathDrawn(points);
        }

        if (_timer >= _updateInterval)
        {
            _timer = 0f;
        }
    }
}