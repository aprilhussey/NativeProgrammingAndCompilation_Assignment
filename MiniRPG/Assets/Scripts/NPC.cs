using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : Interactable
{
	InputActions inputActions;

    public Animator dialogueAnimator;
	public Dialogue dialogue;
    public GameObject dialogueBox;

    Transform target;
    UnityEngine.AI.NavMeshAgent agent;

	public void Awake()
	{
		inputActions = new InputActions();
		inputActions.Enable();
	}

	// Start is called before the first frame update
	void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(interactionTransform.position, target.position);
        if (distance <= radius)
        {
            FaceTarget();

            if (inputActions.Player.Interact.triggered)
            {
                if (!dialogueAnimator.GetBool("open"))
                {
                    Interact();
                }
            }
        }
    }

	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
