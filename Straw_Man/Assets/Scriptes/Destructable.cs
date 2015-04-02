﻿using UnityEngine;
using System.Collections;

public class Destructable : MonoBehaviour 
{
    public WorldPiece m_worldPiece;
    public int m_numPieces = 25;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Explode()
    {
        Destroy(gameObject);

        Transform t = transform;

        for (int i = 0; i < m_numPieces; i++)
        {
            t.TransformPoint(0, -100, 0);
            WorldPiece temp = (WorldPiece)Instantiate(m_worldPiece, t.position, Quaternion.identity);
            temp.rigidbody2D.AddForce(Vector3.right * Random.Range(-50, 50));
            temp.rigidbody2D.AddForce(Vector3.up * Random.Range(100, 400));
        }
    }
}
