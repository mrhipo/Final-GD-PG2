﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float mouseSensitivity = 5;
    public float spineRotation = 40;
    public Animator animator;

    public Cinemachine.CinemachineVirtualCamera camAim;
    Cinemachine.CinemachineComposer composer;
    Cinemachine.CinemachineTransposer transposer;

    Transform spine;

    
    [Header("Ik")]
    HandIk currentIK;
    public HandIk idleIk;
    public HandIk aimIk;

    bool aiming;

    PlayerInput input = new PlayerInput();

    float speedMultiplier = 1;

    private void Start()
    {
        currentIK = idleIk;
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();

        spine = animator.GetBoneTransform(HumanBodyBones.Spine);

        composer = camAim.GetCinemachineComponent<Cinemachine.CinemachineComposer>();
        transposer = camAim.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();
    }

    void Update () {

        //Movement
        animator.SetFloat("Horizontal", input.Horizontal);
        animator.SetFloat("Vertical", input.Vertical * speedMultiplier);

        //Rotation
        transform.Rotate(Vector3.up, input.RotationX);
        
        //Aiming
        CheckAiming();
    }

    bool lastAiming = false;
    private void CheckAiming()
    {
        aiming = input.Aiming;

        //On Aimgin Toggle
        if (lastAiming != aiming)
        {
            ToggleIk(!aiming);

            if (aiming)
            {
                transposer.m_FollowOffset.y = 0;
                composer.m_TrackedObjectOffset.y = 0;
                speedMultiplier = .5f;
            }
            else
            {
                speedMultiplier = 1;
            }
        }

        if (aiming)
        {
            composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y + input.RotationY, -1, 1);
            transposer.m_FollowOffset.y = Mathf.Clamp(transposer.m_FollowOffset.y - input.RotationY, -1,1);

            if (input.Shooting)
            {
                print("SHOOT");
            }
        } 

        lastAiming = aiming;
        camAim.enabled = aiming;
    }

    private void LateUpdate()
    {
        if (aiming)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            spine.forward = ray.GetPoint(20) - spine.position;
            spine.Rotate(Vector3.up, spineRotation);
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, currentIK.rightHand.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, currentIK.rightHand.rotation);

        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, currentIK.leftHand.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, currentIK.leftHand.rotation);
        
    }


    private void ToggleIk(bool idle)
    {
        currentIK = idle ? idleIk : aimIk;
    }

}

[System.Serializable]
public class HandIk
{
    public Transform leftHand;
    public Transform rightHand;
}
