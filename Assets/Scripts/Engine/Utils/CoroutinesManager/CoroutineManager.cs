using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
	public static CoroutineManager Instance { get; private set; }

	void Awake()
	{
		Instance = this;
	}
	
	public Coroutine RunCoroutine(IEnumerator enumerator)
	{
		return Instance.StartCoroutine(enumerator);
	}
	
}
