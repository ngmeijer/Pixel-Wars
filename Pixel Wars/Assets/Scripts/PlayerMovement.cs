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

    [SerializeField] private List<Vector3> listOfPoints = new List<Vector3>();

    private void Start()
    {
        LineController.onPathDrawn += setDestination;
    }

    private void Update()
    {
        if (moveToNewPoint)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(newTarget.x, transform.position.y, newTarget.z), step);

            if ((Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), new Vector3(newTarget.x, 0f, newTarget.z)) < 0.001f))
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

    private void stopMovement()
    {
        moveToNewPoint = false;
    }

    private void setDestination(List<Vector3> pDestinations)
    {
        newTarget = pDestinations[0];
        listOfPoints = pDestinations;
        index = 0;
        moveToNewPoint = true;
    }
}