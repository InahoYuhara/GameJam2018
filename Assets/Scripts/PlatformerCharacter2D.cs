using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

//Unity Standard assets

public class PlatformerCharacter2D : MovingObject
{
    [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Animator m_Anim;            // Reference to the player's animator component.
    public bool m_FacingRight = true;   // For determining which way the player is currently facing.
    public PlayerScript player;

    Transform[] playerGraphics = new Transform[5];
	
	private void Awake()
    {
		// Setting up references.
		m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        playerGraphics[0] = transform.Find("Coat");
        playerGraphics[1] = transform.Find("Coat2");
        playerGraphics[2] = transform.Find("Left Feet");
        playerGraphics[3] = transform.Find("Right Feet");
        playerGraphics[4] = transform.Find("Head");

        if (playerGraphics[0] == null)
        {
            Debug.LogError("Error: Main character's coat can't be found!");
        }
        if (playerGraphics[1] == null)
        {
            Debug.LogError("Error: Main character's coat2 can't be found!");
        }
        if (playerGraphics[2] == null)
        {
            Debug.LogError("Error: Main character's left feet can't be found!");
        }
        if (playerGraphics[3] == null)
        {
            Debug.LogError("Error: Main character's right feet can't be found!");
        }
        if (playerGraphics[4] == null)
        {
            Debug.LogError("Error: Main character's head can't be found!");
        }
    }


    private void FixedUpdate()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }
        m_Anim.SetBool("Ground", m_Grounded);

        // Set the vertical animation
        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

		if (Input.GetKeyDown(KeyCode.P))
		{
			m_Anim.SetTrigger("DanceT");
			m_Anim.SetBool("Dance", true);
			print("started dancing");
		}
		if (Input.GetKeyUp(KeyCode.P))
		{
			m_Anim.SetBool("Dance", false);
			print("stopped dancing");
		}
		/*if(Input.GetKeyDown(KeyCode.P))
			m_Anim.SetTrigger("DanceT");*/
	}


    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch && m_Anim.GetBool("Crouch"))
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        // Set whether or not the character is crouching in the animator
        m_Anim.SetBool("Crouch", crouch);

        //only control the player if grounded or airControl is turned on
		if(m_Anim.GetBool("Dance"))
		{
			move = m_FacingRight ? -1 : 1;
			m_Rigidbody2D.velocity = new Vector2(move/2 * currentSpeed, m_Rigidbody2D.velocity.y);
		}
        else if (m_Grounded || m_AirControl)
        {
            // Reduce the speed if crouching by the crouchSpeed multiplier
            move = (crouch ? move * m_CrouchSpeed : move);

			if (SpeedBoostDuration > 0)
				move *= 1.5f;

            // The Speed animator parameter is set to the absolute value of the horizontal input.
            m_Anim.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            m_Rigidbody2D.velocity = new Vector2(move * currentSpeed, m_Rigidbody2D.velocity.y);

        }
        // If the player should jump...
        if (m_Grounded && jump && m_Anim.GetBool("Ground") && !m_Anim.GetBool("Dance"))
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Anim.SetBool("Ground", false);
            m_Anim.SetTrigger("Jump");
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }


    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        for (int i = 0; i < 4; i++)
            playerGraphics[i].GetComponent<SpriteRenderer>().flipX = !playerGraphics[i].GetComponent<SpriteRenderer>().flipX;
        GetComponent<Animator>().transform.Rotate(0, 180, 0);

        // Multiply the player's x local scale by -1.
        for (int i = 0; i < 4; i++)
        {
            Vector3 theScale = playerGraphics[i].localScale;
            theScale.x *= -1;
            playerGraphics[i].localScale = theScale;
        }

        playerGraphics[playerGraphics.Length - 1].GetComponent<SpriteRenderer>().flipY = !playerGraphics[playerGraphics.Length - 1].GetComponent<SpriteRenderer>().flipY;
    }

    public IEnumerator ChangeSpeedForTime(float speedFactor, float time)
    {
        player.ObstaclesAff++;
        currentSpeed *= speedFactor;
        yield return new WaitForSeconds(time);
        player.ObstaclesAff--;
        if (player.ObstaclesAff==0)
            currentSpeed = m_MaxSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckObstacle(collision);
    }

    private void CheckObstacle(Collider2D collision)
    {
        float slowDownFactor = 0;
        switch (collision.tag)
        {
            case "Bucket":
                slowDownFactor = 0.25f;
                break;
            case "WetFloorSign":
                slowDownFactor = 0.50f;
                break;
            case "Pony":
                slowDownFactor = 0.50f;
                break;
            case "Mop":
                slowDownFactor = 0.50f;
                break;
        }

        if (slowDownFactor != 0)
        {
            Destroy(collision.gameObject);
            player.Lives--;
            StartCoroutine(ChangeSpeedForTime(1 - slowDownFactor, 2f));
        }
    }
}

