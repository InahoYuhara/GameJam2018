using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	private bool paused = false;

	public GameObject PauseUI;

	void Start()
	{
		PauseUI.SetActive(false);
	}

	void Update()
	{
		if (Input.GetButtonDown("Pause"))
		{
			paused = !paused;
		}

		PauseUI.SetActive(paused);
		Time.timeScale = (paused) ? 0 : 1;
	}

	public void Resume()
	{
		paused = false;
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene("main_menu");
	}

	public void Quit()
	{
		Application.Quit();
	}
}
