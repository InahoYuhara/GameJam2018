using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    public int Lives = 3;
    [System.NonSerialized] public int ObstaclesAff = 0;

	public UnityEngine.UI.Text ReasonText;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		DeathCheck();
		
	}

	void DeathCheck()
	{
		if (Lives == 0)
			Die("By running too much into obstacles, you have made your target aware of your presence.");
		if (transform.position.y < -25.0f)
			Die("You have fallen to your death.");
	}

	void Die(string reason)
	{
		DeathScreen screen = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<DeathScreen>();
		ReasonText.text = reason;
		screen.Show();
	}
}
