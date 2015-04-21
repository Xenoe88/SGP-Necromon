using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    private GameObject target;
    private Transform _t, _past;

    void Awake()
    {
        //camera.orthographicSize = ((Screen.height / 2.0f) / 100f);
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

        //if (Mathf.Abs(transform.position.x - _t.position.x) > 0)
        //   transform.position = new Vector3((_t.position.x)+.5f, (_t.position.y), transform.position.z);
        //else
            transform.position = new Vector3((_t.position.x), (_t.position.y), transform.position.z);

    }


}
