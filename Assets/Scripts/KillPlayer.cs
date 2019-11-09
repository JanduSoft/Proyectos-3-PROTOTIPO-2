using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] GameObject deathParticles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(deathParticles, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);

            Invoke("ResetLevel" ,2f);
        }

    }

    void ResetLevel()
    {
        SceneManager.LoadScene("Level_1");
    }
}
