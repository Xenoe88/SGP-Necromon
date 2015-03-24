using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CooldownScript : MonoBehaviour {

    public float fill;
    public GameObject enemy;
    // Update is called once per frame
    void Update()
    {
        Image image = GetComponent<Image>();
        if (enemy.GetComponent<Entity>().m_attackCooldown > 0)
            fill = 1.0f / enemy.GetComponent<Entity>().m_attackCooldown;
        else
            fill = 1.0f;

        image.fillAmount = fill;

    }
    
}
