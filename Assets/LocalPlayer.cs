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
        pcn.OnRealShoot += OnShootDos;
        pcn.OnRotate += OnRotate;
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
