using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Vector3 movePos;
    private Vector2 inputPos;
    
    [SerializeField]
    private float limitSpeed;

    private Rigidbody2D unitRb;

    private Vector2 moveDir;

    private float moveCtr = 0;

    private void Start()
    {
        unitRb = GetComponent<Rigidbody2D>();
    }

    public void UnitMove(float _moveSpd,GameObject _unit)
    {
        movePos = _unit.transform.position - this.transform.position;
        moveDir = movePos.normalized;

        moveCtr += Time.deltaTime;
      
        unitRb.AddForce(moveDir * _moveSpd);

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
        else if(moveX != 0||moveZ != 0)
        {
            transform.up = new Vector2(moveX , moveZ ).normalized;
        }

        // 左右移動
        //float keyX = 0;
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    inputPos.x = -1;
        //}
        //else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    inputPos.x = 1;
        //}
        //else
        //{
        //    inputPos.x = 0;
        //}

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    inputPos.y = 1;
        //}
        //else if (Input.GetKeyDown(KeyCode.S))
        //{
        //    inputPos.y = -1;
        //}
        //else
        //{
        //    inputPos.y = 0;
        //}
       

        //Debug.Log(inputPos.normalized * _moveSpd );
        unitRb.velocity = new Vector2(moveX * _moveSpd, moveZ * _moveSpd);
        
        
        float speedXTemp = Mathf.Clamp(unitRb.velocity.x, -limitSpeed, limitSpeed);　//X方向の速度を制限
        float speedYTemp = Mathf.Clamp(unitRb.velocity.y, -limitSpeed, limitSpeed);  //Y方向の速度を制限
        unitRb.velocity = new Vector3(speedXTemp, speedYTemp);　　　　　　　　　　　//実際に制限した値を代入
    }
}
