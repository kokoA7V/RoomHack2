using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3 movePos;
    private Vector2 moveDir;

    // ���擾
    UnitCore eCore;

    [SerializeField, Header("�s���ԍ�")]
    public int stateNo = 0;      // �s���ԍ�
    [SerializeField, Header("���\�b�h�p�ėp�ԍ�")]
    public int methodNo = 0;   // ���\�b�h�p�ėp�ԍ�

    EnemyCheak emCheak;

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
                    //Debug.Log("Shot�Ɉڍs");
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
        // �^�[�Q�b�g�|�C���g�����Ă邩�ǂ���
        hitsPnt = collision.gameObject.GetComponent<TargetPoint>();
        // ���ĂȂ��Ȃ炻�̂܂ܕԂ�
        if (hitsPnt == null)
        {
            //Debug.Log("���ĂȂ���");
            return;
        }
        // ���Ă�Ȃ烌�C���΂�
        else
        {
            //Debug.Log("tuiteruyo");

            // Ray�𐶐�
            Vector3 origin = this.gameObject.transform.position;
            Vector3 diredtion = collision.gameObject.transform.position - origin;
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
                        Debug.Log("ray��" + hits.collider.gameObject.name + "�ɓ�������");
                        unit = null;
                        break;
                    }
                    else
                    {
                        Debug.Log("TagetPoint������" + hits.collider.gameObject.name + "�ɓ�������");
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
                                        Debug.Log("��ɓ�������" + unitPnt.gameObject.name + "��荡��������" +
                                            hitsPnt.gameObject.name + "�̂ق����D��x��������");
                                        unit = hits.collider.gameObject;
                                    }
                                    else
                                    {
                                        Debug.Log("�����������ǂ��Ƃ��Ƃ���" + unitPnt.gameObject.name +
                                            "���D��x�Ⴂ��");
                                    }
                                    // �ړ����ׂ�obj�ɓ���������Move�Ɉڍs
                                    //methodNo = 0;
                                    //stateNo = (int)State.Move;
                                }
                            }
                        }
                        else
                        {
                            unit = hits.collider.gameObject;
                            Debug.Log("�ŏ��ɓ��������I�u�W�F�N�g" + unit.gameObject.name);
                            // �ړ����ׂ�obj�ɓ���������Move�Ɉڍs
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
