using System.Collections.Generic;
using System.IO;
using UnityEditor;

public static class EnumGenerator
{
    public static void GenerateEnum(string enumName, string path, List<string> collection)
    {
        string enumGenerator = "public enum " + enumName + "\n{\n";

        for (int i = 0; i < collection.Count; i++)
        {
            enumGenerator += "    " + GenerateEnumKey(collection[i]);
            if (i < collection.Count - 1)
                enumGenerator += ",\n";
        }

        enumGenerator += "\n}";

        string fullPath = path + "/" + enumName + ".cs";

        if (!File.Exists(fullPath))
            File.Create(fullPath).Close();

        File.WriteAllText(fullPath, enumGenerator);
        AssetDatabase.Refresh();
    }

    private static string GenerateEnumKey(string name)
    {
        string[] keySplit = name.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
        string key = "";

        for (int i = 0; i < keySplit.Length; i++)
            key += keySplit[i];

        return key;
    }
}
