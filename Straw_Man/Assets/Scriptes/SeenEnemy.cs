using UnityEngine;
using System.Collections;

public class SeenEnemy : MonoBehaviour {



    public Transform sightStart, sightEnd;
    public bool needsCollision = true;

  public bool collision = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        collision = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Default"));
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);
       
        GetComponent<SoldieScript>().seeEnemy = true;

    }
}
