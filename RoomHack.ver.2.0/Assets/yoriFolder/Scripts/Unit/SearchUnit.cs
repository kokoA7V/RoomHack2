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
        // Ray�𐶐�
        Vector3 origin = this.gameObject.transform.position;
        Vector3 diredtion = _obj.gameObject.transform.position - origin;
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
                    //unit = null;
                    break;
                }
                else
                {
                    Debug.Log("ray��" + hits.collider.gameObject.name + "�ɓ�������");
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
                                //    Debug.Log("��ɓ�������" + unitPnt.gameObject.name + "��荡��������" +
                                //        hitsPnt.gameObject.name + "�̂ق����D��x��������");
                                //    unit = hits.collider.gameObject;
                                //}
                                //else
                                //{
                                //    Debug.Log("�����������ǂ��Ƃ��Ƃ���" + unitPnt.gameObject.name +
                                //        "���D��x�Ⴂ��");
                                //}
                            }
                        }
                    }
                    else
                    {
                        unit = hits.collider.gameObject;
                        Debug.Log("�ŏ��ɓ��������I�u�W�F�N�g" + unit.gameObject.name);
                        // �ړ����ׂ�obj�ɓ���������Move�Ɉڍs
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
