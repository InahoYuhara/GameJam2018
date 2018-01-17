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
        Vector2 dir = new Vector2(m_MaxSpeed, 0f);
        m_Rigidbody2D.MovePosition(m_Rigidbody2D.position + dir * Time.fixedDeltaTime);
    }
}
