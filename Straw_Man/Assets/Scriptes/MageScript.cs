using UnityEngine;
using System.Collections;

public class MageScript : MonoBehaviour
{

    public GameObject target;
    public AudioClip sounds;
    private Entity mage;
    public bool attack = false;

    public GameObject fireAtk;
    public bool fire = false;

                public GameObject SFX;

    // Use this for initialization
    void Start()
    {
        SFX = GameObject.FindGameObjectWithTag("MusicController");

        mage = GetComponent<Entity>();
        mage.m_animator = GetComponent<Animator>();
        mage.m_facingDirection = new Vector2(1, 0);

        mage.m_speed = 1;
        mage.m_health = 300;
        mage.m_MaxHealth = 300;

    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        if (mage.m_health <= 0)
        {
            mage.m_animator.SetInteger("AnimState", 5);
            return;
        }
        if (target)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);

            if (target.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);

            }
            else if (target.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (!attack && distance < 15.0f)
            {
                GetComponent<Entity>().m_animator.SetInteger("AnimState", 0);
                rigidbody2D.isKinematic = true;
                transform.position = new Vector3(transform.position.x + (transform.localScale.x * -1.0f * mage.m_speed * .02f), transform.position.y);

            }
            if (mage.m_attackCooldown <= 0)
            {

                mage.m_attackCooldown = 5;

                attack = true;


                if (mage.m_health < 150)
                    mage.m_animator.SetInteger("AnimState", 2);
                else if (distance <= 8)
                {
                    mage.m_animator.SetInteger("AnimState", 1);
                }
                else
                    attack = false;

            }

        }
        else
        {
            GetComponent<Entity>().m_animator.SetInteger("AnimState", 0);

            rigidbody2D.velocity = new Vector2(-transform.localScale.x, 0) * GetComponent<Entity>().m_speed;
        }




    }

    void FireAtk()
    {
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["MageMagicAttack1"].Play();

        target.SendMessage("ModifyHealth", -20, SendMessageOptions.DontRequireReceiver);
        //target.SendMessage("ModifyStatus", Status.CONFUSE, SendMessageOptions.DontRequireReceiver);
        mage.m_animator.SetInteger("AnimState", 0);

        attack = false;


    }
    void CreateFire()
    {
        //GameObject shot = Instantiate(fireAtk, this.transform.position + new Vector3(2.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
        //shot.GetComponent<FireWavescript>().velocity = new Vector2(3.0f, 0.0f);
    }
    void IceAtk()
    {
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["MageMagicAttack2"].Play();

        target.SendMessage("ModifyHealth", -10, SendMessageOptions.DontRequireReceiver);
        //target.SendMessage("ModifyStatus", Status.STUN, SendMessageOptions.DontRequireReceiver);
        mage.m_animator.SetInteger("AnimState", 0);

        attack = false;

    }
    void Die()
    {
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["MageDie"].Play();
        Destroy(this.gameObject);
        target.SendMessage("ModifyGameStatus", SendMessageOptions.DontRequireReceiver);
        Application.LoadLevel("ThroneRoomeScene");
    }
}
