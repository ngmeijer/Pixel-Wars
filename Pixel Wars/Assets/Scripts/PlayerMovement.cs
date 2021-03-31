using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private int index;
    public delegate void OnArriveLastPoint();
    public static OnArriveLastPoint onArrive;

    public delegate void OnUnitSelect(NavMeshAgent pAgent);

    public static OnUnitSelect onSelect;
    
    private float timer;
    private bool moveToNewPoint;
    private Vector3 newTarget;

    [SerializeField] private List<Vector3> listOfPoints = new List<Vector3>();
    private List<NavMeshAgent> agents = new List<NavMeshAgent>();

    private void Start()
    {
        LineController.onPathDrawn += setDestination;
        //TODO : On unit spawned, add navmeshagent to list. 

        onSelect += addAgents;
    }

    private void Update()
    {
        if (moveToNewPoint)
        {
            foreach (NavMeshAgent agent in agents)
            {
                if (agent.pathStatus == NavMeshPathStatus.PathComplete)
                {
                    Debug.Log("moving agent");
                    if (newTarget == listOfPoints[listOfPoints.Count - 1])
                    {
                        moveToNewPoint = false;
                        onArrive?.Invoke();
                    }
                    else
                    {
                        index++;
                        newTarget = listOfPoints[index];
                        agent.SetDestination(newTarget);
                    }
                }
            }
        }

            // float step = moveSpeed * Time.deltaTime;
            // transform.position = Vector3.MoveTowards(transform.position, new Vector3(newTarget.x, transform.position.y, newTarget.z), step);
            //
            // Vector3 currentPosition = new Vector3(transform.position.x, 0f, transform.position.z); 
            // if ((Vector3.Distance(currentPosition, newTarget) < 0.001f))
            // {
            //     if (newTarget == listOfPoints[listOfPoints.Count - 1])
            //     {
            //         moveToNewPoint = false;
            //         onArrive?.Invoke();
            //     }
            //     else
            //     {
            //         index++;
            //         newTarget = listOfPoints[index];
            //     }
            // }
    }

    private void stopMovement()
    {
        moveToNewPoint = false;
    }

    private void setDestination(List<Vector3> pDestinations)
    {
        newTarget = new Vector3(pDestinations[0].x, 0f, pDestinations[0].z);
        listOfPoints = pDestinations;
        index = 0;
        moveToNewPoint = true;
    }

    private void addAgents(NavMeshAgent pAgent)
    {
        agents.Add(pAgent);
    }
}