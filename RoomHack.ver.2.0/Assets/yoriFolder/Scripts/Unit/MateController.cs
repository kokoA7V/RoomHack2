using System.Collections.Generic;
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
    public bool isLeader;

    [SerializeField, Header("リーダーのポジション")]
    private GameObject leaderObj;
    private MateController mateCon;

    private GameObject mateObj;
    [SerializeField, Header("レイの設定")]
    private RayCircle rescuerRayCircle = new RayCircle();

    private static List<string> mNames;

    public string mateName;
    private int nameNum;

    public string leaderTag = "MateLeader"; // リーダーと認識するタグ
    public string MateTag = "Mate";

    public float leaderCheckInterval = 1.0f; // リーダーチェックの間隔

    private float leaderCheckTimer = 0.0f;

    private List<Transform> followers; // 仲間のリスト

    [SerializeField]
    LayerMask lederLayer;
    [SerializeField]
    LayerMask mateLayer;

    [SerializeField, Header("救出対象ならtrue")]
    private bool resFlg = false;
    enum State
    {
        Shot,
        Move,
        Num
    }

    private void Awake()
    {
        mNames = new List<string>() { "John","kokoA7V","OSHO","Eru","NesikoNoNesiko",
            "V","DayBit","Lucy","esuha","Wick","Ethan", "Bond", "Hunt", "A113","Snake" };

        nameNum = Random.Range(0, mNames.Count);

        mateName = mNames[nameNum];
        mNames.RemoveAt(nameNum);
    }
    void Start()
    {
        mateCore = GetComponent<UnitCore>();

        leaderObj = null;
        followers = new List<Transform>();
        isLeader = false;

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

        if (!resFlg) FindNewLeader();
    }

    void Update()
    {
        if (resFlg)
        {
            ResMate();
            return;
        }
        else
        {
            FindNewLeader();

            if (leaderObj == null) SelectNewLeader();

            if (!isLeader) actFuncTbl[leaderObj.GetComponent<MateController>().stateNo]();
            else actFuncTbl[stateNo]();
        }       
    }
    private void ActShot()
    {
        switch (methodNo)
        {
            case 0:
                Debug.Log("Shotに移行" + target);

                plRb.velocity = Vector3.zero;

                mateCore.Shot(mateCore.dmgLayer, pow, burst);

                methodCtr = 1f;
                methodNo++;
                break;
            case 1:
                if (!isLeader)
                {
                    mateCon = leaderObj.GetComponent<MateController>();
                    target = mateCon.target;
                }
                ObjRotation(target);

                plRb.velocity = Vector3.zero;

                if (target == null)
                {
                    methodNo = 0;
                    methodCtr = 0;
                    stateNo = (int)State.Move;
                    isEm = true;
                    break;
                }

                methodCtr -= Time.deltaTime;
                if (methodCtr <= 0)
                {
                    // まだ敵が死んでないならもっかい打つ                    
                    methodNo = 0;
                    methodCtr = 0;
                    stateNo = (int)State.Shot;
                }
                break;
        }
    }
    // 移動して敵がいたらshotに移動する
    private void ActMove()
    {
        Debug.Log("move" + moveSpd + " " + gameObject.name);
        switch (methodNo)
        {
            case 0:
                // リーダーだったらマウスクリックで移動
                if (isLeader)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        mousePos = Input.mousePosition;
                        movePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
                        moveSpd = mateCore.moveSpd;
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
                    //movePos = this.transform.position;
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

    private void ResMate()
    {
        mateObj = rescuerRayCircle.CircleChk();
        if (mateObj!=null)
        {
            resFlg = false;
            FindNewLeader();
        }
    }
    void FindNewLeader()
    {
        GameObject[] leaderCandidates = GameObject.FindGameObjectsWithTag(leaderTag);

        GameObject closestLeader = null;
        float closestDistance = float.MaxValue;

        foreach (var leaderCandidate in leaderCandidates)
        {
            float distanceToCandidate = Vector2.Distance(transform.position, leaderCandidate.transform.position);

            if (distanceToCandidate < closestDistance)
            {
                closestLeader = leaderCandidate;
                closestDistance = distanceToCandidate;
            }
        }

        leaderObj = closestLeader;

        // リーダーかどうかを判定
        isLeader = leaderObj == gameObject;

        // リーダーでない場合、タグをクリア
        if (!isLeader)
        {
            gameObject.layer = LayerMask.NameToLayer("Mate");
            gameObject.tag = "Mate";
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("MateLeader");
        }
    }
    void SelectNewLeader()
    {
        GameObject[] potentialLeaders = GameObject.FindGameObjectsWithTag(MateTag);

        if (potentialLeaders.Length > 0)
        {
            GameObject newLeaderObject = null;

            foreach (var potentialLeader in potentialLeaders)
            {
                if (potentialLeaders.Length == 1 || potentialLeader != gameObject)
                {
                    newLeaderObject = potentialLeader; // リーダーを選出
                    break;
                }
            }

            if (newLeaderObject != null)
            {
                // リーダーを新しいリーダーに設定
                SetLeader(newLeaderObject);
                newLeaderObject.tag = leaderTag; // 新しいリーダータグを付ける
                gameObject.layer = LayerMask.NameToLayer("MateLeader");
            }
        }
    }
    public void SetLeader(GameObject newLeaderObject)
    {
        leaderObj = newLeaderObject;
        isLeader = (leaderObj == gameObject); // リーダーかどうかを再評価

        if (isLeader)
        {
            gameObject.layer = LayerMask.NameToLayer("MateLeader");
            gameObject.tag = leaderTag; // リーダータグを付ける
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Mate");
            gameObject.tag = "Mate"; // タグをクリア
        }
    }
}
