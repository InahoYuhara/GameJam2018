using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    public float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [System.NonSerialized] public bool speedChanged;
    protected Rigidbody2D m_Rigidbody2D;

    // Use this for initialization
    void Start () {
        speedChanged = false;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator ChangeSpeedFor(float newMaxSpeed, float time)
    {
        Debug.Log("Speed change start");
        float oldSpeed = m_MaxSpeed;
        speedChanged = true;
        m_MaxSpeed = newMaxSpeed;
        yield return new WaitForSeconds(time);
        m_MaxSpeed = oldSpeed;
        speedChanged = false;
        Debug.Log("Speed returned");
    }
}
