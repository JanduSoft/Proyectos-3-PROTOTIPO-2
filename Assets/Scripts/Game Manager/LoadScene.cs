using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneToLoad;
    public Animation animationToPlay;
    public AnimationClip[] animationsToPlay = new AnimationClip[2];


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().StopMovement(true);
            StartCoroutine(ShowLoadingScreen());
        }

    }

    public void ShowLoad()
    {
        StartCoroutine(ShowLoadingScreen());
    }

    IEnumerator ShowLoadingScreen()
    {
       
        animationToPlay.clip = animationsToPlay[0];
        animationToPlay.Play();
        yield return new WaitForSeconds(0.5f);
        animationToPlay.clip = animationsToPlay[1];
        animationToPlay.Play();
        yield return new WaitForSeconds(2f);    //to show loading screen
        StartCoroutine(LoadDaScene());
    }

    IEnumerator LoadDaScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (operation.progress<=0.8f)
        {
            //mantains the game stuck in the loop until the level is done loading
            yield return null;
        }

        operation.allowSceneActivation = true;

        yield return null;
    }
}
