using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    InputActions inputActions;
    
    // How close the player needs to get to the object to interact with it
    public float radius = 3f;
    public Transform interactionTransform;
    GameObject player;

    public virtual void Interact()
    {
        // This method is meant to be overwritten
        //Debug.Log("Interacting with " + transform.name);
    }

    void Awake()
    {
		inputActions = new InputActions();
		inputActions.Enable();

		player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
            float distance = Vector3.Distance(interactionTransform.position, player.transform.position);
            if (distance <= radius)
            {
               
                if (inputActions.Player.Interact.triggered)
                {
                    Interact();
                }
            }
    }

    void OnDrawGizmosSelected()
	{
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
	}
}
