using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class incorrectFireScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject torch;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        torch.GetComponent<PickUpDropandIgnite>().turnOffTorch();
        gameObject.SetActive(false);
    }
}
