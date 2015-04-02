using UnityEngine;
using System.Collections;

public class RandomSprite : MonoBehaviour 
{
    public Sprite[] m_sprites;
    public string m_resource;

	// Use this for initialization
	void Start () 
    {
        if (m_resource != "")
        {
            m_sprites = Resources.LoadAll<Sprite>(m_resource);
            GetComponent<SpriteRenderer>().sprite = m_sprites[Random.Range(0, m_sprites.Length)];
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
