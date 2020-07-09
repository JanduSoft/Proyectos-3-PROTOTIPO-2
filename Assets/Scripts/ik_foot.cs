using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ik_foot : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("ACTIVATION")]
    [SerializeField] bool activatedIK = true;
    [Space(15)]

    [Header("Variables")]
    [SerializeField] CharacterController cc;
    [SerializeField] Animator anim;

    [Range(0, 1)]
    [SerializeField] float DistanceToGround;
    [SerializeField] LayerMask layermask;

    private void Start()
    {
        if (activatedIK)
        {
            cc.height = 3.33f;
            cc.center = new Vector3(0, 1.84f, 0);
        }
        else
        {
            cc.height = 3.62f;
            cc.center = new Vector3(0, 1.8f, 0);
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {

        if (anim && activatedIK)
        {
            Debug.Log("ANIMATOR FOUND");
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, anim.GetFloat("ik_leftFootWeight"));
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, anim.GetFloat("ik_leftFootWeight"));

            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, anim.GetFloat("ik_rightFootWeight"));
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, anim.GetFloat("ik_rightFootWeight"));

            //Left foot
            RaycastHit hit;
            Ray ray = new Ray(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, DistanceToGround+1.0f,layermask))
            {
                if (hit.transform.tag == "Untagged")
                {
                    Vector3 footPosition = hit.point;
                    footPosition.y += DistanceToGround;
                    anim.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward,hit.normal));
                }
            }

            //Right foot
            RaycastHit hit2;
            Ray ray2 = new Ray(anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray2, out hit2, DistanceToGround + 1.0f, layermask))
            {
                if (hit2.transform.tag == "Untagged")
                {
                    Vector3 footPosition = hit2.point;
                    footPosition.y += DistanceToGround;
                    anim.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit2.normal));

                }
            }
        }
    }
}
