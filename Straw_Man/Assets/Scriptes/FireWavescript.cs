using UnityEngine;
using System.Collections;

public class FireWavescript : MonoBehaviour 
{
    private Animator animate;
    public Vector2 velocity;
	// Use this for initialization
	void Start () 
    {
        animate = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        animate.SetInteger("AnimState",0);
        rigidbody2D.velocity = velocity;
    }

    void OnTriggerEnter2D(Collider2D _collider)
    {
        
       // animate.SetInteger("AnimState", 1);
        animate.Play("FireHitANimation");

        print(animate.animation);
       _collider.SendMessage("ModifyHealth", -20, SendMessageOptions.DontRequireReceiver);

    }
  
    void Die()
    {

        Destroy(gameObject);
    }
}
