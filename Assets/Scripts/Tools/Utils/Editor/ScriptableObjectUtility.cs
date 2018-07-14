using UnityEditor;
using UnityEngine;

public static class ScriptableObjectUtility
{

    public static T CreateAsset<T>() where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();
        string assetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/SoundData/" + typeof(T).ToString() + ".asset");

        AssetDatabase.CreateAsset(asset, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        return asset;
    }



}
