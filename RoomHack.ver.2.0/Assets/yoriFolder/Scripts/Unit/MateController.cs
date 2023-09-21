using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MateController : MonoBehaviour
{
    // ���擾
    UnitCore mateCore;

    [SerializeField, Header("�s���ԍ�")]
    public int stateNo = 0;      // �s���ԍ�
    [SerializeField, Header("���\�b�h�p�ėp�ԍ�")]
    public int methodNo = 0;   // ���\�b�h�p�ėp�ԍ�

    SightCheak unitSight;

    private TargetPoint hitsPnt;
    private TargetPoint unitPnt;

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

    Vector3 mousePos;
    Vector3 movePos;

    [SerializeField, Header("���[�_�[�C���Ȃ�true")]
    private bool leader;

    [SerializeField, Header("���[�_�[�̃|�W�V����")]
    private GameObject leaderObj;
    [SerializeField, Header("Mate�̃|�W�V����")]
    public GameObject mateObj;

    [SerializeField, Header("���C�̐ݒ�")]
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
                Debug.Log("�҂��Ɉڍs");

                plRb.velocity = Vector2.zero;
                methodCtr = 1.5f;
                methodNo++;
                break;
            case 1:
                methodCtr -= Time.deltaTime;
                if (methodCtr <= 0)
                {
                    plRb.isKinematic = true;
                    Debug.Log("���S�Ɏ~�܂���");
                    isEm = true;
                    methodNo++;
                }
                break;
            case 2:
                Debug.Log("�����������");
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
                Debug.Log("Shot�Ɉڍs");
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
                ////�G��������Shot�Ɉڍs
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
                    Debug.Log("Shot�Ɉڍs");
                    methodNo = 0;
                    methodCtr = 0;
                    stateNo = (int)State.Shot;
                    isEm = false;
                }
                break;
        }
    }

   
    // ������ʂ̃N���X�ɂ��邻��܂ł͂���
    private Vector3 hitsPos;
    private Vector3 unitPos;
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
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.GetComponent<TargetPoint>() != null)
    //    {
    //        collision.gameObject.GetComponent<TargetPoint>().visited = false;
    //    }
    //}
}
