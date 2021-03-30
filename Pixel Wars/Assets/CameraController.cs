using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 30f;
    [SerializeField] private float panSpeed = 20f;
    [SerializeField] private float panBorderThickness = 10f;
    [SerializeField] private Vector2 size;
    
    private void Update()
    {
        checkInput();
    }

    private void checkInput()
    {
        Vector3 pos = transform.position;
        
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

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y += scroll * scrollSpeed * 100f * Time.deltaTime;
        
        transform.position = pos;
    }
}
