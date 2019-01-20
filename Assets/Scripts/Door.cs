using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animation _anim;
    bool _locked;

    public GameObject door;
    public Material doorLocked;
    public Material doorUnLocked;
   
    private void Start()
    {
        _anim = GetComponent<Animation>();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == Layers.player.Index && !_locked)
        {
            SoundManager.instance.PlayFX("Door");
            _anim.Play("ANIM_Door Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Layers.player.Index && !_locked)
        {
            SoundManager.instance.PlayFX("Door");
            _anim.Play("ANIM_Door Close");
        }
    }

    public void LockDoor()
    {
        _locked = true;
        door.GetComponent<Renderer>().material = doorLocked;
    }

    public void UnLockDoor()
    {
        _locked = false;
        door.GetComponent<Renderer>().material = doorUnLocked;
    }

}
