using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSneak : PowerUp {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Collect(GameObject entity)
	{
		base.Collect(entity);
		print("Sneaky sneaky!");
	}
}
