using UnityEngine;
using System.Collections;

public class HitBox : MonoBehaviour
{
    public GameObject m_player;
    public AudioClip m_hit;

    void OnTriggerEnter2D(Collider2D _target)
    {
        _target.SendMessage("ModifyHealth", m_player.GetComponent<Entity>().m_dmg);
        //AudioSource.PlayClipAtPoint(m_hit, Camera.main.transform.position);
    }

    void SetPlayer(GameObject _owner) { m_player = _owner; }
}
