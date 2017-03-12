using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour {

	
	// Update is called once per frame

	public void StartGame()
	{
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
