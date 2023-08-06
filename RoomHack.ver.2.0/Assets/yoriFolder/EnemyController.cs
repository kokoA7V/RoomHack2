using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3 movePos;
    private Vector2 moveDir;

    // 情報取得
    UnitCore eCore;

    [SerializeField, Header("行動番号")]
    public int stateNo = 0;      // 行動番号
    [SerializeField, Header("メソッド用汎用番号")]
    public int methodNo = 0;   // メソッド用汎用番号

    EnemyCheak emCheak;

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

    enum State
    {
        Shot,
        Search,
        Num
    }
    // Start is called before the first frame update
    void Start()
    {
        eCore = GetComponent<UnitCore>();

        burst = 3;

        actFuncTbl = new ActFunc[(int)State.Num];
        actFuncTbl[(int)State.Shot] = ActShot;
        actFuncTbl[(int)State.Search] = ActSearch;

        stateNo = (int)State.Search;

        moveSpd = 10;

        plRb = GetComponent<Rigidbody2D>();

        emCheak = GetComponent<EnemyCheak>();
        eCore.dmgLayer = emDmgLayer;

        unit = null;
    }

    // Update is called once per frame
    void Update()
    {
        actFuncTbl[stateNo]();
        if (unit!=null)
        {
            movePos = unit.transform.position - this.transform.position;
            moveDir = movePos.normalized;
            transform.up = moveDir;
        }
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
                    stateNo = (int)State.Search;
                    isEm = true;
                }
                break;
        }
    }

    void ActSearch()
    {
        switch (methodNo)
        {
            case 0:
                if (emCheak.EnemyCheck() && isEm)
                {
                    //Debug.Log("Shotに移行");
                    methodNo = 0;
                    methodCtr = 0;
                    stateNo = (int)State.Shot;
                    isEm = false;
                }
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("atattayo");
        // ターゲットポイントがついてるかどうか
        hitsPnt = collision.gameObject.GetComponent<TargetPoint>();
        // ついてないならそのまま返す
        if (hitsPnt == null)
        {
            //Debug.Log("ついてないよ");
            return;
        }
        // ついてるならレイを飛ばす
        else
        {
            //Debug.Log("tuiteruyo");

            // Rayを生成
            Vector3 origin = this.gameObject.transform.position;
            Vector3 diredtion = collision.gameObject.transform.position - origin;
            diredtion = diredtion.normalized;
            Ray ray = new Ray(origin, diredtion * 10);

            // Rayを表示
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
            float maxDistance = 10;
            int layerMask = ~(1 << gameObject.layer);
            //LayerMask layerMask = LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer));

            // 何か当たったら名前を返す
            RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, ray.direction * 10, maxDistance, layerMask);
            foreach (RaycastHit2D hits in hit)
            {
                if (hits.collider != null)
                {
                    if (hits.collider.gameObject.layer == 8)
                    {
                        Debug.Log("rayが" + hits.collider.gameObject.name + "に当たった");
                        unit = null;
                        break;
                    }
                    else
                    {
                        Debug.Log("TagetPointをもつ" + hits.collider.gameObject.name + "に当たった");
                        if (unit != null)
                        {
                            if (unit != hits.collider.gameObject)
                            {
                                unitPnt = unit.GetComponent<TargetPoint>();
                                hitsPnt = hits.collider.gameObject.GetComponent<TargetPoint>();
                               
                                if(hitsPnt != null)
                                {
                                    if (unitPnt.priority <= hitsPnt.priority)
                                    {
                                        Debug.Log("先に当たった" + unitPnt.gameObject.name + "より今当たった" +
                                            hitsPnt.gameObject.name + "のほうが優先度が高いよ");
                                        unit = hits.collider.gameObject;
                                    }
                                    else
                                    {
                                        Debug.Log("当たったけどもともとある" + unitPnt.gameObject.name +
                                            "より優先度低いよ");
                                    }
                                    // 移動すべきobjに当たったらMoveに移行
                                    //methodNo = 0;
                                    //stateNo = (int)State.Move;
                                }
                            }
                        }
                        else
                        {
                            unit = hits.collider.gameObject;
                            Debug.Log("最初に当たったオブジェクト" + unit.gameObject.name);
                            // 移動すべきobjに当たったらMoveに移行
                            //methodNo = 0;
                            //stateNo = (int)State.Move;
                        }
                        break;
                    }
                }
            }
        }
    }
}
