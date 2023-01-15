using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckControllerConnection : MonoBehaviour
{
	public GameObject controllerDisconnected;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
	
	private void OnEnable()
	{
		InputSystem.onDeviceChange += OnDeviceChange;
	}

	private void OnDisable()
	{
		InputSystem.onDeviceChange -= OnDeviceChange;
	}

	private void OnDeviceChange(InputDevice device, InputDeviceChange change)
	{
		if (change == InputDeviceChange.Removed && device is Gamepad)
		{
			Debug.Log("Controller disconnected");
			controllerDisconnected.SetActive(true);
			Time.timeScale = 0f;
		}
		else if (change == InputDeviceChange.Added && device is Gamepad)
		{
			Debug.Log("Controller connected");
			controllerDisconnected.SetActive(false);
			Time.timeScale = 1f;
		}
	}
}
