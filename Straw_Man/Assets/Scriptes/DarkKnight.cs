using UnityEngine;
using System.Collections;

public class DarkKnight : MonoBehaviour
{
    private Animator m_animator;
    private Entity m_darkKnight;
    public GameObject m_target, m_primaryAttack, m_specialAttack;
    public float m_attack1Timer = 10.0f, m_attack2Timer = 30.0f;
    public AudioClip m_atk1SFX, m_atk2SFX;

            public GameObject SFX;

    // Use this for initialization
    void Start()
    {
        SFX = GameObject.FindGameObjectWithTag("MusicController");

        m_animator = GetComponent<Animator>();
        m_darkKnight = GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {

        //check health
        if (m_darkKnight.m_isAlive == false)
        {
            SFX.GetComponent<LoadSoundFX>().m_soundFXsources["DarkKnightDie"].Play();
            m_animator.SetInteger("KnightAnim", 4);
        }
        // if we can locate player
        if (m_target = GameObject.FindGameObjectWithTag("Player"))
        {

            if (m_target.GetComponent<PlayerScript>().m_inBossRoom == true)
            {
                print("TESTINGTESTING");
            }

            //print("TESTING DK BUG");
            //always face the player
            transform.localScale = new Vector3(m_target.transform.position.x < transform.position.x ? 0.5f : -0.5f, 0.5f, 0.5f);

            //if he's alive, do Dark Knight stuff
            if (m_target.GetComponent<Entity>().m_isAlive)
            {
                //check distance to him, while it's greater than 3, move in his direction
                float distanceToPlayer = Vector3.Distance(m_target.transform.position, transform.position);
                if (distanceToPlayer > 3.0f)
                {
                    Move();
                }
                else
                {
                    //m_animator.SetInteger("KnightAnim", 2);
                    if (m_attack1Timer <= 0.0f)
                        m_animator.SetInteger("KnightAnim", 3);

                    //check if it's time to use attack 2 (player health <  50% every 20/25/30 sec?)
                    if (m_attack2Timer <= 0 && m_target.GetComponent<Entity>().m_health <= 100)
                        m_animator.SetInteger("KnightAnim", 2);
                }
            }

            m_attack1Timer -= Time.deltaTime;
            m_attack2Timer -= Time.deltaTime;
        }


    }



    void ResetAnim() { m_animator.SetInteger("KnightAnim", 0); }
    void Destroy() { Destroy(gameObject); }
    void PlayATK1SFX() { SFX.GetComponent<LoadSoundFX>().m_soundFXsources["DarkKnightMeleeAttack"].Play(); }
    void PlayATK2SFX() { SFX.GetComponent<LoadSoundFX>().m_soundFXsources["DarkKnightMagicAttack"].Play(); }

    void ChangeScene()
    {
        m_target.SendMessage("ModifyGameStatus", SendMessageOptions.DontRequireReceiver);

        if (SFX.GetComponent<LoadSoundFX>().m_soundFXsources["CourtyardBGM"].isPlaying == true)
            SFX.GetComponent<LoadSoundFX>().m_soundFXsources["CourtyardBGM"].Stop();

        m_target.GetComponent<PlayerController>().m_reLoadPosition = Vector3.zero;
        m_target.GetComponent<PlayerController>().m_RevivePositon = Vector3.zero;
        m_target.GetComponent<PlayerScript>().m_inBossRoom = false;

        print("DK CHANGED SCENES!");


        Application.LoadLevel(12); 
    }

    void Attack1()
    {
        GameObject temp = (GameObject)Instantiate(m_primaryAttack, m_target.transform.position + new Vector3(0.25f, 0.25f, 0.0f), m_target.transform.rotation);
        temp.SendMessage("SetOwner", gameObject, SendMessageOptions.RequireReceiver);
        m_attack1Timer = 10.0f;
    }

    void Attack2()
    {
        int offset;
        if (transform.localScale.x > 0)
            offset = -1;
        else
            offset = 1;
  
        GameObject temp = (GameObject)Instantiate(m_specialAttack, transform.position + new Vector3(offset * 3.0f, 0f, 0.0f), m_target.transform.rotation);
        Physics2D.IgnoreCollision(temp.collider2D, gameObject.collider2D);
        temp.SendMessage("SetPlayer", gameObject, SendMessageOptions.RequireReceiver);
        m_attack2Timer = 30.0f;
    }

    void Move()
    {
        //set Animation
        m_animator.SetInteger("KnightAnim", 1);

        //move
        transform.position = new Vector3(transform.position.x + (transform.localScale.x * -1.0f) * m_darkKnight.m_speed * 0.08f, transform.position.y);
    }
}
