using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntagMoving : MovingObject { 
	// Use this for initialization
	void Start () {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        m_Rigidbody2D.velocity = new Vector2(m_MaxSpeed, m_Rigidbody2D.velocity.y);
    }
}
