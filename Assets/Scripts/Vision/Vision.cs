using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
	public float maxAngle = 50;

	void Update()
	{
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 dir = Input.mousePosition - pos;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

		if (Mathf.Abs(angle) <= maxAngle)
		{
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
		// Snap to maxAngle° if angle greater than that
		else
		{
			transform.rotation = Quaternion.AngleAxis(maxAngle * ((angle > 0) ? 1 : -1), Vector3.forward);
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		
	}
}
