using UnityEngine;
using System.Collections;

public class BombCounterScript : MonoBehaviour 
{
    public GameObject info;
    int l = 0;
    int p = 0;
    void OnGUI()
    {

      
        GUI.Box(new Rect(115,55, 25, 25), l.ToString() );
        GUI.Box(new Rect(185, 55, 25, 25), p.ToString());
    }
    // Update is called once per frame
    void Update()
    {
        //print(l);
       l = info.GetComponent<PlayerInventory>().GetNumBomb();
       p = info.GetComponent<PlayerInventory>().GetNumRevives();
        //OnGUI();
    }
}
