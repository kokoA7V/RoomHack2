using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour, IUnitHack
{
    // 情報取得
    private UnitCore eCore;

    [SerializeField, Header("行動番号")]
    public int stateNo = 0;      // 行動番号
    [SerializeField, Header("メソッド用汎用番号")]
    public int methodNo = 0;   // メソッド用汎用番号

    private Rigidbody2D plRb;

    // デリゲード
    // 関数を型にするためのもの
    private delegate void ActFunc();

    // 関数の配列
    private ActFunc[] actFuncTbl;

    private float moveSpd;
    private int pow;

    // 
    public GameObject unit;

    private float methodCtr = 0;

    private bool isEm = true;

    private int burst;

    int emDmgLayer = 2;

    public Vector3 unitPos;
    private GameObject target;

    private SightCheak eSight;

    [SerializeField, Header("レイの設定")]
    private RayCircle rayCircle = new RayCircle();

    [SerializeField]
    private Vector2 ptArea1;

    [SerializeField]
    private Vector2 ptArea2;

    [SerializeField]
    private Vector2 ptArea3;

    [SerializeField]
    private Vector2 ptArea4;

    [SerializeField]
    private List<Vector3> ptArea;
    private int ptNum;

    private float ptCtr;
    enum State
    {
        Shot,
        Move,
        Search,
        Num
    }

    public string[] word;

    public bool randomFlg;

    public bool hacked { get; set; } = false;

    public Sprite icon;
    public Sprite frameSprite;
    public SpriteRenderer frameSR;

    [SerializeField]
    private Sprite frameEnemySprite;

    [SerializeField]
    private float[] hackTime = new float[3];

    private float time;

    [Multiline]
    public string titleStr;
    [Multiline]
    public string[] lvStr = new string[2];
    [Multiline]
    public string comentStr;

    private bool hackedFlg = false;

    private bool lv1Hack = false;
    void Start()
    {

        ptArea = new List<Vector3>();
        ptNum = 0;

        eCore = GetComponent<UnitCore>();
        eSight = GetComponent<SightCheak>();
        burst = 3;

        eCore.dmgLayer = 2;

        eCore.maxHP += 7;

        actFuncTbl = new ActFunc[(int)State.Num];
        actFuncTbl[(int)State.Shot] = ActShot;
        actFuncTbl[(int)State.Move] = ActMove;
        actFuncTbl[(int)State.Search] = ActSearch;

        stateNo = (int)State.Move;

        moveSpd = eCore.moveSpd;

        plRb = GetComponent<Rigidbody2D>();

        //emCheak = GetComponent<SightCheak>();
        eCore.dmgLayer = emDmgLayer;

        unit = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0) time -= Time.deltaTime;
        else if (hackedFlg && time <= 0)
        {
            hacked = false;
            hackedFlg = false;
            lv1Hack = false;
            frameSR.sprite = frameEnemySprite;
        }

        if (hackedFlg)
        {
            return;
        }
        actFuncTbl[stateNo]();
    }

    private void ActShot()
    {
        switch (methodNo)
        {
            case 0:
                Debug.Log("Shotに移行");
                eCore.Shot(eCore.dmgLayer, pow, burst);
                methodCtr = 1.5f;
                methodNo++;
                break;
            case 1:
                methodCtr -= Time.deltaTime;
                if (methodCtr <= 0)
                {
                    methodNo = 0;
                    methodCtr = 0;
                    stateNo = (int)State.Move;
                    isEm = true;
                }
                break;
        }
    }

    void ActMove()
    {
        GameObject mateUnit = rayCircle.CircleChk();

        switch (methodNo)
        {
            case 0:
                if (lv1Hack) return;

                if (mateUnit != null)
                {
                    Debug.Log("Move" + moveSpd);
                    if (mateUnit.TryGetComponent<MateController>(out MateController pc))
                        unitPos = mateUnit.transform.position;
                    eCore.Move(moveSpd, unitPos);

                    //敵がいたらShotに移行
                    target = eSight.EnemyCheck();
                    if (target != null && isEm)
                    {
                        methodNo++;
                    }
                }
                else if (unit != null)
                {
                    unitPos = unit.transform.position;
                    eCore.Move(moveSpd, unitPos);

                    //敵がいたらShotに移行
                    target = eSight.EnemyCheck();
                    if (target != null && isEm)
                    {
                        methodNo++;
                    }
                }
                break;
            case 1:
                plRb.velocity = Vector3.zero;
                methodNo = 0;
                methodCtr = 0;
                isEm = false;
                stateNo = (int)State.Shot;
                break;
        }
    }

    public void StatusDisp()
    {
        if (!hacked) return;
        if (time <= 0) time = hackTime[GameData.EnemyLv - 1];
        hackedFlg = true;
        Debug.Log("ハッキング完了");
    }

    void ActSearch()
    {
        switch (methodNo)
        {
            case 0:
                eCore.Move(moveSpd, ptArea[ptNum]);

                if (Mathf.Abs(ptArea1.x - this.transform.position.x) <= 1f &&
                       Mathf.Abs(ptArea1.y - this.transform.position.y) <= 1f)
                {
                    plRb.velocity = Vector2.zero;
                    moveSpd = 0;
                    ptCtr += Time.deltaTime;
                    if (ptCtr <= 3f)
                    {
                        ptCtr = 0;
                        methodNo++;
                    }

                }
                break;
            case 1:

                ptNum++;

                if (ptNum >= ptArea.Count) ptNum = 0;

                methodNo = 0;

                break;
        }
    }
}
