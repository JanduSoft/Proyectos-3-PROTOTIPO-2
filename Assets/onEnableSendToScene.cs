using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class onEnableSendToScene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string sceneName;
    void Start()
    {
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
