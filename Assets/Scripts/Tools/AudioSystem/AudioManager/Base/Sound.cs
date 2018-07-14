using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    public float volume;
    public float pitch;
    public float randomVolume;
    public float randomPitch;
    public bool loopable;
    public AudioMixerGroup mixer;

    private AudioSource _audioSource;

    public Sound()
    {
        name = "new sound";
        volume = 1f;
        pitch = 1f;
    }

    public void SetupSound(AudioSource source)
    {
        _audioSource = source;
        _audioSource.clip = clip;
        _audioSource.loop = loopable;
        _audioSource.outputAudioMixerGroup = mixer;
    }

    public void Play()
    {
        _audioSource.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        _audioSource.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2));
        _audioSource.Play();
    }

    public void Stop()
    {
        _audioSource.Stop();
    }

}
