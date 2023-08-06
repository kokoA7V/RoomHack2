using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheak : MonoBehaviour
{
    private RaycastHit2D[] emHit;

    private GameObject pnt;
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
                Debug.Log(emHits.collider.gameObject.name+"�����m����");
                if (emHits.collider.gameObject.TryGetComponent<IUnitDamage>(out var damageable)) return true ;
                else return false;
                //if (pnt != null)
                //{

                //    //pnt�ɓ����Ă�̂Ɠ�����������
                //    if (pnt == emHits.collider.gameObject)
                //    {
                //        return false;
                //    }
                //    else
                //    {
                //        pnt = emHits.collider.gameObject;
                //        return true;
                //    }
                //}
                //// �ŏ��͂������ɗ���
                //else
                //{
                //    pnt = emHits.collider.gameObject;
                //    return true;
                //}
            }
        }
        return false;
    }
}
