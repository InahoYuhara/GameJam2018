using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
	public float maxAngle = 360;

	void Update()
	{
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 dir = Input.mousePosition - pos;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		
	}

	void OnTriggerStay2D(Collider2D col)
	{
		string tagname = col.gameObject.tag;
		if (tagname == "Bucket")
		{
			Debug.Log("A Bucket");
		}
		else if (tagname == "Antag")
		{
			Debug.Log("The Antogonist");
		}
	}
}
