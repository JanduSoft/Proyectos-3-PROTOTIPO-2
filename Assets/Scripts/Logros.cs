using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logros : MonoBehaviour
{
    public static int numberOfRocksPushed = 0;
    public static int numberOfDeaths = 0;
    public static int numberOfEnemiesKilled = 0;
    public static int numberOfVasesBroken = 0;
    public static int numberOfSecretObjects = 0;
    public static int litYourFirstTorch = 0;
    public static int didYourFirstWhip = 0;
    public static int grabbedYourFirstSkull = 0;
    public static int grabbedYourFirstGem = 0;

    private void Awake()
    {
        numberOfDeaths = PlayerPrefs.GetInt("NumberOfDeaths", 0);   //done
        numberOfRocksPushed = PlayerPrefs.GetInt("NumberOfRocksPushed", 0); //done
        litYourFirstTorch = PlayerPrefs.GetInt("LitYourFirstTorch", 0); //done
        didYourFirstWhip = PlayerPrefs.GetInt("DidYourFirstWhip", 0);   //done
        grabbedYourFirstSkull = PlayerPrefs.GetInt("GrabbedYourFirstSkull", 0);
        grabbedYourFirstGem = PlayerPrefs.GetInt("GrabbedYourFirstGem", 0);

        numberOfEnemiesKilled = PlayerPrefs.GetInt("NumberOfEnemiesKilled", 0);
        numberOfVasesBroken = PlayerPrefs.GetInt("NumberOfVasesBroken", 0);
        numberOfSecretObjects = PlayerPrefs.GetInt("NumberOfSecretObjects", 0); //done
    }

    public static void CallAchievement(int index)
    {
        string achievementName;

        switch(index)
        {
            case 1:
                achievementName = "The unknown";
                break;
            case 2:
                achievementName = "The gardens";
                break;
            case 3:
                achievementName = "The catacombs";
                break;
            case 4:
                achievementName = "The bridge";
                break;
            case 5:
                achievementName = "The cave";
                break;
            case 6:
                achievementName = "The biggest treasure";
                break;
            case 7:
                achievementName = "So strong!";
                break;
            case 8:
                achievementName = "This is lit!";
                break;
            case 9:
                achievementName = "I believe I can fly";
                break;
            case 10:
                achievementName = "What a headache";
                break;
            case 11:
                achievementName = "So shiny";
                break;
            case 12:
                achievementName = "I hate those things";
                break;
            case 13:
                achievementName = "Making a mess";
                break;
            case 14:
                achievementName = "Bridge down!";
                break;
            case 15:
                achievementName = "Our little secret";
                break;
            case 16:
                achievementName = "Treasure hunter";
                break;
            case 17:
                achievementName = "Zero deaths";
                break;
            default:
                achievementName = "##ERROR##";
                break;
        }


        Debug.Log("Achievement " + index + " unlocked: " + achievementName);
    }
}
