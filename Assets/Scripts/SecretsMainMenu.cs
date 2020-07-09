using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretsMainMenu : MonoBehaviour
{

    [SerializeField] GameObject[] objects;

    void Start()
    {
        if (PlayerPrefs.GetInt("Secret1") == 1)
        {
            objects[0].SetActive(true);
        }

        if (PlayerPrefs.GetInt("Secret2") == 1)
        {
            objects[1].SetActive(true);
        }

        if (PlayerPrefs.GetInt("Secret3") == 1)
        {
            objects[2].SetActive(true);
        }

        if (PlayerPrefs.GetInt("Secret4") == 1)
        {
            objects[3].SetActive(true);
        }

        if (PlayerPrefs.GetInt("Secret5") == 1)
        {
            objects[4].SetActive(true);
        }

        if (PlayerPrefs.GetInt("Secret6") == 1)
        {
            objects[5].SetActive(true);
        }
    }

   
}
