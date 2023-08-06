using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheak : MonoBehaviour
{
    private RaycastHit2D[] emHit;

    private GameObject pnt;
    public bool EnemyCheck()
    {
        // Ray‚ğ¶¬
        Vector3 origin = this.gameObject.transform.position;
        Vector3 diredtion = this.transform.up;
        Ray emCheackray = new Ray(origin, diredtion);

        // Ray‚ğ•\¦
        Debug.DrawRay(emCheackray.origin, emCheackray.direction , Color.blue);

        // ray‚Ì‹——£‚ğ§ŒÀ
        float maxDistance = 0.7f;

        // ©•ªˆÈŠO‚É“–‚½‚é‚æ‚¤‚É‚·‚é
        int layerMask = ~(1 << gameObject.layer);

        // ‰½‚©“–‚½‚Á‚½‚çpnt‚Éonj‚ğ“ü‚ê‚é
        emHit = Physics2D.RaycastAll(emCheackray.origin, emCheackray.direction , maxDistance, layerMask);
        foreach (RaycastHit2D emHits in emHit)
        {
            if (emHits.collider != null)
            {
                Debug.Log(emHits.collider.gameObject.name+"‚ğŒŸ’m‚µ‚½");
                if (emHits.collider.gameObject.TryGetComponent<IUnitDamage>(out var damageable)) return true ;
                else return false;
                //if (pnt != null)
                //{

                //    //pnt‚É“ü‚Á‚Ä‚é‚Ì‚Æ“¯‚¶‚¾‚Á‚½‚ç
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
                //// Å‰‚Í‚±‚Á‚¿‚É—ˆ‚é
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
