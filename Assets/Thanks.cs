using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Thanks : MonoBehaviour
{
    [SerializeField] float time = 10;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GoToMainMenu", time);
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
