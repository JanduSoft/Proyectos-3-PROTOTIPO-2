using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Rigidbody thisRB;
    [SerializeField] GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy");
        player = GameObject.Find("Character").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(name != "Fire01")
            thisRB.velocity = (new Vector3(player.position.x, player.position.y + 1.5f, player.position.z) - transform.position).normalized * 17;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(DestroyAllGO());
        else if (!other.CompareTag("Path"))
        {
            StartCoroutine(DestroyGO());
        }
    }
    IEnumerator DestroyGO()
    {
        yield return new WaitForEndOfFrame();
        enemy.SendMessage("DestroyMe", gameObject);
    }
    IEnumerator DestroyAllGO()
    {
        yield return new WaitForEndOfFrame();
        enemy.SendMessage("DestroyAll");
    }
}
