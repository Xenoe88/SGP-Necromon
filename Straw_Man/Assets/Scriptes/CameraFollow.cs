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
        transform.position = new Vector3((_t.position.x), (_t.position.y), transform.position.z);
       
    }
    public void CameraMove(string _dir)
    {
        if(_dir == "Up")
        {
           // transform.position = new Vector3((_t.position.x), (_t.position.y + 15), transform.position.z);

        }
        if (_dir == "Down")
        {

        }
        if (_dir == "Left")
        {

        }
        if (_dir == "Right")
        {
          //  transform.position = new Vector3((_t.position.x), (_t.position.y +10), transform.position.z);

        }
    }

}
