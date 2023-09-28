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
    // �ړ����ēG��������shot�Ɉړ�����
    private void ActMove()
    {
        GameObject emunit = rayCircle.CircleChk();
        Debug.Log("move" + unit);
        switch (methodNo)
        {
            case 0:
                // ���[�_�[��������}�E�X�N���b�N�ňړ�
                if (leader)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        mousePos = Input.mousePosition;
                        movePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
                    }
                }
                // ������烊�[�_�[�ɂ��Ă���
                else
                {
                    // ���[�_�[���������烊�[�_�[�ɂȂ�
                    if (leaderObj == null)
                    {
                        leader = true;

                        mateObj.GetComponent<MateController>().leaderObj = this.gameObject;
                    }
                    else
                    {
                        movePos = leaderObj.transform.position;
                    }

                    // ������x���[�_�[�ɋ߂Â�����~�܂�
                    if (Mathf.Abs(movePos.x - this.transform.position.x) <= 1f &&
                        Mathf.Abs(movePos.y - this.transform.position.y) <= 1f)
                    {
                        plRb.velocity = Vector2.zero;
                        moveSpd = 0;
                    }
                    else
                    {
                        moveSpd = mateCore.moveSpd;
                    }
                }

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
}
