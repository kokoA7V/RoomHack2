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
        audioMixer.GetFloat("BGMVolume", out float bgmVolume); //AudioMixer�Őݒ肵��"BGMGroup"���Ăяo��
        BGMSlider.value = bgmVolume;
        audioMixer.GetFloat("SEVolume", out float seVolume);�@ //AudioMixer�Őݒ肵��"SEGrouop"���Ăяo��
        SESlider.value = seVolume;
    }

    public void SetBGM(float volume)
    {
        audioMixer.SetFloat("BGMVolume", volume);
    }

    public void SetSE(float volume)
    {
        audioMixer.SetFloat("SEVolume", volume);
    }


}