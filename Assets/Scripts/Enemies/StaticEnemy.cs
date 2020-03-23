using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StaticEnemy : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject ArrowPrefab;
    [SerializeField] GameObject shootingPoint;
    [SerializeField] float viewingAngle;
    [SerializeField] float viewingDistance;
    [SerializeField] float shotTiming = 1;
    [SerializeField] List<GameObject> listShots = new List<GameObject>();
    float angleBetweenEnemyandPlayer = 0;
    [SerializeField] float time = 0;
    Quaternion startingRotation;

    [SerializeField] GameObject SpawnPoint = null;
    // Start is called before the first frame update
    void Start()
    {
        startingRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        angleBetweenEnemyandPlayer = Vector3.Angle(transform.forward, new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z) - transform.position);
        time += Time.deltaTime;
        if (angleBetweenEnemyandPlayer < viewingAngle)
        {
            Player.GetComponent<playerDeath>().spawnPoint = SpawnPoint;
            Player.GetComponent<playerDeath>().StaticEnemyDetected = true;
            Quaternion finalRot = Quaternion.LookRotation(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z) - transform.position);
            //Vector3 newRotV3 = Vector3.Lerp(transform.rotation, new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z) - transform.position, 0.5f);
            transform.DOLookAt(Player.transform.position, 0.5f);
            if(time > shotTiming)
            {
                GameObject aux = Instantiate(ArrowPrefab, shootingPoint.transform.position, Quaternion.LookRotation(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z) - transform.position));
                listShots.Add(aux);
                aux.transform.LookAt(Player.transform);
                aux.GetComponent<Rigidbody>().velocity = aux.transform.forward * 20; 
                time = 0;
            }
        }
        
    }
    public void DestroyMe(GameObject go)
    {
        listShots.Remove(go);
        Destroy(go);
    }
    public void DestroyAll()
    {
        gameObject.transform.rotation = startingRotation;
        for (int i = 0; i < listShots.Count; i++)
            Destroy(listShots[i]);
    }
}
