using UnityEngine;

public class MateController : MonoBehaviour
{
    // 情報取得
    UnitCore mateCore;

    [SerializeField, Header("行動番号")]
    public int stateNo = 0;      // 行動番号
    [SerializeField, Header("メソッド用汎用番号")]
    public int methodNo = 0;   // メソッド用汎用番号

    private SightCheak unitSight;
    public GameObject target;

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

    Vector3 mousePos;
    Vector3 movePos;

    [SerializeField, Header("リーダー任命ならtrue")]
    public bool leader;

    [SerializeField, Header("リーダーのポジション")]
    private GameObject leaderObj;
    private MateController mateCon;
    [SerializeField, Header("Mateのポジション")]
    public GameObject mateObj;
    //[SerializeField, Header("レイの設定")]
    //private RayCircle rayCircle = new RayCircle();

    enum State
    {
        Shot,
        Move,
        Num
    }
    void Start()
    {
        mateCore = GetComponent<UnitCore>();

        if (!leader) mateCon = leaderObj.GetComponent<MateController>();

        moveSpd = mateCore.moveSpd;
        mateCore.dmgLayer = 1;
        burst = 3;

        actFuncTbl = new ActFunc[(int)State.Num];
        actFuncTbl[(int)State.Shot] = ActShot;
        actFuncTbl[(int)State.Move] = ActMove;

        stateNo = (int)State.Move;

        //moveSpd = 10;

        plRb = GetComponent<Rigidbody2D>();

        unitSight = GetComponent<SightCheak>();

        unit = null;

        movePos = this.transform.position;
    }

    void Update()
    {
        if (leaderObj == null && !leader)
        {
            leader = true;
            movePos = this.transform.position;
            Debug.Log("リーダー変わったよ");
            if (mateObj != null) mateObj.GetComponent<MateController>().leaderObj = this.gameObject;
        }

        if (!leader) actFuncTbl[leaderObj.GetComponent<MateController>().stateNo]();
        else actFuncTbl[stateNo]();

        Debug.Log("StateNo " + stateNo);
    }
    private void ActShot()
    {
        switch (methodNo)
        {
            case 0:
                Debug.Log("Shotに移行");
                
                plRb.velocity = Vector3.zero;
                
                mateCore.Shot(mateCore.dmgLayer, pow, burst);
                
                methodCtr = 2f;
                methodNo++;
                break;
            case 1:
                if (!leader)
                {
                    mateCon = leaderObj.GetComponent<MateController>();
                    target = mateCon.target;
                }
                ObjRotation(target);

                plRb.velocity = Vector3.zero;

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
    // 移動して敵がいたらshotに移動する
    private void ActMove()
    {
        Debug.Log("move" + unit);
        switch (methodNo)
        {
            case 0:
                // リーダーだったらマウスクリックで移動
                if (leader)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        mousePos = Input.mousePosition;
                        movePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
                    }
                }
                // 違ったらリーダーについていく
                else
                {
                    movePos = leaderObj.transform.position;

                    // ある程度リーダーに近づいたら止まる
                    if (Mathf.Abs(movePos.x - this.transform.position.x) <= 1f &&
                        Mathf.Abs(movePos.y - this.transform.position.y) <= 1f)
                    {
                        plRb.velocity = Vector2.zero;
                        moveSpd = 0;
                    }
                    else moveSpd = mateCore.moveSpd;
                }

                mateCore.Move(moveSpd, movePos);

                ////敵がいたらShotに移行
                target = unitSight.EnemyCheck();
                if (target != null && isEm)
                {
                    movePos = this.transform.position;
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
    private void ObjRotation(GameObject obj)
    {
        if (obj == null) return;
        // 敵の位置から自分の位置を引いて、敵を向く方向ベクトルを計算
        Vector3 direction = obj.transform.position - this.transform.position;

        // ベクトルを角度に変換して敵を向く
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation, 5f);
    }
}
