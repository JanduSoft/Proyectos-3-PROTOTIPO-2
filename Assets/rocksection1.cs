using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocksection1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject part1;
    [SerializeField] GameObject part2;
    [SerializeField] GameObject rock;
    [SerializeField] Transform point1;
    [SerializeField] Transform point2;
    Vector3 centerPoint;
    
    void Start()
    {
        Vector3 uv = (point2.position - point1.position) / 2;
        centerPoint = point1.position + uv;
    }

    // Update is called once per frame
    void Update()
    {
        //if (rock.transform.position.z <= centerPoint.z)
        //{
        //    //rock child of part 1
        //    rock.transform.SetParent(part1.transform);
            
        //}
        //else if(rock.transform.position.z>centerPoint.z && part2.activeSelf)
        //{
        //    //rock child of part 2
        //    rock.transform.SetParent(part2.transform);
        //}
        //else if (rock.transform.position.z > centerPoint.z && part1.activeSelf)
        //{
        //    //rock child of part 1
        //    rock.transform.SetParent(part1.transform);
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(point1.position, point2.position);
        Gizmos.color = Color.green;
        Vector3 uv = (point2.position - point1.position)/2;
        Vector3 center = point1.position + uv;
        Gizmos.DrawSphere(center, 0.5f);
    }
}
