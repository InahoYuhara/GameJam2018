using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    [SerializeField] public float m_MaxSpeed = 10;                   // The fastest the player can travel in the x axis.
    [SerializeField] protected float currentSpeed;
    protected Rigidbody2D m_Rigidbody2D;
    private int numOfSlowdowns;

    // Use this for initialization
    void Start () {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        currentSpeed = m_MaxSpeed;
        numOfSlowdowns = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator ChangeSpeedForTime(float speedFactor, float time)
    {
        currentSpeed *= speedFactor;
        yield return new WaitForSeconds(time);
        currentSpeed = m_MaxSpeed;
    }

    public IEnumerator ChangeSpeedForObstacle(float speedFactor, float time)
    {
        numOfSlowdowns++;
        currentSpeed *= speedFactor;
        yield return new WaitForSeconds(time);
        numOfSlowdowns--;
        if(numOfSlowdowns == 0)
            currentSpeed = m_MaxSpeed;
    }
}
