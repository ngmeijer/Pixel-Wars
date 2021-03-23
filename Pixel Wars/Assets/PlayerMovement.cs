using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LineController line;
    private int _index;

    public delegate void OnArriveLastPoint();

    public OnArriveLastPoint onArrive;

    private void Start()
    {
        LineController.onPathDrawn += setDestination;
    }

    private IEnumerator setDestination(List<Vector3> pDestination)
    {
        Debug.Log("Moving player to new destination.");

        while (transform.position != pDestination[_index])
        {
            transform.Translate(pDestination[_index]);
        }

        _index++;

        yield return null;
    }
}
