using UnityEngine;

public class DebugAudioPlay : MonoBehaviour
{
    [SerializeField]
    AudioSource seAudioSource;

    private void Start()
    {
        Debug.Log("Space�L�[��SE�Đ�");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            seAudioSource.Play();
        }
    }
}