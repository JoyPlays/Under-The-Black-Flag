using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
	public AudioManager audioManager;
	// Update is called once per frame

	public void StartGame()
	{
		audioManager.StopMusic();
		SceneManager.LoadScene("WorldMap");
	}

	public void ReturnToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

}
