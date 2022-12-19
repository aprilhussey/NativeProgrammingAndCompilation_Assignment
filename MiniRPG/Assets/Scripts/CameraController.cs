using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    InputActions inputActions;

    public Transform target;
    public Vector3 offset;
    public float height = 2f;

    public float zoomSpeed = 5f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    private float currentZoom = 10f;
    private Vector2 zoomInput = new Vector2();

    public float lookSpeed = 100f;
    private Vector2 lookInput = new Vector2();
    private float currentLookY;
    private float currentLookX;

    void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Player.Zoom.performed += context => zoomInput = context.ReadValue<Vector2>();
        inputActions.Player.Look.performed += context => lookInput = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        Zoom();
        Look();
    }

    void Zoom()
    {
        if (zoomInput.y > 0f) // Mouse scroll up
        {
            currentZoom -= zoomSpeed;
        }
        else if (zoomInput.y < 0f)   // Mouse scroll down
        {
            currentZoom += zoomSpeed;
        }

        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        GetComponent<Transform>().position = target.position - offset * currentZoom;
        GetComponent<Transform>().LookAt(target.position + Vector3.up * height);
    }
    void Look()
    {
        currentLookY -= lookInput.y * lookSpeed * Time.deltaTime;
        Vector3 localX = GetComponent<Transform>().TransformDirection(Vector3.left);

        GetComponent<Transform>().RotateAround(target.position, localX, currentLookY);

        currentLookX += lookInput.x * lookSpeed * Time.deltaTime;
        GetComponent<Transform>().RotateAround(target.position, Vector3.up, currentLookX);
    }
}
