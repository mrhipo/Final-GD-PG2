using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    List<Portal> _portals = new List<Portal>();
    int _closedPortals;

    public Door exitDoor;

	void Start ()
    {
        _portals.AddRange(FindObjectsOfType<Portal>());

        GlobalEvent.Instance.AddEventHandler<PortalClosedEvent>(OnPortalClosed);
	}

    private void OnPortalClosed(PortalClosedEvent portalClosedEvent)
    {
        _closedPortals++;
        if(_closedPortals >= _portals.Count)
        {
            _closedPortals = 0;
            UnlockLevelExit();
        }
    }

    private void UnlockLevelExit()
    {
        exitDoor.GetComponent<Animation>().Play("ANIM_Door Open");
    }
    
}
