using System.Collections;
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

    SightCheak unitSight;

    private TargetPoint hitsPnt;
    private TargetPoint unitPnt;

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
    private bool leader;

    [SerializeField, Header("リーダーのポジション")]
    private GameObject leaderObj;
    [SerializeField, Header("Mateのポジション")]
    public GameObject mateObj;

    [SerializeField, Header("レイの設定")]
    private RayCircle rayCircle = new RayCircle();


    enum State
    {
        Wait,
        Shot,
        Move,
        Search,
        Num
    }
    void Start()
    {
        mateCore = GetComponent<UnitCore>();

        moveSpd = mateCore.moveSpd;
        mateCore.dmgLayer = 1;
        burst = 3;

        actFuncTbl = new ActFunc[(int)State.Num];
        actFuncTbl[(int)State.Wait] = ActWait;
        actFuncTbl[(int)State.Shot] = ActShot;
        actFuncTbl[(int)State.Move] = ActMove;
        actFuncTbl[(int)State.Search] = ActSearch;

        stateNo = (int)State.Move;

        //moveSpd = 10;

        plRb = GetComponent<Rigidbody2D>();

        unitSight = GetComponent<SightCheak>();

        unit = null;

        movePos = this.transform.position;
    }

    void Update()
    {
        actFuncTbl[stateNo]();
        Debug.Log("StateNo " + stateNo);


    }
    private void ActWait()
    {
        switch (methodNo)
        {
            case 0:
                Debug.Log("待ちに移行");

                plRb.velocity = Vector2.zero;
                methodCtr = 1.5f;
                methodNo++;
                break;
            case 1:
                methodCtr -= Time.deltaTime;
                if (methodCtr <= 0)
                {
                    plRb.isKinematic = true;
                    Debug.Log("完全に止まった");
                    isEm = true;
                    methodNo++;
                }
                break;
            case 2:
                Debug.Log("もう動けるよ");
                plRb.isKinematic = false;
                methodNo = 0;
                break;
        }

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
    private void ActMove()
    {
        Debug.Log("move" + unit);
        switch (methodNo)
        {
            case 0:
                if (leader)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        mousePos = Input.mousePosition;
                        movePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
                    }
                }
                else
                {
                    if (leaderObj == null)
                    {
                        leader = true;
                        mateObj.GetComponent<MateController>().leaderObj = this.gameObject;
                    }
                    movePos = leaderObj.transform.position;
                }
                Debug.Log("Move" + moveSpd);

                mateCore.Move(moveSpd, movePos);
                ////敵がいたらShotに移行
                if (unitSight.EnemyCheck() && isEm)
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
    void ActSearch()
    {
        switch (methodNo)
        {
            case 0:
                if (unitSight.EnemyCheck() && isEm)
                {
                    Debug.Log("Shotに移行");
                    methodNo = 0;
                    methodCtr = 0;
                    stateNo = (int)State.Shot;
                    isEm = false;
                }
                break;
        }
    }

   
    // いずれ別のクラスにするそれまではここ
    private Vector3 hitsPos;
    private Vector3 unitPos;
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    //Debug.Log("atattayo");
    //    // ターゲットポイントがついてるかどうか
    //    hitsPnt = collision.gameObject.GetComponent<TargetPoint>();
    //    // ついてたら処理
    //    if (hitsPnt != null)
    //    {
    //        // Shot中なら処理しない
    //        if (stateNo == (int)State.Shot)
    //        {
    //            return;
    //        }
    //        // Rayを生成
    //        Vector3 origin = this.gameObject.transform.position;
    //        Vector3 diredtion = hitsPnt.gameObject.transform.position - origin;
    //        diredtion = diredtion.normalized;
    //        Ray ray = new Ray(origin, diredtion * 10);

    //        // Rayを表示
    //        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
    //        float maxDistance = 10;
    //        // 自分は当たらないようにする
    //        int layerMask = ~(1 << gameObject.layer);
    //        //LayerMask layerMask = LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer));

    //        // 何か当たったら名前を返す
    //        RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, ray.direction * 10, maxDistance, layerMask);
    //        foreach (RaycastHit2D hits in hit)
    //        {
    //            if (hits.collider != null)
    //            {
    //                // 壁に当たったらのunitの中身をnullにする
    //                if (hits.collider.gameObject.layer == 8)
    //                {
    //                    Debug.Log("rayが(壁)" + hits.collider.gameObject.name + "に当たった");
    //                    unit = null;
    //                    break;
    //                }
    //                else
    //                {
    //                    Debug.Log("rayが" + hits.collider.gameObject.name + "に当たった");
    //                    // unitの中身が入っていたら
    //                    if (unit != null)
    //                    {
    //                        unitPos = unit.gameObject.transform.position;
    //                        hitsPos = hits.collider.gameObject.transform.position;
    //                        // unitと自分の距離が0.5以下だったらmoveSpdを0にする
    //                        if (Mathf.Abs(Vector2.Distance(unitPos, origin)) <= 0.5 &&
    //                            !unit.GetComponent<TargetPoint>().visited)
    //                        {
    //                            Debug.Log(unit + "0.5以下");
    //                            Debug.Log(Mathf.Abs(Vector2.Distance(unitPos, origin)));
    //                            moveSpd = 0;
    //                            unit.GetComponent<TargetPoint>().visited = true;
    //                            plRb.velocity = Vector2.zero;
    //                        }
    //                        // unitとレイが当たったobjが違ったら
    //                        if (unit.gameObject != hits.collider.gameObject)
    //                        {
    //                            TargetPoint hitVis = hits.collider.gameObject.GetComponent<TargetPoint>();

    //                            unit = hits.collider.gameObject;
    //                            // unitと自分の距離より今当たったpointの距離が短かったらそっちに移動する
    //                            //if (Mathf.Abs(Vector2.Distance(unitPos, origin)) >=
    //                            //    Mathf.Abs(Vector2.Distance(hitsPos, origin)) &&
    //                            //    !hitVis.visited )
    //                            //{
    //                            //    Debug.Log("先に当たった" + unit.gameObject.name + "より今当たった" +
    //                            //    hits.collider.gameObject.name + "のほうが優先度が高いよ");
    //                            //    unit = hits.collider.gameObject;
    //                            //    moveSpd = mateCore.moveSpd;
    //                            //    stateNo = (int)State.Move;
    //                            //    break;
    //                            //}
    //                            //else
    //                            //{
    //                            //    Debug.Log("当たったけどもともとある" + unit.gameObject.name +
    //                            //            "より優先度低いよ");
    //                            //    if (!hitVis.visited)
    //                            //    {
    //                            //        Debug.Log(unit.gameObject.name);
    //                            //        unit = hits.collider.gameObject;
    //                            //        moveSpd = mateCore.moveSpd;
    //                            //        stateNo = (int)State.Move;
    //                            //    }                      
    //                            //    break;
    //                            //}

    //                        }
    //                        else
    //                        {
    //                            Debug.Log("同じものと当たったよ");
    //                        }
    //                    }
    //                    // unitに何もなかったら
    //                    else
    //                    {
    //                        unit = hits.collider.gameObject;
    //                        Debug.Log("最初に当たったオブジェクト" + unit.gameObject.name);
    //                        // 移動すべきobjに当たったらMoveに移行
    //                        stateNo = (int)State.Move;
    //                        break;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.GetComponent<TargetPoint>() != null)
    //    {
    //        collision.gameObject.GetComponent<TargetPoint>().visited = false;
    //    }
    //}
}
