using UnityEngine;

public class WinManager : MonoBehaviour
{
    public LifeObject bossLifeObject;
    public GameObject winPanel;

    void Start() { bossLifeObject.OnDead += WinGame; }

    private void WinGame()
    {
        bossLifeObject.OnDead -= WinGame;
        winPanel.SetActive(true);
        GlobalEvent.Instance.Dispatch(new LevelCompletedEvent() { level = 3 });
    }
}
