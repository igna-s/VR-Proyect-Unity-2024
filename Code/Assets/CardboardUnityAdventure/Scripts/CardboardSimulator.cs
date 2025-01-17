using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardSimulator : MonoBehaviour
{
    public bool UseCardboardSimulator = true;

    [SerializeField] private float horizontalSpeed = 0.5f;
    [SerializeField] private float verticalSpeed = 0.5f;
    [SerializeField] private float rotationX = 0.0f;
    [SerializeField] private float rotationY = 0.0f;
    private Camera cam;

    void Start() 
    {
#if UNITY_EDITOR
        cam = Camera.main;
#endif
    }

    void Update()
    {
#if UNITY_EDITOR
        if (!UseCardboardSimulator)
            return;
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed; 
            rotationY += mouseX;
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -45, 45);
            cam.transform.localEulerAngles = new Vector3(rotationX, rotationY, 0.0f);
        }
#endif
    }

    public void UpdatePlayerPositonSimulator()
    {
        rotationX = 0;
        rotationY = cam.transform.localEulerAngles.y;
    }

}
