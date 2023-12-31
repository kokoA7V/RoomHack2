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
        audioMixer.GetFloat("BGMVolume", out float bgmVolume); //AudioMixerで設定した"BGMGroup"を呼び出し
        BGMSlider.value = bgmVolume;
        audioMixer.GetFloat("SEVolume", out float seVolume);　 //AudioMixerで設定した"SEGrouop"を呼び出し
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