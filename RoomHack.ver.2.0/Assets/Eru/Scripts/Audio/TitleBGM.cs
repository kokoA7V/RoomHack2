using UnityEngine;

public class TitleBGM : MonoBehaviour
{
    void Awake()
    {
        if(AudioPlay.instance != null) AudioPlay.instance.bgmMute = true;
    }
}
