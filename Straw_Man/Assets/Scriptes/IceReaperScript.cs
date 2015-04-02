using UnityEngine;
using System.Collections;

public class IceReaperScript : MonoBehaviour
{
    public bool isNecro = false;
    public GameObject target = null;
    public GameObject projectile =null;
    public float atkrange = 5.0f;
    bool fired = false;

    public GameObject m_rune;
    public int slot = 6;

    // Use this for initialization
    void Start()
    {
        GetComponent<Entity>().m_dmg = -15;
        GetComponent<Entity>().m_facingDirection = new Vector2(-1, 0);
        GetComponent<Entity>().m_speed = 1;
        GetComponent<Entity>().m_health = 40;
        GetComponent<Entity>().m_attackCooldown = 14;
        GetComponent<Entity>().m_animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            //if (Vector3.Distance(transform.position, target.transform.position) <= atkrange && GetComponent<Entity>().m_attackCooldown <= 0)
            //{
            //    GetComponent<Entity>().m_animator.SetInteger("AnimState", 1);
            //}
            if (!fired)
            {

                GameObject reference = (GameObject)Instantiate(projectile, transform.position + new Vector3(1, 0, 0), transform.rotation);
                reference.GetComponent<IceShotScript>().m_OwnerTag = tag;
                reference.GetComponent<IceShotScript>().m_direction = new Vector2(1,0);

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
                reference.GetComponent<IceShotScript>().m_direction = new Vector2(-1,-1);
                
                reference = (GameObject)Instantiate(projectile, transform.position + new Vector3(-1, 1, 0), transform.rotation);
                reference.GetComponent<IceShotScript>().m_OwnerTag = tag;
                reference.GetComponent<IceShotScript>().m_direction = new Vector2(-1, 1);

                reference = (GameObject)Instantiate(projectile, transform.position + new Vector3(1, -1, 0), transform.rotation);
                reference.GetComponent<IceShotScript>().m_OwnerTag = tag;
                reference.GetComponent<IceShotScript>().m_direction = new Vector2(1, -1);

                fired = true;
            }
        }

    }
    public void Attack(GameObject Shot, int numShot, float degOffset, Vector2 pos, float posOffset, Vector2 RotAxis, Collider2D owner )
    {
        //GameObject[] bullet = new GameObject[numShot];
        //Quaternion[] shootDirections = 
        GameObject temp = (GameObject)Instantiate(projectile, transform.position, Camera.main.transform.rotation);
        temp.gameObject.rigidbody2D.velocity = new Vector2(-1, 0);

    }
    void OnTriggerEnter2D(Collider2D _collider)
    {
        target = _collider.gameObject;


    }
    void OnTriggerExit2D()
    {
        target = null;
    }
    public void ModifyHealth(int _dmg)
    {
        GetComponent<Entity>().m_health += _dmg;
        GetComponent<Entity>().m_animator.SetInteger("AnimState", 2);
         

        if( GetComponent<Entity>().m_health <= 0 )
             GetComponent<Entity>().m_animator.SetInteger("AnimState", 3);
        
    }
    
    public void MakeNecro()
    {
        isNecro = true;
        tag = "Player";
    }
    public void Die()
    {
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
        Destroy(gameObject);
    }
}
