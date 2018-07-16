﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animation _anim;
   
    private void Start()
    {
        _anim = GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == Layers.player)
        {
            Debug.Log("Entre");
            SoundManager.instance.PlayFX("Door");
            _anim.Play("ANIM_Door Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Layers.player)
        {
            SoundManager.instance.PlayFX("Door");
            _anim.Play("ANIM_Door Close");
        }
    }

}
