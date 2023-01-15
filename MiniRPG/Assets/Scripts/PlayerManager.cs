using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
	CharacterAnimator characterAnimator;
	public GameObject player;

	InputActions inputActions;

	public GameObject deathMenu;
	public GameObject restartFirstButton;

	#region Singleton

	public static PlayerManager instance;

	void Awake()
	{
		instance = this;

		characterAnimator = player.GetComponent<CharacterAnimator>();

		inputActions = new InputActions();
		inputActions.Enable();
	}

	#endregion

	public void KillPlayer()
	{
		StartCoroutine(DeathAnim());
	}

	IEnumerator DeathAnim()
	{
		// Player death animation
		characterAnimator.OnDeath();

		yield return new WaitForSeconds(1f);
		Gamepad.current.SetMotorSpeeds(0, 0);

		// Stop player movement
		Time.timeScale = 0;

		// Enable Restart prompt
		deathMenu.SetActive(true);
		// Clear selected button
		EventSystem.current.SetSelectedGameObject(null);
		// Set selected button
		EventSystem.current.SetSelectedGameObject(restartFirstButton);
	}
}
