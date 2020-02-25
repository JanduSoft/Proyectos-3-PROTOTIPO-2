using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class playerDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void killPlayer(float _secondsToRestart=0)
    {
        StartCoroutine(killPlayerInSeconds(_secondsToRestart));
    }

    IEnumerator killPlayerInSeconds(float _s)
    {
        GetComponent<PlayerMovement>().StopMovement(true);
        yield return new WaitForSeconds(_s);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);   //reloads the same scene
    }
}

