using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;
using Debug = UnityEngine.Debug;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed = 20f;

    [Header("Level border settings")]
    [SerializeField] private float panBorderThickness = 10f;
    [SerializeField] private Vector2 size;

    [Header("Scroll settings")]
    [SerializeField] private float scrollSpeed = 3f;
    [SerializeField] private float minHeight = 3f;
    [SerializeField] private float maxHeight = 10f;
    private Camera cam;
    private Vector3 pos;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        //checkInput();
    }
    
    #region PC Controls

    private void checkInput()
    {
        pos = transform.position;
        
        handlePan();
        handleZoom();
        
        transform.position = pos;
    }

    private void handlePan()
    {
        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        
        if (Input.mousePosition.y <= 0 + panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x <= 0 + panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        
        pos.x = Mathf.Clamp(pos.x,-size.x, size.x);
        pos.z = Mathf.Clamp(pos.z, -size.y, size.y);
    }

    private void handleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        cam.orthographicSize -= scroll * scrollSpeed;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minHeight, maxHeight);
    }
    
    #endregion

    #region Mobile Controls
    public void StopZoom()
    {
        cam.orthographicSize = cam.orthographicSize;
    }
    
    public void ZoomInOnClick()
    {
        cam.orthographicSize -= scrollSpeed * Time.deltaTime;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minHeight, maxHeight);
    }
    
    public void ZoomOutOnClick()
    {
        cam.orthographicSize += scrollSpeed * Time.deltaTime;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minHeight, maxHeight);
    }

    public void PanDown()
    {
        pos = transform.position;

        pos.z -= panSpeed * Time.deltaTime;
        pos.z = Mathf.Clamp(pos.z, -size.y, size.y);
        
        transform.position = pos;
    }

    public void PanUp()
    {
        pos = transform.position;

        pos.z += panSpeed * Time.deltaTime;
        pos.z = Mathf.Clamp(pos.z, -size.y, size.y);
        
        transform.position = pos;
    }
    
    public void PanRight()
    {
        pos = transform.position;
        
        pos.x += panSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -size.x, size.x);

        transform.position = pos;
    }
    
    public void PanLeft()
    {
        pos = transform.position;

        pos.x -= panSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -size.x, size.x);
        
        transform.position = pos;
    }
    
    #endregion
}