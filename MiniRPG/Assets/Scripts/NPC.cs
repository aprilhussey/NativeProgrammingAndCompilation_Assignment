using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : Interactable
{
    Transform target;
    UnityEngine.AI.NavMeshAgent agent;
    
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
        }
    }

    public override void Interact()
    {
        base.Interact();
        //open dialogue
        // if player says yes
        // checks if player has requested amount of
        // items in inventory
        // if true
        // takes them
        // if false
        // dialogue that says, sorry but you don't have enough
        // please find item for then come back
    }

	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}
}
