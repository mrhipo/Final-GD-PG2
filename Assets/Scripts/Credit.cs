using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit : MonoBehaviour, IUpdate 
{
    public int amount;
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
        //sumar creditos
        SoundManager.instance.PlayFX("Item Collected");
        GlobalEvent.Instance.Dispatch(new CreditsPickedEvent(amount));
        Destroy(gameObject);
    }
}
