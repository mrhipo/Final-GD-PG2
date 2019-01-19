using UnityEngine;
using UnityEngine.Events;

public class MouseOverTrigger : MonoBehaviour
{
    public UnityEvent OnMouseOverEvent;
    public float triggerTime = 3;
    float time;

    bool over;

    private void OnMouseOver()
    {
        time += Time.deltaTime;
        over = true;
        if (time >= triggerTime)
        {
            time = triggerTime;
            OnMouseOverEvent.Invoke();
            OnMouseOverEvent.RemoveAllListeners();
            Destroy(this);
        }
    }

    private void OnMouseExit()
    {
        over = false;
    }

    private void OnGUI()
    {
        if(over)
            GUI.Label(new Rect(0, Screen.height-30, Screen.width, Screen.height), "Scanning %" + Mathf.Round(100*(time / triggerTime)));        
    }
}
