using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour
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
    private bool isPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("hasDoneMain"))
        {
            gameObject.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("hasDoneMain", 1);   //1 = true
            mainCamera.SetActive(false);
            GameObject.Find("Game Manager").GetComponent<PauseScript>().canPause = false;
            EventSystem.current.SetSelectedGameObject(playButton);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(true);
        if (EventSystem.current.currentSelectedGameObject == null) EventSystem.current.SetSelectedGameObject(playButton);
        if (EventSystem.current != null)
        {
            if (playButton.name == EventSystem.current.currentSelectedGameObject.name && !isPlayed)
            {
                playHighlighted.gameObject.SetActive(true);
                exitHighlighted.gameObject.SetActive(false);
            }
            else if (exitButton.name == EventSystem.current.currentSelectedGameObject.name && !isPlayed)
            {
                playHighlighted.gameObject.SetActive(false);
                exitHighlighted.gameObject.SetActive(true);
            }
            if (tweening) time += Time.deltaTime;
            if (time > 2)
            {
                GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(false);
                mainCamera.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }

    public void Play()
    {
        isPlayed = true;
        playHighlighted.gameObject.SetActive(false);
        exitHighlighted.gameObject.SetActive(false);

        play.DOFade(0, 0.5f);
        exit.DOFade(0, 0.5f);
        title.DOFade(0, 1);
        backGroud.DOFade(0, 2);
        transform.DOMove(mainCamera.transform.position, 2f);
        transform.DOLocalRotateQuaternion(mainCamera.transform.rotation, 1.8f);
        tweening = true;

        GameObject.Find("Game Manager").GetComponent<PauseScript>().canPause = true;

    }
    public void Exit()
    {
        Application.Quit();
    }
}
