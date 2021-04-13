using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public delegate IEnumerator OnArriveLastPoint();
    public static OnArriveLastPoint onArrive;

    private int pointIndex;
    private float timer;
    private bool moveToNewPoint;
    private Vector3 newTarget;

    [SerializeField] private List<Vector3> listOfPoints = new List<Vector3>();
    [SerializeField] private List<NavMeshAgent> agents = new List<NavMeshAgent>();

    private void Start()
    {
        LineController.onPathDrawn += startMovement;
        LegionUnitSelector.onUnitSelect += addAgents;
    }

    private void Update()
    {
        if (!moveToNewPoint || agents.Count <= 0)
            return;

        if (listOfPoints.Count <= 0)
            return;

        if (newTarget == listOfPoints[listOfPoints.Count - 1])
        {
            stopMovement();
            return;
        }

        setNewPointDestination();
    }

    //---------------------------------------------------------------------------------------------------
    //-----------------------------------------stopMovement();-------------------------------------------
    //---------------------------------------------------------------------------------------------------
    private void stopMovement()
    {
        moveToNewPoint = false;
        StartCoroutine(onArrive());
        agents.Clear();
        pointIndex = 0;
        listOfPoints.Clear();
    }

    //---------------------------------------------------------------------------------------------------
    //-----------------------------------startMovement(List<Vector3> pPosition)--------------------------
    //---------------------------------------------------------------------------------------------------
    private void startMovement(List<Vector3> pPosition)
    {
        moveToNewPoint = true;
        listOfPoints = pPosition;
    }

    //----------------------------------------------------------------------------------------------------
    //-----------------------------------------setNewPointDestination();----------------------------------
    //----------------------------------------------------------------------------------------------------
    private void setNewPointDestination()
    {
        int lastIndex = pointIndex;
        bool increasePointIndex = false;

        for (int agentIndex = 0; agentIndex < agents.Count; agentIndex++)
        {
            if (agents[agentIndex].pathStatus == NavMeshPathStatus.PathInvalid ||
                agents[agentIndex].pathStatus == NavMeshPathStatus.PathPartial)
            {
                stopMovement();
            }
            
            newTarget = new Vector3(listOfPoints[lastIndex + 1].x, listOfPoints[lastIndex + 1].y,
                listOfPoints[lastIndex + 1].z);
            agents[agentIndex].SetDestination(newTarget);

            if (Vector3.Distance(agents[agentIndex].transform.position, newTarget) < 1.5f)
            {
                increasePointIndex = true;
            }
        }

        if (increasePointIndex)
            pointIndex++;
    }

    //-----------------------------------------------------------------------------------------------------
    //-------------------------------addAgents(GameObject pObject)-----------------------------------------
    //-----------------------------------------------------------------------------------------------------
    private void addAgents(GameObject pObject)
    {
        NavMeshAgent agent = pObject.GetComponent<NavMeshAgent>();
        agents.Add(agent);
    }
}