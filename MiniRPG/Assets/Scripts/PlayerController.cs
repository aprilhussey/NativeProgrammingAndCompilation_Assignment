using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputActions inputActions;

    public float speed;
    private Vector2 movementInput = new Vector2();

    void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Player.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 direction = new Vector3(movementInput.x, 0, movementInput.y);
        
        GetComponent<Rigidbody>().AddRelativeForce(direction * speed);
    }
}
