using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    public Vector3 destiny;
    public float speed = 1f;
    public float distance = 2f;

    public GameObject nodePrefab;
    public GameObject player;
    public GameObject lastNode;

    bool done = false;

    public LineRenderer lineRenderer;
    int vertexCount = 2;
    public List<GameObject> Node = new List<GameObject>();


    private Transform myObjective;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("HookSpawner");
        lastNode = transform.gameObject;

        Node.Add(transform.gameObject);

        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destiny, speed);

        if (transform.position != destiny)
        {
            if (Vector3.Distance(player.transform.position, lastNode.transform.position) > distance)
            {
                CreatNode();
            }
        }
        else if (done == false)
        {
            done = true;
            lastNode.GetComponent<HingeJoint>().connectedBody = player.GetComponent<Rigidbody>();
        }

        RenderLine();
    }

    void RenderLine()
    {
        lineRenderer.positionCount =vertexCount;

        int i;
        for (i = 0; i < Node.Count; i++)
        {
            lineRenderer.SetPosition(i, Node[i].transform.position);
        }

        lineRenderer.SetPosition(i, player.transform.position);
    }

    void CreatNode()
    {
        Vector3 posToCreate = player.transform.position - lastNode.transform.position;
        posToCreate.Normalize();
        posToCreate *= distance;
        posToCreate += lastNode.transform.position;

        GameObject gO= (GameObject)Instantiate(nodePrefab, posToCreate, Quaternion.identity);

        gO.transform.SetParent(transform);

        lastNode.GetComponent<HingeJoint>().connectedBody = gO.GetComponent<Rigidbody>();

        lastNode = gO;

        Node.Add(lastNode);
        vertexCount++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WhipPoint"))
        {
            destiny = other.transform.position;

        }
    }
}
