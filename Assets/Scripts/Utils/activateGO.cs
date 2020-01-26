using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateGO : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Interactuable
    {
        NONE,
        PLAYER,
        BLOCK
    }
    [SerializeField] GameObject go;
    [SerializeField] Interactuable interactuable;
    

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && interactuable == Interactuable.PLAYER) go.SetActive(true);
        if (other.tag == "Block" && interactuable == Interactuable.BLOCK)
        {
            go.SetActive(true);
            Debug.Log("ENCIENDETE PUTA!!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Block" && interactuable == Interactuable.BLOCK) go.SetActive(false);
    }
}
