using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public float maxAngle;
    public float powerupCooldown;		// Time in seconds
	public float antagLostCooldown;		// Time in seconds

    public bool AntagFocused = false;
    //public bool powerupFocused = false;

    private PlatformerCharacter2D playerScript;
	private GameObject powerUp;

	[SerializeField] private float powerUpPickupTime;
	[SerializeField] private float targetLostMaxTime;

	private void Start()
    {
        playerScript = GetComponentInParent<PlatformerCharacter2D>();
    }

    void Update()
    {
		// Rotate the vision cone if the game isn't paused
		if (Time.timeScale > 0)
		{
			Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
			Vector3 dir = Input.mousePosition - pos;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

			if (Math.Abs(angle) > 90)
			{
				if (playerScript.m_FacingRight)
				{
					playerScript.Flip();
				}
			}
			else
			{
				if (!playerScript.m_FacingRight)
				{
					playerScript.Flip();
				}
			}

			// Powerups pickup
			if (powerUp != null)
			{
				powerupCooldown += Time.deltaTime;
				//print(powerupCooldown);
				if (powerupCooldown >= powerUpPickupTime)
				{
					powerUp.GetComponent<PowerUp>().Collect(this.transform.parent.gameObject);
					powerUp = null;
					powerupCooldown = 0;
				}
			}

			// Antag lost
			if (!AntagFocused)
			{
				antagLostCooldown += Time.deltaTime;
				if (antagLostCooldown >= targetLostMaxTime)
					playerScript.GetComponent<PlayerScript>().Die("You lost track of your target for too long.");
			}
			else
				antagLostCooldown = 0;
		}

    }

    public void ChangeHitBoxVision(float angle)
    {
        GameObject head = GameObject.Find("Head");

        head.GetComponent<PolygonCollider2D>().points = new Vector2[] { new Vector2(0, 0), new Vector2(8f, Convert.ToSingle((180 / Math.PI) * Mathf.Tan(angle / 2) * 8f)), new Vector2(8f, -(Convert.ToSingle((180 / Math.PI) * Mathf.Tan(angle / 2) * 8f))) };
    }

    public void ChangeVisionAngle(float angle)
    {
        ChangeHitBoxVision(angle);

        GameObject[] blackObjects = GameObject.FindGameObjectsWithTag("BlackObj");

        blackObjects[0].transform.rotation = Quaternion.AngleAxis(-(angle / 2), Vector3.forward);
        blackObjects[1].transform.rotation = Quaternion.AngleAxis(angle / 2, Vector3.forward);
    }

    /*public void ChangeVisionAngleSmooth(float angle)
    {
        StartCoroutine(ChangeVisionAngleSmoothIE(angle));
    }

    private IEnumerator ChangeVisionAngleSmoothIE(float angle)
    {
        ChangeHitBoxVision(angle);
        GameObject[] blackObjects = GameObject.FindGameObjectsWithTag("BlackObj");
        float timeSinceStart = 0f;

        while (blackObjects[0].transform.rotation.z <= -angle/2 - 1 && blackObjects[0].transform.rotation.z >= -angle / 2 + 1 && blackObjects[1].transform.rotation.z >= angle/2 - 1 && blackObjects[1].transform.rotation.z >= angle / 2 - 1)
        {
            Debug.Log(blackObjects[0].transform.localEulerAngles);
            Debug.Log(-angle / 2);
            blackObjects[0].transform.rotation = Quaternion.AngleAxis(-(angle * (Time.deltaTime + timeSinceStart) / 2), Vector3.forward);
            blackObjects[1].transform.rotation = Quaternion.AngleAxis(angle * (Time.deltaTime + timeSinceStart) / 2, Vector3.forward);
            timeSinceStart += Time.deltaTime;
            yield return null;
        }
        yield break;
    }*/

    void OnTriggerEnter2D(Collider2D col)
    {
        string tagname = col.gameObject.tag;

        if (tagname == "Antag")
        {
            AntagFocused = true;
        }
        else if (tagname == "PowerUps")
        {
            //powerupFocused = true;
			powerUp = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        string tagname = col.gameObject.tag;
        if (tagname == "Antag")
        {
            AntagFocused = false;
        }
        else if (tagname == "PowerUps")
        {
            //powerupFocused = false;
			powerupCooldown = 0;
			powerUp = null;
        }
    }
}
