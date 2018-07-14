using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5;
    public float mouseSensitivity = 5;
    public float spineRotation = 40;
    public Animator animator;
    public Transform bulletSpawn;
    public Cinemachine.CinemachineVirtualCamera camAim;
    Cinemachine.CinemachineComposer composer;
    Cinemachine.CinemachineTransposer transposer;

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
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();

        spine = animator.GetBoneTransform(HumanBodyBones.Spine);

        composer = camAim.GetCinemachineComponent<Cinemachine.CinemachineComposer>();
        transposer = camAim.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();
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
       
        composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y + input.RotationY, -1, 1);
        transposer.m_FollowOffset.y = Mathf.Clamp(transposer.m_FollowOffset.y - input.RotationY, -1,1);

        if (canShoot && input.Shooting)
        {
            Shoot();
        }

        lastAiming = aiming;
    }

    private void Shoot()
    {
        var bullet = bullets.GetObjectFromPool().GetComponent<Bullet>();
        bullet.OnDead += () => bullets.DisablePoolObject(bullet.gameObject);

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,500f))
        {
            bullet.Initialize(bulletSpawn.position, hit.point);
        }
        else
        {
            bullet.Initialize(bulletSpawn.position, ray.GetPoint(100));
        }
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
