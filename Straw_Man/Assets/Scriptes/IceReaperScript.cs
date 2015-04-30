using UnityEngine;
using System.Collections;

public class IceReaperScript : MonoBehaviour
{
    public bool isNecro = false;
    public GameObject target = null;
    public GameObject projectile = null;
    public float atkrange = 3.0f;
    public bool fired = false;
    int spd;

    public Entity m_Ice;
    public GameObject m_rune;
    public int slot = 7;
    public AudioClip HitSound;
    public AudioClip deathSound;


    public bool Hit = false;

                public GameObject SFX;

    // Use this for initialization
    void Start()
    {
        SFX = GameObject.FindGameObjectWithTag("MusicController");

        GetComponent<Entity>().m_dmg = -15;
        GetComponent<Entity>().m_facingDirection = new Vector2(-1, 0);
        spd = GetComponent<Entity>().m_speed = 50;
        GetComponent<Entity>().m_health = 60;
        GetComponent<Entity>().m_MaxHealth = 60;

        GetComponent<Entity>().m_attackCooldown = 14;
        GetComponent<Entity>().m_animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //CheckHealth();
        if (target)
        {
            Vector3 move = (target.transform.position - transform.position).normalized;
            //following target
            if (move.x > 0)
                rigidbody2D.velocity = new Vector2(.5f, rigidbody2D.velocity.y);
            else if (move.x < 0)
                rigidbody2D.velocity = new Vector2(-.5f, rigidbody2D.velocity.y);
            if (move.y > 0)
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, .5f);
            else if (move.y < 0)
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -.5f);



            if (Vector3.Distance(transform.position, target.transform.position) <= atkrange && !fired && GetComponent<Entity>().m_attackCooldown <= 0)
            {
                

                GetComponent<Entity>().m_animator.SetInteger("AnimState", 1);
                GetComponent<Entity>().m_attackCooldown = 8;
            }
            else if (fired && GetComponent<Entity>().m_attackCooldown > 0)
            {
                GetComponent<Entity>().m_animator.SetInteger("AnimState", 0);

            }


        }
        if (GetComponent<Entity>().m_attackCooldown <= 0)
        {
            fired = false;
        }

    }
    public void Attack()
    {

        if (fired)
            return;
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["IceReaperAttack"].Play();

        GameObject reference = (GameObject)Instantiate(projectile, transform.position + new Vector3(1, 0, 0), transform.rotation);
        reference.GetComponent<IceShotScript>().m_OwnerTag = tag;
        reference.GetComponent<IceShotScript>().m_direction = new Vector2(1, 0);

        reference = (GameObject)Instantiate(projectile, transform.position + new Vector3(-1, 0, 0), transform.rotation);
        reference.GetComponent<IceShotScript>().m_OwnerTag = tag;
        reference.GetComponent<IceShotScript>().m_direction = new Vector2(-1, 0);

        reference = (GameObject)Instantiate(projectile, transform.position + new Vector3(0, 1, 0), transform.rotation);
        reference.GetComponent<IceShotScript>().m_OwnerTag = tag;
        reference.GetComponent<IceShotScript>().m_direction = new Vector2(0, 1);

        reference = (GameObject)Instantiate(projectile, transform.position + new Vector3(0, -1, 0), transform.rotation);
        reference.GetComponent<IceShotScript>().m_OwnerTag = tag;
        reference.GetComponent<IceShotScript>().m_direction = new Vector2(0, -1);

        reference = (GameObject)Instantiate(projectile, transform.position + new Vector3(1, 1, 0), transform.rotation);
        reference.GetComponent<IceShotScript>().m_OwnerTag = tag;
        reference.GetComponent<IceShotScript>().m_direction = new Vector2(1, 1);

        reference = (GameObject)Instantiate(projectile, transform.position + new Vector3(-1, -1, 0), transform.rotation);
        reference.GetComponent<IceShotScript>().m_OwnerTag = tag;
        reference.GetComponent<IceShotScript>().m_direction = new Vector2(-1, -1);

        reference = (GameObject)Instantiate(projectile, transform.position + new Vector3(-1, 1, 0), transform.rotation);
        reference.GetComponent<IceShotScript>().m_OwnerTag = tag;
        reference.GetComponent<IceShotScript>().m_direction = new Vector2(-1, 1);

        reference = (GameObject)Instantiate(projectile, transform.position + new Vector3(1, -1, 0), transform.rotation);
        reference.GetComponent<IceShotScript>().m_OwnerTag = tag;
        reference.GetComponent<IceShotScript>().m_direction = new Vector2(1, -1);

        fired = true;
        //GameObject[] bullet = new GameObject[numShot];
        //Quaternion[] shootDirections = 
        //GameObject temp = (GameObject)Instantiate(projectile, transform.position, Camera.main.transform.rotation);
        //temp.gameObject.rigidbody2D.velocity = new Vector2(-1, 0);

    }
    void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.gameObject.tag != this.tag && (_collider.gameObject.tag == "Player" || _collider.gameObject.tag == "Enemy"))
            target = _collider.gameObject;
       
    }


    void OnTriggerExit2D(Collider2D _collider)
    {
        if (_collider.gameObject == target)
        {
            rigidbody2D.velocity = new Vector2(0, 0);
            target = null;
        }
    }
    public void ModifyHealth(int _dmg)
    {
        
        GetComponent<Entity>().m_health += _dmg;
        GetComponent<Entity>().m_animator.SetInteger("AnimState", 2);
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["IceReaperTakeDamage"].Play();


    }
    void CheckHealth()
    {
        if (GetComponent<Entity>().m_health <= 0)
        {
            SFX.GetComponent<LoadSoundFX>().m_soundFXsources["IceReaperDie"].Play();
            

            GetComponent<Entity>().m_animator.SetInteger("AnimState", 3);

        }
        else
            GetComponent<Entity>().m_animator.SetInteger("AnimState", 0);

    }
    public void MakeNecro()
    {

        isNecro = true;
        tag = "Player";

        SFX = GameObject.FindGameObjectWithTag("MusicController");
        SFX.GetComponent<LoadSoundFX>().m_soundFXsources["IceReaperBattleCry"].Play();

    }
   void Die()
    {
       

        print(gameObject);
        
        int randomVariable = Random.Range(0, 100);

        if (randomVariable >= 0 && randomVariable <= 20 && isNecro == false)
        {
            GameObject temp = (GameObject)Instantiate(m_rune, transform.position, transform.rotation);
            temp.SendMessage("SetID", slot, SendMessageOptions.DontRequireReceiver);
            //TODO
        }

        if (isNecro)
        {

            GetComponent<Entity>().Owner.GetComponent<PlayerInventory>().SendMessage("EnemyActive", slot, SendMessageOptions.RequireReceiver);

        }

        Destroy(this.gameObject);
    }
}
