using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpArmor : PowerUp {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Collect(GameObject entity)
	{
		if (entity.GetComponent<PlayerScript>().AddOneLife()) //collect only if needed
		{
			base.Collect(entity);
			print("Armor collected!");
		}
	}
}
