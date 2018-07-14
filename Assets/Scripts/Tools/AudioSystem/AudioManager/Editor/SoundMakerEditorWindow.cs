using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;

public class SoundMakerEditorWindow : EditorWindow
{
    private SoundData _soundData;
    private Vector2 _scrollView;

    private List<SoundData> _allSoundManagers;
    private SoundData _soundManagerSetup;

    private Vector2 _scrollPosition;

    private Texture bttnClose;
    private Texture bttnAdd;
    private Texture bttnClipboard;

    private Texture2D backgroundTexture;
    private Rect backgroundSecction;
    private Color backgroundColor = new Color(0.49f, 0.67f, 0.68f);
    private GUISkin _skin;

    [MenuItem("HK Tools/SoundMaker")]
    private static void ShowWindow()
    {
        SoundMakerEditorWindow window = GetWindow<SoundMakerEditorWindow>();
        window.minSize = window.maxSize = new Vector2(614, 680);
        window.Show();
    }

    public static void CloseWindow()
    {
        GetWindow<SoundMakerEditorWindow>().Close();
    }

    private void OnEnable()
    {
        LoadResources();

        string lastLoaded = EditorPrefs.GetString("LastLoaded");

        if (lastLoaded != "")
            _soundManagerSetup = AssetDatabase.LoadAssetAtPath<SoundData>(lastLoaded);


        backgroundTexture = new Texture2D(1, 1);
        backgroundTexture.SetPixel(0, 0, backgroundColor);
        backgroundTexture.Apply();
        backgroundSecction = new Rect(0, 0, 614, 680);
    }

    private void OnDisable()
    {
        if (_soundManagerSetup != null)
            EditorPrefs.SetString("LastLoaded", AssetDatabase.GetAssetPath(_soundManagerSetup));
    }

    private void OnFocus()
    {
        FindAllSoundManagerSetups();
    }

    private void OnGUI()
    {
        GUI.DrawTexture(backgroundSecction, backgroundTexture);

        GUILayout.Label(new GUIContent("Sound Manager", "Customize your sounds!"), _skin.GetStyle("Header"));
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        _soundManagerSetup = (SoundData)EditorGUILayout.ObjectField("SoundManagerSetup:", _soundManagerSetup, typeof(SoundData), false);
        if (GUILayout.Button(new GUIContent(bttnAdd), _skin.GetStyle("Button"), GUILayout.ExpandWidth(false)))
            CreateSoundManagerSetup();

        if (GUI.changed && _soundManagerSetup == null)
            FindAllSoundManagerSetups();

        EditorGUILayout.EndHorizontal();

        if (CheckData())
            DrawCustomConfig();

        Repaint();
    }

    private bool CheckData()
    {
        if (_soundManagerSetup != null)
            return true;

        EditorGUILayout.HelpBox("No data selected", MessageType.Error);

        if (GUILayout.Button(new GUIContent("Create Data"), _skin.GetStyle("Button")))
            CreateSoundManagerSetup();

        DrawAllSoundMagaerSetups();

        return false;
    }

    private void FindAllSoundManagerSetups()
    {
        _allSoundManagers = new List<SoundData>();
        string[] allPaths = AssetDatabase.FindAssets("t:SoundData");

        for (int i = 0; i < allPaths.Length; i++)
        {
            allPaths[i] = AssetDatabase.GUIDToAssetPath(allPaths[i]);
            _allSoundManagers.Add(AssetDatabase.LoadAssetAtPath<SoundData>(allPaths[i]));
        }
    }

    private void DrawAllSoundMagaerSetups()
    {
        for (int i = _allSoundManagers.Count - 1; i >= 0; i--)
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(new GUIContent(_allSoundManagers[i].name), _skin.GetStyle("Button")))
                _soundManagerSetup = _allSoundManagers[i];
            if (GUILayout.Button(new GUIContent(bttnClose), _skin.GetStyle("Button"), GUILayout.ExpandWidth(false)))
            {
                AssetDatabase.MoveAssetToTrash(AssetDatabase.GetAssetPath(_allSoundManagers[i]));
                _allSoundManagers.RemoveAt(i);
            }

            EditorGUILayout.EndHorizontal();
        }

    }

    private void CreateSoundManagerSetup()
    {
        _soundManagerSetup = ScriptableObjectUtility.CreateAsset<SoundData>();
    }

    private void DrawCustomConfig()
    {
        GUILayout.Space(10);

        _soundManagerSetup.name = EditorGUILayout.TextField("Setup Name:", _soundManagerSetup.name);
        CheckSetupName();

        DrawMusicValues();

        GUILayout.Space(10);

        DrawSoundValues();

    }

    private void DrawMusicValues()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Music setup", EditorStyles.boldLabel);
        DrawSoundValues(_soundManagerSetup.music);

        EditorGUILayout.EndVertical();
    }

    private void DrawSoundValues()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Sounds", EditorStyles.boldLabel);

        if (GUILayout.Button(new GUIContent(bttnAdd), _skin.GetStyle("Button"), GUILayout.ExpandWidth(false)))
            AddSound();

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, false, true, GUILayout.Height(320), GUILayout.Width(600));
        DrawSoundList();

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    private void DrawSoundList()
    {
        if (_soundManagerSetup.sounds == null)
            _soundManagerSetup.sounds = new Sound[0];

        if (_soundManagerSetup.sounds.Length == 0)
            EditorGUILayout.HelpBox("No sounds data!", MessageType.Info);

        for (int i = 0; i < _soundManagerSetup.sounds.Length; i++)
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

            DrawSoundValues(_soundManagerSetup.sounds[i]);

            if (GUILayout.Button(new GUIContent(bttnClose), _skin.GetStyle("Button"), GUILayout.ExpandWidth(false)))
                RemoveSound(i);
            if (GUILayout.Button(new GUIContent(bttnClipboard), _skin.GetStyle("Button"), GUILayout.ExpandWidth(false)))
                EditorGUIUtility.systemCopyBuffer = _soundManagerSetup.sounds[i].name;

            GUILayout.Space(10);
            EditorGUILayout.EndHorizontal();
        }
    }

    private void DrawSoundValues(Sound sound)
    {
        EditorGUILayout.BeginVertical();

        sound.name = EditorGUILayout.TextField("Name:", sound.name);
        sound.clip = (AudioClip)EditorGUILayout.ObjectField("Clip:", sound.clip, typeof(AudioClip), false);
        sound.mixer = (AudioMixerGroup)EditorGUILayout.ObjectField("Output:", sound.mixer, typeof(AudioMixerGroup), false);
        sound.volume = EditorGUILayout.Slider("Volume:", sound.volume, 0.1f, 1f);
        sound.pitch = EditorGUILayout.Slider("Pitch:", sound.pitch, 0.5f, 1.5f);
        sound.randomVolume = EditorGUILayout.Slider("RandomVolume:", sound.randomVolume, 0f, 0.5f);
        sound.randomPitch = EditorGUILayout.Slider("RandomPitch:", sound.randomPitch, 0f, 0.5f);
        sound.loopable = EditorGUILayout.Toggle("Loop:", sound.loopable);

        if (sound.clip == null)
            EditorGUILayout.HelpBox("Warning! no clip assigned!", MessageType.Warning);
        else if (sound.volume < 0.15f)
            EditorGUILayout.HelpBox("Warning! volume is to low", MessageType.Warning);
        EditorGUILayout.EndVertical();
    }

    private void AddSound()
    {
        Sound[] temp = (Sound[])_soundManagerSetup.sounds.Clone();
        _soundManagerSetup.sounds = new Sound[temp.Length + 1];
        _soundManagerSetup.sounds[0] = new Sound();

        for (int i = 1; i < _soundManagerSetup.sounds.Length; i++)
            _soundManagerSetup.sounds[i] = temp[i - 1];

    }

    private void RemoveSound(int index)
    {
        Sound[] temp = (Sound[])_soundManagerSetup.sounds.Clone();
        _soundManagerSetup.sounds = new Sound[temp.Length - 1];

        for (int i = 0; i < _soundManagerSetup.sounds.Length; i++)
        {
            if (i < index)
                _soundManagerSetup.sounds[i] = temp[i];
            else if (i >= index)
                _soundManagerSetup.sounds[i] = temp[i + 1];
        }
    }


    private void CheckSetupName()
    {
        string pathAndName = AssetDatabase.GetAssetPath(_soundManagerSetup);
        string[] splitPath = pathAndName.Split('/');

        if (splitPath[splitPath.Length - 1].Split('.')[0] != _soundManagerSetup.name)
            AssetDatabase.RenameAsset(pathAndName, _soundManagerSetup.name);
    }

    private void LoadResources()
    {
        _skin = Resources.Load<GUISkin>("Editor/GUISkin");
        bttnClose = (Texture)Resources.Load("Editor/ButtonClose");
        bttnAdd = (Texture)Resources.Load("Editor/ButtonAdd");
        bttnClipboard = (Texture)Resources.Load("Editor/ButtonClipboard");

    }
}

