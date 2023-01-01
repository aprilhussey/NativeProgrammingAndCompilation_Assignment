using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    InputActions inputActions;

    private Vector2 movementInput = new Vector2();
	private float interactInput;
    private float inventoryInput;
	private float dropInput;

	public float lookSpeed = 100f;

    Transform cameraTransform;

    private NavMeshAgent agent;

    void Awake()
    {
        instance = this;

        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Player.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
        inputActions.Player.Interact.performed += context => interactInput = context.ReadValue<float>();
		inputActions.Player.Inventory.performed += context => inventoryInput = context.ReadValue<float>();
		inputActions.Player.Drop.performed += context => dropInput = context.ReadValue<float>();

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

    public bool Interact()
    {
		// If interact is pressed and object is interactable
		if (interactInput > 0)
        {
            interactInput = 0f;
            return true;
		}
        else // (interactInput < 1)
        {
            return false;
        }
    }

    public bool Inventory()
    {
        if (inventoryInput > 0)
        {
            inventoryInput = 0f;
            return true;
        }
		else // (interactInput < 1)
		{
			return false;
		}
	}

    public bool Drop()
    {
        if (dropInput > 0)
        {
            dropInput = 0f;
            return true;
        }
		else // (interactInput < 1)
		{
			return false;
		}
	}
}
