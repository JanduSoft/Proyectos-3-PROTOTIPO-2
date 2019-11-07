using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLevel1 : MonoBehaviour
{
    #region VARIABLES
    enum Light
    {
        NONE,
        RED,
        GREEN,
        BLUE,
        YELLOW,
        WHITE,
        PURPLE,
        ORANGE
    }

    [SerializeField] GameObject switchLight;
    [SerializeField] GameObject wallLight1;
    [SerializeField] GameObject wallLight2;
    [SerializeField] GameObject wallLight3;
    [SerializeField] Light MyLight;
    [SerializeField] SwitchController controller;

    public bool isPressed = false;

    #endregion

    #region ON TRIGGER ENTER/EXIT
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            switchLight.SetActive(true);

            if (isPressed == false)
            {
                switch (MyLight)
                {
                    case Light.RED:
                        {
                            controller.AddSwitchPressed(3);
                            wallLight1.SetActive(true);
                            wallLight2.SetActive(true);
                            wallLight3.SetActive(true);

                            break;
                        }
                    case Light.GREEN:
                        {
                            controller.AddSwitchPressed(1);
                            wallLight1.SetActive(true);

                            break;
                        }
                    case Light.BLUE:
                        {
                            controller.AddSwitchPressed(2);
                            wallLight1.SetActive(true);
                            wallLight2.SetActive(true);
                            break;
                        }
                    default:
                        break;
                }
            }
            

            isPressed = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //switchLight.SetActive(false);

            switch (MyLight)
            {
                case Light.RED:
                    {
                        wallLight1.SetActive(false);
                        wallLight2.SetActive(false);
                        wallLight3.SetActive(false);

                        break;
                    }
                case Light.GREEN:
                    {
                        wallLight1.SetActive(false);

                        break;
                    }
                case Light.BLUE:
                    {
                        wallLight1.SetActive(false);
                        wallLight2.SetActive(false);
                        break;
                    }
                default:
                    break;
            }
        }
    }
    #endregion
}
