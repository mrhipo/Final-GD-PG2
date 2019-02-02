using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocalPlayer : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>(true).gameObject.SetActive(true);
        GetComponentInChildren<PlayerController>(true).enabled = true;

    }
}
