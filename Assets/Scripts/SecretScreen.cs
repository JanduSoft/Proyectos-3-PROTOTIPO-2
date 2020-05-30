using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class SecretScreen : MonoBehaviour
{
    #region VARIABLES
    public struct Item
    {
        public int ID;
        public bool isDiscovered;
        public bool isSelected;
        public string name;
        public string description;
        public Transform position;
        public GameObject mark;
        public GameObject interrogante;
        public GameObject itemImage;
        public GameObject model;
    };
    public Item[] secrets;
    [Header("Sounds")]
    [SerializeField] AudioSource openSound;
    [SerializeField] AudioSource closeSound;
    [SerializeField] AudioSource moveSound;

    ///FOR CONTROLLER
    InputDevice inputDevice;
    private void Awake()
    {
        inputDevice = InputManager.ActiveDevice;
    }
    float horizontalMove;
    float verticalMove;
    bool upPressed = false;
    bool rightPressed = false;
    bool downPressed = false;
    bool leftPressed = false;

    PauseMenuBehavior pauseMenu;

    ///LOS ELEMENTOS DEL MENU
    #region ELEMENTO 1
    [Header("ELEMENTO 1")]
    [SerializeField] bool isDiscovered1;
    [SerializeField] bool isSelected1;
    [SerializeField] string nameObject1;
    [SerializeField] string description1;
    [SerializeField] Transform position1;
    [SerializeField] GameObject mark1;
    [SerializeField] public GameObject interrogante1;
    [SerializeField] public GameObject itemImage1;
    [SerializeField] public GameObject model1;

    #endregion

    #region ELEMENTO 2
    [Header("ELEMENTO 2")]
    [SerializeField] bool isDiscovered2;
    [SerializeField] bool isSelected2;
    [SerializeField] string nameObject2;
    [SerializeField] string description2;
    [SerializeField] Transform position2;
    [SerializeField] GameObject mark2;
    [SerializeField] public GameObject interrogante2;
    [SerializeField] public GameObject itemImage2;
    [SerializeField] public GameObject model2;
    #endregion

    #region ELEMENTO 3
    [Header("ELEMENTO 3")]
    [SerializeField] bool isDiscovered3;
    [SerializeField] bool isSelected3;
    [SerializeField] Transform position3;
    [SerializeField] string nameObject3;
    [SerializeField] string description3;
    [SerializeField] GameObject mark3;
    [SerializeField] public GameObject interrogante3;
    [SerializeField] public GameObject itemImage3;
    [SerializeField] public GameObject model3;
    #endregion

    #region ELEMENTO 4
    [Header("ELEMENTO 4")]
    [SerializeField] bool isDiscovered4;
    [SerializeField] bool isSelected4;
    [SerializeField] string nameObject4;
    [SerializeField] string description4;
    [SerializeField] Transform position4;
    [SerializeField] GameObject mark4;
    [SerializeField] public GameObject interrogante4;
    [SerializeField] public GameObject itemImage4;
    [SerializeField] public GameObject model4;
    #endregion

    #region ELEMENTO 5
    [Header("ELEMENTO 5")]
    [SerializeField] bool isDiscovered5;
    [SerializeField] bool isSelected5;
    [SerializeField] string nameObject5;
    [SerializeField] string description5;
    [SerializeField] Transform position5;
    [SerializeField] GameObject mark5;
    [SerializeField] public GameObject interrogante5;
    [SerializeField] public GameObject itemImage5;
    [SerializeField] public GameObject model5;
    #endregion

    #region ELEMENTO 6
    [Header("ELEMENTO 6")]
    [SerializeField] bool isDiscovered6;
    [SerializeField] bool isSelected6;
    [SerializeField] string nameObject6;
    [SerializeField] string description6;
    [SerializeField] Transform position6;
    [SerializeField] GameObject mark6;
    [SerializeField] public GameObject interrogante6;
    [SerializeField] public GameObject itemImage6;
    [SerializeField] public GameObject model6;
    #endregion

    [Header("NOMBRE Y DESCRIPCION")]
    [SerializeField] Text nameText;
    [SerializeField] Text descriptionText;
    int currentSelected;

    [Header("CONTROL DEL MENU")]
    [SerializeField] GameObject menu;
    [HideInInspector] public bool isOpened = false;

    #endregion

    #region START
    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenuCanvas").GetComponent<PauseMenuBehavior>();

        currentSelected = 1;

        secrets = new Item[6];

        for (int i = 0; i < secrets.Length; i++)
        {
            switch (i)
            {
                ///-------------ELEMENTO 1
                case 0:
                    {
                        secrets[i].ID = i;

                        //Leemos del player Prefs
                        int discover = PlayerPrefs.GetInt("Secret1");
                        if (discover == 0)
                        {
                            secrets[i].isDiscovered = false;
                        }
                        else if (discover == 1)
                        {
                            secrets[i].isDiscovered = true; 
                        }

                        secrets[i].isSelected = isSelected1;
                        secrets[i].name = nameObject1;
                        secrets[i].description = description1;
                        secrets[i].position = position1;
                        secrets[i].mark = mark1;
                        secrets[i].interrogante = interrogante1;
                        secrets[i].itemImage = itemImage1;
                        secrets[i].model = model1;

                        ///comprobar si esta descubierto el objeto
                        if (secrets[i].isDiscovered)
                        {
                            secrets[i].interrogante.SetActive(false);
                            secrets[i].itemImage.SetActive(true);

                            nameText.text = secrets[i].name;
                            descriptionText.text = secrets[i].description;
                        }
                        else
                        {
                            secrets[i].interrogante.SetActive(true);
                            secrets[i].itemImage.SetActive(false);

                            nameText.text = "?????????????";
                            descriptionText.text = "";
                        }

                        secrets[i].mark.SetActive(true);
                        break;
                    }
                ///-------------ELEMENTO 2
                case 1:
                    {
                        secrets[i].ID = i;

                        //Leemos del player Prefs
                        int discover = PlayerPrefs.GetInt("Secret2");
                        if (discover == 0)
                        {
                            secrets[i].isDiscovered = false;
                        }
                        else if (discover == 1)
                        {
                            secrets[i].isDiscovered = true; ;
                        }

                        secrets[i].isSelected = isSelected2;
                        secrets[i].name = nameObject2;
                        secrets[i].description = description2;
                        secrets[i].position = position2;
                        secrets[i].mark = mark2;
                        secrets[i].interrogante = interrogante2;
                        secrets[i].itemImage = itemImage2;
                        secrets[i].model = model2;


                        ////comprobar si esta descubierto el objeto
                        if (secrets[i].isDiscovered)
                        {
                            secrets[i].interrogante.SetActive(false);
                            secrets[i].itemImage.SetActive(true);
                        }
                        else
                        {
                            secrets[i].interrogante.SetActive(true);
                            secrets[i].itemImage.SetActive(false);
                        }

                        break;
                    }
                ///-------------ELEMENTO 3
                case 2:
                    {
                        secrets[i].ID = i;

                        //Leemos del player Prefs
                        int discover = PlayerPrefs.GetInt("Secret3");
                        if (discover == 0)
                        {
                            secrets[i].isDiscovered = false;
                        }
                        else if (discover == 1)
                        {
                            secrets[i].isDiscovered = true; ;
                        }

                        secrets[i].isSelected = isSelected3;
                        secrets[i].name = nameObject3;
                        secrets[i].description = description3;
                        secrets[i].position = position3;
                        secrets[i].mark = mark3;
                        secrets[i].interrogante = interrogante3;
                        secrets[i].itemImage = itemImage3;
                        secrets[i].model = model3;

                        ////comprobar si esta descubierto el objeto
                        if (secrets[i].isDiscovered)
                        {
                            secrets[i].interrogante.SetActive(false);
                            secrets[i].itemImage.SetActive(true);
                        }
                        else
                        {
                            secrets[i].interrogante.SetActive(true);
                            secrets[i].itemImage.SetActive(false);
                        }
                        break;
                    }
                ///-------------ELEMENTO 4
                case 3:
                    {
                        secrets[i].ID = i;

                        //Leemos del player Prefs
                        int discover = PlayerPrefs.GetInt("Secret4");
                        if (discover == 0)
                        {
                            secrets[i].isDiscovered = false;
                        }
                        else if (discover == 1)
                        {
                            secrets[i].isDiscovered = true; ;
                        }

                        secrets[i].isSelected = isSelected4;
                        secrets[i].name = nameObject4;
                        secrets[i].description = description4;
                        secrets[i].position = position4;
                        secrets[i].mark = mark4;
                        secrets[i].interrogante = interrogante4;
                        secrets[i].itemImage = itemImage4;
                        secrets[i].model = model4;

                        ////comprobar si esta descubierto el objeto
                        if (secrets[i].isDiscovered)
                        {
                            secrets[i].interrogante.SetActive(false);
                            secrets[i].itemImage.SetActive(true);
                        }
                        else
                        {
                            secrets[i].interrogante.SetActive(true);
                            secrets[i].itemImage.SetActive(false);
                        }

                        break;
                    }
                ///-------------ELEMENTO 5
                case 4:
                    {
                        secrets[i].ID = i;

                        //Leemos del player Prefs
                        int discover = PlayerPrefs.GetInt("Secret5");
                        if (discover == 0)
                        {
                            secrets[i].isDiscovered = false;
                        }
                        else if (discover == 1)
                        {
                            secrets[i].isDiscovered = true; ;
                        }

                        secrets[i].isSelected = isSelected5;
                        secrets[i].name = nameObject5;
                        secrets[i].description = description5;
                        secrets[i].position = position5;
                        secrets[i].mark = mark5;
                        secrets[i].interrogante = interrogante5;
                        secrets[i].itemImage = itemImage5;
                        secrets[i].model = model5;

                        ////comprobar si esta descubierto el objeto
                        if (secrets[i].isDiscovered)
                        {
                            secrets[i].interrogante.SetActive(false);
                            secrets[i].itemImage.SetActive(true);
                        }
                        else
                        {
                            secrets[i].interrogante.SetActive(true);
                            secrets[i].itemImage.SetActive(false);
                        }

                        break;
                    }
                ///-------------ELEMENTO 6
                case 5:
                    {
                        secrets[i].ID = i;

                        //Leemos del player Prefs
                        int discover = PlayerPrefs.GetInt("Secret6");
                        if (discover == 0)
                        {
                            secrets[i].isDiscovered = false;
                        }
                        else if (discover == 1)
                        {
                            secrets[i].isDiscovered = true; ;
                        }

                        secrets[i].isSelected = isSelected6;
                        secrets[i].name = nameObject6;
                        secrets[i].description = description6;
                        secrets[i].position = position6;
                        secrets[i].mark = mark6;
                        secrets[i].interrogante = interrogante6;
                        secrets[i].itemImage = itemImage6;
                        secrets[i].model = model6;

                        ////comprobar si esta descubierto el objeto
                        if (secrets[i].isDiscovered)
                        {
                            secrets[i].interrogante.SetActive(false);
                            secrets[i].itemImage.SetActive(true);
                        }
                        else
                        {
                            secrets[i].interrogante.SetActive(true);
                            secrets[i].itemImage.SetActive(false);
                        }

                        break;
                    }
                default:
                    break;
            }
        }
    }
    #endregion

    #region UPDATE
    void Update()
    {
        if (isOpened)
        {
            horizontalMove = inputDevice.LeftStickX;
            verticalMove = inputDevice.LeftStickY;

            MenuNavigationController();
            MenuNavigationKeyboard();
        }

        ///DETECTAR SI ABRES EL MENU, EL BOTON ES TEMPORAL
        if (Input.GetKeyDown(KeyCode.I) || InputManager.ActiveDevice.RightBumper.WasReleased)
        {
            if (!pauseMenu.isPaused)
            {
                if (isOpened)
                {
                    isOpened = false;
                    menu.SetActive(false);
                    Time.timeScale = 1;

                    //UI Sound
                    closeSound.Play();
                }
                else
                {
                    if (secrets[0].isDiscovered)
                    {

                        nameText.text = secrets[0].name;
                        descriptionText.text = secrets[0].description;
                    }
                    isOpened = true;
                    menu.SetActive(true);
                    Time.timeScale = 0;

                    //UI Sound
                    openSound.Play();
                }
            }
            
        }

        #region  PRIMERA LOGICA CONTROLAR EL MENU
        ///COMPROBACION QUE FUNCIONA LA SELECCION Y EL LLENADO DE ELEMENTOS

        /* for (int i = 0; i < secrets.Length; i++)
         {
             switch (i)
             {
                 ///-------------ELEMENTO 1
                 case 0:
                     {
                         ///controlTimeline the selection
                         if (isSelected1)
                         {
                             secrets[i].mark.SetActive(true);
                             nameText.text = secrets[i].name;
                             descriptionText.text = secrets[i].description;

                         }
                         else
                         {
                             secrets[i].mark.SetActive(false);
                         }

                         ///active image if is discoverted
                         if (isDiscovered1)
                         {
                             secrets[i].interrogante.SetActive(false);
                             secrets[i].itemImage.SetActive(true);
                         }
                         else
                         {
                             secrets[i].interrogante.SetActive(true);
                             secrets[i].itemImage.SetActive(false);
                         }
                         break;
                     }
                 ///-------------ELEMENTO 2
                 case 1:
                     {
                         ///controlTimeline the selection
                         if (isSelected2)
                         {
                             secrets[i].mark.SetActive(true);
                             nameText.text = secrets[i].name;
                             descriptionText.text = secrets[i].description;

                         }
                         else
                         {
                             secrets[i].mark.SetActive(false);
                         }

                         ///active image if is discoverted
                         if (isDiscovered2)
                         {
                             secrets[i].interrogante.SetActive(false);
                             secrets[i].itemImage.SetActive(true);
                         }
                         else
                         {
                             secrets[i].interrogante.SetActive(true);
                             secrets[i].itemImage.SetActive(false);
                         }
                         break;
                     }
                 ///-------------ELEMENTO 3
                 case 2:
                     {
                         ///controlTimeline the selection
                         if (isSelected3)
                         {
                             secrets[i].mark.SetActive(true);
                             nameText.text = secrets[i].name;
                             descriptionText.text = secrets[i].description;

                         }
                         else
                         {
                             secrets[i].mark.SetActive(false);
                         }

                         ///active image if is discoverted
                         if (isDiscovered3)
                         {
                             secrets[i].interrogante.SetActive(false);
                             secrets[i].itemImage.SetActive(true);
                         }
                         else
                         {
                             secrets[i].interrogante.SetActive(true);
                             secrets[i].itemImage.SetActive(false);
                         }
                         break;
                     }
                 ///-------------ELEMENTO 4
                 case 3:
                     {
                         ///controlTimeline the selection
                         if (isSelected4)
                         {
                             secrets[i].mark.SetActive(true);
                             nameText.text = secrets[i].name;
                             descriptionText.text = secrets[i].description;

                         }
                         else
                         {
                             secrets[i].mark.SetActive(false);
                         }

                         ///active image if is discoverted
                         if (isDiscovered4)
                         {
                             secrets[i].interrogante.SetActive(false);
                             secrets[i].itemImage.SetActive(true);
                         }
                         else
                         {
                             secrets[i].interrogante.SetActive(true);
                             secrets[i].itemImage.SetActive(false);
                         }
                         break;
                     }
                 ///-------------ELEMENTO 5
                 case 4:
                     {
                         ///controlTimeline the selection
                         if (isSelected5)
                         {
                             secrets[i].mark.SetActive(true);
                             nameText.text = secrets[i].name;
                             descriptionText.text = secrets[i].description;

                         }
                         else
                         {
                             secrets[i].mark.SetActive(false);
                         }

                         ///active image if is discoverted
                         if (isDiscovered5)
                         {
                             secrets[i].interrogante.SetActive(false);
                             secrets[i].itemImage.SetActive(true);
                         }
                         else
                         {
                             secrets[i].interrogante.SetActive(true);
                             secrets[i].itemImage.SetActive(false);
                         }
                         break;
                     }
                 ///-------------ELEMENTO 6
                 case 5:
                     {
                         ///controlTimeline the selection
                         if (isSelected6)
                         {
                             secrets[i].mark.SetActive(true);
                             nameText.text = secrets[i].name;
                             descriptionText.text = secrets[i].description;

                         }
                         else
                         {
                             secrets[i].mark.SetActive(false);
                         }

                         ///active image if is discoverted
                         if (isDiscovered6)
                         {
                             secrets[i].interrogante.SetActive(false);
                             secrets[i].itemImage.SetActive(true);
                         }
                         else
                         {
                             secrets[i].interrogante.SetActive(true);
                             secrets[i].itemImage.SetActive(false);
                         }
                         break;
                     }
                 default:
                     break;
             }  

         }*/
        #endregion
    }
    #endregion

    #region MENU NAVIGATION WITH CONTROLLER
    void MenuNavigationController()
    {
        #region UP CONTROL
        if (verticalMove >= 0.95f)
        {
            if (!upPressed)
            {
                upPressed = true;
                //Camiamos el objecto seleccionado
                MoveUp();
            }
        }
        else
        {
            upPressed = false;
        }
        #endregion

        #region RIGHT CONTROL
        if (horizontalMove >= 0.95f)
        {
            if (!rightPressed)
            {
                rightPressed = true;

                //Camiamos el objecto seleccionado
                MoveRight();
            }
        }
        else
        {
            rightPressed = false;
        }
        #endregion

        #region DOWN CONTROL
        if (verticalMove <= -0.95f)
        {
            if (!downPressed)
            {
                downPressed = true;
                //Camiamos el objecto seleccionado
                MoveDown();
            }
        }
        else
        {
            downPressed = false;
        }
        #endregion

        #region LEFT CONTROL
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
        #endregion
    }
    #endregion

    #region MENU NAVIGATION WITH KEYBOARD
    void MenuNavigationKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            secrets[currentSelected - 1].mark.SetActive(false);

            if (currentSelected == 1 || currentSelected == 4)
            {
                currentSelected += 2;
            }
            else
            {
                currentSelected -= 1;
            }
            SelectNewItem(currentSelected);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            secrets[currentSelected - 1].mark.SetActive(false);

            if (currentSelected == 3 || currentSelected == 6)
            {
                currentSelected -= 2;
            }
            else
            {
                currentSelected += 1;
            }
            SelectNewItem(currentSelected);
        }
    }
    #endregion

    #region MOVE UP
    void MoveUp()
    {
        secrets[currentSelected - 1].mark.SetActive(false);
        if (currentSelected < 4)
        {
            currentSelected += 3;
        }
        else
        {
            currentSelected -= 3;
        }

        SelectNewItem(currentSelected);
        
    }
    #endregion

    #region MOVE RIGHT
    void MoveRight()
    {
        secrets[currentSelected - 1].mark.SetActive(false);

        if (currentSelected == 3 || currentSelected == 6)
        {
            currentSelected -= 2;
        }
        else
        {
            currentSelected += 1;
        }
        SelectNewItem(currentSelected);
    }
    #endregion

    #region MOVE DOWN
    void MoveDown()
    {
        secrets[currentSelected - 1].mark.SetActive(false);
        if (currentSelected < 4)
        {
            currentSelected += 3;
        }
        else
        {
            currentSelected -= 3;
        }

        SelectNewItem(currentSelected);
    }
    #endregion

    #region MOVE LEFT
    void MoveLeft()
    {
        secrets[currentSelected - 1].mark.SetActive(false);

        if (currentSelected == 1 || currentSelected == 4)
        {
            currentSelected += 2;
        }
        else
        {
            currentSelected -= 1;
        }
        SelectNewItem(currentSelected);
    }
    #endregion

    #region SELECT NEW ITEM
    void SelectNewItem(int _newSelection)
    {
        secrets[_newSelection - 1].mark.SetActive(true);

        if (secrets[_newSelection - 1].isDiscovered)
        {

            nameText.text = secrets[_newSelection - 1].name;
            descriptionText.text = secrets[_newSelection - 1].description;
        }
        else
        {

            nameText.text = "?????????????";
            descriptionText.text = "";
        }

        //UI Sound
        moveSound.Play();
    }
    #endregion

    #region DISCOVER NEW OBJECT
    public void DiscoverNewObject(int _object)
    {
        secrets[ _object -1 ].isDiscovered = true;

        secrets[ _object - 1 ].interrogante.SetActive(false);
        secrets[ _object - 1 ].itemImage.SetActive(true);                    
    }
    #endregion
}
