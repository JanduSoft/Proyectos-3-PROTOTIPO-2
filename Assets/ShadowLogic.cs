using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShadowLogic : MonoBehaviour
{
    [SerializeField] Transform player;
    float diferenceSize;
    [Header("SHADOW")]
    [SerializeField] GameObject shadow;
    [SerializeField] float maxSize;
    [SerializeField] float minSize;
    [SerializeField] float maxDistance;
    [SerializeField] float minDistance;
    float newSize;
    Ray ray;
    RaycastHit hit;

    bool isHitted = false;

    private void Start()
    {
        diferenceSize = maxSize - minSize;
    }

    void Update()
    {
        

        Debug.DrawRay(player.position, Vector3.down, Color.green);

        isHitted = Physics.Raycast(player.position, Vector3.down, out hit);

        float distanceToGround = Vector3.Distance(hit.transform.position, player.position);

        if (isHitted)
        {
            if (distanceToGround >= maxDistance)
            {
                //Debug.Log("La sombra tiene que ser minuscula");
                this.transform.localScale = new Vector3(minSize, minSize, minSize);
            }
            else if (distanceToGround <= minDistance)
            {
                //Debug.Log("La sombra tiene que ser enorme");
                this.transform.localScale = new Vector3(maxSize, maxSize, maxSize);
            }
            else
            {                
                
                float divSize = (maxSize - minSize) / 10;
                float divDistance = (maxDistance - minDistance) / 10;

                for (int i = 1 ; i <= 10; i++)
                {             

                    if (distanceToGround > minDistance && distanceToGround <= (minDistance + divDistance*i))
                    {
                        Debug.Log(minDistance + divDistance * i);
                        newSize = minSize + divSize * i;
                        break;
                    }
                }

                this.transform.localScale = new Vector3(newSize, newSize, newSize);
            }
            
        }

    }
}
