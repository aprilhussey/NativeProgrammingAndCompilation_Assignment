using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIButtonFunctions : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject optionsMenu;
	[SerializeField] private GameObject creditsMenu;

	[SerializeField] private GameObject optionsFirstButton;
	[SerializeField] private GameObject optionsClosedButton;
	
    [SerializeField] private GameObject creditsFirstButton;
	[SerializeField] private GameObject creditsClosedButton;

	public void Play()
    {
        SceneManager.LoadScene("Game_MAIN");
        Time.timeScale = 1.0f;
    }
    
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Options()
    {
        optionsMenu.SetActive(true);

		// Clear selected button
		EventSystem.current.SetSelectedGameObject(null);
		// Set selected button
		EventSystem.current.SetSelectedGameObject(optionsFirstButton);
	}

    public void Credits()
    {
        creditsMenu.SetActive(true);

		// Clear selected button
		EventSystem.current.SetSelectedGameObject(null);
		// Set selected button
		EventSystem.current.SetSelectedGameObject(creditsFirstButton);
	}

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        if (optionsMenu.activeInHierarchy)
        {
            optionsMenu.SetActive(false);

            // Clear selected button
            EventSystem.current.SetSelectedGameObject(null);
            // Set selected button
            EventSystem.current.SetSelectedGameObject(optionsClosedButton);
        }
        else if (creditsMenu.activeInHierarchy)
        {
            creditsMenu.SetActive(false);

			// Clear selected button
			EventSystem.current.SetSelectedGameObject(null);
			// Set selected button
			EventSystem.current.SetSelectedGameObject(creditsClosedButton);
		}
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game_MAIN");
        Time.timeScale = 1.0f;
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu_MAIN");
    }
}
