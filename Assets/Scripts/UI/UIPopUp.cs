using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopUp : MonoBehaviour
{
    public Text popUp;
    public string description;

    public void PopOn()
    {
        popUp.text = description;
    }

    public void PopOff()
    {
        popUp.text = "";
    }

}
