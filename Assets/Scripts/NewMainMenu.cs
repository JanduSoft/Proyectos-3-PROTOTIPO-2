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
        BACK,
        BACK_CREDITS,
        LOW,
        MEDIUM,
        HIGH,
        LEFT_RESOLUTION,
        MIDLE_RESOLUTION,
        RIGHT_RESOLUTION
    };

    public enum MenuType
    {
        NONE,
        MAIN_MENU,
        SETTINGS,
        CREDITS
    }

    [SerializeField] GameObject continueFullAlpha;
    [SerializeField] GameObject continueEmptyAlpha;
    [Header("MAIN BUTTONS")]
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject newGameButton;
    [SerializeField] GameObject settingButton;
    [SerializeField] GameObject creditsButton;
    [SerializeField] GameObject quitButton;

    [Header("SETTINGS BUTTONS")]
    [SerializeField] GameObject leftResolution;
    [SerializeField] GameObject midleResolution;
    [SerializeField] GameObject rightResolution;
    [SerializeField] GameObject lowGraphics;
    [SerializeField] GameObject mediumGraphics;
    [SerializeField] GameObject highGraphics;
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
    bool rightPressed = false;
    bool leftPressed = false;
    bool isMuted = false;
    bool isFullscreen = true;
    ButtonType currentButton = ButtonType.CONTINUE;
    MenuType currentMenu = MenuType.MAIN_MENU;
    ///Inpud device
    InputDevice inputDevice;

    InControlManager inputController;

    bool isUpPressed = false;
    bool isDownPressed = false;
    #endregion

    #region START
    private void Start()
    {
        PlayerPrefs.SetInt("Muted", 0);

        if (PlayerPrefs.GetInt("LevelSaved") == 0)
        {
            continueFullAlpha.SetActive(false);
            continueEmptyAlpha.SetActive(true);
        }
        else
        {
            continueFullAlpha.SetActive(true);
            continueEmptyAlpha.SetActive(false);
        }

        
        inputController = GameObject.Find("ControlPrefab").GetComponent<InControlManager>();

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
        switch (inputController.controller)
        {
            case InControlManager.ControllerType.PS4:
                verticalMove = inputDevice.LeftStickY;
                horizontalMove = inputDevice.LeftStickX;
                break;
            case InControlManager.ControllerType.XBOX:
                verticalMove = inputDevice.LeftStickY;
                horizontalMove = inputDevice.LeftStickX;
                break;
            default:
                break;
        }

        //Checking navigation
        KeyboardNavigation();
        GamepadNavigation();

        //Checking Input
        if (inputDevice.Action1.WasPressed || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
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
                        currentButton = ButtonType.LEFT_RESOLUTION;
                        currentMenu = MenuType.SETTINGS;

                        settingButton.SetActive(false);
                        continueButton.SetActive(true);
                        mainMenu.SetActive(false);

                        settingsMenu.SetActive(true);
                        leftResolution.SetActive(true);

                        break;
                    }
                case ButtonType.CREDITS:
                    {
                        currentButton = ButtonType.BACK_CREDITS;
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
                        leftResolution.SetActive(true);

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
                        if (PlayerPrefs.GetInt("Muted") == 1)
                        {
                            PlayerPrefs.SetInt("Muted", 0);
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
                            PlayerPrefs.SetInt("Muted", 1);
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
                case ButtonType.BACK_CREDITS:
                    {

                        currentButton = ButtonType.CONTINUE;
                        currentMenu = MenuType.MAIN_MENU;
                        creditsMenu.SetActive(false);
                        settingsMenu.SetActive(false);
                        mainMenu.SetActive(true);
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
        ///ARRIBA
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!isUpPressed)
            {
                isUpPressed = true;
                MoveUp();
                soundNavigation.Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            isUpPressed = false;
        }
        ///ABAJO
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!isDownPressed)
            {
                isDownPressed = true;
                MoveDown();
                soundNavigation.Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            isDownPressed = false;
        }

        ///IZQUIERDA
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!leftPressed)
            {
                leftPressed = true;
                MoveLeft();
            }
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftPressed = false;
        }
        ///DERECHA
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!rightPressed)
            {
                rightPressed = true;
                MoverRight();
            }
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightPressed = false;
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
                MoveUp();
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
                MoveDown();

                soundNavigation.Play();
            }
        }
        else
        {
            downPressed = false;
        }

        ///MIRAMOS SI APRETA IZQUIERDA
        if (horizontalMove <= -0.95f)
        {
            if (!leftPressed)
            {
                leftPressed = true;
                //Camiamos el objecto seleccionado
                MoveLeft();
            }
        }
        else
        {
            leftPressed = false;
        }
        ///MIRAMOS SI APRETA DERECHA
        if (horizontalMove >= 0.95f)
        {
            if (!rightPressed)
            {
                rightPressed = true;
                //Camiamos el objecto seleccionado
                MoverRight();
            }
        }
        else
        {
            rightPressed = false;
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

    #region MOVE UP
    void MoveUp()
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
                    currentButton = ButtonType.LOW;
                    lowGraphics.SetActive(true);
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
            case ButtonType.LEFT_RESOLUTION:
                {
                    leftResolution.SetActive(false);
                    currentButton = ButtonType.BACK;
                    backButton.SetActive(true);
                    break;
                }
            case ButtonType.MIDLE_RESOLUTION:
                {
                    midleResolution.SetActive(false);
                    currentButton = ButtonType.BACK;
                    backButton.SetActive(true);
                    break;
                }
            case ButtonType.RIGHT_RESOLUTION:
                {
                    rightResolution.SetActive(false);
                    currentButton = ButtonType.BACK;
                    backButton.SetActive(true);
                    break;
                }
            case ButtonType.LOW:
                {
                    lowGraphics.SetActive(false);
                    currentButton = ButtonType.LEFT_RESOLUTION;
                    leftResolution.SetActive(true);
                    break;
                }
            case ButtonType.MEDIUM:
                {
                    mediumGraphics.SetActive(false);
                    currentButton = ButtonType.MIDLE_RESOLUTION;
                    midleResolution.SetActive(true);
                    break;
                }
            case ButtonType.HIGH:
                {
                    highGraphics.SetActive(false);
                    currentButton = ButtonType.RIGHT_RESOLUTION;
                    rightResolution.SetActive(true);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    #endregion

    #region MOVE DOWN
    void MoveDown()
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
                    currentButton = ButtonType.LEFT_RESOLUTION;
                    leftResolution.SetActive(true);
                    break;
                }
            case ButtonType.LEFT_RESOLUTION:
                {
                    leftResolution.SetActive(false);
                    currentButton = ButtonType.LOW;
                    lowGraphics.SetActive(true);
                    break;
                }
            case ButtonType.MIDLE_RESOLUTION:
                {
                    midleResolution.SetActive(false);
                    currentButton = ButtonType.MEDIUM;
                    mediumGraphics.SetActive(true);
                    break;
                }
            case ButtonType.RIGHT_RESOLUTION:
                {
                    rightResolution.SetActive(false);
                    currentButton = ButtonType.HIGH;
                    highGraphics.SetActive(true);
                    break;
                }
            case ButtonType.LOW:
                {
                    lowGraphics.SetActive(false);
                    currentButton = ButtonType.FULLSCREEN_CHECK;
                    fullscreenButton.color = Color.yellow;
                    break;
                }
            case ButtonType.MEDIUM:
                {
                    mediumGraphics.SetActive(false);
                    currentButton = ButtonType.FULLSCREEN_CHECK;
                    fullscreenButton.color = Color.yellow;
                    break;
                }
            case ButtonType.HIGH:
                {
                    highGraphics.SetActive(false);
                    currentButton = ButtonType.FULLSCREEN_CHECK;
                    fullscreenButton.color = Color.yellow;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    #endregion

    #region MOVE LEFT
    void MoveLeft()
    {
        switch (currentButton)
        {
            case ButtonType.LEFT_RESOLUTION:
                {
                    leftResolution.SetActive(false);
                    currentButton = ButtonType.RIGHT_RESOLUTION;
                    rightResolution.SetActive(true);
                    soundNavigation.Play();
                    break;
                }
            case ButtonType.MIDLE_RESOLUTION:
                {
                    midleResolution.SetActive(false);
                    currentButton = ButtonType.LEFT_RESOLUTION;
                    leftResolution.SetActive(true);
                    soundNavigation.Play();
                    break;
                }
            case ButtonType.RIGHT_RESOLUTION:
                {
                    rightResolution.SetActive(false);
                    currentButton = ButtonType.MIDLE_RESOLUTION;
                    midleResolution.SetActive(true);
                    soundNavigation.Play();
                    break;
                }
            case ButtonType.LOW:
                {
                    lowGraphics.SetActive(false);
                    currentButton = ButtonType.HIGH;
                    highGraphics.SetActive(true);
                    soundNavigation.Play();
                    break;
                }
            case ButtonType.MEDIUM:
                {
                    mediumGraphics.SetActive(false);
                    currentButton = ButtonType.LOW;
                    lowGraphics.SetActive(true);
                    soundNavigation.Play();
                    break;
                }
            case ButtonType.HIGH:
                {
                    highGraphics.SetActive(false);
                    currentButton = ButtonType.MEDIUM;
                    mediumGraphics.SetActive(true);
                    soundNavigation.Play();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    #endregion

    #region MOVE RIGHT
    void MoverRight()
    {
        switch (currentButton)
        {
            case ButtonType.LEFT_RESOLUTION:
                {
                    leftResolution.SetActive(false);
                    currentButton = ButtonType.MIDLE_RESOLUTION;
                    midleResolution.SetActive(true);
                    soundNavigation.Play();
                    break;
                }
            case ButtonType.MIDLE_RESOLUTION:
                {
                    midleResolution.SetActive(false);
                    currentButton = ButtonType.RIGHT_RESOLUTION;
                    rightResolution.SetActive(true);
                    soundNavigation.Play();
                    break;
                }
            case ButtonType.RIGHT_RESOLUTION:
                {
                    rightResolution.SetActive(false);
                    currentButton = ButtonType.LEFT_RESOLUTION;
                    leftResolution.SetActive(true);
                    soundNavigation.Play();
                    break;
                }
            case ButtonType.LOW:
                {
                    lowGraphics.SetActive(false);
                    currentButton = ButtonType.MEDIUM;
                    mediumGraphics.SetActive(true);
                    break;
                }
            case ButtonType.MEDIUM:
                {
                    mediumGraphics.SetActive(false);
                    currentButton = ButtonType.HIGH;
                    highGraphics.SetActive(true);
                    break;
                }
            case ButtonType.HIGH:
                {
                    highGraphics.SetActive(false);
                    currentButton = ButtonType.LOW;
                    lowGraphics.SetActive(true);
                    soundNavigation.Play();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    #endregion
}
