using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugStart : MonoBehaviour
{
    [SerializeField] Fade fade;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //���N���b�N������1�b������FadeIn���ALoadScene�Ɉڍs����
            fade.FadeIn(1f, () => SceneManager.LoadScene("LoadScene"));
        }
    }
}
