using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActivateBridgeCinematic : MonoBehaviour
{
    [SerializeField] GameObject bridgeCinematic;
    [SerializeField] GameObject rope;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Torch")
        {
            bridgeCinematic.SetActive(true);
            bridgeCinematic.GetComponent<PlayableDirector>().Play();
            Destroy(rope, 0.5f);
            Destroy(bridgeCinematic, 4.56f);


            if (PlayerPrefs.GetInt("DroppedTheBridge") == 0) 
            {
                Logros.droppedTheBridge = 1;
                PlayerPrefs.SetInt("DroppedTheBridge",Logros.droppedTheBridge);
                Logros.CallAchievement(14); //drop the bridge achievement
            }
        }
    }
}
