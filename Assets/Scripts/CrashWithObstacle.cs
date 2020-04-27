using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashWithObstacle : MonoBehaviour
{
    public enum ObstacleType
    {
        NONE,
        OBSTACLE,
        WALL,
        FLOOR,
        ANY
    }

    [SerializeField] GameObject particlesDestroy;

    [SerializeField] ObstacleType[] obstacle;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < obstacle.Length; i++)
        {
            switch (obstacle[i])
            {
                case ObstacleType.NONE:
                    {
                        break;
                    }
                case ObstacleType.OBSTACLE:
                    {
                        if (other.CompareTag("Block"))
                        {
                            Destroy(this.gameObject);
                            Destroy(Instantiate(particlesDestroy, this.transform.position, Quaternion.identity), 1f);
                        }
                        break;
                    }
                case ObstacleType.WALL:
                    {
                        if (other.CompareTag("Wall"))
                        {
                            Destroy(this.gameObject);
                            Destroy(Instantiate(particlesDestroy, this.transform.position, Quaternion.identity), 1f);
                        }
                        break;
                    }
                case ObstacleType.FLOOR:
                    {
                        if (other.CompareTag("Floor"))
                        {
                            Destroy(this.gameObject);
                            Destroy(Instantiate(particlesDestroy, this.transform.position, Quaternion.identity), 1f);
                        }
                        break;
                    }
                case ObstacleType.ANY:
                    {
                        if (other.CompareTag("Stone") || other.CompareTag("Wall") || other.CompareTag("Floor"))
                        {
                            Destroy(this.gameObject);
                            Destroy(Instantiate(particlesDestroy, this.transform.position, Quaternion.identity), 1f);
                        }
                        break;
                    }
                default:
                    break;
            }


        }
    }
     private void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < obstacle.Length; i++)
        {
            switch (obstacle[i])
            {
                case ObstacleType.NONE:
                    {
                        break;
                    }
                case ObstacleType.OBSTACLE:
                    {
                        if (other.CompareTag("Block"))
                        {
                            Destroy(this.gameObject);
                            Destroy(Instantiate(particlesDestroy, this.transform.position, Quaternion.identity), 1f);
                        }
                        break;
                    }
                case ObstacleType.WALL:
                    {
                        if (other.CompareTag("Wall"))
                        {
                            Destroy(this.gameObject);
                            Destroy(Instantiate(particlesDestroy, this.transform.position, Quaternion.identity), 1f);
                        }
                        break;
                    }
                case ObstacleType.FLOOR:
                    {
                        if (other.CompareTag("Floor"))
                        {
                            Destroy(this.gameObject);
                            Destroy(Instantiate(particlesDestroy, this.transform.position, Quaternion.identity), 1f);
                        }
                        break;
                    }
                case ObstacleType.ANY:
                    {
                        if (other.CompareTag("Stone") || other.CompareTag("Wall") || other.CompareTag("Floor"))
                        {
                            Destroy(this.gameObject);
                            Destroy(Instantiate(particlesDestroy, this.transform.position, Quaternion.identity), 1f);
                        }
                        break;
                    }
                default:
                    break;
            }


        }
    }

}
