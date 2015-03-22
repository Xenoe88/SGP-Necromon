using UnityEngine;
using System.Collections;

public class HitBox : MonoBehaviour
{
    public Entity m_player;
    public AudioClip m_hit;

    void OnTriggerEnter2D(Collider2D _target)
    {
        AudioSource.PlayClipAtPoint(m_hit,Camera.main.transform.position);
        _target.SendMessage("ModifyHealth", m_player.m_dmg);
    }
}
