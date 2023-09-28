using UnityEngine;

public class AriConditionerController : MonoBehaviour, IUnitHack
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

    public SpriteRenderer frameSR;

    public Sprite frameSprite;

    [SerializeField]
    private Sprite frameEnemySprite;

    [SerializeField]
    private float[] hackTime = new float[3];

    private float time;

    private bool hackedFlg = false;

    void Start()
    {

    }

    void Update()
    {
        if (time > 0) time -= Time.deltaTime;
        else if (hackedFlg && time <= 0)
        {
            hacked = false;
            hackedFlg = false;
            frameSR.sprite = frameEnemySprite;
        }

        if (hackedFlg && GameData.AriConditionerLv == 1)
        {
            Debug.Log("エアコン停止");
        }
        else if (hackedFlg && GameData.AriConditionerLv == 2)
        {

            Debug.Log("冷暖房起動");
        }
    }

    public void StatusDisp()
    {
        if (!hacked) return;
        if (time <= 0) time = hackTime[GameData.AriConditionerLv - 1];
        hackedFlg = true;
    }
}
