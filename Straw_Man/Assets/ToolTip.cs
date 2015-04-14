using UnityEngine;
using System.Collections;

public class ToolTip : MonoBehaviour {

    public GameObject m_Tooltip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 tooltipPos = Camera.main.WorldToViewportPoint(transform.position + new Vector3(15,2,0));
        m_Tooltip.transform.position = tooltipPos;
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
