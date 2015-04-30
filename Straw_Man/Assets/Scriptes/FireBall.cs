using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour
{
    public Vector3 m_target;
    private GameObject m_targetCheck;
    public bool m_fired = false;
    public Animator m_animator;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_fired = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;
        ////we don't want to keep changing the direction, we just want to set it once
        move = (m_target - transform.position).normalized;

        transform.Translate(move * 0.1f, Space.World);
    }

    void SetTarget(GameObject _target)
    {
        m_target = _target.transform.position;
        m_targetCheck = _target;
    }

    void OnTriggerEnter2D(Collider2D _target)
    {
        if (m_animator == null)
        {
            m_animator = GetComponent<Animator>();

        }
        m_animator.SetInteger("FireballAnim", 1);

        if (_target.tag == m_targetCheck.tag)
            _target.SendMessage("ModifyHealth", -10, SendMessageOptions.DontRequireReceiver);
    }

    void Destroy() { Destroy(gameObject); }
}
