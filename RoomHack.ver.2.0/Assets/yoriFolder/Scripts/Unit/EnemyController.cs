using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour ,IUnitHack
{
    private Vector3 movePos;
    private Vector2 moveDir;

    // ���擾
    UnitCore eCore;

    [SerializeField, Header("�s���ԍ�")]
    public int stateNo = 0;      // �s���ԍ�
    [SerializeField, Header("���\�b�h�p�ėp�ԍ�")]
    public int methodNo = 0;   // ���\�b�h�p�ėp�ԍ�

    SightCheak emCheak;

    TargetPoint hitsPnt;
    TargetPoint unitPnt;

    private Rigidbody2D plRb;

    // �f���Q�[�h
    // �֐����^�ɂ��邽�߂̂���
    private delegate void ActFunc();

    // �֐��̔z��
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

    [SerializeField, Header("���C�̐ݒ�")]
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
    private float hackTime = 10f;

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
                Debug.Log("Shot�Ɉڍs");
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

                //�G��������Shot�Ɉڍs
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
        if (time <= 0) time = hackTime;
        hackedFlg = true;
        Debug.Log("�n�b�L���O����");
    }

    void ActSearch()
    {
        //switch (methodNo)
        //{
        //    case 0:
        //        if (emCheak.EnemyCheck() && isEm)
        //        {
        //            //Debug.Log("Shot�Ɉڍs");
        //            methodNo = 0;
        //            methodCtr = 0;
        //            stateNo = (int)State.Shot;
        //            isEm = false;
        //        }
        //        break;
        //}
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    //Debug.Log("atattayo");
    //    // �^�[�Q�b�g�|�C���g�����Ă邩�ǂ���
    //    hitsPnt = collision.gameObject.GetComponent<TargetPoint>();
    //    // ���Ă��珈��
    //    if (hitsPnt != null)
    //    {
    //        // Shot���Ȃ珈�����Ȃ�
    //        if (stateNo == (int)State.Shot)
    //        {
    //            return;
    //        }
    //        // Ray�𐶐�
    //        Vector3 origin = this.gameObject.transform.position;
    //        Vector3 diredtion = hitsPnt.gameObject.transform.position - origin;
    //        diredtion = diredtion.normalized;
    //        Ray ray = new Ray(origin, diredtion * 10);

    //        // Ray��\��
    //        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
    //        float maxDistance = 10;
    //        // �����͓�����Ȃ��悤�ɂ���
    //        int layerMask = ~(1 << gameObject.layer);
    //        //LayerMask layerMask = LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer));

    //        // �������������疼�O��Ԃ�
    //        RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, ray.direction * 10, maxDistance, layerMask);
    //        foreach (RaycastHit2D hits in hit)
    //        {
    //            if (hits.collider != null)
    //            {
    //                // �ǂɓ����������unit�̒��g��null�ɂ���
    //                if (hits.collider.gameObject.layer == 8)
    //                {
    //                    Debug.Log("ray��(��)" + hits.collider.gameObject.name + "�ɓ�������");
    //                    unit = null;
    //                    break;
    //                }
    //                else
    //                {
    //                    Debug.Log("ray��" + hits.collider.gameObject.name + "�ɓ�������");
    //                    // unit�̒��g�������Ă�����
    //                    if (unit != null)
    //                    {
    //                        unitPos = unit.gameObject.transform.position;
    //                        hitsPos = hits.collider.gameObject.transform.position;
    //                        // unit�Ǝ����̋�����0.5�ȉ���������moveSpd��0�ɂ���
    //                        if (Mathf.Abs(Vector2.Distance(unitPos, origin)) <= 0.5 &&
    //                            !unit.GetComponent<TargetPoint>().visited)
    //                        {
    //                            Debug.Log(unit + "0.5�ȉ�");
    //                            Debug.Log(Mathf.Abs(Vector2.Distance(unitPos, origin)));
    //                            moveSpd = 0;
    //                            unit.GetComponent<TargetPoint>().visited = true;
    //                            plRb.velocity = Vector2.zero;
    //                        }
    //                        // unit�ƃ��C����������obj���������
    //                        if (unit.gameObject != hits.collider.gameObject)
    //                        {
    //                            TargetPoint hitVis = hits.collider.gameObject.GetComponent<TargetPoint>();

    //                            unit = hits.collider.gameObject;
    //                            // unit�Ǝ����̋�����荡��������point�̋������Z�������炻�����Ɉړ�����
    //                            //if (Mathf.Abs(Vector2.Distance(unitPos, origin)) >=
    //                            //    Mathf.Abs(Vector2.Distance(hitsPos, origin)) &&
    //                            //    !hitVis.visited )
    //                            //{
    //                            //    Debug.Log("��ɓ�������" + unit.gameObject.name + "��荡��������" +
    //                            //    hits.collider.gameObject.name + "�̂ق����D��x��������");
    //                            //    unit = hits.collider.gameObject;
    //                            //    moveSpd = mateCore.moveSpd;
    //                            //    stateNo = (int)State.Move;
    //                            //    break;
    //                            //}
    //                            //else
    //                            //{
    //                            //    Debug.Log("�����������ǂ��Ƃ��Ƃ���" + unit.gameObject.name +
    //                            //            "���D��x�Ⴂ��");
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
    //                            Debug.Log("�������̂Ɠ���������");
    //                        }
    //                    }
    //                    // unit�ɉ����Ȃ�������
    //                    else
    //                    {
    //                        unit = hits.collider.gameObject;
    //                        Debug.Log("�ŏ��ɓ��������I�u�W�F�N�g" + unit.gameObject.name);
    //                        // �ړ����ׂ�obj�ɓ���������Move�Ɉڍs
    //                        stateNo = (int)State.Move;
    //                        break;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
}
