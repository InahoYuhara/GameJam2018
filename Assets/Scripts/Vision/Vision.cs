using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
	GameObject[] BlackObjects;

	void Start ()
	{
		BlackObjects = GameObject.FindGameObjectsWithTag("BlackObject");
	}

	void Update()
	{
		
	}

	void FixedUpdate()
	{
		/*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		BlackObjects[0].transform.rotation = new Quaternion(ray.direction.x, ray.direction.y, ray.direction.z, 0);
		BlackObjects[1].transform.rotation = new Quaternion(ray.direction.x, -ray.direction.y, ray.direction.z, 0);*/
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		
	}
}
