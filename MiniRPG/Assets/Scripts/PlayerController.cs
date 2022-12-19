using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputActions inputActions;

    public float speed = 10f;
    private Vector2 movementInput = new Vector2();

    public float lookSpeed = 100f;
    private Vector2 lookInput = new Vector2();

    void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Player.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
        inputActions.Player.Look.performed += context => lookInput = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Look();
    }

    void Movement()
    {
        Vector3 direction = new Vector3(movementInput.x, 0, movementInput.y);

        GetComponent<Rigidbody>().AddRelativeForce(direction * speed);
    }

    void Look()
    {
        GetComponent<Transform>().Rotate(Vector3.up * lookInput.x * lookSpeed * Time.deltaTime);
    }
}
