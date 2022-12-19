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

    private float interactInput;
	private float interactRadius = 0.1f;
	public Interactable focus;

    void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Player.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
        inputActions.Player.Look.performed += context => lookInput = context.ReadValue<Vector2>();
        inputActions.Player.Interact.performed += context => interactInput = context.ReadValue<float>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Look();
        Interact();
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
