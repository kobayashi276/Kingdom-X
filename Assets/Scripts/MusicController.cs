using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    const string mixer_music = "Music";
    const string mixer_sfx = "SFX";
    // Start is called before the first frame update
    void Start()
    {
        float musicValue = 0;
        audioMixer.GetFloat("Music",out musicValue);
        musicSlider.value = musicValue;

        musicValue = 0;
        audioMixer.GetFloat("SFX",out musicValue);
        sfxSlider.value = musicValue;
    }

    public void SetVolumneMusic(float volumne)
    {
        Debug.Log(musicSlider.value);
        audioMixer.SetFloat("Music", musicSlider.value);
    }

    public void SetVolumneSFX(float volumne)
    {
        Debug.Log(musicSlider.value);
        audioMixer.SetFloat("SFX", sfxSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
