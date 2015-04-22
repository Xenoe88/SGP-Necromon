using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    float Healthmax = 0;
    float currentHealth;
    float percentage;
    public SpriteRenderer subject;
    Vector3 bar;
    // Use this for initialization
    void Start()
    {
        bar = subject.transform.localScale;

    }
    // Update is called once per frame
    void Update()
    {
        if(Healthmax <=0)
            Healthmax = subject.GetComponentInParent<Entity>().m_health;

        currentHealth = GetComponentInParent<Entity>().m_health;
        
        percentage =currentHealth / Healthmax;
        print(percentage);
        if(percentage < 1)
         subject.transform.localScale = new Vector3(bar.x * percentage, bar.y, bar.z);
    }
}
