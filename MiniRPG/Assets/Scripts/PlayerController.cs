using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    InputActions inputActions;

    //public float speed = 10f;
    private Vector2 movementInput = new Vector2();

    public float lookSpeed = 100f;

    private float interactInput;
	private const float interactRadius = 3f;
	public Interactable focus;

    Transform cameraTransform;

    private UnityEngine.AI.NavMeshAgent agent;

    void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Player.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
        inputActions.Player.Interact.performed += context => interactInput = context.ReadValue<float>();

        agent = GetComponent<NavMeshAgent>();

        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Interact();
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

    void Interact()
    {
		// If interact is pressed and object is interactable
		if (interactInput > 0)
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRadius);

            // Find the closest object
            Interactable closestObject = null;
            float closestDistance = float.MaxValue;

			foreach (Collider collider in colliderArray)
            {
                // Check if the object has a the Interactable component
                Interactable interactable = collider.gameObject.GetComponent<Interactable>();
                if (interactable != null)
                {
                    // Calculate the distance to the object
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    
                    // Check if this is the closest object so far
                    if (distance < closestDistance)
                    {
                        closestObject = interactable;
                        closestDistance = distance;
                    }
                }
            }
            if (closestObject != null)
            {
				SetFocus(closestObject);
			}
		}
        else // (interactInput < 1)
        {
            RemoveFocus();
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocus();
            }
            focus = newFocus;
        }
        newFocus.OnFocus(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocus();
        }
        focus = null;
    }

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, interactRadius);
	}
}
