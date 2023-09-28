using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightCheak : MonoBehaviour
{
    private RaycastHit2D[] emHit;
    public bool EnemyCheck()
    {
        // Ray�𐶐�
        Vector3 origin = this.gameObject.transform.position;
        Vector3 diredtion = this.transform.up;
        Ray emCheackray = new Ray(origin, diredtion);

        // Ray��\��
        Debug.DrawRay(emCheackray.origin, emCheackray.direction , Color.blue);

        // ray�̋����𐧌�
        float maxDistance = 0.7f;

        // �����ȊO�ɓ�����悤�ɂ���
        int layerMask = ~(1 << gameObject.layer);

        // ��������������pnt��onj������
        emHit = Physics2D.RaycastAll(emCheackray.origin, emCheackray.direction , maxDistance, layerMask);
        foreach (RaycastHit2D emHits in emHit)
        {
            if (emHits.collider != null)
            {
                Debug.Log(emHits.collider.gameObject.name+"�����m����(EnemyCheck)");
                if (emHits.collider.gameObject.TryGetComponent<IUnitDamage>(out var damageable)) {
                    if (damageable.dmgLayer==0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else return false;
            }
        }
        return false;
    }

    private GameObject unit;
    private Vector3 hitsPos;
    private Vector3 unitPos;
    public int SerchRay(GameObject _obj, int _stateNo)
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
                    Debug.Log("ray��(��)" + hits.collider.gameObject.name + "�ɓ�������");
                    //unit = null;
                    break;
                }
                else
                {
                    Debug.Log("ray��" + hits.collider.gameObject.name + "�ɓ�������");
                    if (unit != null)
                    {
                        unitPos = unit.gameObject.transform.position;
                        hitsPos = hits.collider.gameObject.transform.position;

                        // �����ɂ���ėD������߂�
                        if (Vector2.Distance(unitPos, origin) >= Vector2.Distance(hitsPos, origin))
                        {
                            Debug.Log("��ɓ�������" + unit.gameObject.name + "��荡��������" +
                                    hits.collider.gameObject.name + "�̂ق����D��x��������");
                            unit = hits.collider.gameObject;
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
                        //stateNo = (int)State.Move;
                        return 2;
                    }
                    break;
                }
            }
        }
        return _stateNo;
    }
}
