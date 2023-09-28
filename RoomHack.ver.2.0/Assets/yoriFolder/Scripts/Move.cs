using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Vector3 movePos;
    
    [SerializeField]
    private float limitSpeed;

    private Rigidbody2D unitRb;

    private Vector3 moveDir;

    private void Start()
    {
        unitRb = GetComponent<Rigidbody2D>();
    }

    public void UnitMove(float _moveSpd,Vector3 _unit)
    {
        Debug.Log("動いてる");
        movePos = _unit - this.transform.position;
        //movePos = _unit.transform.position - this.transform.position;
        moveDir = movePos.normalized;

        //unitRb.AddForce(moveDir * _moveSpd);
        unitRb.velocity = moveDir * _moveSpd;

        if (Mathf.Abs(movePos.x) <= 0.3f && Mathf.Abs(movePos.y) <= 0.3f)
        {
            Debug.Log("止まるよ");
            unitRb.velocity = Vector2.zero;
        }
        // 力の加わる方向に正面を合わせる
        transform.up = movePos.normalized;

        // スピードに制限をかける
        float speedXTemp = Mathf.Clamp(unitRb.velocity.x, -limitSpeed, limitSpeed);　//X方向の速度を制限
        float speedYTemp = Mathf.Clamp(unitRb.velocity.y, -limitSpeed, limitSpeed);  //Y方向の速度を制限
        unitRb.velocity = new Vector3(speedXTemp, speedYTemp);　　　　　　　　　　　//実際に制限した値を代入
    }

    public void DemoMove(float _moveSpd, GameObject _unit)
    {

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (_unit != null)
        {
            movePos = _unit.transform.position - this.transform.position;
            moveDir = movePos.normalized;

            // 力の加わる方向に正面を合わせる
            transform.up = movePos.normalized;
        }
        else if (moveX != 0 || moveZ != 0)
        {
            transform.up = new Vector2(unitRb.velocity.x, unitRb.velocity.y).normalized;
        }

        unitRb.velocity = new Vector2(moveX * _moveSpd, moveZ * _moveSpd);        
        
        float speedXTemp = Mathf.Clamp(unitRb.velocity.x, -limitSpeed, limitSpeed);　//X方向の速度を制限
        float speedYTemp = Mathf.Clamp(unitRb.velocity.y, -limitSpeed, limitSpeed);  //Y方向の速度を制限
        unitRb.velocity = new Vector3(speedXTemp, speedYTemp);　　　　　　　　　　　//実際に制限した値を代入
    }
}
