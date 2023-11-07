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

    private float moveSpd;

    public LayerMask obstacleLayer; // 障害物として扱うレイヤー

    private void Start()
    {
        unitRb = GetComponent<Rigidbody2D>();

        InitPos();
    }

    public void UnitMove(float _moveSpd, Vector3 _unit)
    {

        UpdateTargetPos();
        ShortCutTargetPos();

        Debug.Log("動いてる");
        unit = _unit;

        //movePos = _targetPos - this.transform.position;
        movePos = _unit - this.transform.position;
        moveDir = movePos.normalized;


        RaycastHit2D hit = Physics2D.Raycast(transform.position, movePos, 1 , obstacleLayer);

        //if (hit.collider == null)
        //{
        //    float distanceToAlly = Vector3.Distance(transform.position, _unit);
        //    if (distanceToAlly < 2)
        //    {
        //        Vector3 moveAwayDirection = -movePos.normalized;
        //        transform.Translate(moveAwayDirection * moveSpd * Time.deltaTime);
        //    }
        //    // 障害物がない場合、自身を味方に向かって移動
        //    else unitRb.velocity = moveDir * _moveSpd;

        //}
        //else
        //{
        //    // 障害物がある場合、避けるロジックを実装
        //    Vector3 normal = hit.normal; // 衝突した表面の法線ベクトルを取得

        //    // 障害物が左側にある場合
        //    if (normal.x > 0)
        //    {
        //        // 障害物を右に避ける
        //        Vector3 newMoveDirection = new Vector3(movePos.y, -movePos.x, 0);
        //        transform.Translate(newMoveDirection.normalized * _moveSpd * Time.deltaTime);
        //    }
        //    // 障害物が右側にある場合
        //    else if (normal.x < 0)
        //    {
        //        // 障害物を左に避ける
        //        Vector3 newMoveDirection = new Vector3(-movePos.y, movePos.x, 0);
        //        transform.Translate(newMoveDirection.normalized * _moveSpd * Time.deltaTime);
        //    }

        //}
        //unitRb.AddForce(moveDir * _moveSpd);

        unitRb.velocity = moveDir * _moveSpd;

        if (Mathf.Abs(movePos.x) <= 0.2f && Mathf.Abs(movePos.y) <= 0.2f)
        {
            Debug.Log("止まるよ"+gameObject.name);
            unitRb.velocity = Vector2.zero;
            movePos = Vector3.zero;
            //_targetPos = _playerPosHistory.Dequeue();
        }
        // 力の加わる方向に正面を合わせる
        else transform.up = movePos.normalized;

        // スピードに制限をかける
        float speedXTemp = Mathf.Clamp(unitRb.velocity.x, -limitSpeed, limitSpeed);　//X方向の速度を制限
        float speedYTemp = Mathf.Clamp(unitRb.velocity.y, -limitSpeed, limitSpeed);  //Y方向の速度を制限
        unitRb.velocity = new Vector3(speedXTemp, speedYTemp);　　　　　　　　　　　//実際に制限した値を代入
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

                // たぶん引っかかってるのでワープさせちゃう
                transform.position = _targetPos;
            }
            _playerPosHistory.Enqueue(currentPlayerPos);
        }
    }

    void InitPos()
    {
        // 最初は自分の位置を目的地にしておく
        _targetPos = transform.position;
        _playerPosHistory.Enqueue(unit);

        //for (int i = 0; i < PosHistorySize; ++i)
        //{
        //    var guide = Instantiate(posGuidePrefab);
        //    guide.transform.position = transform.position;
        //    _posGuides.Add(guide);
        //}
    }

    // Mateが近くに来た時の処理
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
