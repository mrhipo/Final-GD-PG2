using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Layers : MonoBehaviour
{
    public static MyLayer enemies;
    public static MyLayer player;
    public static MyLayer shootable;

    static Layers()
    {
        enemies = new MyLayer("Enemies");
        player = new MyLayer("Player");
        shootable = new MyLayer("Enemies", "Player", "Door","Camera", "Default");
    }

}

public class MyLayer
{

    private LayerMask mask;
    private int index;

    public MyLayer(params string[] layerName)
    {
        foreach (var layer in layerName)
        {
            index = LayerMask.NameToLayer(layer);
            mask |= 1 << index;

        }

        if (layerName.Length > 1)
            index = -1;

    }

    public LayerMask Mask
    {
        get { return mask; }
    }

    public int Index
    {
        get
        {
            if (index == -1) Debug.LogError("NO NO NO ! Soy un Layer Multiple. No tengo un Valor de indice unico!");
            return index;
        }
    }

}