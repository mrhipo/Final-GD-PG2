using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{

    public static DialogueSystem instance;
    Text text;

    void Start()
    {
        instance = this;
        text = GetComponent<Text>();
    }

    public static void ShowText(string text, float time)
    {
        instance.text.gameObject.SetActive(true);
        instance.text.text = text;
        FrameUtil.AfterDelay(time, () => instance.text.gameObject.SetActive(false));
    }

}
