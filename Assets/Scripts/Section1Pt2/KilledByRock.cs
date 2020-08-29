using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilledByRock : MonoBehaviour
{
    [SerializeField] GameObject pedra;
    [SerializeField] GameObject foc;
    [SerializeField] GameObject staff;
    [SerializeField] GameObject crushedStaff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == pedra.tag)
        {
            Logros.numberOfEnemiesKilled++;
            PlayerPrefs.SetInt("NumberOfEnemiesKilled", Logros.numberOfEnemiesKilled);
            if (Logros.numberOfEnemiesKilled == 3)
            {
                Logros.CallAchievement(12);
            }

            Destroy(gameObject);
            staff.transform.rotation = crushedStaff.transform.rotation;
            staff.transform.position = crushedStaff.transform.position;
            foc.SetActive(false);
        }
    }
}
