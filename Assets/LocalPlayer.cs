using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LocalPlayer : NetworkBehaviour
{
    public PlayerControllerNetwork pcn;
    public PlayerStats stats;
    public Animator animator;

    Image imageHp;
    public void Start()
    {
        pcn = GetComponentInChildren<PlayerControllerNetwork>(true);
        stats = GetComponentInChildren<PlayerStats>(true);
        stats.IsNetworking = true;
        pcn.OnRealShoot += OnShootDos;
        pcn.OnRotate += OnRotate;
        animator = GetComponentInChildren<Animator>();
        if (!isLocalPlayer)
        {
            animator.enabled = false;
        }
        else
        {
            stats.lifeObject.OnDead += OnDead;

            imageHp = FindObjectOfType<NetworkHud>().imageFill;
            stats.lifeObject.OnLifeChange += () =>
            {
                imageHp.fillAmount = stats.lifeObject.hp.Percentage;
            };
        }
    }

    public void OnDead()
    {
        CmdOnDead();
    }

    [Command]
    public void CmdOnDead()
    {
        StartCoroutine(WaitToRespawn());
        RpcOnDead();
    }

    private IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(3);
        RpcRespawn();
        Respos();
    }

    [ClientRpc]
    public void RpcOnDead()
    {
        pcn.canMove = false;
        pcn.OnRealShoot -= OnShootDos;
        pcn.OnRotate -= OnRotate;
    }

    [ClientRpc]
    public void RpcRespawn()
    {
        pcn.OnRealShoot += OnShootDos;
        pcn.OnRotate += OnRotate;
        pcn.canMove = true;
        animator.SetTrigger("StandUp");
        stats.lifeObject.Heal(1000);
    }

    public void Respos()
    {
        transform.position = FindObjectOfType<NetworkRandomPosition>().RandomPos();
        RpcRespos(transform.position);
    }

    [ClientRpc]
    public void RpcRespos(Vector3 pos)
    {
        transform.position = pos;
    }

    private void OnShootDos(Vector3 o, Vector3 d)
    {
        CmdFire(o , d);
    }

    private void OnRotate(Vector3 obj)
    {
        CmdOnRotate(obj);  
    }

    [Command]
    private void CmdOnRotate(Vector3 v3)
    {
        //pcn.spine.eulerAngles = v3;
        RpcOnRotate(v3);
    }

    [ClientRpc]
    private void RpcOnRotate(Vector3 v3)
    {
        if(pcn!=null && pcn.spine != null)
        pcn.spine.eulerAngles = v3;
    }

    public override void OnStartLocalPlayer()
    {
        GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>(true).gameObject.SetActive(true);
        GetComponentInChildren<PlayerControllerNetwork>(true).enabled = true;
    }

    [Command]
    public void CmdFire(Vector3 o, Vector3 d)
    {
        GameObject bullet = GameObject.Instantiate(pcn.bulletNetworkPrefab);
        bullet.GetComponent<BulletNetwork>().Initialize(o, d, stats.damage);
        NetworkServer.Spawn(bullet);
    }

}
