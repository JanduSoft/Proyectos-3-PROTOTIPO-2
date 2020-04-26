using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using InControl;


public class PauseMenuBehavior : MonoBehaviour
{
    #region VARIABLES
    public enum Movement
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT
    };

    public enum MenuType
    {
        NONE,
        PAUSE,
        SETTINGS
    };

    public enum ButtonType
    {
        NONE,
        RESUME,
        RESTART,
        SETTINGS,
        QUIT,
        RESOLUTION,
        GRAPHICS,
        FULLSCREEN,
        MUSIC,
        SOUNDS,
        BACK
    };

    [Header("BACKGROUND")]
    [SerializeField] GameObject background;

    [Header("MENUS")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingMenu;
    [SerializeField] float timeTransition = 0.75f;

    [Header("PAUSE BUTTONS")]
    [SerializeField] Image resumeBtn;
    [SerializeField] Image restartBtn;
    [SerializeField] Image settingsBtn;
    [SerializeField] Image quitBtn;
    [Header("SETTINGS BUTTONS")]
    [SerializeField] Image resolutionBtn;
    [SerializeField] Image graphicsBtn;
    [SerializeField] Image fullScreenBtn;
    [SerializeField] Image musicBtn;
    [SerializeField] Image soundsBtn;
    [SerializeField] Image backBtn;
    Image currentButton;

    //para la nacegacion
    bool isPaused = false;
    MenuType currentMenu = MenuType.PAUSE;
    ButtonType currentButtonSelected = ButtonType.RESUME;
    float horizontalMove;
    float verticalMove;
    bool upPressed = false;
    bool rightPressed = false;
    bool downPressed = false;
    bool leftPressed = false;


    Vector3 initaialPausePosition;
    Vector3 initaialSettingsPosition;

    ///Inpud device
    InputDevice inputDevice;

    //para guardar el nombre de la escena
    string currentScene;

    //player
    PlayerMovement player;
    bool canPressButtons = true;
    #endregion

    #region START
    private void Start()
    {
        player = GameObject.Find("Character").GetComponent<PlayerMovement>();
        //Nos guardamos el nombre de la escena actual
        currentScene = SceneManager.GetActiveScene().name;

        //reestableciondo la seleccion del menu
        currentMenu = MenuType.PAUSE;
        currentButtonSelected = ButtonType.RESUME;
        currentButton = resumeBtn;
        currentButton.color = Color.yellow;

        ///Guardando las posiciones iniciales de los menus
        initaialPausePosition = pauseMenu.transform.position;
        initaialSettingsPosition = settingMenu.transform.position;

        //Seteamso el Inpud Device
        inputDevice = InputManager.ActiveDevice;
    }
    #endregion

    #region UPDATE
    void Update()
    {
        //Comprovamos si se abre el menu o se cierra con el boton options
        if (Input.GetKeyDown(KeyCode.Escape) || inputDevice.MenuWasPressed)
        {
            if (isPaused)
            {
                if (canPressButtons)
                {
                    ResumeGame();
                }                
            }
            else
            {
                PauseMenu();
            }
        }

        //Comprovamos los eventos si esta el menu abierto
        if (isPaused)
        {
            horizontalMove = inputDevice.LeftStickX;
            verticalMove = inputDevice.LeftStickY;


            if (canPressButtons)
            {
                //comprobamos si en el mando se ha dado la redonda o a la B
                if (inputDevice.Action2.WasPressed)
                {
                    if (currentMenu == MenuType.PAUSE)
                    {
                        ResumeGame();
                    }
                    else if (currentMenu == MenuType.SETTINGS)
                    {
                        TransitionToPauseMenu();
                    }
                }
                //Comprobamos la seleccion de los botones
                if (inputDevice.Action1.WasPressed || Input.GetKeyDown(KeyCode.Space))
                {
                        switch (currentButtonSelected)
                        {
                            case ButtonType.RESUME:
                                {
                                    ResumeGame();
                                    break;
                                }
                            case ButtonType.RESTART:
                                {
                                    RestartGame();
                                    break;
                                }
                            case ButtonType.SETTINGS:
                                {
                                    TransitionToSettingsMenu();
                                    break;
                                }
                            case ButtonType.QUIT:
                                {
                                    QuitGame();
                                    break;
                                }
                            case ButtonType.BACK:
                                {
                                    TransitionToPauseMenu();
                                    break;
                                }
                            default:
                                break;
                        }                        
                    
                }


                //////////////////////////////////////////////////////////////////////////// NAVEGACION CON MANDO
                //comprobamos la navegación por el menu
                GamepadNavigation();
                KeyboardNavigation();
            }
        }
    }
    #endregion

    #region RESTART SELECTION ON PAUSE MENU
    void RestartSelectionOnPauseMenu()
    {

        currentButton.color = Color.white;
        currentMenu = MenuType.PAUSE;
        currentButtonSelected = ButtonType.RESUME;
        currentButton = resumeBtn;
        currentButton.color = Color.yellow;
    }
    #endregion

    #region RESTART SELECTION ON SETTINGS MENU
    void RestartSelectionOnSettingsMenu()
    {

        currentButton.color = Color.white;
        currentMenu = MenuType.SETTINGS;
        currentButtonSelected = ButtonType.RESOLUTION;
        currentButton = resolutionBtn;
        currentButton.color = Color.yellow;
    }
    #endregion

    #region PAUSE MENU
    void PauseMenu()
    {
        canPressButtons = false;
        //player.canMove = false;
        player.StopMovement(true);
        //Restablecemos la seleccion
        //reestableciondo la seleccion del menu
        RestartSelectionOnPauseMenu();

        isPaused = true;

        ///Activamos el fondo
        background.SetActive(true);

        ///Paramos el tiempo
        Invoke("PauseTime", timeTransition + 0.5f);

        ///Animamos La aparicion del menu
        pauseMenu.transform.DOMoveX(this.transform.position.x, timeTransition);

        //ADD SOUND


    }
    #endregion

    #region PAUSE TIME
    void PauseTime()
    {
        ///Paramos el tiempo
        Time.timeScale = 0;
        canPressButtons = true;
    }
    #endregion

    #region RESUME GAME
    void ResumeGame()
    {
        //player.canMove = true;
        ///Reestablecemos el tiempo
        Time.timeScale = 1;

        ///Reestablecemos las posiciones de losmenus
        pauseMenu.transform.position = initaialPausePosition;
        settingMenu.transform.position = initaialSettingsPosition;

        //Desactivamos el fondo
        background.SetActive(false);

        isPaused = false;


        player.StopMovement(false);

        //ADD SOUND

    }
    #endregion

    #region RESTART GAME
    void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene);
    }
    #endregion

    #region QUIT GAME
    void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
    #endregion

    #region TRANSITION TO PAUSE MENU
    void TransitionToPauseMenu()
    {
        Time.timeScale = 1f;

        PauseMenu();

        settingMenu.transform.DOMoveX(initaialSettingsPosition.x, timeTransition);
    }
    #endregion

    #region TRANSITION TO SETTINGS MENU
    void TransitionToSettingsMenu()
    {
        canPressButtons = false;

        Time.timeScale = 1f;

        ///Paramos el tiempo
        Invoke("PauseTime", timeTransition + 0.5f);

        //Reestablecemos la seleccion en el menu de settings
        RestartSelectionOnSettingsMenu();

        //Animamaos la  transicion
        pauseMenu.transform.DOMoveX(initaialPausePosition.x, timeTransition);
        settingMenu.transform.DOMoveX(this.transform.position.x, timeTransition);
    }
    #endregion

    #region SELCET BUTTON
    void SelectButton(ButtonType newButton)
    {
        switch (newButton)
        {
            case ButtonType.RESUME:
                {
                    currentButton.color = Color.white;
                    currentButtonSelected = ButtonType.RESUME;
                    currentButton = resumeBtn;
                    currentButton.color = Color.yellow;
                    break;
                }
            case ButtonType.RESTART:
                {
                    currentButton.color = Color.white;
                    currentButtonSelected = ButtonType.RESTART;
                    currentButton = restartBtn;
                    currentButton.color = Color.yellow;
                    break;
                }
            case ButtonType.SETTINGS:
                {
                    currentButton.color = Color.white;
                    currentButtonSelected = ButtonType.SETTINGS;
                    currentButton = settingsBtn;
                    currentButton.color = Color.yellow;
                    break;
                }
            case ButtonType.QUIT:
                {
                    currentButton.color = Color.white;
                    currentButtonSelected = ButtonType.QUIT;
                    currentButton = quitBtn;
                    currentButton.color = Color.yellow;
                    break;
                }
            case ButtonType.RESOLUTION:
                {
                    currentButton.color = Color.white;
                    currentButtonSelected = ButtonType.RESOLUTION;
                    currentButton = resolutionBtn;
                    currentButton.color = Color.yellow;
                    break;
                }
            case ButtonType.GRAPHICS:
                {
                    currentButton.color = Color.white;
                    currentButtonSelected = ButtonType.GRAPHICS;
                    currentButton = graphicsBtn;
                    currentButton.color = Color.yellow;
                    break;
                }
            case ButtonType.FULLSCREEN:
                {
                    currentButton.color = Color.white;
                    currentButtonSelected = ButtonType.FULLSCREEN;
                    currentButton = fullScreenBtn;
                    currentButton.color = Color.yellow;
                    break;
                }
            case ButtonType.MUSIC:
                {
                    currentButton.color = Color.white;
                    currentButtonSelected = ButtonType.MUSIC;
                    currentButton = musicBtn;
                    currentButton.color = Color.yellow;
                    break;
                }
            case ButtonType.SOUNDS:
                {
                    currentButton.color = Color.white;
                    currentButtonSelected = ButtonType.SOUNDS;
                    currentButton = soundsBtn;
                    currentButton.color = Color.yellow;
                    break;
                }
            case ButtonType.BACK:
                {
                    currentButton.color = Color.white;
                    currentButtonSelected = ButtonType.BACK;
                    currentButton = backBtn;
                    currentButton.color = Color.yellow;
                    break;
                }
            default:
                break;
        }
    } 
    #endregion

    #region  MOVE SELECTION
    void MoveSelection(Movement newMovement)
    {
        if (currentMenu == MenuType.PAUSE)
        {
            switch (currentButtonSelected)
            {
                case ButtonType.RESUME:
                    {
                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.QUIT);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.RESTART);
                        }

                        break;
                    }
                case ButtonType.RESTART:
                    {
                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.RESUME);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.SETTINGS);
                        }
                        break;
                    }
                case ButtonType.SETTINGS:
                    {
                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.RESTART);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.QUIT);
                        }
                        break;
                    }
                case ButtonType.QUIT:
                    {
                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.SETTINGS);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.RESUME);
                        }
                        break;
                    }
                default:
                    {
                        Debug.Log("ALGO NO FUNCIONA BIEN");
                        break;
                    }
            }
        }
        else if (currentMenu == MenuType.SETTINGS)
        {
            switch (currentButtonSelected)
            {
                case ButtonType.RESOLUTION:
                    {
                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.BACK);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.GRAPHICS);
                        }

                        break;
                    }
                case ButtonType.GRAPHICS:
                    {
                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.RESOLUTION);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.FULLSCREEN);
                        }

                        break;
                    }
                case ButtonType.FULLSCREEN:
                    {
                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.GRAPHICS);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.MUSIC);
                        }

                        break;
                    }
                case ButtonType.MUSIC:
                    {
                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.FULLSCREEN);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.SOUNDS);
                        }

                        break;
                    }
                case ButtonType.SOUNDS:
                    {
                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.MUSIC);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.BACK);
                        }

                        break;
                    }
                case ButtonType.BACK:
                    {
                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.SOUNDS);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.RESOLUTION);
                        }

                        break;
                    }
                default:
                    break;
            }
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
                MoveSelection(Movement.UP);
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
                MoveSelection(Movement.DOWN);
            }
        }
        else
        {
            downPressed = false;
        }
    }
    #endregion

    #region KEYBOARD NAVIGATIO
    void KeyboardNavigation()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveSelection(Movement.UP);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveSelection(Movement.DOWN);
        }
    }
    #endregion

}
