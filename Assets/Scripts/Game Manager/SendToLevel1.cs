using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;


public class SendToLevel1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayableDirector pd;
    [SerializeField] GameObject destroyableCanvas;
    [SerializeField] AudioSource music;

    void Start()
    {
        pd.Stop();
        music.Stop();
        GetComponent<LoadScene>().ShowLoad();
        Destroy(destroyableCanvas,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
