using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SavingTime : MonoBehaviour
{
    public enum Moment
    {
        NONE,
        START,
        END
    };

    [SerializeField] Moment currentMoment;

    void CreateText()
    {
        //Path of the file
        string path = Application.dataPath + "/RESULTADOS/TimeLog.txt";

        //Delete File if it exist
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        File.WriteAllText(path, "Time Start:  "+System.DateTime.Now + "\n\n");
        
    }

    void AddFinalTime()
    {
        //Path of the file
        string path = Application.dataPath + "/RESULTADOS/TimeLog.txt";

        //Add time to the file
        File.AppendAllText(path, "Time Finished:  " + System.DateTime.Now + "\n\n");
    }

    void Start()
    {
        if (currentMoment == Moment.START)
        {
            CreateText();
        }
        else if (currentMoment == Moment.END)
        {
            AddFinalTime();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
