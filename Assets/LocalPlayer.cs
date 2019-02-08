using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocalPlayer : NetworkBehaviour
{
    public PlayerControllerNetwork pcn;
    public PlayerStats stats;

    public void Start()
    {
        pcn = GetComponentInChildren<PlayerControllerNetwork>(true);
        stats = GetComponentInChildren<PlayerStats>(true);
        pcn.OnRealShoot += OnShoot;
    }
    public override void OnStartLocalPlayer()
    {
        GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>(true).gameObject.SetActive(true);
        GetComponentInChildren<PlayerControllerNetwork>(true).enabled = true;


    }

    private void OnShoot()
    {
        CmdFire();
    }

    [Command]
    public void CmdFire()
    {
        GameObject bullet = GameObject.Instantiate(pcn.bulletNetworkPrefab);
        bullet.GetComponent<BulletNetwork>().Initialize(pcn.bulletSpawn.position, pcn.bulletSpawn.position - pcn.bulletSpawn.right * 100, stats.damage);
        bullet.GetComponent<BulletNetwork>().RpcInitialized(pcn.bulletSpawn.position, pcn.bulletSpawn.position - pcn.bulletSpawn.right * 100, stats.damage);
        NetworkServer.Spawn(bullet);
    }

}
