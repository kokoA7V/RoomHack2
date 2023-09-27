using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Vector3 movePos;

    [SerializeField]
    private float limitSpeed;

    private Rigidbody2D unitRb;

    private Vector3 moveDir;

    const int PosHistorySize = 16;
    private Queue<Vector3> _playerPosHistory = new Queue<Vector3>();

    private Vector3 _targetPos;
    private Vector3 _prevPlayerPos;

    private Vector3 unit;
    private void Start()
    {
        unitRb = GetComponent<Rigidbody2D>();

        InitPos();
    }

    public void UnitMove(float _moveSpd, Vector3 _unit)
    {

        UpdateTargetPos();
        ShortCutTargetPos();

        Debug.Log("�����Ă�");
        unit = _unit;

        //movePos = _targetPos - this.transform.position;
        movePos = _unit - this.transform.position;
        moveDir = movePos.normalized;

        //unitRb.AddForce(moveDir * _moveSpd);
        unitRb.velocity = moveDir * _moveSpd;

        if (Mathf.Abs(movePos.x) <= 0.5f && Mathf.Abs(movePos.y) <= 0.5f)
        {
            Debug.Log("�~�܂��");
            unitRb.velocity = Vector2.zero;
            //_targetPos = _playerPosHistory.Dequeue();
        }
        // �͂̉��������ɐ��ʂ����킹��
        transform.up = movePos.normalized;

        // �X�s�[�h�ɐ�����������
        float speedXTemp = Mathf.Clamp(unitRb.velocity.x, -limitSpeed, limitSpeed);�@//X�����̑��x�𐧌�
        float speedYTemp = Mathf.Clamp(unitRb.velocity.y, -limitSpeed, limitSpeed);  //Y�����̑��x�𐧌�
        unitRb.velocity = new Vector3(speedXTemp, speedYTemp);�@�@�@�@�@�@�@�@�@�@�@//���ۂɐ��������l����
    }

    void UpdateTargetPos()
    {
        Vector3 currentPlayerPos = unit;
        if (Vector3.Distance(currentPlayerPos, _prevPlayerPos) > 1.5f)
        {
            _prevPlayerPos = currentPlayerPos;
            if (_playerPosHistory.Count >= PosHistorySize)
            {
                _targetPos = _playerPosHistory.Dequeue();

                // ���Ԃ�����������Ă�̂Ń��[�v�������Ⴄ
                transform.position = _targetPos;
            }
            _playerPosHistory.Enqueue(currentPlayerPos);
        }
    }

    void InitPos()
    {
        // �ŏ��͎����̈ʒu��ړI�n�ɂ��Ă���
        _targetPos = transform.position;
        _playerPosHistory.Enqueue(unit);

        //for (int i = 0; i < PosHistorySize; ++i)
        //{
        //    var guide = Instantiate(posGuidePrefab);
        //    guide.transform.position = transform.position;
        //    _posGuides.Add(guide);
        //}
    }

    // Mate���߂��ɗ������̏���
    void ShortCutTargetPos()
    {
        if (Vector3.Distance(transform.position, unit) < 7.0f &&
            _playerPosHistory.Count >= 4)
        {
            while (_playerPosHistory.Count > 4)
            {
                _playerPosHistory.Dequeue();
            }
            _targetPos = _playerPosHistory.Peek();
        }
    }
}
