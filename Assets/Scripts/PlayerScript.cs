using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    public int Lives = 3;
    [System.NonSerialized] public int ObstaclesAff = 0;

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
		if (Lives == 0 ||
			transform.position.y < -25.0f)
		{
			Die();
		}
	}

	void Die()
	{
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<DeathScreen>().Show();
	}
}
