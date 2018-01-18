using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : PowerUp {

	public float BuffDuration;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Collect(GameObject entity)
	{
		base.Collect(entity);

		print("Speed up!");

		entity.GetComponent<MovingObject>().SpeedBoostDuration += BuffDuration;


		//raise player speed
	}
}
