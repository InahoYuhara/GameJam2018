using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpVision : PowerUp {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Collect(GameObject entity)
	{
		base.Collect(entity);
        Vision vision = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Vision>();
        StartCoroutine(ChangeVisionForTime(vision, 15));
	}

    public IEnumerator ChangeVisionForTime(Vision vision, float time)
    {
        vision.ChangeVisionAngle(vision.defaultAngle + 30);
        yield return new WaitForSeconds(time);
        vision.ChangeVisionAngle(vision.defaultAngle);
    }
}
