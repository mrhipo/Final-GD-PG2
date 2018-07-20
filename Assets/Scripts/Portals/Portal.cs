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
    bool _triggered;

	// Use this for initialization
	void Start ()
    {
        //enemyType = new List<Enemies>();
        _effects = GetComponentInChildren<Effects>();
        _effects.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SpwanEnemies()
    {        
        if (!_triggered)
        {
            _triggered = true;
            _effects.gameObject.SetActive(true);
            SoundManager.instance.PlayFX("Portal Activated");

            FrameUtil.RepeatAction(spawnRate, amountToSpwan, Spawn, ClosePortal);
        }
    }

    void Spawn()
    {
        //Instanciar enemigo.
        print("spwan");
    }

    void ClosePortal()
    {
        _effects.gameObject.SetActive(false);
        SoundManager.instance.StopFX("Portal Activated");
    }

}
