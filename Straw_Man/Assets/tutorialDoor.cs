using UnityEngine;
using System.Collections;

public class tutorialDoor : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D _target)
    {
        if (_target.tag == "Player")
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");

            int length = player.Length;
            for (int i = 0; i < length; i++)
                Destroy(player[i].gameObject);

            Application.LoadLevel(1); 
        }
    }
}
