using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
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

    private Light[] lights = new Light[3];
    public int counter = 0;
    private int counterTime;
    private bool wallsDown;

    [SerializeField] AudioSource audio;

    [Header("LIGHTS")]
    [SerializeField] GameObject light_1;
    [SerializeField] GameObject light_2;
    [SerializeField] GameObject light_3;

    [SerializeField] GameObject wallLight_1;

    [SerializeField] GameObject wallLight_2;
    [SerializeField] GameObject wallLight_3;

    [SerializeField] GameObject wallLight_4;
    [SerializeField] GameObject wallLight_5;
    [SerializeField] GameObject wallLight_6;

    [Header("BUTTONS")]
    [SerializeField] SwitchLevel1 button_1;
    [SerializeField] SwitchLevel1 button_2;
    [SerializeField] SwitchLevel1 button_3;

    [Header("WALL ANIMATIONS")]

    [SerializeField] Animator animatorWall1;
    [SerializeField] Animator animatorWall2;
    [SerializeField] Animator animatorWall3;
    [SerializeField] Animator animatorWall4;
    [SerializeField] GameObject bounds;

    [Header("WALLS")]
    [SerializeField] GameObject wallExterior;
    [SerializeField] GameObject wallInteriror1;
    [SerializeField] GameObject wallInteriror2;
    [SerializeField] GameObject wallInteriror3;
    [SerializeField] GameObject wallInteriror4;


    #endregion

    #region UPDATE
    // Update is called once per frame
    void Update()
    {
        if (counter == 3)
        {
            counterTime++;

            if (counterTime >= 120)
            {
                counter = 0;
                light_1.SetActive(false);
                light_2.SetActive(false);
                light_3.SetActive(false);

                wallLight_1.SetActive(false);

                wallLight_2.SetActive(false);
                wallLight_3.SetActive(false);

                wallLight_4.SetActive(false);
                wallLight_5.SetActive(false);
                wallLight_6.SetActive(false);

                if (lights[0] == Light.GREEN && lights[1] == Light.BLUE && lights[2] == Light.RED)
                {
                    Debug.Log("SE BAJAN LOS MUROS");

                    bounds.SetActive(false);

                    animatorWall1.SetBool("Active", true);
                    animatorWall2.SetBool("Active", true);
                    animatorWall3.SetBool("Active", true);
                    animatorWall4.SetBool("Active", true);
                    wallsDown = true;
                    counterTime = 0;
                    counter = 0;
                    audio.Play();

                }
                else
                {
                    button_1.isPressed = false;
                    button_2.isPressed = false;
                    button_3.isPressed = false;
                }
            }
            

        }

        if (wallsDown)
        {
            counterTime++;

            if (counterTime >= 200)
            {
                wallExterior.SetActive(true);
            }
            else if (counterTime >= 300)
            {
                wallInteriror1.SetActive(false);
                wallInteriror2.SetActive(false);
                wallInteriror3.SetActive(false);
                wallInteriror4.SetActive(false);
            }
        }
    }

    #endregion

    #region ADD SWITCH PRESSED
    public void AddSwitchPressed(int _light)
    {
        if (_light == 1)
        {
            lights[counter] = Light.GREEN;
        }
        else if (_light == 2)
        {
            lights[counter] = Light.BLUE;
        }
        else if (_light == 3)
        {
            lights[counter] = Light.RED;
        }
        
        counter++;
    }
    #endregion
}
