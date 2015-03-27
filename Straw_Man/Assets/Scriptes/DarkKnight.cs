using UnityEngine;
using System.Collections;

public class DarkKnight : MonoBehaviour
{
    private Animator m_animator;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ResetAnim() { m_animator.SetInteger("KnightAnim", 0); }
    void Destroy() { Destroy(gameObject); }
}
