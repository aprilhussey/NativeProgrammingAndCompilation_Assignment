using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    InputActions inputActions;

    private Vector2 movementInput = new Vector2();
	public float lookSpeed = 100f;

    Transform cameraTransform;
    private NavMeshAgent agent;

	void Awake()
    {
		inputActions = new InputActions();
		inputActions.Enable();
		inputActions.Player.Movement.performed += context => movementInput = context.ReadValue<Vector2>();

		agent = GetComponent<NavMeshAgent>();

        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (movementInput.x == 0 && movementInput.y == 0)
        {
			agent.updateRotation = false;
		}
        else
        {
            agent.updateRotation = true;

			Vector2 direction = new Vector2(movementInput.x, movementInput.y);
			// Use the camera's rotation to oriten the movement of the NavMeshAgent
			agent.destination = transform.position + cameraTransform.rotation * new Vector3(direction.x, 0, direction.y);
		}
    }
}
