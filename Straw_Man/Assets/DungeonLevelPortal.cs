﻿using UnityEngine;
using System.Collections;

public class DungeonLevelPortal : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    void LoadScene()
    {
        Application.LoadLevel("DungeonScene");
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.GetComponent<PlayerScript>().m_gameStatus > 0)
        {
            LoadScene();
            //GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<LoadingScreen>().ChangeLevel("DungeonScene");

        }
    }
}
