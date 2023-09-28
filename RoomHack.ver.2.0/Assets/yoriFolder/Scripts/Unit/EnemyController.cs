using UnityEngine;

public class EnemyController : MonoBehaviour, IUnitHack
{
    private Vector3 movePos;
    private Vector2 moveDir;

    // 情報取得
    UnitCore eCore;

    [SerializeField, Header("行動番号")]
    public int stateNo = 0;      // 行動番号
    [SerializeField, Header("メソッド用汎用番号")]
    public int methodNo = 0;   // メソッド用汎用番号

    SightCheak emCheak;

    TargetPoint hitsPnt;
    TargetPoint unitPnt;

    private Rigidbody2D plRb;

    // デリゲード
    // 関数を型にするためのもの
    private delegate void ActFunc();

    // 関数の配列
    private ActFunc[] actFuncTbl;

    private float moveSpd;
    private int pow;

    // 
    private GameObject unit;

    private float methodCtr = 0;

    private bool isEm = true;

    private int burst;

    int emDmgLayer = 2;

    private Vector3 unitPos;
    private Vector3 hitsPos;

    private SightCheak eSight;

    [SerializeField, Header("レイの設定")]
    private RayCircle rayCircle = new RayCircle();

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

    void Start()
    {
        eCore = GetComponent<UnitCore>();
        eSight = GetComponent<SightCheak>();
        burst = 3;

        eCore.dmgLayer = 2;

        actFuncTbl = new ActFunc[(int)State.Num];
        actFuncTbl[(int)State.Shot] = ActShot;
        actFuncTbl[(int)State.Move] = ActMove;
        actFuncTbl[(int)State.Search] = ActSearch;

        stateNo = (int)State.Move;

        moveSpd = eCore.moveSpd;

        plRb = GetComponent<Rigidbody2D>();

        emCheak = GetComponent<SightCheak>();
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
            frameSR.sprite = frameEnemySprite;
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
        GameObject unit = rayCircle.CircleChk();

        switch (methodNo)
        {
            case 0:
                if (unit == null) return;
                Debug.Log("Move" + moveSpd);
                if (unit.TryGetComponent<MateController>(out MateController pc))
                    unitPos = unit.transform.position;
                eCore.Move(moveSpd, unitPos);

                //敵がいたらShotに移行
                if (eSight.EnemyCheck() && isEm)
                {
                    methodNo++;
                    break;
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
        //switch (methodNo)
        //{
        //    case 0:
        //        if (emCheak.EnemyCheck() && isEm)
        //        {
        //            //Debug.Log("Shotに移行");
        //            methodNo = 0;
        //            methodCtr = 0;
        //            stateNo = (int)State.Shot;
        //            isEm = false;
        //        }
        //        break;
        //}
    }
}
