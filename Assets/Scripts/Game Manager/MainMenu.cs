using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu :  MonoBehaviour
{
    [SerializeField] Text play;
    [SerializeField] Text exit;
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject exitButton;
    [SerializeField] Text playHighlighted;
    [SerializeField] Text exitHighlighted;
    [SerializeField] Image title;
    [SerializeField] Image backGroud;
    [SerializeField] GameObject mainCamera;
    bool tweening = false;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(true);

        if (playButton.name == EventSystem.current.currentSelectedGameObject.name)
        {
            playHighlighted.gameObject.SetActive(true);
            exitHighlighted.gameObject.SetActive(false);
        }
        else if(exitButton.name == EventSystem.current.currentSelectedGameObject.name)
        {
            playHighlighted.gameObject.SetActive(false);
            exitHighlighted.gameObject.SetActive(true);
        }
        if (tweening) time += Time.deltaTime;
        if (time > 2)
        {
            GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(false);
            gameObject.SetActive(false);
        }
    }

    public void Play()
    {
        play.DOFade(0, 1);
        exit.DOFade(0, 1);
        title.DOFade(0, 1);
        backGroud.DOFade(0, 2);
        transform.DOMove(mainCamera.transform.position, 2f);
        transform.DOLocalRotateQuaternion(mainCamera.transform.rotation, 1.8f);
        tweening = true;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
