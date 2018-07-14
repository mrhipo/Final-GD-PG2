﻿using UnityEngine;

public class FreezeSpell : MonoBehaviour, IUpdate
{
    public float power;
    public float speed;

    void Start()
    {
        UpdateManager.instance.AddUpdate(this);
    }

    void IUpdate.Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        UpdateManager.instance.RemoveUpdate(this);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //ToDo: DoDamage.
            //collision.gameObject.GetComponent<Enemy>().DoDamage(power);

            //ToDo: Do Freeze.
            Freeze freeze = collision.gameObject.GetComponent<Freeze>();

            if (freeze)
                freeze.RestartTime();
            else
            {
                freeze = collision.gameObject.AddComponent<Freeze>();
                freeze.duration = power * 2f;
            }
        }

        Destroy(gameObject);
    }


}
