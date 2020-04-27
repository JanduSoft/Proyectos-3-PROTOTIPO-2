using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class NewMainMenu : MonoBehaviour
{
    #region VARIABLES    
    public enum ButtonType
    {
        NONE,
        CONTINUE,
        NEW_GAME,
        QUIT
    };

    [Header("BUTTONS")]
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject newGameButton;
    [SerializeField] GameObject quitButton;
    [SerializeField] AudioSource soundNavigation;
    [SerializeField] AudioSource soundAccept;
    [SerializeField] AudioSource soundDenied;

    float horizontalMove;
    float verticalMove;

    bool upPressed = false;
    bool downPressed = false;

    ButtonType currentButton = ButtonType.CONTINUE;

    ///Inpud device
    InputDevice inputDevice;
    #endregion

    #region START
    private void Start()
    {
        //Seteamso el Inpud Device
        inputDevice = InputManager.ActiveDevice;
        continueButton.SetActive(true);
    }
    #endregion

    #region UPDATE
    void Update()
    {
        //getting input Gamepad
        verticalMove = inputDevice.LeftStickY;

        //Checking navigation
        KeyboardNavigation();
        GamepadNavigation();

        //Checking Input
        if (inputDevice.Action1.WasPressed || Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentButton)
            {
                case ButtonType.CONTINUE:
                    {
                        if (PlayerPrefs.GetInt("LevelSaved") != 0)
                        {
                            soundAccept.Play();
                            switch (PlayerPrefs.GetInt("LevelSaved"))
                            {
                                case 1:
                                    {
                                        SceneManager.LoadScene("InitialCinematic");
                                        break;
                                    }
                                case 2:
                                    {
                                        SceneManager.LoadScene("TestTutorialPart1");
                                        break;
                                    }
                                case 3:
                                    {
                                        SceneManager.LoadScene("TestTutorialPart2");
                                        break;
                                    }
                                case 4:
                                    {
                                        SceneManager.LoadScene("NewSection1Part1");
                                        break;
                                    }
                                case 5:
                                    {
                                        SceneManager.LoadScene("Section2Part1");
                                        break;
                                    }
                                case 6:
                                    {
                                        SceneManager.LoadScene("Section1Part2");
                                        break;
                                    }
                                case 7:
                                    {
                                        SceneManager.LoadScene("Section1Part3");
                                        break;
                                    }
                                default:
                                    break;
                            }
                        }
                        else if (PlayerPrefs.GetInt("LevelSaved") == 0)
                        {
                            soundDenied.Play();
                        }
                        break;
                    }
                case ButtonType.NEW_GAME:
                    {
                        PlayerPrefs.SetInt("LevelSaved", 1);
                        SceneManager.LoadScene("InitialCinematic");

                        RestartSecrets();
                        break;
                    }
                case ButtonType.QUIT:
                    {
                        Application.Quit();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

    }
    #endregion

    #region KEYBOARD NAVIGATION
    void KeyboardNavigation()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {

            switch (currentButton)
            {
                case ButtonType.CONTINUE:
                    {
                        continueButton.SetActive(false);
                        currentButton = ButtonType.QUIT;
                        quitButton.SetActive(true);
                        break;
                    }
                case ButtonType.NEW_GAME:
                    {
                        newGameButton.SetActive(false);
                        currentButton = ButtonType.CONTINUE;
                        continueButton.SetActive(true);
                        break;
                    }
                case ButtonType.QUIT:
                    {
                        quitButton.SetActive(false);
                        currentButton = ButtonType.NEW_GAME;
                        newGameButton.SetActive(true);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            soundNavigation.Play();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            switch (currentButton)
            {
                case ButtonType.CONTINUE:
                    {
                        continueButton.SetActive(false);
                        currentButton = ButtonType.NEW_GAME;
                        newGameButton.SetActive(true);
                        break;
                    }
                case ButtonType.NEW_GAME:
                    {
                        newGameButton.SetActive(false);
                        currentButton = ButtonType.QUIT;
                        quitButton.SetActive(true);
                        break;
                    }
                case ButtonType.QUIT:
                    {
                        quitButton.SetActive(false);
                        currentButton = ButtonType.CONTINUE;
                        continueButton.SetActive(true);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            soundNavigation.Play();
        }
    }
    #endregion

    #region GAMEPAD NAVIGATION
    void GamepadNavigation()
    {
        ///MIRAMOS SI APRETA ARRIBA
        if (verticalMove >= 0.95f)
        {
            if (!upPressed)
            {
                upPressed = true;
                //Camiamos el objecto seleccionado
                switch (currentButton)
                {
                    case ButtonType.CONTINUE:
                        {
                            continueButton.SetActive(false);
                            currentButton = ButtonType.QUIT;
                            quitButton.SetActive(true);
                            break;
                        }
                    case ButtonType.NEW_GAME:
                        {
                            newGameButton.SetActive(false);
                            currentButton = ButtonType.CONTINUE;
                            continueButton.SetActive(true);
                            break;
                        }
                    case ButtonType.QUIT:
                        {
                            quitButton.SetActive(false);
                            currentButton = ButtonType.NEW_GAME;
                            newGameButton.SetActive(true);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                soundNavigation.Play();
            }
        }
        else
        {
            upPressed = false;
        }
        ///MIRAMOS SI APRETA ABAJO
        if (verticalMove <= -0.95f)
        {
            if (!downPressed)
            {
                downPressed = true;
                //Camiamos el objecto seleccionado
                switch (currentButton)
                {
                    case ButtonType.CONTINUE:
                        {
                            continueButton.SetActive(false);
                            currentButton = ButtonType.NEW_GAME;
                            newGameButton.SetActive(true);
                            break;
                        }
                    case ButtonType.NEW_GAME:
                        {
                            newGameButton.SetActive(false);
                            currentButton = ButtonType.QUIT;
                            quitButton.SetActive(true);
                            break;
                        }
                    case ButtonType.QUIT:
                        {
                            quitButton.SetActive(false);
                            currentButton = ButtonType.CONTINUE;
                            continueButton.SetActive(true);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                soundNavigation.Play();
            }
        }
        else
        {
            downPressed = false;
        }

    }
    #endregion

    #region RESTART SECRETS
    void RestartSecrets()
    {
        PlayerPrefs.SetInt("Secret1",0);
        PlayerPrefs.SetInt("Secret2", 0);
        PlayerPrefs.SetInt("Secret3", 0);
        PlayerPrefs.SetInt("Secret4", 0);
        PlayerPrefs.SetInt("Secret5", 0);
        PlayerPrefs.SetInt("Secret6", 0);
    }
    #endregion
}
