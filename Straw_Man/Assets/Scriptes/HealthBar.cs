using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public float Healthmax;
    float currentHealth;
    float percentage;
    public GameObject Health;
    Vector3 bar = new Vector3(0.0f, 0.0f, 0.0f);

    bool on = false;
    // Use this for initialization
    void Start()
    {
        Healthmax = 50;
    }
    // Update is called once per frame
    void Update()
    {
        bar = gameObject.transform.localPosition;

       bar += new Vector3(0.0f,2.0f,0.0f);
            currentHealth = gameObject.GetComponentInParent<Entity>().m_health;

        
        percentage = currentHealth / gameObject.GetComponent<Entity>().m_MaxHealth;
     
       

        if (percentage < 1 && on == false)
        {
            on = true;
            Instantiate(Health,bar, gameObject.transform.rotation);
            Health.transform.localScale = new Vector3(1, 1, 1);
         
        }

        if (Health)
        {
            Health.transform.localPosition = bar;
            Health.transform.localScale = new Vector3(Health.transform.localScale.x * percentage, 1, 1);

        }

    }
}
