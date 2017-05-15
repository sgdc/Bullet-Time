using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIK : MonoBehaviour
{
    public Transform HandIKTarget;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetLookAtWeight(1);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        anim.SetLookAtPosition(HandIKTarget.position);
        anim.SetIKPosition(AvatarIKGoal.RightHand, HandIKTarget.position);
        anim.SetIKRotation(AvatarIKGoal.RightHand, HandIKTarget.rotation);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, HandIKTarget.position);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, HandIKTarget.rotation);
    }
}
