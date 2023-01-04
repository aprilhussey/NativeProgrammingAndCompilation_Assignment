using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonFunctions : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject optionsMenu;
	[SerializeField] private GameObject creditsMenu;

	public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
    }

    public void Credits()
    {
        creditsMenu.SetActive(true);
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
            pauseMenu.SetActive(true);
        }
        else if (creditsMenu.activeInHierarchy)
        {
            creditsMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }
}
