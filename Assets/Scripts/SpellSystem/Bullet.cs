﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IUpdate {

    public Vector3 Destination { get; set; }
    public float speed = 10;
    public float lifeTime = 10;

    public Action BulletDestroy = delegate { };

    private Damage damage;

    public void Initialize(Vector3 init, Vector3 dest, float damageAmount)
    {
        damage = damage ?? new Damage(gameObject, damageAmount);
        damage.Amount = damageAmount;

        transform.position = init;
        transform.forward = dest - init;
        if (lifeTimeCoroutine != null) StopCoroutine(lifeTimeCoroutine);
        lifeTimeCoroutine = StartCoroutine(LifeTime());
        UpdateManager.instance.AddUpdate(this);

    }

    void IUpdate.Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
       var hitObject = other.gameObject.GetComponent<HitObject>();
        if (hitObject != null)
        {
            BulletDead();
            hitObject.OnTakeDamage(damage);
        }
    }

    private void BulletDead()
    {
        BulletDestroy();
        StopCoroutine(lifeTimeCoroutine);
        UpdateManager.instance.RemoveUpdate(this);
    }

    Coroutine lifeTimeCoroutine;
    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        BulletDead();
    }

    public static GameObject Factory()
    {
        return GameObject.Instantiate((GameObject)Resources.Load("Prefabs/Spells/Bullet"));
    }

    public static void OnInit(GameObject obj)
    {
        obj.SetActive(true);
        UpdateManager.instance.AddUpdate(obj.GetComponent<Bullet>());
    }

    public static void OnStore(GameObject obj)
    {
        obj.SetActive(false);
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        UpdateManager.instance.RemoveUpdate(obj.GetComponent<Bullet>());
    }

   
}
