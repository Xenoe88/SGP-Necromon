﻿using UnityEngine;
using System.Collections;

public class TwoWayPlatform : MonoBehaviour 
{
    Collider2D tempLvl;
    public GameObject player;
    bool dropping;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 top = new Vector3(player.collider2D.bounds.center.x, player.collider2D.bounds.max.y + 0.1f, 0);
        Vector3 bot = new Vector3(player.collider2D.bounds.center.x, player.collider2D.bounds.min.y - 0.1f, 0);

        RaycastHit2D HitAbove = Physics2D.Raycast(top, Vector3.up, .1f);
        RaycastHit2D HitBelow = Physics2D.Raycast(bot, -Vector3.up, 1);

        if (HitAbove && HitAbove.collider.CompareTag("TwoWay"))
        {
            
              Physics2D.IgnoreCollision(HitAbove.collider, this.collider2D, true);
            //player.rigidbody2D.velocity = new Vector2(player.rigidbody2D.velocity.x, player.rigidbody2D.velocity.y + player.GetComponent<PlayerController>().m_jumpHeight);

        } 
        if(HitBelow && !HitAbove && !dropping)
            Physics2D.IgnoreCollision(HitBelow.collider, this.collider2D, false);
        
        //Two Way Platform Drop down
        if (Input.GetKeyDown("s"))
        {
           
            if (HitBelow && HitBelow.collider.CompareTag("TwoWay"))
                Physics2D.IgnoreCollision(HitBelow.collider, this.collider2D, true);
           dropping = true;
        }
        if (HitAbove && dropping)
        {
         
            Physics2D.IgnoreCollision(HitAbove.collider, this.collider2D, false);
            dropping = false;
        }
	
	}
}
