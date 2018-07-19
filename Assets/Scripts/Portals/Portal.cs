using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    //public List<Enemies> enemyType;
    public int amountToSpwan;
    public float spawnRate;
    public float spawnDelay;

    Effects _effects;
    int _count;
    bool _triggered;

	// Use this for initialization
	void Start ()
    {
        //enemyType = new List<Enemies>();
        _effects = GetComponentInChildren<Effects>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SpwanEnemies()
    {
        if (!_triggered)
        {
            _effects.gameObject.SetActive(true);
            SoundManager.instance.PlayFX("Portal Activated");
            FrameUtil.AfterDelay(spawnDelay, null);
            while(_count < amountToSpwan)
            {
                Spawn();
                _count++;
                FrameUtil.AfterDelay(spawnRate, null);
            }
            _count = 0;
            _effects.gameObject.SetActive(false);
            SoundManager.instance.StopFX("Portal Activated");
        }
    }

    void Spawn()
    {
        //Instanciar enemigo.
        print("spwan" + _count);
    }

}
