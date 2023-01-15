using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
	[SerializeField] private GameObject inventoryFirstButton;
	[SerializeField] private GameObject uiEventSystem;
	[SerializeField] private GameObject inventoryEventSystem;

	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject pauseFirstButton;

	InputActions inputActions;

    private Vector2 movementInput = new Vector2();
	public float lookSpeed = 100f;

    Transform cameraTransform;
    private NavMeshAgent agent;

	void Awake()
    {
		inputActions = new InputActions();
		inputActions.Enable();
		inputActions.Player.Movement.performed += context => movementInput = context.ReadValue<Vector2>();

		agent = GetComponent<NavMeshAgent>();

        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
		Inventory();
		Pause();
    }

    void Movement()
    {
        if (movementInput.x == 0 && movementInput.y == 0)
        {
			agent.updateRotation = false;
		}
        else
        {
            agent.updateRotation = true;

			Vector2 direction = new Vector2(movementInput.x, movementInput.y);
			// Use the camera's rotation to oriten the movement of the NavMeshAgent
			agent.destination = transform.position + cameraTransform.rotation * new Vector3(direction.x, 0, direction.y);
		}
    }

    void Inventory()
    {
        if (inputActions.Player.Inventory.triggered || inputActions.Inventory.Player.triggered)
        {
            if (!pauseMenu.activeInHierarchy)
            {
                if (inventoryUI.activeInHierarchy)
                {
                    Time.timeScale = 1f;
                    inventoryUI.SetActive(false);
                    uiEventSystem.SetActive(true);
                    inventoryEventSystem.SetActive(false);
                }
                else if (!inventoryUI.activeInHierarchy)
                {
                    Time.timeScale = 0f;
                    inventoryUI.SetActive(true);
                    uiEventSystem.SetActive(false);
                    inventoryEventSystem.SetActive(true);

					// Clear selected button
					EventSystem.current.SetSelectedGameObject(null);
					// Set selected button
					EventSystem.current.SetSelectedGameObject(inventoryFirstButton);
				}
            }
        }
	}

    void Pause()
    {
        if (inputActions.Player.Pause.triggered)
        {
            if (!inventoryUI.activeInHierarchy)
            {
                if (pauseMenu.activeInHierarchy)
                {
					Time.timeScale = 1f; 
                    pauseMenu.SetActive(false);
                }
                else if (!pauseMenu.activeInHierarchy)
                {

					Time.timeScale = 0f;
					Gamepad.current.SetMotorSpeeds(0, 0);
					pauseMenu.SetActive(true);

					// Clear selected button
					EventSystem.current.SetSelectedGameObject(null);
					// Set selected button
					EventSystem.current.SetSelectedGameObject(pauseFirstButton);
				}
			}
        }
    }
}
