using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour
{

    public GameObject pauseMenuPanel;

    private Animator anim;

    private bool isPaused = false;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        anim = pauseMenuPanel.GetComponent<Animator>();
        anim.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
            if (Input.GetKeyUp(KeyCode.Escape) && !isPaused)
            {
                print("pooi");

                PauseGame();
            }
            else if (Input.GetKeyUp(KeyCode.Escape) && isPaused)
            {

                UnPauseGame();
                
            }
        
    }

    public void PauseGame()
    {
        anim.enabled = true;
        anim.Play("PauseMenuSlideIn");
        isPaused = true;
        Time.timeScale = 0;

        
    }

    public void UnPauseGame()
    {
        isPaused = false;
        anim.Play("PauseMenuSlideOut");
        Time.timeScale = 1;
        
    }
}
