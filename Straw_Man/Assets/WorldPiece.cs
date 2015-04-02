using UnityEngine;
using System.Collections;

public class WorldPiece : MonoBehaviour 
{
    private Color m_startColor, m_EndColor;
    private float m_time = 0;

	// Use this for initialization
	void Start () 
    {
        m_startColor = GetComponent<SpriteRenderer>().color;
        m_EndColor = new Color(m_startColor.r, m_startColor.g, m_startColor.b, 0.0f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_time += Time.deltaTime;

        renderer.material.color = Color.Lerp(m_startColor, m_EndColor, m_time * 0.5f);

        if (renderer.material.color.a <= 0.0f)
            Destroy(gameObject);
	}
}
