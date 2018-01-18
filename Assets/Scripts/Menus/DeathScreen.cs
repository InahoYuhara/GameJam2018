using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
	public GameObject deathScreen;

	void Start ()
	{
		deathScreen.gameObject.SetActive(false);
	}
	
	public void Show()
	{
		deathScreen.gameObject.SetActive(true);
		Time.timeScale = 0;
	}
}
