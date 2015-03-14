using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    //start 2:30 pm
    //end 

    public float movement;
    public int jumpHeight;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float groundRadius;
    public bool isGrounded;
    public int jumpCount;
    public int fallRate;
    public GameObject jumpParticles;
    public int numActiveNecromon;
    public GameObject hitBox;
    public Entity player;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
