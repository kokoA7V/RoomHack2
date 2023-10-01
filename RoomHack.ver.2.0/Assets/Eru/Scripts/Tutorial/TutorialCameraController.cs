using UnityEngine;

public class TutorialCameraController : MonoBehaviour ,IUnitHack
{
    public string[] word;

    public bool randomFlg;

    public bool hacked { get; set; } = false;

    public Sprite icon;

    [Multiline]
    public string titleStr;

    [Multiline]
    public string[] lvStr = new string[2];

    [Multiline]
    public string comentStr;

    [SerializeField]
    private GameObject noiseObj;

    public SpriteRenderer frameSR;

    public Sprite frameSprite;

    [SerializeField]
    private Sprite frameEnemySprite;


    public void StatusDisp()
    {
        Destroy(noiseObj);
    }
}
