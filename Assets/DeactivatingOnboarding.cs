using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatingOnboarding : MonoBehaviour
{
    [SerializeField] GameObject tutoSprites;
    // Update is called once per frame
    void Update()
    {
        tutoSprites.SetActive(false);
    }
}
