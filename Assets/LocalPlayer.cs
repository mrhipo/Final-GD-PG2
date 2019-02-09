﻿using System;
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
        pcn.OnRealShoot += OnShootDos;
        pcn.OnRotate += OnRotate;
        animator = GetComponentInChildren<Animator>();
        stats.lifeObject.OnDead += OnDead;
        if (!isLocalPlayer)
        {
            animator.enabled = false;
        }
        else
        {
            imageHp = FindObjectOfType<NetworkHud>().imageFill;
            stats.lifeObject.OnLifeChange += () =>
            {
                imageHp.fillAmount = stats.lifeObject.hp.Percentage;
            };
        }
    }

    private void OnDead()
    {
        CmdOnDead();
    }

    [Command]
    private void CmdOnDead()
    {
        animator.SetTrigger("Dead");
        RpcOnDead();
    }

    [ClientRpc]
    private void RpcOnDead()
    {
        animator.SetTrigger("Dead");
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
        pcn.spine.eulerAngles = v3;
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
