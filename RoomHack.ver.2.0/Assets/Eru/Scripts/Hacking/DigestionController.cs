using UnityEngine;

public class DigestionController : MonoBehaviour, IUnitHack
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

    [Min(1)]
    public int lv = 1;

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

        if (hackedFlg && GameData.DigestionLv == 1)
        {
            Debug.Log("誤警報");
            return;
        }
        else if (hackedFlg && GameData.DigestionLv == 2)
        {

            Debug.Log("閉じ込め");
        }
        else if (hackedFlg && GameData.DigestionLv == 3)
        {
            Debug.Log("気絶");
        }
    }

    public void StatusDisp()
    {
        if (!hacked) return;
        if (time <= 0) time = hackTime[GameData.DigestionLv - 1];
        hackedFlg = true;
    }
}
