using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera _camera;
    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        var scroll = -Input.GetAxis("Mouse ScrollWheel");
        _camera.orthographicSize = Mathf.Clamp(scroll + _camera.orthographicSize, 0, 8);
    }
}
