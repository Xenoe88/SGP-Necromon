using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoldieScript : MonoBehaviour
{
    public bool isNecro = false;
    //private bool block = false;
    public AudioSource audioSource;
    public AudioClip walkingSFX, stabSFX, blockSFX, dmgSFX, knockbackSFX, deathSFX;

    public GameObject target = null;
    public GameObject summoner = null;
    public Entity m_soldier;
    public GameObject m_rune;
    public int slot = 3;

    public GameObject music;

                public GameObject SFX;

    // Use this for initialization
    void Start()
    {
        print("test");
        SFX = GameObject.FindGameObjectWithTag("MusicController");

        audioSource = GetComponent<AudioSource>();

        GetComponent<Entity>().m_dmg = -2;
        GetComponent<Entity>().m_facingDirection = new Vector2(1, 0);
        GetComponent<Entity>().m_speed = 1;
        GetComponent<Entity>().m_health = 70;
        GetComponent<Entity>().m_MaxHealth = 70;

        GetComponent<Entity>().m_attackCooldown = 0;
        music = GameObject.FindGameObjectWithTag("MusicController");


        this.transform.localScale = new Vector3((transform.localScale.x == 1) ? -1 : 1, 1, 1);


        GetComponent<Entity>().m_animator = GetComponent<Animator>();

    }

    void OnGUI()
    {
        //Rect position = new Rect(this.transform.localPosition.x , this.transform.localPosition.y + 1000, 100, 25);
        //Color color = new Color(1, 0, 0);

        //Texture2D texture = new Texture2D(1, 1);
        //texture.SetPixel(0, 0, color);
        //texture.Apply();
        //GUI.skin.box.normal.background = texture;
        //GUI.Box(position, GUIContent.none);
    }

    private Rect Rect(int p1, int p2, int p3, int p4)
    {
        throw new System.NotImplementedException();
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Entity>().m_health > 0)
        {
            //AudioSource.PlayClipAtPoint(walkingSFX, transform.localPosition);

            if (isNecro)
            {
                summoner = GameObject.FindGameObjectWithTag("Player");
            }
            if (summoner)
            {
                if (Mathf.Abs(Vector3.Distance(transform.localPosition, summoner.transform.localPosition)) > 5)
                {
                    //target = summoner;
                }
            }
            else if (target)
            {
                FollowTarget();

                if (GetComponent<Entity>().m_attackCooldown <= 0 && Mathf.Abs(Vector3.Distance(target.gameObject.transform.position, this.transform.position)) < 1 && target.tag != this.tag)
                {
                    SFX.GetComponent<LoadSoundFX>().m_soundFXsources["SoldierAttack"].Play();
                    
                    GetComponent<Entity>().m_animator.SetInteger("AnimState", 2);
                    KnockBack();
                    GetComponent<Entity>().m_attackCooldown = 2;
                }

            }
            else
            {
                music.GetComponent<LoadSoundFX>().m_soundFXsources["SoldierDie"].Play();

                GetComponent<Entity>().m_animator.SetInteger("AnimState", 1);
            }

            rigidbody2D.velocity = new Vector2(-transform.localScale.x, 0) * GetComponent<Entity>().m_speed;

      
        }
        else
        {


            GetComponent<Entity>().m_animator.SetInteger("AnimState", 4);
        }
    }
    void FollowTarget()
    {
        if ((target.transform.position.x < transform.position.x))
            transform.localScale = new Vector3(1, 1, 1);
        if ((target.transform.position.x > transform.position.x))
            transform.localScale = new Vector3(-1, 1, 1);

    }
    void KnockBack()
    {
        float num = Random.Range(0.0f, 1.0f);

        if (num > .80f)
        {
            SFX.GetComponent<LoadSoundFX>().m_soundFXsources["SoldierKnockBack"].Play();

            target.gameObject.transform.localPosition = target.gameObject.transform.localPosition + (new Vector3(1.0f, 0.2f, 0.0f) * target.transform.localScale.x);

        }
    }
    public void Die()
    {
        int randomVariable = Random.Range(0, 100);

        if (randomVariable >= 0 && randomVariable <= 20 && isNecro == false)
        {
            GameObject temp = (GameObject)Instantiate(m_rune, transform.position, transform.rotation);
            temp.SendMessage("SetID", slot, SendMessageOptions.DontRequireReceiver);
        }

        if (isNecro)
        {
            GetComponent<Entity>().Owner.GetComponent<PlayerInventory>().SendMessage("EnemyActive", slot, SendMessageOptions.RequireReceiver);
            GetComponent<PlayerInventory>().SendMessage("EnemyActive", m_rune, SendMessageOptions.DontRequireReceiver);
        }

        Destroy(gameObject);
    }
    public bool Block()
    {
        float num = Random.Range(0.0f, 1.0f);

        if (num > .90f)
        {
            SFX.GetComponent<LoadSoundFX>().m_soundFXsources["SoldierBlock"].Play();

            return true;
        }
        return false;
    }
    void ModifyHealth(int _amount)
    {
        if (Block())
        {
            GetComponent<Entity>().m_animator.SetInteger("AnimState", 3);
            AudioSource.PlayClipAtPoint(blockSFX, transform.localPosition);

            return;
        }

        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["SoldierTakeDamage"].Play();

        GetComponent<Entity>().m_animator.SetInteger("AnimState", 2);
        GetComponent<Entity>().m_health += _amount;
    }
    void OnTriggerEnter2D(Collider2D _target)
    {
        if ((_target.gameObject.tag == "Player" || _target.gameObject.tag == "Enemy") && _target.gameObject.tag != this.tag)
            target = _target.gameObject;

    }
    void OnTriggerExit2D()
    {
        if(target)
            if (target.gameObject.tag != this.tag)
            target = null;
    }
    //Function called as part of the animation in Unity 
    public void Attack()
    {
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["SoldierAttack"].Play();

        target.SendMessage("ModifyHealth", GetComponent<Entity>().m_dmg, SendMessageOptions.DontRequireReceiver);
        GetComponent<Entity>().m_animator.SetInteger("AnimState", 0);
    }

    public void MakeNecro()
    {
        isNecro = true;
        this.tag = "Player";
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["SoldierWarCry"].Play();
    }

}
