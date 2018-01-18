using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public float maxAngle = 360;
    public float powerupCooldown = 0;

    public bool AntagFocused = false;
    public bool powerupFocused = false;

    private PlatformerCharacter2D playerScript;

    private void Start()
    {
        playerScript = GetComponentInParent<PlatformerCharacter2D>();
    }

    void Update()
    {
        // Rotate the vision cone
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

    }

    public void changeVisionAngle(float angle)
    {
        GameObject head = GameObject.Find("Head");

        head.GetComponent<PolygonCollider2D>().points = new Vector2[] { new Vector2(0, 0), new Vector2(8f, Convert.ToSingle((180 / Math.PI) * Mathf.Tan(angle / 2) * 8f)), new Vector2(8f, -(Convert.ToSingle((180 / Math.PI) * Mathf.Tan(angle / 2) * 8f))) };

        GameObject[] blackObjects = GameObject.FindGameObjectsWithTag("BlackObj");

        blackObjects[0].transform.rotation = Quaternion.AngleAxis(-(angle / 2), Vector3.forward);
        blackObjects[1].transform.rotation = Quaternion.AngleAxis(angle / 2, Vector3.forward);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        string tagname = col.gameObject.tag;
        if (tagname == "Antag")
        {
            AntagFocused = true;
        }
        else if (tagname == "PowerUps")
        {
            powerupFocused = true;
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
            powerupFocused = false;
        }
    }
}
