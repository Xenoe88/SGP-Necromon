using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour
{

    public GameObject pauseMenuPanel;
    public Canvas canvas;
    private Animator anim;

    private bool isPaused = false;

    // Use this for initialization
    void Start()
    {
       pauseMenuPanel = GameObject.FindGameObjectWithTag("PauseMenu");
      

       
        Time.timeScale = 1;
        if (pauseMenuPanel)
            anim = pauseMenuPanel.GetComponent<Animator>();
        anim.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Escape) && !isPaused)
        {
            PauseGame();
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && isPaused)
        {

            UnPauseGame();

        }

    }

    public void PauseGame()
    {
        anim.SetInteger("PauseState", 0);

        anim.enabled = true;
        anim.Play("PauseMenuSlideIn");
        isPaused = true;
        Time.timeScale = 0;


    }

    public void UnPauseGame()
    {
        isPaused = false;
        anim.SetInteger("PauseState", 1);
        Time.timeScale = 1;

    }
}
