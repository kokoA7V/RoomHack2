
using UnityEngine;

public class TutorialBGM : MonoBehaviour
{
    void Awake()
    {
        AudioPlay.instance.bgmMute = false;
        AudioPlay.instance.BGMPlay(0);
    }
}
