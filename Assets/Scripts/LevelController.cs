using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : SaveObject
{
    List<Portal> _portals = new List<Portal>();
    int _closedPortals;
    public GameObject loading;

    public Door exitDoor;

	void Start ()
    {
        base.Start();
        exitDoor.LockDoor();
        _portals.AddRange(FindObjectsOfType<Portal>());
        GlobalEvent.Instance.Dispatch(new LevelStartEvent("Close all portals.               Portals closed: " + _closedPortals + " / " + _portals.Count));
        GlobalEvent.Instance.AddEventHandler<PortalClosedEvent>(OnPortalClosed);
	}

    private void OnPortalClosed(PortalClosedEvent portalClosedEvent)
    {
        _closedPortals++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        FindObjectOfType<HudEventHandler>().missionObjetive.text = "Close all portals.               Portals closed: " + _closedPortals + " / " + _portals.Count;
        if (_closedPortals >= _portals.Count)
        {
            _closedPortals = 0;
            FindObjectOfType<HudEventHandler>().missionObjetive.text = "Escapes from the facilities";
            UnlockLevelExit();
        }
    }

    private void UnlockLevelExit()
    {
        exitDoor.UnLockDoor();
        exitDoor.GetComponent<Animation>().Play("ANIM_Door Open");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.player.Index)
        {
            loading.SetActive(true);
            GlobalEvent.Instance.Dispatch(new LevelCompletedEvent { level = 1 });
            SceneManager.LoadScene(3);

        }
    }

    public override void Save()
    {
        SaveData(Key, JsonUtility.ToJson(GetPlayerMemento()));
    }

    public LevelControllerMement GetPlayerMemento()
    {
        return new LevelControllerMement
        {
            closedPortals = this._closedPortals
        };
    }

    public override void Load()
    {
        var playerm = GetValue<LevelControllerMement>();
        _closedPortals = playerm.closedPortals;
        UpdateUI();
    }

    [System.Serializable]
    public class LevelControllerMement
    {
        public int closedPortals;
    }
}
