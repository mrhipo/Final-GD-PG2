using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFadeOut : MonoBehaviour
{
    private LifeObject _lifeObject;
    public float time;

    void Start()
    {
        _lifeObject = GetComponent<LifeObject>();
        _lifeObject.OnDead += OnDead;
    }

    private void OnDead()
    {
        _lifeObject.OnDead -= OnDead;
        FrameUtil.AfterDelay(time, () => Destroy(gameObject));
    }
}
