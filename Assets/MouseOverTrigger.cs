using UnityEngine;
using UnityEngine.Events;

public class MouseOverTrigger : MonoBehaviour
{
    public UnityEvent OnMouseOverEvent;
    public float triggerTime = 3;
    float time;

    private void OnMouseOver()
    {
        time += Time.deltaTime;
        if (time >= triggerTime)
        {
            OnMouseOverEvent.Invoke();
            OnMouseOverEvent.RemoveAllListeners();
            Destroy(this);
        }
    }

}
