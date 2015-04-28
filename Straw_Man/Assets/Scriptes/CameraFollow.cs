using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    private GameObject target;
    private Transform _t, _past;

    void Awake()
    {
        camera.orthographicSize = 4;
    }
    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        _t = target.transform;
        transform.position = new Vector3((_t.position.x), (_t.position.y), transform.position.z);
    }


    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (target.GetComponent<PlayerController>().m_inMenu == true)
            {
                camera.orthographicSize = 5;
                GameObject temp = GameObject.FindGameObjectWithTag("CameraLocation");
                transform.position = temp.transform.position;
            }
            else
            {
                camera.orthographicSize = 4;
                transform.position = new Vector3((_t.position.x), (_t.position.y), transform.position.z);
            }
        }

    }


}
