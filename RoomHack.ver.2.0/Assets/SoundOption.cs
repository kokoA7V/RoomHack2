using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider BGMSlider;
    public Slider SESlider;

    private void Start()
    {
        audioMixer.GetFloat("BGMGroup", out float bgmVolume); //AudioMixer‚Åİ’è‚µ‚½"BGMGroup"‚ğŒÄ‚Ño‚µ
        BGMSlider.value = bgmVolume;
        audioMixer.GetFloat("SEGroup", out float seVolume);@ //AudioMixer‚Åİ’è‚µ‚½"SEGrouop"‚ğŒÄ‚Ño‚µ
        SESlider.value = seVolume;
    }

    public void SetBGM(float volume)
    {
        audioMixer.SetFloat("BGMGroup", volume);
    }

    public void SetSE(float volume)
    {
        audioMixer.SetFloat("SEGroup", volume);
    }


}