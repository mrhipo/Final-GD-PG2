using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBoss : Portal
{

    int deadCounter;

    public override void OnKilledEnemy()
    {
        deadCounter++;
        if(deadCounter == amountToSpwan)
        {
            triggered = false;
            SpwanEnemies();
            deadCounter = 0;
        }
    }

}

