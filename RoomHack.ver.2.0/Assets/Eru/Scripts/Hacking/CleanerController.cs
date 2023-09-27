using UnityEngine;

public class CleanerController : MonoBehaviour, IUnitHack
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

    private Rigidbody2D rb;

    [SerializeField, Header("ˆÚ“®‘¬“x")]
    private float speed = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * speed;
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

        if (hackedFlg && GameData.CleanerLv == 1)
        {
            Debug.Log("’âŽ~");
            return;
        }
        else if (hackedFlg && GameData.CleanerLv == 2)
        {
            Debug.Log("–\‘–");
        }
        else if (hackedFlg && GameData.CleanerLv == 3)
        {
            Debug.Log("”š”j");
            Destroy(gameObject);
        }
    }

    public void StatusDisp()
    {
        if (!hacked) return;
        if (time <= 0) time = hackTime[GameData.CleanerLv - 1];
        hackedFlg = true;
    }
}
