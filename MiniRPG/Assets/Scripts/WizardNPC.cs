using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WizardNPC : NPC
{
	public Quest quest;

	public PlayerStats playerStats;

	public GameObject questMenu;

	public override void Interact()
	{
		base.Interact();
		radius = 0.1f;

		dialogueBox.SetActive(true);

		TriggerDialogue();
	}

	public void AcceptQuest()
	{
		questMenu.SetActive(false);
		quest.isActive = true;
		
		Time.timeScale = 1f;
		StartCoroutine(DelayRadius());
		playerStats.quest = quest;
	}

	public void DenyQuest()
	{
		questMenu.SetActive(false);
		quest.isActive = false;

		Time.timeScale = 1f;
		StartCoroutine(DelayRadius());
	}

	IEnumerator DelayRadius()
	{
		dialogueAnimator.SetBool("open", false);
		yield return new WaitForSeconds(0.5f);
		radius = 2f;
	}
}
