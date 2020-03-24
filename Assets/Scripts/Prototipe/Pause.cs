using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    [SerializeField] int level;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) /*|| Input.GetButtonDown("Submit")*/)
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            switch (level)
            {
                case 1:
                    {
                        SceneManager.LoadScene("MainScene");
                        break;
                    }
                case 2:
                    {
                        SceneManager.LoadScene("Level_1");
                        break;
                    }
                case 3:
                    {
                        SceneManager.LoadScene("Level_2");
                        break;
                    }
                default:
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("MainScene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Level_1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("Level_2");
        }

    }
    
}
