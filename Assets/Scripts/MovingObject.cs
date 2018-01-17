using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    public float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    protected Rigidbody2D m_Rigidbody2D;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator ChangeSpeedFor(float newMaxSpeed, float time)
    {
        float oldSpeed = m_MaxSpeed;
        m_MaxSpeed = newMaxSpeed;
        yield return new WaitForSeconds(time);
        m_MaxSpeed = oldSpeed;
    }
}
