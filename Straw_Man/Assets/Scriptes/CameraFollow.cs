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

    }


    // Update is called once per frame
    void Update()
    {
        //if(_t == _past)

        //if (target.GetComponent<PlayerController>().m_movement > 0)
            transform.position = new Vector3((_t.position.x ), (_t.position.y), transform.position.z);

        //if (target.GetComponent<PlayerController>().m_movement < 0)
        //    transform.position = new Vector3((_t.position.x), (_t.position.y), transform.position.z);

    }


}
