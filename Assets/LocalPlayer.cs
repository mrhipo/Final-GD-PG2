using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocalPlayer : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        transform.parent.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>(true).gameObject.SetActive(true);
    }
}
