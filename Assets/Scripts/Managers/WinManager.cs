using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public LifeObject bossLifeObject;
    public GameObject winPanel;
    public PlayerController player;

    void Start() { bossLifeObject.OnDead += WinGame; }

    private void WinGame()
    {
        bossLifeObject.OnDead -= WinGame;
        player.enabled = false;
        Mouse.ShowCursor(true);
        winPanel.SetActive(true);
        GlobalEvent.Instance.Dispatch(new LevelCompletedEvent() { level = 3 });
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
