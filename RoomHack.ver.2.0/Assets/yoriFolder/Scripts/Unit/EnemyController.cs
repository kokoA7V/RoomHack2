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

    [Min(1)]
    public int lv = 1;

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
    private List<GameObject> ptArea;
    private int ptNum;

    private float ptCtr;

    private bool turnFlg;
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

    private int moveNo = 0;
    private int i = 0;

    [SerializeField]
    private bool bossFlg = false;

    private bool atkEnemyFlg = false;
    void Start()
    {
        ptNum = 0;

        eCore = GetComponent<UnitCore>();
        eSight = GetComponent<SightCheak>();
        burst = 3;

        eCore.dmgLayer = 2;
        if (bossFlg)
        {
            eCore.maxHP += 5;
        }
        eCore.maxHP += 7;

        pow = 10;

        actFuncTbl = new ActFunc[(int)State.Num];
        actFuncTbl[(int)State.Shot] = ActShot;
        actFuncTbl[(int)State.Move] = ActMove;
        actFuncTbl[(int)State.Search] = ActSearch;

        stateNo = (int)State.Search;

        moveSpd = eCore.moveSpd;

        plRb = GetComponent<Rigidbody2D>();

        //emCheak = GetComponent<SightCheak>();
        eCore.dmgLayer = emDmgLayer;

        unit = null;

        i = 0;
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
        if (lv1Hack && GameData.EnemyLv == 1) return;
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
                else
                {
                    methodNo = 0;
                    stateNo = (int)State.Search;
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
        lv1Hack = true;
        Debug.Log("ハッキング完了");
    }

    void ActSearch()
    {
        GameObject mateUnit = rayCircle.CircleChk();
        if (mateUnit != null)
        {
            unitPos = mateUnit.transform.position;
            methodNo = 0;
            methodCtr = 0;
            stateNo = (int)State.Move;
        }
        switch (methodNo)
        {
            case 0:

                unitPos = ptArea[i].transform.position;
                eCore.Move(moveSpd, unitPos);
                Vector3 unitDis = unitPos - this.gameObject.transform.position;
                if (Mathf.Abs(unitDis.x) <= 0.3f && Mathf.Abs(unitDis.y) <= 0.3f)
                {
                    Debug.Log("止まるよ");
                    plRb.velocity = Vector2.zero;
                    unitPos = Vector3.zero;
                    i++;
                    methodNo++;
                }
                break;
            case 1:
                if (i >= ptArea.Count)
                {
                    i = 0;
                }
                ObjRotation(ptArea[i]);

                if (turnFlg) methodNo = 0;
                break;
        }
    }
    private void ObjRotation(GameObject obj)
    {
        // 敵の位置から自分の位置を引いて、敵を向く方向ベクトルを計算
        Vector3 direction = obj.transform.position - this.transform.position;

        // ベクトルを角度に変換して敵を向く
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation, 2f);

        // 目標の角度に対する許容誤差内になると向いたこととみなす
        float currentAngle = transform.eulerAngles.z;
        if (Mathf.Abs(angle - currentAngle) < 3f && Mathf.Abs(angle - currentAngle) > -3f ||
            Mathf.Abs(angle - currentAngle) > 357f && Mathf.Abs(angle - currentAngle) < 363f) turnFlg = true;
        else turnFlg = false;
    }
}
