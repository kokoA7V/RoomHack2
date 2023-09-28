using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour ,IUnitHack
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
    private GameObject left, right;

    public SpriteRenderer leftFrameSR, rightFrameSR;

    public Sprite frameMateSprite;
    [SerializeField]
    private Sprite frameEnemySprite;

    [SerializeField]
    private float hackTime = 10f;

    private float time;

    private BoxCollider2D bc2d;

    private bool flg = false;
    private bool openFlg = false;

    [SerializeField]
    private Transform leftBoard, rightBoard;
    [SerializeField]
    private Transform leftStart, rightStart;

    [SerializeField]
    private float speed;

    private bool hackedFlg = false;

    void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
        bc2d.isTrigger = false;
    }

    private void Update()
    {
        if (time > 0) time -= Time.deltaTime;
        else if (hackedFlg && time <= 0)
        {
            hacked = false;
            hackedFlg = false;
            leftFrameSR.sprite = frameEnemySprite;
            rightFrameSR.sprite = frameEnemySprite;
            bc2d.isTrigger = false;
            if (openFlg)
            {
                StartCoroutine(Move());
                flg = true;
            }
        }
    }

    public void StatusDisp()
    {
        if (flg || !hacked) return;
        if (time <= 0) time = hackTime;
        hackedFlg = true;
        bc2d.isTrigger = !bc2d.isTrigger;
        StartCoroutine(Move());
        flg = true;
    }

    private IEnumerator Move()
    {
        if (!openFlg)
        {
            while (Vector3.Distance(left.transform.position, leftBoard.position) > 0.01f || Vector3.Distance(right.transform.position, rightBoard.position) > 0.01f)
            {
                left.transform.position = Vector3.MoveTowards(left.transform.position, leftBoard.position, speed * Time.deltaTime);
                right.transform.position = Vector3.MoveTowards(right.transform.position, rightBoard.position, speed * Time.deltaTime);
                yield return null;
            }
            openFlg = true;
        }
        else
        {
            while (Vector3.Distance(left.transform.position, leftStart.position) > 0.01f || Vector3.Distance(right.transform.position, rightStart.position) > 0.01f)
            {
                left.transform.position = Vector3.MoveTowards(left.transform.position, leftStart.position, speed * Time.deltaTime);
                right.transform.position = Vector3.MoveTowards(right.transform.position, rightStart.position, speed * Time.deltaTime);
                yield return null;
            }
            openFlg = false;
        }
        flg = false;
        yield break;
    }
}
