using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    [SerializeField] Fade fade;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            In();
        }
    }

    //FadeIn
    public void In()
    {
        //1•b‚©‚¯‚ÄFadeIn‚·‚é
        //FadeIn‚¾‚¯‚µ‚½‚¢‚È‚ç‚±‚Á‚¿
        //fade.FadeIn(1f);

        //1•b‚©‚¯‚ÄFadeIn‚µI‚í‚Á‚½‚çScene2‚ÖˆÚs‚·‚é
        //SceneˆÚs‚à‚µ‚½‚¢‚È‚ç‚±‚Á‚¿
        fade.FadeIn(1f, () => SceneManager.LoadScene("Scene2"));
    }
}
