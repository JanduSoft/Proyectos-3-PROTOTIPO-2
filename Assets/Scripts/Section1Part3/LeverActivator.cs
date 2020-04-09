using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverActivator : MonoBehaviour
{
    // Start is called before the first frame update
    public OpenGateInCorrectWay.LeverColors leverColor;
    public OpenGateInCorrectWay PuzzleScript;
    public void ActivateObject()
    {
        PuzzleScript.activateLight((int)leverColor);
        PuzzleScript.actualColors.Add(leverColor);
    }
}
