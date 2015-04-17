using UnityEngine;
using System.Collections;

public class Cheat : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int numNecromon = gameObject.GetComponent<PlayerInventory>().runes.Length;

        //Collect all Necromon
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            for (int i = 0; i < numNecromon; i++)
                gameObject.GetComponent<PlayerInventory>().runes[i].collected = true;
        }

        // Increment equipped necromon
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            gameObject.GetComponent<PlayerInventory>().m_selectedRune += 1;

            //keep array in bounds
            if (gameObject.GetComponent<PlayerInventory>().m_selectedRune >= numNecromon)
                gameObject.GetComponent<PlayerInventory>().m_selectedRune = numNecromon - 1;
        }

        // Decrement equipped necromon
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            gameObject.GetComponent<PlayerInventory>().m_selectedRune -= 1;

            //keep array in bounds
            if (gameObject.GetComponent<PlayerInventory>().m_selectedRune < 0)
                gameObject.GetComponent<PlayerInventory>().m_selectedRune = 0;
        }

        //add revive
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            gameObject.GetComponent<PlayerInventory>().AddRevive();
        }

        //add bomb
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            gameObject.GetComponent<PlayerInventory>().AddBomb();
        }

        //revive player
        if (Input.GetKeyDown(KeyCode.Keypad5) && gameObject.GetComponent<Entity>().m_isAlive == false)
        {
            gameObject.GetComponent<PlayerController>().Revive();
        }
    }
}
