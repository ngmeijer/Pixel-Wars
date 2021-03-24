using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionUnitSelector : MonoBehaviour
{
    private LineRenderer renderer;
    private Camera cam;
    private float timer;
    [SerializeField] private float updateInterval;
    [SerializeField] private List<Vector2> selectionVertices = new List<Vector2>();
    [SerializeField] private GameObject selectedUnitsParent;

    public delegate void OnUnitsSelected(List<GameObject> pUnitSelected);

    public static OnUnitsSelected OnUnitsSelectDone;
    private PolygonCollider2D polCollider;
    private Mesh collisionMesh;
    [SerializeField] private List<GameObject> selectedUnits = new List<GameObject>();

    private void Awake()
    {
        renderer = GetComponent<LineRenderer>();
        cam = Camera.main;
        collisionMesh = new Mesh();
        polCollider = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        handleLineDrawingInput();
    }

    private void handleLineDrawingInput()
    {
        timer += Time.deltaTime;
        if ((Input.GetMouseButton(0)) && (timer >= updateInterval) && (renderer.positionCount >= 1))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldMousePos = cam.ScreenToWorldPoint(mousePosition);

            Vector3 correctPosition = new Vector3(worldMousePos.x, worldMousePos.y, 0f);
            selectionVertices.Add(correctPosition);

            renderer.positionCount += 1;

            renderer.SetPosition(0, selectionVertices[0]);
            renderer.SetPosition(renderer.positionCount - 1, correctPosition);
            timer = 0f;
        }

        if (Input.GetMouseButtonUp(0) && selectionVertices.Count >= 1)
        {
            bakeCollisionMesh();
            detectSelectionCollision();
            OnUnitsSelectDone?.Invoke(selectedUnits);
            clearSelectionAreaVertexList();
        }
    }

    private void bakeCollisionMesh()
    {
        renderer.BakeMesh(collisionMesh, true);
        polCollider.points = selectionVertices.ToArray();
    }

    private void detectSelectionCollision()
    {

    }

    private void clearSelectionAreaVertexList()
    {
        selectionVertices.Clear();
        renderer.positionCount = 1;
    }

    private void OnTriggerEnter2D(Collider2D pOtherCollider)
    {
        if (pOtherCollider.CompareTag("PlayerUnit"))
        {
            selectedUnits.Add(pOtherCollider.gameObject);
        }
    }
}
