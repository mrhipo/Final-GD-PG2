using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperSpecial : SpecialAttackBehaviour
{

    //Cabeceada para testeo
    public bool active;

    //Cabeceada para testeo
    public override bool Condition
    {
        get
        {
            var b = (enemyStats.currentState & statesCondition) > 0 && active;
            if (b)
            {
                //Cabeceada para testeo
                active = false;
                //Cabeceada para testeo
                FrameUtil.AfterDelay(8, () => active = true);
                //Cabeceada para testeo
                FrameUtil.AfterDelay(4, () => enemyStats.fsm.SetTrigger("SpecialFinish"));
            }
            return b;
        }
    }

}
