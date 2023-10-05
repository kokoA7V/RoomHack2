using UnityEngine;

public class HomeBGM : MonoBehaviour
{
    void Awake()
    {
        AudioPlay.instance.bgmMute = false;
        AudioPlay.instance.BGMPlay(1);
    }
}
