using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSkillDispatcher : MonoBehaviour
{
    public string skilName;
    public Color skillColor;
    public GameObject skillObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            GlobalEvent.Instance.Dispatch(new NewSkillEvent(skilName, skillColor, skillObj));
            Destroy(gameObject);
        }
    }

}
