using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    //public List<Enemies> enemyType;
    public int amountToSpwan;
    public int spwanRate;
    public float spawnDelay;

    Effects _effects;

	// Use this for initialization
	void Start ()
    {
        //enemyType = new List<Enemies>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SpwanEnemies()
    {
        _effects.gameObject.SetActive(true);
    }
}
