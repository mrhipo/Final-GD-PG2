using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    Light light;
    public float randomValue = .8f;

    void Start()
    {
        light = GetComponent<Light>();    
    }

    void Update()
    {
        if (Random.value > randomValue)
            light.intensity = Random.value;
    }
}
