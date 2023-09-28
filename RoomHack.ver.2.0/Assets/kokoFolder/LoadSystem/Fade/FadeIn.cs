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
        //1�b������FadeIn����
        //FadeIn�����������Ȃ炱����
        //fade.FadeIn(1f);

        //1�b������FadeIn���I�������Scene2�ֈڍs����
        //Scene�ڍs���������Ȃ炱����
        fade.FadeIn(1f, () => SceneManager.LoadScene("Scene2"));
    }
}
