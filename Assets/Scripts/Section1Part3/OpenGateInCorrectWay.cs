using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenGateInCorrectWay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] levers = new GameObject[3];
    //0 blue
    //1 red
    //2 purple
    public GameObject[] lights = new GameObject[3];
    //0 blue
    //1 red
    //2 purple
    [Space(15)]
    public GameObject gate;
    public GameObject pedestal;

    [SerializeField] GameObject swordPickUp;

    [Space(15)]
    [Header("SOUND")]
    [SerializeField] AudioSource audios;
    [SerializeField] AudioClip openGateAudio;
    [SerializeField] AudioClip pedestalMovingAudio;

    public enum LeverColors { BLUE, RED, PURPLE};
    [SerializeField] public List<LeverColors> actualColors = new List<LeverColors>();
    private List<LeverColors> winningCombo = new List<LeverColors>();

    void Start()
    {
        swordPickUp.GetComponent<PickUpDropandThrow>().enabled = false;
        actualColors.Clear();
        winningCombo.Add(LeverColors.PURPLE);
        winningCombo.Add(LeverColors.BLUE);
        winningCombo.Add(LeverColors.RED);
    }

    // Update is called once per frame
    void Update()
    {
        if (actualColors.Count>=3)
        {
            //if it's correct, puzzle is solved

            if (checkCombination(actualColors,winningCombo))
            {
                foreach(GameObject aux in levers)
                {
                    aux.GetComponent<LeverScript>().enabled = false;
                }
                actualColors.Clear();
                StartCoroutine(OpenGates());
            }

            //if it's not correct, reset
            else
            {
                actualColors.Clear();
                foreach(GameObject aux in lights)
                {
                    aux.GetComponent<Light>().DOIntensity(0, 1);
                }
            }

        }
    }

    IEnumerator OpenGates()
    {
        Camera.main.DOShakePosition(3, 3, 5, 30, true);
        gate.transform.DOLocalMoveZ(105.30f, 3, false);
        audios.clip = openGateAudio;
        audios.volume = 0.7f;
        audios.Play();
        yield return new WaitForSeconds(2f);
        Camera.main.DOShakePosition(5, 3, 7, 50, true);
        pedestal.transform.DOLocalMoveY(5.60f, 5, false);
        audios.volume = 1f;
        audios.clip = pedestalMovingAudio;
        swordPickUp.GetComponent<PickUpDropandThrow>().enabled = true;
        swordPickUp.GetComponent<PickUpDropandThrow>().SetStartingPoint(swordPickUp.gameObject.transform.position+Vector3.up*5);
        audios.Play();
    }

    bool checkCombination(List<LeverColors> a, List<LeverColors>b)
    {
        if (a.Count != b.Count)
            return false;

        for (int i=0; i<a.Count;i++)
        {
            if (a[i] != b[i]) return false;
        }

        return true;
    }

    public void activateLight(int index)
    {
        lights[index].GetComponent<Light>().DOIntensity(1.5f, 1);
    }
}
