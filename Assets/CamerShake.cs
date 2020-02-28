using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamerShake : MonoBehaviour
{
    [Header("FOR CAMERA SHAKE")]
    [SerializeField] Camera myCamera;
    [SerializeField] float durationShake;
    [SerializeField] float strength;
    [SerializeField] int vibrato;
    [SerializeField] float randomness;

   public void StartShake(float duration)
   {
        myCamera.DOShakePosition(duration, strength, vibrato, randomness, true);
   }
}
