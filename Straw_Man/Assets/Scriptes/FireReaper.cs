using UnityEngine;
using System.Collections;

public class FireReaper : MonoBehaviour
{
    private Entity m_fireReaper;
    private Animator m_animation;
    public Vector3 move;

    public bool m_isNecro = false, m_isTargeting = false;
    public RuneScript m_rune;
    public int slot = 1;

    public GameObject m_target, m_fireBall;
    public float distanceTest;

    // Use this for initialization
    void Start()
    {
        m_fireReaper = GetComponent<Entity>();
        m_animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //die
        if (m_fireReaper.m_isAlive == false)
            m_animation.SetInteger("FireReaperAnim", 3);

        //if we aren't already targeting someone to attack, set target to the closest enemy
        if (m_isTargeting == false)
            m_target = FindClosestTarget();


        
        if (Vector3.Distance(m_target.transform.position, transform.position) < 8.0f)
        {
            //get move direction
            move = (m_target.transform.position - transform.position).normalized;

            //turn if necessary
            if (move.x > 0)
                transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            else if (move.x < 0)
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            //attack if we are close enough
            distanceTest = Vector3.Distance(m_target.transform.position, transform.position);

            //move, but not if we're too close
            if (distanceTest > 1.0f)
                transform.Translate(move * m_fireReaper.m_speed * 0.025f, Space.World);

            //if we're this close, we can attack
            if (distanceTest < 5.0f && m_fireReaper.m_attackCooldown <= 0)
            {
                //m_isAttacking = true; //so we stop moving around
                m_animation.SetInteger("FireReaperAnim", 2);
                m_fireReaper.m_attackCooldown = 5.0f;
            }

            m_fireReaper.m_attackCooldown -= Time.deltaTime;
        }
    }

    GameObject FindClosestTarget()
    {
        GameObject closestTarget = null;
        string tag;
        if (m_isNecro)
            tag = "Enemy";
        else
            tag = "Player";

        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag(tag);

        //loop thru all possible targets
        for (int i = 0; i < possibleTargets.Length; i++)
        {
            //just set the first one, but paranoid check in case there are no other enemies in the scene
            if (i == 0 && possibleTargets[i] != gameObject)
                closestTarget = possibleTargets[i];
            else
            {
                //if any of the others are closer, set the variable
                if (Vector3.Distance(closestTarget.transform.position, transform.position) >= Vector3.Distance(possibleTargets[i].transform.position, transform.position))
                {
                    //don't let the enemy target itself
                    if (gameObject != possibleTargets[i])
                        closestTarget = possibleTargets[i];
                }
            }
        }

        //set targeting to true so we don't keep switching targets
        m_isTargeting = true;
        return closestTarget;
    }

    GameObject FindPlayerToFollow()
    {
        GameObject closestTarget = null;
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("Player");

        //loop thru all possible targets
        for (int i = 0; i < possibleTargets.Length; i++)
        {
            //just set the first one
            if (possibleTargets[i] != gameObject)
                closestTarget = possibleTargets[i];
            //else
            //{
            //    //if any of the others are closer, set the variable
            //    if (Vector3.Distance(closestTarget.transform.position, transform.position) >= Vector3.Distance(possibleTargets[i].transform.position, transform.position))
            //    {
            //        //don't let the enemy target itself
            //        if (gameObject != possibleTargets[i])
            //            closestTarget = possibleTargets[i];
            //    }
            //}
        }

        //set targeting to true so we don't keep switching targets
        m_isTargeting = true;

        return closestTarget;
    }

    void Attack()
    {
        GameObject reference = (GameObject)Instantiate(m_fireBall, transform.position, transform.rotation);
        Physics2D.IgnoreCollision(reference.collider2D, this.collider2D);
        reference.SendMessage("SetTarget", m_target, SendMessageOptions.RequireReceiver);

        m_isTargeting = false;
    }

    void Destroy()
    {
        int randomVariable = Random.Range(0, 100);

        //TODO - also need to test if we've already collected this enemies rune
        if (randomVariable >= 0 && randomVariable <= 20 && m_isNecro == false)
        {
            GameObject temp = (GameObject)Instantiate(m_rune, transform.position, transform.rotation);
            temp.SendMessage("SetID", slot, SendMessageOptions.DontRequireReceiver);
        }

        if (m_isNecro)
        {
           GetComponent<PlayerInventory>().SendMessage("EnemyActive", slot, SendMessageOptions.DontRequireReceiver);
        }

        Destroy(gameObject);
    }

    public void MakeNecro()
    {
        m_isNecro = true;
        this.tag = "Player";
    }
    void ResetAnim() { m_animation.SetInteger("FireReaperAnim", 0); }
}
