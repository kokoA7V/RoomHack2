using UnityEngine;

public class DebugAudioPlay : MonoBehaviour
{
    public void BGMAudioPlay(int value)
    {
        AudioPlay.instance.BGMPlay(value);
    }

    public void SEAudioPlay(int value)
    {
        AudioPlay.instance.SEPlay(value);
    }
}