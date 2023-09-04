using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchUnit : MonoBehaviour
{
    private TargetPoint hitsPnt;
    private TargetPoint unitPnt;
    private GameObject unit;
    public int SerchRay(GameObject _obj,int _methodNo)
    {
        // Rayを生成
        Vector3 origin = this.gameObject.transform.position;
        Vector3 diredtion = _obj.gameObject.transform.position - origin;
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
                    //unit = null;
                    break;
                }
                else
                {
                    Debug.Log("rayが" + hits.collider.gameObject.name + "に当たった");
                    if (unit != null)
                    {
                        if (unit != hits.collider.gameObject)
                        {
                            unitPnt = unit.GetComponent<TargetPoint>();
                            hitsPnt = hits.collider.gameObject.GetComponent<TargetPoint>();

                            if (hitsPnt != null)
                            {
                                //if (unitPnt.priority <= hitsPnt.priority)
                                //{
                                //    Debug.Log("先に当たった" + unitPnt.gameObject.name + "より今当たった" +
                                //        hitsPnt.gameObject.name + "のほうが優先度が高いよ");
                                //    unit = hits.collider.gameObject;
                                //}
                                //else
                                //{
                                //    Debug.Log("当たったけどもともとある" + unitPnt.gameObject.name +
                                //        "より優先度低いよ");
                                //}
                            }
                        }
                    }
                    else
                    {
                        unit = hits.collider.gameObject;
                        Debug.Log("最初に当たったオブジェクト" + unit.gameObject.name);
                        // 移動すべきobjに当たったらMoveに移行
                        //stateNo = (int)State.Move;
                        return 2;
                    }
                    break;
                }
            }
        }
        return _methodNo;
    }
}
