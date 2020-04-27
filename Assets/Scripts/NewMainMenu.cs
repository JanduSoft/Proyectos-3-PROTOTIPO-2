using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;
using UnityEngine.UI;

public class NewMainMenu : MonoBehaviour
{
    #region VARIABLES    
    public enum ButtonType
    {
        NONE,
        CONTINUE,
        NEW_GAME,
        SETTINGS,
        CREDITS,
        QUIT,
        FULLSCREEN,
        FULLSCREEN_CHECK,
        MUTE,
        BACK
    };

    public enum MenuType
    {
        NONE,
        MAIN_MENU,
        SETTINGS,
        CREDITS
    }

    [Header("BUTTONS")]
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject newGameButton;
    [SerializeField] GameObject settingButton;
    [SerializeField] GameObject creditsButton;
    [SerializeField] GameObject quitButton;
    
    [SerializeField] Image fullscreenButton;
    [SerializeField] GameObject fullscreenchecK;
    [SerializeField] Image muteButton;
    [SerializeField] GameObject muteCheck;
    [SerializeField] GameObject backButton;
    [Header("MENUS")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject creditsMenu;
    [SerializeField] GameObject settingsMenu;
    [Header("SOUNDS")]
    [SerializeField] AudioSource soundNavigation;
    [SerializeField] AudioSource soundAccept;
    [SerializeField] AudioSource soundDenied;

    float horizontalMove;
    float verticalMove;

    bool upPressed = false;
    bool downPressed = false;
    bool isMuted = false;
    bool isFullscreen = true;
    ButtonType currentButton = ButtonType.CONTINUE;
    MenuType currentMenu = MenuType.MAIN_MENU;
    ///Inpud device
    InputDevice inputDevice;
    #endregion

    #region START
    private void Start()
    {
        isFullscreen = Screen.fullScreen;
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
                case ButtonType.SETTINGS:
                    {
                        currentButton = ButtonType.FULLSCREEN_CHECK;
                        currentMenu = MenuType.SETTINGS;
                        settingButton.SetActive(false);
                        continueButton.SetActive(true);
                        mainMenu.SetActive(false);
                        settingsMenu.SetActive(true);
                        break;
                    }
                case ButtonType.CREDITS:
                    {
                        currentButton = ButtonType.BACK;
                        currentMenu = MenuType.CREDITS;
                        creditsButton.SetActive(false);
                        continueButton.SetActive(true);
                        mainMenu.SetActive(false);
                        creditsMenu.SetActive(true);
                        break;
                    }
                case ButtonType.QUIT:
                    {
                        Application.Quit();
                        break;
                    }
                case ButtonType.BACK:
                    {
                        backButton.SetActive(false);
                        fullscreenButton.color = Color.yellow;

                        currentButton = ButtonType.CONTINUE;
                        currentMenu = MenuType.MAIN_MENU;
                        creditsMenu.SetActive(false);
                        settingsMenu.SetActive(false);
                        mainMenu.SetActive(true);
                        break;
                    }
                case ButtonType.FULLSCREEN_CHECK:
                    {
                        if (isFullscreen)
                        {
                            isFullscreen = false;
                            Screen.fullScreen = false;
                            fullscreenchecK.SetActive(false);
                        }
                        else if (!isFullscreen)
                        {
                            isFullscreen = true;
                            Screen.fullScreen = true;
                            fullscreenchecK.SetActive(true);
                        }
                        break;
                    }
                case ButtonType.MUTE:
                    {
                        if (isMuted)
                        {
                            muteCheck.SetActive(false);

                            isMuted = false;

                            AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
                            for (int index = 0; index < sources.Length; ++index)
                            {
                                sources[index].mute = false;
                            }

                        }
                        else
                        {
                            muteCheck.SetActive(true);
                            isMuted = true;

                            AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
                            for (int index = 0; index < sources.Length; ++index)
                            {
                                sources[index].mute = true;
                            }
                        }
                        break;
                    }
                default:
                    {
                        Screen.fullScreen = false;
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
                case ButtonType.SETTINGS:
                    {
                        settingButton.SetActive(false);
                        currentButton = ButtonType.NEW_GAME;
                        newGameButton.SetActive(true);
                        break;
                    }
                case ButtonType.CREDITS:
                    {
                        creditsButton.SetActive(false);
                        currentButton = ButtonType.SETTINGS;
                        settingButton.SetActive(true);
                        break;
                    }
                case ButtonType.QUIT:
                    {
                        quitButton.SetActive(false);
                        currentButton = ButtonType.CREDITS;
                        creditsButton.SetActive(true);
                        break;
                    }
                case ButtonType.FULLSCREEN_CHECK:
                    {
                        fullscreenButton.color = Color.white;
                        currentButton = ButtonType.BACK;
                        backButton.SetActive(true);
                        break;
                    }
                case ButtonType.MUTE:
                    {
                        muteButton.color = Color.white;
                        currentButton = ButtonType.FULLSCREEN_CHECK;
                        fullscreenButton.color = Color.yellow;
                        break;
                    }
                case ButtonType.BACK:
                    {
                        backButton.SetActive(false);
                        currentButton = ButtonType.MUTE;
                        muteButton.color = Color.yellow;
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
                        currentButton = ButtonType.SETTINGS;
                        settingButton.SetActive(true);
                        break;
                    }
                case ButtonType.SETTINGS:
                    {
                        settingButton.SetActive(false);
                        currentButton = ButtonType.CREDITS;
                        creditsButton.SetActive(true);
                        break;
                    }
                case ButtonType.CREDITS:
                    {
                        creditsButton.SetActive(false);
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
                case ButtonType.FULLSCREEN_CHECK:
                    {
                        fullscreenButton.color = Color.white;
                        currentButton = ButtonType.MUTE;
                        muteButton.color = Color.yellow;
                        break;
                    }
                case ButtonType.MUTE:
                    {
                        muteButton.color = Color.white;
                        currentButton = ButtonType.BACK;
                        backButton.SetActive(true);
                        break;
                    }
                case ButtonType.BACK:
                    {
                        backButton.SetActive(false);
                        currentButton = ButtonType.FULLSCREEN_CHECK;
                        fullscreenButton.color = Color.yellow;
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
                    case ButtonType.SETTINGS:
                        {
                            settingButton.SetActive(false);
                            currentButton = ButtonType.NEW_GAME;
                            newGameButton.SetActive(true);
                            break;
                        }
                    case ButtonType.CREDITS:
                        {
                            creditsButton.SetActive(false);
                            currentButton = ButtonType.SETTINGS;
                            settingButton.SetActive(true);
                            break;
                        }
                    case ButtonType.QUIT:
                        {
                            quitButton.SetActive(false);
                            currentButton = ButtonType.CREDITS;
                            creditsButton.SetActive(true);
                            break;
                        }
                    case ButtonType.FULLSCREEN_CHECK:
                        {
                            fullscreenButton.color = Color.white;
                            currentButton = ButtonType.BACK;
                            backButton.SetActive(true);
                            break;
                        }
                    case ButtonType.MUTE:
                        {
                            muteButton.color = Color.white;
                            currentButton = ButtonType.FULLSCREEN_CHECK;
                            fullscreenButton.color = Color.yellow;
                            break;
                        }
                    case ButtonType.BACK:
                        {
                            backButton.SetActive(false);
                            currentButton = ButtonType.MUTE;
                            muteButton.color = Color.yellow;
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
                            currentButton = ButtonType.SETTINGS;
                            settingButton.SetActive(true);
                            break;
                        }
                    case ButtonType.SETTINGS:
                        {
                            settingButton.SetActive(false);
                            currentButton = ButtonType.CREDITS;
                            creditsButton.SetActive(true);
                            break;
                        }
                    case ButtonType.CREDITS:
                        {
                            creditsButton.SetActive(false);
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
                    case ButtonType.FULLSCREEN_CHECK:
                        {
                            fullscreenButton.color = Color.white;
                            currentButton = ButtonType.MUTE;
                            muteButton.color = Color.yellow;
                            break;
                        }
                    case ButtonType.MUTE:
                        {
                            muteButton.color = Color.white;
                            currentButton = ButtonType.BACK;
                            backButton.SetActive(true);
                            break;
                        }
                    case ButtonType.BACK:
                        {
                            backButton.SetActive(false);
                            currentButton = ButtonType.FULLSCREEN_CHECK;
                            fullscreenButton.color = Color.yellow;
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
