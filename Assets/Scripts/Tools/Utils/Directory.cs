using UnityEngine;

public static class Directory
{
    public static string achievementsEnumDataPath = Application.dataPath + "/Scripts/Tools/AchievementSystem/Achievement/Base";
    public static string achievementsDataPath = Application.streamingAssetsPath + "/AchievementData/Achievement.json";

    public static string localizationEnumDataPath = Application.dataPath + "/Scripts/Tools/LocalizationManagerSystem/LocalizationManager/Base";
    public static string localizationKeysDataPath = Application.streamingAssetsPath + "/LocalizationData/Keys/LocalizationKeys.json";
    public static string localizationDataFolder = Application.streamingAssetsPath + "/LocalizationData/Data";

    public static string soundManagerDataFolder = Application.streamingAssetsPath + "/SoundManager/Data";
}
