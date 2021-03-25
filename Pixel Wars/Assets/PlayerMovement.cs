using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LineController line;
    [SerializeField] private float moveSpeed = 5f;
    private int index;

    public delegate void OnArriveLastPoint();

    public static OnArriveLastPoint onArrive;
    private float timer;
    private bool moveToNewPoint;
    private Vector3 newTarget;

    private List<Vector3> listOfPoints = new List<Vector3>();

    private void Start()
    {
        LineController.onPathDrawn += setDestination;
    }

    private void Update()
    {
        if (moveToNewPoint)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, newTarget, step);

            if ((Vector2.Distance(transform.position, newTarget) < 0.001f))
            {
                if (newTarget == listOfPoints[listOfPoints.Count - 1])
                {
                    moveToNewPoint = false;
                    onArrive?.Invoke();
                }
                else
                {
                    index++;
                    newTarget = listOfPoints[index];
                }
            }
        }
    }

    private void setDestination(List<Vector3> pDestinations)
    {
        newTarget = new Vector3(pDestinations[0].x, 0f, pDestinations[0].z);
        listOfPoints = pDestinations;
        index = 0;
        moveToNewPoint = true;
    }
}