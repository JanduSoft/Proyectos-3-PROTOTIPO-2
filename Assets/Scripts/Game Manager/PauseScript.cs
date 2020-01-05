using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool isPaused = false;
    public Canvas pauseCanvas;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            btn_Resume();
        }
    }

    void PauseGame()
    {
        isPaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0;
        GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(true);

        pauseCanvas.enabled = true;
    }

    void ResumeGame()
    {
        isPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1;
        GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(false);

        pauseCanvas.enabled = false;
    }

    public void btn_Resume()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }
    
    public void btn_Quit()
    {
        //This function now quits the app, but it should take you to main menu when it's done!
        Debug.Log("Quit has been pressed!");
        Application.Quit();
    }


}
