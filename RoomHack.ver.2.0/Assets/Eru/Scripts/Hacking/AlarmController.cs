using UnityEngine;

public class AlarmController : MonoBehaviour, IUnitHack
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

        if (hackedFlg && GameData.AlarmLv == 1)
        {
            Debug.Log("警報装置停止");
            return;
        }
        else if(hackedFlg && GameData.AlarmLv == 2)
        {

            Debug.Log("警報装置範囲アップ");
        }
        else if(hackedFlg && GameData.AlarmLv == 3)
        {
            Debug.Log("警報装置破壊");
            Destroy(gameObject);
        }
    }

    public void StatusDisp()
    {
        if (!hacked) return;
        if (time <= 0) time = hackTime[GameData.AlarmLv - 1];
        hackedFlg = true;
    }
}
