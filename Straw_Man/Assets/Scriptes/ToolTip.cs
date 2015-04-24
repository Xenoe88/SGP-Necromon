using UnityEngine;
using System.Collections;

public class ToolTip : MonoBehaviour {

    public GameObject m_Tooltip;
    private GameObject camera;

	// Use this for initialization
	void Start () {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 tooltipPos = Camera.main.WorldToViewportPoint(transform.position + new Vector3(-150,0,0));
        //Vector3 tooltipPos = camera.transform.position;
        //Vector3 tooltipPos = ;        
        m_Tooltip.transform.position = camera.transform.position;
	}

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            m_Tooltip.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            m_Tooltip.SetActive(false);
        }
    }
}
