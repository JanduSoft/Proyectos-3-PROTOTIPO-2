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
            StartCoroutine(LoadDaScene());
        }

    }

    IEnumerator LoadDaScene()
    {
        animationToPlay.clip = animationsToPlay[0];
        animationToPlay.Play();
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (operation.progress<=0.8f)
        {
            //mantains the game stuck in the loop until the level is done loading
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        //when operation is done, play press any key to continue
        animationToPlay.clip = animationsToPlay[1];
        animationToPlay.Play();

        while (operation.progress==0.9f)
        {
            if (Input.anyKeyDown)
            {
                operation.allowSceneActivation = true;
                break;
            }

            yield return null;
        }

        yield return null;
    }
}
