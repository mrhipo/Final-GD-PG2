﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour  , ISpeed {

    public float speed = 5;
    public float mouseSensitivity = 5;
    public float spineRotation = 40;
    public Animator animator;
    public Transform bulletSpawn;
    public Cinemachine.CinemachineVirtualCamera camAim;
    Cinemachine.CinemachineComposer composer;
    Cinemachine.CinemachineTransposer transposer;
    public Transform directionBullet;
    Transform spine;

    
    [Header("Ik")]
    public HandIk aimIk;

    bool aiming;

    PlayerInput input = new PlayerInput();

    PlayerStats stats;
    float speedMultiplier = 1;

    bool canShoot = true;

    Pool<GameObject> bullets;


    private void Start()
    {
        bullets = new Pool<GameObject>(5, Bullet.Factory, Bullet.OnInit, Bullet.OnStore, true);
        stats = GetComponent<PlayerStats>();

        animator = GetComponent<Animator>();

        Mouse.ShowCursor(false);

        GlobalEvent.Instance.AddEventHandler<PlayerDeadEvent>(OnDead);
        spine = animator.GetBoneTransform(HumanBodyBones.Spine);

        composer = camAim.GetCinemachineComponent<Cinemachine.CinemachineComposer>();
        transposer = camAim.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();
    }

    private void OnDead()
    {
        enabled = false;
    }

    void Update () {

        //Movement
        animator.SetFloat("Horizontal", input.Horizontal);
        animator.SetFloat("Vertical", input.Vertical*speedMultiplier);
        
        transform.position += (input.Horizontal * transform.right + input.Vertical * transform.forward).normalized * speed * Time.deltaTime * speedMultiplier * (input.Vertical <0?.7f:1); 
        
        //Rotation
        transform.Rotate(Vector3.up, input.RotationX);
        
        //Aiming
        CheckAiming();
    }

    bool lastAiming = false;

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    private void CheckAiming()
    {
        aiming = input.Aiming;

        //On Aimgin Toggle
        if (lastAiming != aiming)
        {
            if (aiming)
            {
                camAim.m_Lens.FieldOfView = 35;
                speedMultiplier = .5f;
            }
            else
            {
                camAim.m_Lens.FieldOfView = 70;
                speedMultiplier = 1;
            }
        }
       
        composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y + input.RotationY * mouseSensitivity, -1, 1);
        transposer.m_FollowOffset.y = Mathf.Clamp(transposer.m_FollowOffset.y - input.RotationY * mouseSensitivity, -1,1);

        if (canShoot && input.Shooting)
        {
            Shoot();
        }

        lastAiming = aiming;
    }

    private void Shoot()
    {
        SoundManager.instance.PlayFX("Normal Shot");
        var bullet = bullets.GetObjectFromPool().GetComponent<Bullet>();
        bullet.BulletDestroy += () => bullets.DisablePoolObject(bullet.gameObject);
        bullet.Initialize(bulletSpawn.position, bulletSpawn.position - directionBullet.right * 100, stats.damage);
        StartCoroutine(ToggleShoot());
    }

    private IEnumerator ToggleShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(stats.attackSpeed);
        canShoot = true;
    }

    private void LateUpdate()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        spine.forward = ray.GetPoint(20) - spine.position;
        spine.Rotate(Vector3.up, spineRotation);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, aimIk.rightHand.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, aimIk.rightHand.rotation);

        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, aimIk.leftHand.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, aimIk.leftHand.rotation);
        
    }
 
}

[System.Serializable]
public class HandIk
{
    public Transform leftHand;
    public Transform rightHand;
}
