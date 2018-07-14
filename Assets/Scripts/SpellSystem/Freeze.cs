using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour, IUpdate
{
    public float duration;

    private float _enemySpeed;
    private float _timer;

    //ToDo: True name.
    //private Enemy owner;

    void Start()
    {
        UpdateManager.instance.AddUpdate(this);

        //owner = GetComponent<Enemy>();
        //ToDo: Chupar speed de enemy
        //_enemySpeed = owner.speed;
        //ToDo: Setear speed del enemigo en 0.
        //owner.speed = 0;
    }

    public void RestartTime() { _timer = 0; }

    public void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= duration)
        {
            //Setear speed del enemigo a la original.
            //owner.speed = _enemySpeed;

            UpdateManager.instance.RemoveUpdate(this);
            Destroy(this);
        }
    }
}
