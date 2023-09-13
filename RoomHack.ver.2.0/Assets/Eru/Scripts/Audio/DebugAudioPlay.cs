using UnityEngine;

public class DebugAudioPlay : MonoBehaviour
{
    [SerializeField]
    AudioSource seAudioSource;

    private void Start()
    {
        Debug.Log("SpaceÉLÅ[Ç≈SEçƒê∂");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            seAudioSource.Play();
        }
    }
}