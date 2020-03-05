using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelJumper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene(0,LoadSceneMode.Single);   
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);

        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);

        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene(3, LoadSceneMode.Single);

        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene(4, LoadSceneMode.Single);

        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            SceneManager.LoadScene(5, LoadSceneMode.Single);

        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);   //reloads the same scene you're in
        }
    }
}
