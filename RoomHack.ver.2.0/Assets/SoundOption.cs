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
        audioMixer.GetFloat("BGMGroup", out float bgmVolume); //AudioMixer�Őݒ肵��"BGMGroup"���Ăяo��
        BGMSlider.value = bgmVolume;
        audioMixer.GetFloat("SEGroup", out float seVolume);�@ //AudioMixer�Őݒ肵��"SEGrouop"���Ăяo��
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