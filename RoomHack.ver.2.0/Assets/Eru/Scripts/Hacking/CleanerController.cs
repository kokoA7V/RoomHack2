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

    [SerializeField, Header("移動速度")]
    private float speed = 3f;

    [SerializeField, Header("攻撃対象レイヤー")]
    private LayerMask layerMask;

    private bool flg = false;

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
            rb.velocity = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * speed;
            flg = false;
        }

        if (hackedFlg && GameData.CleanerLv == 1)
        {
            Debug.Log("停止");
            rb.velocity = Vector2.zero;
            return;
        }
        else if (hackedFlg && GameData.CleanerLv == 2)
        {
            Debug.Log("暴走");
            flg = true;
        }
        else if (hackedFlg && GameData.CleanerLv == 3)
        {
            Debug.Log("爆破");
            Destroy(gameObject);
        }

        Vector2 movementDirection = rb.velocity.normalized;
        float angleInRadians = Mathf.Atan2(movementDirection.y, movementDirection.x);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleInDegrees + 180f));
    }

    public void StatusDisp()
    {
        if (!hacked) return;
        if (time <= 0) time = hackTime[GameData.CleanerLv - 1];
        hackedFlg = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(flg) rb.velocity = 1.5f * speed * new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

        if (collision.gameObject.layer == layerMask) Debug.Log("攻撃");
    }
}
