using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    };
    public Item[] secrets;

    [Header("ELEMENTO 1")]
        [SerializeField] bool isDiscovered1;
        [SerializeField] bool isSelected1;
        [SerializeField] string nameObject1;
        [SerializeField] string description1;
        [SerializeField] Transform position1;
        [SerializeField] GameObject mark1;
        [SerializeField] public GameObject interrogante1;
        [SerializeField] public GameObject itemImage1;
    [Header("ELEMENTO 2")]
        [SerializeField] bool isDiscovered2;
        [SerializeField] bool isSelected2;
        [SerializeField] string nameObject2;
        [SerializeField] string description2;
        [SerializeField] Transform position2;
        [SerializeField] GameObject mark2;
        [SerializeField] public GameObject interrogante2;
        [SerializeField] public GameObject itemImage2;
    [Header("ELEMENTO 3")]
        [SerializeField] bool isDiscovered3;
        [SerializeField] bool isSelected3;
        [SerializeField] Transform position3;
        [SerializeField] string nameObject3;
        [SerializeField] string description3;
        [SerializeField] GameObject mark3;
        [SerializeField] public GameObject interrogante3;
        [SerializeField] public GameObject itemImage3;
    [Header("ELEMENTO 4")]
        [SerializeField] bool isDiscovered4;
        [SerializeField] bool isSelected4;
        [SerializeField] string nameObject4;
        [SerializeField] string description4;
        [SerializeField] Transform position4;
        [SerializeField] GameObject mark4;
        [SerializeField] public GameObject interrogante4;
        [SerializeField] public GameObject itemImage4;
    [Header("ELEMENTO 5")]
        [SerializeField] bool isDiscovered5;
        [SerializeField] bool isSelected5;
        [SerializeField] string nameObject5;
        [SerializeField] string description5;
        [SerializeField] Transform position5;
        [SerializeField] GameObject mark5;
        [SerializeField] public GameObject interrogante5;
        [SerializeField] public GameObject itemImage5;
    [Header("ELEMENTO 6")]
        [SerializeField] bool isDiscovered6;
        [SerializeField] bool isSelected6;
        [SerializeField] string nameObject6;
        [SerializeField] string description6;
        [SerializeField] Transform position6;
        [SerializeField] GameObject mark6;
        [SerializeField] public GameObject interrogante6;
        [SerializeField] public GameObject itemImage6;

    [Header("NOMBRE Y DESCRIPCION")]
        [SerializeField] Text nameText;
        [SerializeField] Text descriptionText;

    #endregion

    #region START
    void Start()
    {
        secrets = new Item[6];

        for (int i = 0; i < secrets.Length; i++)
        {
            switch (i)
            {
                ///-------------ELEMENTO 1
                case 0:
                    {
                        secrets[i].ID = i;
                        secrets[i].isDiscovered = isDiscovered1;
                        secrets[i].isSelected = isDiscovered1;
                        secrets[i].name = nameObject1;
                        secrets[i].description = description1;
                        secrets[i].position = position1;
                        secrets[i].mark = mark1;
                        secrets[i].interrogante = interrogante1;
                        secrets[i].itemImage = itemImage1;
                        break;
                    }
                ///-------------ELEMENTO 2
                case 1:
                    {
                        secrets[i].ID = i;
                        secrets[i].isDiscovered = isDiscovered2;
                        secrets[i].isSelected = isDiscovered2;
                        secrets[i].name = nameObject2;
                        secrets[i].description = description2;
                        secrets[i].position = position2;
                        secrets[i].mark = mark2;
                        secrets[i].interrogante = interrogante2;
                        secrets[i].itemImage = itemImage2;
                        break;
                    }
                ///-------------ELEMENTO 3
                case 2:
                    {
                        secrets[i].ID = i;
                        secrets[i].isDiscovered = isDiscovered3;
                        secrets[i].isSelected = isDiscovered3;
                        secrets[i].name = nameObject3;
                        secrets[i].description = description3;
                        secrets[i].position = position3;
                        secrets[i].mark = mark3;
                        secrets[i].interrogante = interrogante3;
                        secrets[i].itemImage = itemImage3;
                        break;
                    }
                ///-------------ELEMENTO 4
                case 3:
                    {
                        secrets[i].ID = i;
                        secrets[i].isDiscovered = isDiscovered4;
                        secrets[i].isSelected = isDiscovered4;
                        secrets[i].name = nameObject4;
                        secrets[i].description = description4;
                        secrets[i].position = position4;
                        secrets[i].mark = mark4;
                        secrets[i].interrogante = interrogante4;
                        secrets[i].itemImage = itemImage4;
                        break;
                    }
                ///-------------ELEMENTO 5
                case 4:
                    {
                        secrets[i].ID = i;
                        secrets[i].isDiscovered = isDiscovered5;
                        secrets[i].isSelected = isDiscovered5;
                        secrets[i].name = nameObject5;
                        secrets[i].description = description5;
                        secrets[i].position = position5;
                        secrets[i].mark = mark5;
                        secrets[i].interrogante = interrogante5;
                        secrets[i].itemImage = itemImage5;
                        break;
                    }
                ///-------------ELEMENTO 6
                case 5:
                    {
                        secrets[i].ID = i;
                        secrets[i].isDiscovered = isDiscovered6;
                        secrets[i].isSelected = isDiscovered6;
                        secrets[i].name = nameObject6;
                        secrets[i].description = description6;
                        secrets[i].position = position6;
                        secrets[i].mark = mark6;
                        secrets[i].interrogante = interrogante6;
                        secrets[i].itemImage = itemImage6;
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
        ///COMPROBACION QUE FUNCIONA LA SELECCION Y EL LLENADO DE ELEMENTOS
        for (int i = 0; i < secrets.Length; i++)
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
                        break;
                    }
                default:
                    break;
            }  

        }
    }
    #endregion
}
