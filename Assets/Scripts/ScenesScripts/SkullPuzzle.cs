using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullPuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> skullCupArray = new List<GameObject>();
    public List<bool> answers = new List<bool>();
    public Animation cellAnim;
    bool puzzleDone = false;

    void Start()
    {
        foreach (var aux in skullCupArray)
        {
            answers.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!puzzleDone)
        {
            for (int i=0; i<skullCupArray.Count;i++)
            {
                answers[i] = skullCupArray[i].GetComponent<AddSkull>().isActivated;
            }

            if (checkAnswers())
            {
                Debug.Log("All active!");
                cellAnim.Play();
                puzzleDone = true;

            }
            else if (!checkAnswers())
            {
                Debug.Log("Not all active");
            }

        }

    }

    bool checkAnswers()
    {
        foreach(var ans in answers)
        {
            if (ans == false) return false;
        }

        return true;
    }
}
