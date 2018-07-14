using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider fxSlider;

    void Start()
    {
        RestoreValues();
    }

    public void SetMusicVolume(float volume)
    {
        if (volume <= -25) volume = -80;

        SetVolume("MusicVolume", volume);
    }

    public void SetFXVolume(float volume)
    {
        if (volume <= -25) volume = -80;

        SetVolume("FXVolume", volume);
    }

    private void SetVolume(string mixerGroup, float volume)
    {
        audioMixer.SetFloat(mixerGroup, volume);
        PlayerPrefs.SetFloat(mixerGroup, volume);
    }

    private void RestoreValues()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float fxVolume = PlayerPrefs.GetFloat("FXVolume", 0f);

        musicSlider.value = musicVolume;
        fxSlider.value = fxVolume;
    }
}
