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

        stateNo = (int)State.Search;

        moveSpd = 10;

        plRb = GetComponent<Rigidbody2D>();

        unitSight = GetComponent<SightCheak>();

        unit = null;
    }

    void Update()
    {
        actFuncTbl[stateNo]();
        //Debug.Log("ugoiteru");
        //ActMove();
        Debug.Log(stateNo);
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
                if (methodCtr<=0)
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
    private void ActMove()
    {
        if ( unit!=null )
        {
            switch (methodNo)
            {
                case 0:
                    Debug.Log("Move");
                    mateCore.Move(moveSpd, unit);
                    ////�G��������Shot�Ɉڍs
                    //if (unitSight.EnemyCheck() && isEm)
                    //{
                    //    methodNo++;
                    //    break;
                    //}
                    break;
                case 1:
                    plRb.velocity = Vector3.zero;
                    Debug.Log("Shot�Ɉڍs");
                    methodNo = 0;
                    methodCtr = 0;
                    isEm = false;
                    stateNo = (int)State.Shot;
                    break;
            }
        }
        else
        {
            methodNo = 0;
            methodCtr = 0;
            stateNo = (int)State.Search;
            isEm = false;
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("atattayo");
        // �^�[�Q�b�g�|�C���g�����Ă邩�ǂ���
        hitsPnt = collision.gameObject.GetComponent<TargetPoint>();
        // ���ĂȂ��Ȃ炻�̂܂ܕԂ�
        if (hitsPnt == null)
        {
            //Debug.Log("���ĂȂ���");
            return;
        }
        if (hitsPnt.visited)
        {
            return;
        }
        // ���Ă�Ȃ烌�C���΂�
        else
        {
            if (stateNo == (int)State.Shot)
            {
                return;
            }
            // Ray�𐶐�
            Vector3 origin = this.gameObject.transform.position;
            Vector3 diredtion = hitsPnt.gameObject.transform.position - origin;
            diredtion = diredtion.normalized;
            Ray ray = new Ray(origin, diredtion * 10);

            // Ray��\��
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
            float maxDistance = 10;
            int layerMask = ~(1 << gameObject.layer);
            //LayerMask layerMask = LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer));

            // �������������疼�O��Ԃ�
            RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, ray.direction * 10, maxDistance, layerMask);
            foreach (RaycastHit2D hits in hit)
            {
                if (hits.collider != null)
                {
                    if (hits.collider.gameObject.layer == 8)
                    {
                        Debug.Log("ray��(��)" + hits.collider.gameObject.name + "�ɓ�������");
                        unit = null;

                        break;
                    }
                    else
                    {
                        Debug.Log("ray��" + hits.collider.gameObject.name + "�ɓ�������");
                        if (unit != null)
                        {
                            unitPos = unit.gameObject.transform.position;
                            hitsPos = hits.collider.gameObject.transform.position;

                            if (Mathf.Abs(Vector2.Distance(unitPos, origin))<=0.5)
                            {
                                if (stateNo==(int)State.Move)
                                {
                                    stateNo = (int)State.Wait;
                                    unit.GetComponent<TargetPoint>().visited = true;
                                }
                            }
                            // �����ɂ���ėD������߂�
                            if (Vector2.Distance(unitPos, origin) <= Vector2.Distance(hitsPos, origin))
                            {
                                if (stateNo==(int)State.Wait)
                                {
                                    if (unit.gameObject!=hits.collider.gameObject)
                                    {
                                        Debug.Log("��ɓ�������" + unit.gameObject.name + "��荡��������" +
                                       hits.collider.gameObject.name + "�̂ق����D��x��������");
                                        unit.GetComponent<TargetPoint>().visited = false;
                                        unit = hits.collider.gameObject;
                                        stateNo = (int)State.Move;
                                    }                                    
                                }                               
                            }
                            else
                            {
                                Debug.Log("�����������ǂ��Ƃ��Ƃ���" + unit.gameObject.name +
                                        "���D��x�Ⴂ��");
                            }
                        }
                        else
                        {
                            unit = hits.collider.gameObject;
                            Debug.Log("�ŏ��ɓ��������I�u�W�F�N�g" + unit.gameObject.name);
                            // �ړ����ׂ�obj�ɓ���������Move�Ɉڍs
                            stateNo = (int)State.Move;
                        }
                        break;
                    }
                }
            }
        }
        unit = collision.gameObject;
}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (unit != null)
        {
            if (unit=collision.gameObject)
            {
                unit = null;
            }
        }        
    }
}
