using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    private Transform _t, _past;

    void Awake()
    {
        camera.orthographicSize = ((Screen.height / 2.0f) / 100f);
    }
    // Use this for initialization
    void Start()
    {
        _t = target.transform;

    }


    // Update is called once per frame
    void Update()
    {
        //if(_t == _past)

        if (target.GetComponent<PlayerController>().m_movement > 0)
            transform.position = new Vector3((_t.position.x * 1.2f), (_t.position.y), transform.position.z);

        if (target.GetComponent<PlayerController>().m_movement < 0)
            transform.position = new Vector3((_t.position.x), (_t.position.y), transform.position.z);

    }


}
