using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CommandLineConsole : MonoBehaviour
{

    public InputField input;
    public DebugPanel debugPanel;
    public GameObject helpPanel;

    public Dictionary<string, Action<string>> actionMap = new Dictionary<string, Action<string>>();

    private void Start()
    {
        actionMap.Add("maxhp", MaxHp);
        actionMap.Add("help", Help);
        actionMap.Add("maxmana", MaxMana);
        actionMap.Add("addxp", AddXp);
        actionMap.Add("addcredits", AddCredits);
        actionMap.Add("ihavethepower", AllSkills);
        actionMap.Add("restart", RestartLevel);
    }

    private void AllSkills(string obj)
    {
        GlobalEvent.Instance.Dispatch(new NewSkillEvent((SpellType)0, Color.red, null));
        GlobalEvent.Instance.Dispatch(new NewSkillEvent((SpellType)1, Color.red, null));
        GlobalEvent.Instance.Dispatch(new NewSkillEvent((SpellType)2, Color.red, null));
        GlobalEvent.Instance.Dispatch(new NewSkillEvent((SpellType)3, Color.red, null));
    }

    private void AddCredits(string obj)
    {
        var twoValues = obj.Split(' ');
        int val = 0;
        if (int.TryParse(twoValues[1], out val))
            GlobalEvent.Instance.Dispatch<CreditsPickedEvent>(new CreditsPickedEvent(val));
    }

    private void MaxMana(string obj)
    {
        debugPanel.playerStats.RecoverMp(100000);
    }

    private void Help(string obj)
    {
        helpPanel.SetActive(true);
    }

    private void MaxHp(string obj)
    {
        debugPanel.playerLife.Heal(10000);
    }

    private void AddXp(string obj)
    {
        var twoValues = obj.Split(' ');
        int val = 0;
        if (int.TryParse(twoValues[1], out val))
            GlobalEvent.Instance.Dispatch<ExperiencePickedEvent>(new ExperiencePickedEvent() { amount = val });
    }

    private void RestartLevel(string obj)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SendCommand()
    {
        var twoValues = input.text.Split(' ');
        var key = twoValues.First().ToLower();
        if (actionMap.ContainsKey(key))
        {

            actionMap[key](input.text);
            input.text = "";
        }
    }
}
