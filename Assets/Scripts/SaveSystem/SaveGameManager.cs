using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameManager : MonoBehaviour
{

    public static bool loadGame;
    public static HashSet<int> nameSaved = new HashSet<int>();
    public static Dictionary<int, string> pokePref = new Dictionary<int, string>();

    public static SaveObject[] allSaveObjects;

    public static PlayerMemento playerMemento = null;

    static bool canSaveGame = false;
    private void Start()
    {
        canSaveGame = false;

        allSaveObjects = FindObjectsOfType<SaveObject>();

        if (loadGame)
        {
            StartCoroutine(ReLoadGame());
            loadGame = false;
        }
        else
        {
            canSaveGame = true;
        }

        GlobalEvent.Instance.AddEventHandler<LevelCompletedEvent>(OnCompleteLevel);
        GlobalEvent.Instance.AddEventHandler<LevelStartEvent>(OnLevelStart);
    }

    private void OnLevelStart()
    {
        if(playerMemento != null){
            FindObjectOfType<SavePlayer>().Load(playerMemento,false);
            playerMemento = null;
        }
    }

    private void OnCompleteLevel()
    {
        playerMemento = FindObjectOfType<SavePlayer>().GetPlayerMemento();
    }

    private IEnumerator ReLoadGame()
    {
        yield return new WaitForSeconds(.5f);
        LoadGame("checkpoints");
        yield return new WaitForSeconds(3);
        canSaveGame = true;

    }

    public static void SaveGame(string fileName)
    {
        if (!canSaveGame) return;
        nameSaved.Clear();
        pokePref.Clear();
        foreach (var item in allSaveObjects)
            item.Save();
        SaveFileGame(fileName);
    }

    public static void LoadGame(string fileName)
    {
        PlayerPrefs.DeleteAll();
        pokePref.Clear();
        LoadFileGame(fileName);
        GlobalEvent.Instance.Dispatch<LoadGameEvent>(new LoadGameEvent());
    }

    public static string ReadFileSave(string fileName)
    {
        return File.ReadAllText(GetPath(fileName));
    }

    public static string GetPath(string fileName)
    {
        return Application.dataPath + "/" + fileName + ".txt";
    }

    public static void LoadFileGame(string fileName)
    {
        pokePref = JsonConvert.DeserializeObject<Dictionary<int, string>>(ReadFileSave(fileName));
    }

    public static void SaveFileGame(string fileName)
    {
        File.WriteAllText(GetPath(fileName), JsonConvert.SerializeObject(pokePref, Formatting.Indented));
    }

}
