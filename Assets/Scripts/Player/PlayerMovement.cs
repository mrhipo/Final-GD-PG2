﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float mouseSensitivity = 5;
    public float spineRotation = 40;
    public Animator animator;

    public Cinemachine.CinemachineVirtualCamera camAim;
    Cinemachine.CinemachineComposer composer;
    Cinemachine.CinemachineTransposer transposer;

    Transform spine;

    [Header("Ik")]
    public Transform ikLeftHand;
    public Transform ikRightHand;

    bool aiming;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();

        spine = animator.GetBoneTransform(HumanBodyBones.Spine);

        composer = camAim.GetCinemachineComponent<Cinemachine.CinemachineComposer>();
        transposer = camAim.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();
    }

    void Update () {

        //Movement
        animator.SetFloat("Horizontal", Horizontal);
        animator.SetFloat("Vertical", Vertical);

        //Rotation
        transform.Rotate(Vector3.up, RotationX );
        
        //Inputs
        CheckInput();

    }

    bool lastAiming = false;
    private void CheckInput()
    {
        aiming = Input.GetKey(KeyCode.Mouse1);

        if (aiming && lastAiming != aiming)
        {
            transposer.m_FollowOffset.y = 0;
            composer.m_TrackedObjectOffset.y = 0;
        }

        if (aiming)
        {
            composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y + RotationY, -1, 1);
            transposer.m_FollowOffset.y = Mathf.Clamp(transposer.m_FollowOffset.y - RotationY, -1,1);
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
        if (aiming)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKPosition(AvatarIKGoal.RightHand, ikRightHand.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, ikRightHand.rotation);

            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, ikLeftHand.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, ikLeftHand.rotation);
        }
        
    }

    protected virtual float Vertical {get { return Input.GetAxis("Vertical"); } }
    
    protected virtual float Horizontal { get { return Input.GetAxis("Horizontal"); } }
    
    protected virtual float RotationX { get { return Input.GetAxis("Mouse X"); } }
    
    protected virtual float RotationY { get { return Input.GetAxis("Mouse Y") * Time.deltaTime; } }



}
