using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
	GameObject player;
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update()
	{
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 dir = Input.mousePosition - pos;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		
	}
}
