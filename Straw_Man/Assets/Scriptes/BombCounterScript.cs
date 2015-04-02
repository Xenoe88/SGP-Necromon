using UnityEngine;
using System.Collections;

public class BombCounterScript : MonoBehaviour 
{
    public PlayerScript info;
    void OnGUI()
    {
        int l = info.GetComponent<PlayerInventory>().GetNumBomb();
        int p = info.GetComponent<PlayerInventory>().GetNumRevives();

        GUI.Box(new Rect(115, 55, 25, 25), l.ToString() );
        GUI.Box(new Rect(185, 55, 25, 25), p.ToString());
    }
    // Update is called once per frame
    void Update()
    {
        //OnGUI();
    }
}
