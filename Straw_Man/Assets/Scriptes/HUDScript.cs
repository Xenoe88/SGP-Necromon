﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDScript : MonoBehaviour {

     public float fill;
     public PlayerScript player = null;
	// Update is called once per frame
	void Update () 
    {
        Image image = GetComponent<Image>();
        if (player)
        {
            fill = (float)player.m_player.m_health / 200.0f;

            image.fillAmount = fill;
        }
    }
}