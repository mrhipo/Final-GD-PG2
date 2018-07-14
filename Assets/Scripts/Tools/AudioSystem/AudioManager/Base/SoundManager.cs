using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioMixerGroup auxioMixer;
    public SoundData soundData;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        Initialize();
        InitializeSound(soundData.music, "Level music");

        PlayMusic();
    }

    private void Initialize()
    {

        for (int i = 0; i < soundData.sounds.Length; i++)
            InitializeSound(soundData.sounds[i], "Sound_" + i + "_" + soundData.sounds[i].name);
    }

    private void InitializeSound(Sound sound, string soundName)
    {
        GameObject fx = new GameObject(soundName);
        fx.transform.parent = this.transform;
        sound.SetupSound(fx.AddComponent<AudioSource>());
    }

    public void PlayMusic()
    {
        soundData.music.Play();
    }

    public void PlayFX(string name)
    {

        foreach (Sound item in soundData.sounds)
        {
            if (item.name == name)
            {
                item.Play();
                return;
            }
        }
    }

    public void StopMusic()
    {
        soundData.music.Stop();
    }

}
