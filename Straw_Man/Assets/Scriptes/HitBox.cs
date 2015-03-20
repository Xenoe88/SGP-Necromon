using UnityEngine;
using System.Collections;

public class HitBox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D _target)
    {
        _target.SendMessage("ModifyHealth", -5); //TODO: -5 shouldn't be hardcoded!
        print(GetComponent<Entity>().m_dmg);
        print("^^ This should equal the players Dmg");
    }
}
