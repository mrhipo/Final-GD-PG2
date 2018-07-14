using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatoTest : MonoBehaviour
{
    public Spell spell;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && spell.CanUseSpell)
            spell.UseSpell(transform.position, transform.forward);
    }
}
