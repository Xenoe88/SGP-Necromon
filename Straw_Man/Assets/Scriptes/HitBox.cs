using UnityEngine;
using System.Collections;

public class HitBox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D _target)
    {
        _target.SendMessage("ModifyHealth", GetComponent<Entity>().m_dmg);
        print(GetComponent<Entity>().m_dmg);
        print("^^ This should equal the players Dmg");
    }
}
