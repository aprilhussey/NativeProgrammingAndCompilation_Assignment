using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
	CharacterAnimator characterAnimator;
	public GameObject player;

	#region Singleton

	public static PlayerManager instance;

	void Awake()
	{
		instance = this;

		characterAnimator = player.GetComponent<CharacterAnimator>();
	}

	#endregion

	public void KillPlayer()
	{
		// Player death animation
		characterAnimator.OnDeath();
		// Restart prompt

		// testing
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
