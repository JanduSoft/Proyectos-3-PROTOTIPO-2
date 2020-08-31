using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using InControl;
using UnityEngine.Audio;


public class PauseMenuBehavior : MonoBehaviour
{
    #region VARIABLES

    public Animation animationToPlay;
    public AnimationClip[] animationsToPlay = new AnimationClip[2];

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
        BACK,
        LEFT_RESOLUTION,
        MIDLE_RESOLUTION,
        RIGHT_RESOLUTION,
        LOW,
        MEDIUM,
        HIGH
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
    [SerializeField] GameObject fullScreenCheck;
    [SerializeField] Image musicBtn;
    [SerializeField] GameObject musicCheck;
    [SerializeField] Image soundsBtn;
    [SerializeField] Image backBtn;
    [SerializeField] Image soundLevel1;
    [SerializeField] Image soundLevel2;
    [SerializeField] Image soundLevel3;
    [SerializeField] Image soundLevel4;
    [SerializeField] Image soundLevel5;

    [Header("RESOLUTION")]
        [SerializeField] Text leftResolutionText;
        [SerializeField] Text midleResolutionText;
        [SerializeField] Text rightResolutionText;

        [SerializeField] Image leftResolutionBtn;
        [SerializeField] GameObject leftResolutionCheck;
        [SerializeField] Image midleResolutionBtn;
        [SerializeField] GameObject midleResolutionCheck;
        [SerializeField] Image rightResolutionBtn;
        [SerializeField] GameObject rightResolutionCheck;

        [Header("GRAPHICS")]
        [SerializeField] Image lowBtn;
        [SerializeField] GameObject lowCheck;
        [SerializeField] Image mediumBtn;
        [SerializeField] GameObject mediumCheck;
        [SerializeField] Image highBtn;
        [SerializeField] GameObject highCheck;
        int indexLow = 0;
        int indexMedium = 2;
        int indexHigh = 5;

    [Header("SOUNDS")]
    [SerializeField] AudioSource openSound;
    [SerializeField] AudioSource closeSound;
    [SerializeField] AudioSource moveSound;

    Image currentButton;

    //para la nacegacion
    [HideInInspector]public bool isPaused = false;
    MenuType currentMenu = MenuType.PAUSE;
    ButtonType currentButtonSelected = ButtonType.RESUME;
    float horizontalMove;
    float verticalMove;
    bool upPressed = false;
    bool rightPressed = false;
    bool downPressed = false;
    bool leftPressed = false;

    bool checkedFullScreen = true;
    bool isMuted = false;

    bool isUpPressed = false;
    bool isDownPressed = false;


    Vector3 initaialPausePosition;
    Vector3 initaialSettingsPosition;

    ///Inpud device
    //InputDevice inputDevice;

    //para guardar el nombre de la escena
    string currentScene;

    //player
    PlayerMovement player;
    bool canPressButtons = true;

    //Secret Menu
    SecretScreen secretMenu;

    //InControlManager inputController;

    Resolution[] resolutions;

    int soundLevel = 3;

    public AudioMixer audioMixer;

    float[] volumeLevels = { -80.0f, -64.0f, -48.0f, -32.0f, -16.0f, 0.0f };
    #endregion

    #region START
    private void Start()
    {

        pauseMenu.SetActive(false);
        settingMenu.SetActive(false);

        soundLevel = PlayerPrefs.GetInt("Volume");
        UpdateVolumelevel();

        Cursor.visible = false;

        resolutions = Screen.resolutions;
        

        leftResolutionText.text = resolutions[0].width + " x " + resolutions[0].height;
        midleResolutionText.text = resolutions[resolutions.Length / 2].width + " x " + resolutions[resolutions.Length / 2].height;
        rightResolutionText.text = resolutions[resolutions.Length - 1].width + " x " + resolutions[resolutions.Length-1].height;


        #region CHECKING IS MUTED
        ///////CHECK IF IS MUTED
        if (PlayerPrefs.GetInt("Muted") == 0)
        {
            isMuted = false;
            UnMute();
        }
        else
        {
            isMuted = true;
            Mute();
        }
        #endregion

        #region CHECKING QUALITY SETTINGS
        ///////CHECK QUALITY SETTINGS
        //LOW
        if (QualitySettings.GetQualityLevel() == 0 || QualitySettings.GetQualityLevel() == 1)
        {
            lowCheck.SetActive(true);

            mediumCheck.SetActive(false);
            highCheck.SetActive(false);
        }
            //MEDIUM
        else if (QualitySettings.GetQualityLevel() == 2 || QualitySettings.GetQualityLevel() == 3)
        {
            mediumCheck.SetActive(true);

            lowCheck.SetActive(false);
            highCheck.SetActive(false);
        }
            //HIGH
        else if (QualitySettings.GetQualityLevel() == 4 || QualitySettings.GetQualityLevel() == 5)
        {
            highCheck.SetActive(true);

            lowCheck.SetActive(false);
            mediumCheck.SetActive(false);
        }
        #endregion

//        inputController = GameObject.Find("ControlPrefab").GetComponent<InControlManager>();

        secretMenu = GameObject.Find("CanvasSecretScreen").GetComponent<SecretScreen>();
        secretMenu.isOpened = false;

        ///comprobar si está en full screen
        if (Screen.fullScreen)
        {
            checkedFullScreen = true;
            fullScreenCheck.SetActive(true);
        }
        else if (!Screen.fullScreen)
        {
            checkedFullScreen = false;
            fullScreenCheck.SetActive(false);
        }

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
        //inputDevice = InputManager.ActiveDevice;

        //SABE GAME
        SaveGame();
    }
    #endregion


    public void ShowLoad(string sceneToLoad)
    {
        StartCoroutine(ShowLoadingScreen(sceneToLoad));
    }

    IEnumerator ShowLoadingScreen(string sceneToLoad)
    {

        animationToPlay.clip = animationsToPlay[0];
        animationToPlay.Play();
        yield return new WaitForSeconds(0.5f);
        GetComponent<Canvas>().enabled = false;
        animationToPlay.clip = animationsToPlay[1];
        animationToPlay.Play();
        yield return new WaitForSeconds(2f);    //to show loading screen
        StartCoroutine(LoadDaScene(sceneToLoad));
    }

    IEnumerator LoadDaScene(string sceneToLoad)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (operation.progress <= 0.8f)
        {
            //mantains the game stuck in the loop until the level is done loading
            yield return null;
        }

        operation.allowSceneActivation = true;

        yield return null;
    }

    #region UPDATE
    void Update()
    {
        if (!secretMenu.isOpened)
        {
            //Comprovamos si se abre el menu o se cierra con el boton options
            //if (Input.GetKeyDown(KeyCode.Escape) || inputDevice.MenuWasPressed)
            if (GeneralInputScript.Input_GetKeyDown("Pause"))
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
                horizontalMove = GeneralInputScript.Input_GetAxis("MoveHorizontal");
                verticalMove = GeneralInputScript.Input_GetAxis("MoveVertical");
                
                ///Guardamos el axis solo si hay un mando
                //switch (inputController.controller)
                //{                    
                //    case InControlManager.ControllerType.PS4:
                //        horizontalMove = inputDevice.LeftStickX;
                //        verticalMove = inputDevice.LeftStickY;
                //        break;
                //    case InControlManager.ControllerType.XBOX:
                //        horizontalMove = inputDevice.LeftStickX;
                //        verticalMove = inputDevice.LeftStickY;
                //        break;
                //    default:
                //        break;
                //}
                


                if (canPressButtons)
                {
                    //comprobamos si en el mando se ha dado la redonda o a la B
                    //if (inputDevice.Action2.WasPressed)
                    if (GeneralInputScript.Input_GetKeyDown("Throw"))
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
                    //if (inputDevice.Action1.WasPressed || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                    if (GeneralInputScript.Input_GetKeyDown("Jump"))
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
                            case ButtonType.LEFT_RESOLUTION:
                                {
                                    Screen.SetResolution(resolutions[0].width, resolutions[0].height, Screen.fullScreen);
                                    closeSound.Play();
                                    leftResolutionCheck.SetActive(true);

                                    midleResolutionCheck.SetActive(false);
                                    rightResolutionCheck.SetActive(false);
                                    break;
                                }
                            case ButtonType.MIDLE_RESOLUTION:
                                {
                                    Screen.SetResolution(   resolutions[resolutions.Length / 2].width,
                                                            resolutions[resolutions.Length / 2].height, 
                                                            Screen.fullScreen);
                                    closeSound.Play();
                                    midleResolutionCheck.SetActive(true);

                                    leftResolutionCheck.SetActive(false);
                                    rightResolutionCheck.SetActive(false);
                                    break;
                                }
                            case ButtonType.RIGHT_RESOLUTION:
                                {
                                    Screen.SetResolution(resolutions[resolutions.Length - 1].width,
                                                            resolutions[resolutions.Length - 1].height,
                                                            Screen.fullScreen);
                                    closeSound.Play();
                                    rightResolutionCheck.SetActive(true);

                                    midleResolutionCheck.SetActive(false);
                                    leftResolutionCheck.SetActive(false);
                                    break;
                                }
                            case ButtonType.LOW:
                                {
                                    QualitySettings.SetQualityLevel(indexLow);
                                    closeSound.Play();
                                    lowCheck.SetActive(true);

                                    mediumCheck.SetActive(false);
                                    highCheck.SetActive(false);
                                    break;
                                }
                            case ButtonType.MEDIUM:
                                {
                                    QualitySettings.SetQualityLevel(indexMedium);
                                    closeSound.Play();
                                    mediumCheck.SetActive(true);

                                    lowCheck.SetActive(false);
                                    highCheck.SetActive(false);
                                    break;
                                }
                            case ButtonType.HIGH:
                                {
                                    QualitySettings.SetQualityLevel(indexHigh);
                                    closeSound.Play();
                                    highCheck.SetActive(true);

                                    lowCheck.SetActive(false);
                                    mediumCheck.SetActive(false);
                                    break;
                                }
                            case ButtonType.FULLSCREEN:
                                {
                                    if (checkedFullScreen)
                                    {
                                        Screen.fullScreen = false;
                                        checkedFullScreen = false;
                                        fullScreenCheck.SetActive(false);
                                    }
                                    else
                                    {
                                        Screen.fullScreen = true;
                                        checkedFullScreen = true;
                                        fullScreenCheck.SetActive(true);
                                    }
                                    break;
                                }
                            case ButtonType.MUSIC:
                                {
                                    if (isMuted)
                                    {
                                       //UnMute();
                                    }
                                    else
                                    {
                                        //Mute();
                                    }
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
        currentButtonSelected = ButtonType.LEFT_RESOLUTION;
        currentButton = leftResolutionBtn;
        currentButton.color = Color.yellow;
    }
    #endregion

    #region PAUSE MENU
    void PauseMenu()
    {

        pauseMenu.SetActive(true);
        settingMenu.SetActive(true);

        openSound.Play();
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
        Invoke("PauseTime", timeTransition + 0.1f);

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
        pauseMenu.SetActive(false);
        settingMenu.SetActive(false);
        closeSound.Play();
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
        ShowLoad(currentScene);
        //SceneManager.LoadScene(currentScene);
    }
    #endregion

    #region QUIT GAME
    void QuitGame()
    {
        Time.timeScale = 1;
        ShowLoad("MainMenu");
        //SceneManager.LoadScene("MainMenu");
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
        moveSound.Play();
        canPressButtons = false;

        Time.timeScale = 1f;

        ///Paramos el tiempo
        Invoke("PauseTime", timeTransition + 0.1f);

        //Reestablecemos la seleccion en el menu de settings
        RestartSelectionOnSettingsMenu();

        //Animamaos la  transicion
        pauseMenu.transform.DOMoveX(initaialPausePosition.x, timeTransition);
        settingMenu.transform.DOMoveX(this.transform.position.x, timeTransition);
    }
    #endregion

    #region MUTE
    void Mute()
    {
        musicCheck.SetActive(true);
        isMuted = true;

        AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        for (int index = 0; index < sources.Length; ++index)
        {
            sources[index].mute = true;
        }
    }
    #endregion

    #region UNMUTE
    void UnMute()
    {
        musicCheck.SetActive(false);

        isMuted = false;

        AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        for (int index = 0; index < sources.Length; ++index)
        {
            sources[index].mute = false;
        }
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
            case ButtonType.LEFT_RESOLUTION:
                {
                    currentButton.color = Color.white;
                    currentButtonSelected = ButtonType.LEFT_RESOLUTION;
                    currentButton = leftResolutionBtn;
                    currentButton.color = Color.yellow;
                    break;
                }
            case ButtonType.MIDLE_RESOLUTION:
                {
                    currentButton.color = Color.white;
                    currentButtonSelected = ButtonType.MIDLE_RESOLUTION;
                    currentButton = midleResolutionBtn;
                    currentButton.color = Color.yellow;
                    break;
                }
            case ButtonType.RIGHT_RESOLUTION:
                {
                    currentButton.color = Color.white;
                    currentButtonSelected = ButtonType.RIGHT_RESOLUTION;
                    currentButton = rightResolutionBtn;
                    currentButton.color = Color.yellow;
                    break;
                }
            case ButtonType.LOW:
                {
                    currentButton.color = Color.white;
                    currentButtonSelected = ButtonType.LOW;
                    currentButton = lowBtn;
                    currentButton.color = Color.yellow;
                    break;
                }
            case ButtonType.MEDIUM:
                {
                    currentButton.color = Color.white;
                    currentButtonSelected = ButtonType.MEDIUM;
                    currentButton = mediumBtn;
                    currentButton.color = Color.yellow;
                    break;
                }
            case ButtonType.HIGH:
                {
                    currentButton.color = Color.white;
                    currentButtonSelected = ButtonType.HIGH;
                    currentButton = highBtn;
                    currentButton.color = Color.yellow;
                    break;
                }
            case ButtonType.FULLSCREEN:
                {
                    if (currentButtonSelected != ButtonType.MUSIC)
                    {
                        currentButton.color = Color.white;
                    }                    
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
                    //currentButton.color = Color.yellow;
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
                    if (currentButtonSelected != ButtonType.MUSIC)
                    {
                        currentButton.color = Color.white;
                    }
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
        moveSound.Play();

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
                case ButtonType.LEFT_RESOLUTION:
                    {
                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.BACK);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.LOW);
                        }
                        else if (newMovement == Movement.LEFT)
                        {
                            SelectButton(ButtonType.RIGHT_RESOLUTION);
                        }
                        else if (newMovement == Movement.RIGHT)
                        {
                            SelectButton(ButtonType.MIDLE_RESOLUTION);
                        }

                        break;
                    }
                case ButtonType.MIDLE_RESOLUTION:
                    {
                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.BACK);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.MEDIUM);
                        }
                        else if (newMovement == Movement.LEFT)
                        {
                            SelectButton(ButtonType.LEFT_RESOLUTION);
                        }
                        else if (newMovement == Movement.RIGHT)
                        {
                            SelectButton(ButtonType.RIGHT_RESOLUTION);
                        }

                        break;
                    }
                case ButtonType.RIGHT_RESOLUTION:
                    {
                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.BACK);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.HIGH);
                        }
                        else if (newMovement == Movement.LEFT)
                        {
                            SelectButton(ButtonType.MIDLE_RESOLUTION);
                        }
                        else if (newMovement == Movement.RIGHT)
                        {
                            SelectButton(ButtonType.LEFT_RESOLUTION);
                        }

                        break;
                    }
                case ButtonType.LOW:
                    {
                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.LEFT_RESOLUTION);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.FULLSCREEN);
                        }
                        else if (newMovement == Movement.LEFT)
                        {
                            SelectButton(ButtonType.HIGH);
                        }
                        else if (newMovement == Movement.RIGHT)
                        {
                            SelectButton(ButtonType.MEDIUM);
                        }

                        break;
                    }
                case ButtonType.MEDIUM:
                    {
                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.MIDLE_RESOLUTION);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.FULLSCREEN);
                        }
                        else if (newMovement == Movement.LEFT)
                        {
                            SelectButton(ButtonType.LOW);
                        }
                        else if (newMovement == Movement.RIGHT)
                        {
                            SelectButton(ButtonType.HIGH);
                        }

                        break;
                    }
                case ButtonType.HIGH:
                    {
                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.RIGHT_RESOLUTION);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.FULLSCREEN);
                        }
                        else if (newMovement == Movement.LEFT)
                        {
                            SelectButton(ButtonType.MEDIUM);
                        }
                        else if (newMovement == Movement.RIGHT)
                        {
                            SelectButton(ButtonType.LOW);
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
                        else if (newMovement == Movement.LEFT)
                        {
                            SelectButton(ButtonType.MEDIUM);
                        }
                        else if (newMovement == Movement.RIGHT)
                        {
                            SelectButton(ButtonType.LOW);
                        }

                        break;
                    }
                case ButtonType.FULLSCREEN:
                    {
                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.LOW);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.MUSIC);
                        }

                        break;
                    }
                case ButtonType.MUSIC:
                    {
                        Debug.Log("VOLUMEN ACTUAL:  " + soundLevel);

                        if (newMovement == Movement.UP)
                        {
                            SelectButton(ButtonType.FULLSCREEN);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.BACK);
                        }
                        else if (newMovement == Movement.LEFT)
                        {
                            if (soundLevel > 0)
                            {
                                soundLevel--;
                                UpdateVolumelevel();
                                PlayerPrefs.SetInt("Volume", soundLevel);
                            }
                            else
                            {
                                Debug.Log("EL VOLUMEN ESTA AL MINIMO");
                            }
                        }
                        else if (newMovement == Movement.RIGHT)
                        {
                            if (soundLevel < 5)
                            {
                                soundLevel++;
                                UpdateVolumelevel();
                                PlayerPrefs.SetInt("Volume", soundLevel);
                            }
                            else
                            {
                                Debug.Log("EL VOLUMEN ESTA AL MAXIMO");
                            }
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
                            SelectButton(ButtonType.MUSIC);
                        }
                        else if (newMovement == Movement.DOWN)
                        {
                            SelectButton(ButtonType.LEFT_RESOLUTION);
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
        ///MIRAMOS SI APRETA ARRIBA O ABAJO
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

        ///MIRAMOS SI APRETA IZQUIERDA O DERECHA
        if (horizontalMove >= 0.95f)
        {
            if (!rightPressed)
            {
                rightPressed = true;
                //Camiamos el objecto seleccionado
                MoveSelection(Movement.RIGHT);
            }
        }
        else
        {
            rightPressed = false;
        }
        if (horizontalMove <= -0.95f)
        {
            if (!leftPressed)
            {
                leftPressed = true;
                //Camiamos el objecto seleccionado
                MoveSelection(Movement.LEFT);
            }
        }
        else
        {
            leftPressed = false;
        }

    }
    #endregion

    #region KEYBOARD NAVIGATIO
    void KeyboardNavigation()
    {
        ////ARRIBA
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!isUpPressed)
            {
                isUpPressed = true;
                MoveSelection(Movement.UP);
            }
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            isUpPressed = false;
        }
        ////ABAJO
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!isDownPressed)
            {
                isDownPressed = true;
                MoveSelection(Movement.DOWN);
            }            
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            isDownPressed = false;
        }
        ////IZQUIERDA
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!leftPressed)
            {
                leftPressed = true;
                MoveSelection(Movement.LEFT);
            }
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftPressed = false;
        }
        ////DERECHA
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!rightPressed)
            {
                rightPressed = true;
                MoveSelection(Movement.RIGHT);
            }
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightPressed = false;
        }
    }
    #endregion

    #region SAVE GAME
    void SaveGame()
    {
        if (currentScene == "TestTutorialPart1")
        {
            PlayerPrefs.SetInt("LevelSaved", 2);
        }
        else if (currentScene == "TestTutorialPart2")
        {
            PlayerPrefs.SetInt("LevelSaved", 3);
        }
        else if (currentScene == "NewSection1Part1")
        {
            PlayerPrefs.SetInt("LevelSaved", 4);
        }
        else if (currentScene == "Section2Part1")
        {
            PlayerPrefs.SetInt("LevelSaved", 5);
        }
        else if (currentScene == "Section1Part2")
        {
            PlayerPrefs.SetInt("LevelSaved", 6);
        }
        else if (currentScene == "Section1Part3")
        {
            PlayerPrefs.SetInt("LevelSaved", 7);
        }
    }
    #endregion

    #region UPDATE VOLUME
    void UpdateVolumelevel()
    {
        switch (soundLevel)
        {
            case 0:
                {
                    soundLevel1.color = Color.white;
                    soundLevel2.color = Color.white;
                    soundLevel3.color = Color.white;
                    soundLevel4.color = Color.white;
                    soundLevel5.color = Color.white;
                    break;
                }
            case 1:
                {
                    soundLevel1.color = Color.yellow;
                    soundLevel2.color = Color.white;
                    soundLevel3.color = Color.white;
                    soundLevel4.color = Color.white;
                    soundLevel5.color = Color.white;
                    break;
                }
            case 2:
                {
                    soundLevel1.color = Color.yellow;
                    soundLevel2.color = Color.yellow;
                    soundLevel3.color = Color.white;
                    soundLevel4.color = Color.white;
                    soundLevel5.color = Color.white;
                    break;
                }
            case 3:
                {
                    soundLevel1.color = Color.yellow;
                    soundLevel2.color = Color.yellow;
                    soundLevel3.color = Color.yellow;
                    soundLevel4.color = Color.white;
                    soundLevel5.color = Color.white;
                    break;
                }
            case 4:
                {
                    soundLevel1.color = Color.yellow;
                    soundLevel2.color = Color.yellow;
                    soundLevel3.color = Color.yellow;
                    soundLevel4.color = Color.yellow;
                    soundLevel5.color = Color.white;
                    break;
                }
            case 5:
                {
                    soundLevel1.color = Color.yellow;
                    soundLevel2.color = Color.yellow;
                    soundLevel3.color = Color.yellow;
                    soundLevel4.color = Color.yellow;
                    soundLevel5.color = Color.yellow;
                    break;
                }
            default:
                break;
        }

        audioMixer.SetFloat("Volume", volumeLevels[soundLevel]);
    }
    #endregion

}
