using UnityEngine;

public class Interactable : MonoBehaviour
{
    // How close the player needs to get to the object to interact with it
    public float radius = 3f;
    public Transform interactionTransform;
    GameObject player;

    public virtual void Interact()
    {
        // This method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
            float distance = Vector3.Distance(interactionTransform.position, player.transform.position);
            if (distance <= radius)
            {
                bool interact = PlayerController.instance.Interact();
                if (interact)
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
