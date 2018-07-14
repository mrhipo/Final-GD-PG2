using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Utils
{
    #region JsonUtility

    public static void SaveData<T>(string path, T o)
    {
        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }

        File.WriteAllText(path, JsonUtility.ToJson(o));
    }

    public static void SaveListData<T>(string path, List<T> o)
    {
        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }

        string json = "";

        for (int i = 0; i < o.Count; i++)
            json += JsonUtility.ToJson(o[i]);

        File.WriteAllText(path, json);
    }

    public static T LoadData<T>(string path)
    {
        if (!File.Exists(path))
            throw new System.Exception("File not exist!");

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(json);
    }

    public static List<T> LoadListData<T>(string path)
    {
        List<T> l = new List<T>();

        if (File.Exists(path))
        {
            string[] json = File.ReadAllText(path).Split(new char[] { '}' }, System.StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < json.Length; i++)
                l.Add(JsonUtility.FromJson<T>(json[i] + "}"));
        }

        return l;
    }

    #endregion

    #region SplitUtility

    public static string GenerateSpriteAssetPath(string completePath)
    {
        string[] splittedPath = completePath.Split(new char[] { '/' }, System.StringSplitOptions.RemoveEmptyEntries);

        string path = "";

        for (int i = 2; i < splittedPath.Length; i++)
        {
            if (i > 2)
                path += "/";
            path += splittedPath[i];
        }

        return path.Split(new char[] { '.' })[0];
    }

    #endregion
}
