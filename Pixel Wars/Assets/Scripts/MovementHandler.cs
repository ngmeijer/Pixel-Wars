using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementHandler : MonoBehaviour
{
    [SerializeField] protected List<NavMeshAgent> agents = new List<NavMeshAgent>();
}
