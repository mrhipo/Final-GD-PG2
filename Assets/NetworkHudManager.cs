using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkHudManager : MonoBehaviour
{
    [HideInInspector]
    public Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<Text>();
    }

}
