using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    [SerializeField] public float m_MaxSpeed = 10;                   // The fastest the player can travel in the x axis.
    [SerializeField] protected float currentSpeed;
    protected Rigidbody2D m_Rigidbody2D;
    private int numOfSlowdowns;

	private float speedBoostDuration;

	public float SpeedBoostDuration
	{
		get { return speedBoostDuration; }
		set { speedBoostDuration = value; }
	}

    // Use this for initialization
    void Start () {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        currentSpeed = m_MaxSpeed;
        numOfSlowdowns = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (speedBoostDuration > 0)
            speedBoostDuration -= Time.deltaTime;
	}
}
