using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour, IUpdate
{
    // Start is called before the first frame update
    public int angle;

    public void Start()
    {
        UpdateManager.instance.AddUpdate(this);
    }

    public void Update()
    {
        transform.Rotate(Vector3.up, angle);
    }

    private void OnTriggerEnter(Collider other)
    {
        UpdateManager.instance.RemoveUpdate(this);
        SoundManager.instance.PlayFX("Item Collected");
        //Evento
        Destroy(gameObject);
    }
}
