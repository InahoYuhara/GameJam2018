﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void Collect(GameObject entity)
	{
		print(entity.name + " collected me! (" + gameObject.name + ')');

		gameObject.SetActive(false);
	}
}
