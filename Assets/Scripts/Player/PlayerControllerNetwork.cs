using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControllerNetwork : MonoBehaviour  , ISpeed {

    public float speed = 5;
    public float mouseSensitivity = 5;
    public float spineRotation = 40;
    public Animator animator;
    public Transform bulletSpawn;
    public Cinemachine.CinemachineVirtualCamera camAim;
    Cinemachine.CinemachineComposer composer;
    Cinemachine.CinemachineTransposer transposer;

    public GameObject bulletNetworkPrefab;

    public Transform spine;
    
    [Header("Ik")]
    public HandIk aimIk;

    bool aiming;

    PlayerInput input = new PlayerInput();

    public bool canMove;

    PlayerStats stats;
    float speedMultiplier = 1;

    bool canShoot = true;

    private void Start()
    {
        stats = GetComponent<PlayerStats>();

        animator = GetComponent<Animator>();

        Mouse.ShowCursor(false);

        GlobalEvent.Instance.AddEventHandler<PlayerDeadEvent>(OnDead);
        //spine = animator.GetBoneTransform(HumanBodyBones.Spine);
        Debug.Log(spine.name);
        composer = camAim.GetCinemachineComponent<Cinemachine.CinemachineComposer>();
        transposer = camAim.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();
    }

    private void OnDead()
    {
        enabled = false;
    }

    void Update () {

        if (!canMove) return;
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
       
        composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y + input.RotationY, -1, 1);
        transposer.m_FollowOffset.y = Mathf.Clamp(transposer.m_FollowOffset.y - input.RotationY, -1,1);
        if (input.Shooting)
            if (canShoot)
                Shoot();

        lastAiming = aiming;
    }

    public Action<Vector3,Vector3> OnRealShoot = delegate { };
    public Action<Vector3> OnRotate = delegate { };

    public void Shoot()
    {
        SoundManager.instance.PlayFX("Normal Shot");

        //(pcn.bulletSpawn.position, pcn.bulletSpawn.position - pcn.bulletSpawn.right * 100, stats.damage

        OnRealShoot(bulletSpawn.position, bulletSpawn.position - bulletSpawn.right * 100);
        //var bullet = Instantiate<Bullet>(bulletNetworkPrefab);
       // bullet.Initialize(bulletSpawn.position, -bulletSpawn.right*100, stats.damage);
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
        if (!enabled) return;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        spine.forward = ray.GetPoint(20) - spine.position;
        spine.Rotate(Vector3.up, spineRotation);
        OnRotate(spine.eulerAngles);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!enabled) return;
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
