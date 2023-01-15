using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WizardNPC : NPC
{
	public Quest quest;

	public PlayerStats playerStats;

	public GameObject questMenu;

	public Animator wizardAnimator;

	public float timeLastEntered;

	public void Awake()
	{
		base.Awake();
		timeLastEntered = Time.time;
	}

	public override void Interact()
	{
		base.Interact();
		if (timeLastEntered < (Time.time - 3))
		{
			wizardAnimator.SetBool("interacting", true);

			dialogueBox.SetActive(true);

			TriggerDialogue();
		}
	}

	public void AcceptQuest()
	{
		this.timeLastEntered = Time.time;
		questMenu.SetActive(false);
		quest.isActive = true;

		Time.timeScale = 1f;
		CloseOut();
		playerStats.quest = quest;
	}

	public void DenyQuest()
	{
		this.timeLastEntered = Time.time;
		questMenu.SetActive(false);
		quest.isActive = false;

		Time.timeScale = 1f;
		CloseOut();
	}

	private void CloseOut()
	{
		dialogueAnimator.SetBool("open", false);
		wizardAnimator.SetBool("interacting", false);
		
	}
}
