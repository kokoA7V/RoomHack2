using UnityEngine;

public class BackButton : MonoBehaviour
{
    public void OnClick()
    {
        AudioPlay.instance.SEPlay(3);
        FadeManager.Instance.LoadScene("TitleScene", 2.0f);
    }
}
