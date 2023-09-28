using UnityEngine;

public class SightCheak :MonoBehaviour
{


    private RaycastHit2D[] emHit;

    [SerializeField, Header("レイの最大距離")]
    private float detectionRange = 5f;

    [SerializeField, Header("レイの本数")]
    private int numberOfRays = 12;
    public GameObject EnemyCheck()
    {

        for (int i = 0; i < numberOfRays; i++)
        {

            float angle = (i * 90/ numberOfRays) + 45 + this.transform.rotation.eulerAngles.z;
            float radians = angle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            Vector3 origin = this.transform.position;
            
            Ray emCheackray = new Ray(origin, direction);

            // Rayを表示
            Debug.DrawRay(emCheackray.origin, emCheackray.direction, Color.blue);

            // rayの距離を制限
            float maxDistance = 0.7f;

            // 自分以外に当たるようにする
            int layerMask = ~(1 << this.gameObject.layer);

            // 何か当たったらpntにonjを入れる
            emHit = Physics2D.RaycastAll(emCheackray.origin, emCheackray.direction, maxDistance, layerMask);
            foreach (RaycastHit2D emHits in emHit)
            {
                if (emHits.collider != null)
                {
                    Debug.Log(emHits.collider.gameObject.name + "を検知した(EnemyCheck)");
                    if (emHits.collider.gameObject.TryGetComponent<IUnitDamage>(out var damageable))
                    {
                        if (damageable.dmgLayer == 0)
                        {
                            return null;
                        }
                        else
                        {
                            return emHits.collider.gameObject;
                        }
                    }
                    else return null;
                }
            }
        }       
        return null;
    }

    //private GameObject unit;
    //private Vector3 hitsPos;
    //private Vector3 unitPos;
    //public int SerchRay(GameObject _obj, int _stateNo)
    //{
    //    // Rayを生成
    //    Vector3 origin = this.gameObject.transform.position;
    //    Vector3 diredtion = _obj.gameObject.transform.position - origin;
    //    diredtion = diredtion.normalized;
    //    Ray ray = new Ray(origin, diredtion * 10);

    //    // Rayを表示
    //    Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
    //    float maxDistance = 10;
    //    int layerMask = ~(1 << gameObject.layer);
    //    //LayerMask layerMask = LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer));

    //    // 何か当たったら名前を返す
    //    RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, ray.direction * 10, maxDistance, layerMask);
    //    foreach (RaycastHit2D hits in hit)
    //    {
    //        if (hits.collider != null)
    //        {
    //            if (hits.collider.gameObject.layer == 8)
    //            {
    //                Debug.Log("rayが(壁)" + hits.collider.gameObject.name + "に当たった");
    //                //unit = null;
    //                break;
    //            }
    //            else
    //            {
    //                Debug.Log("rayが" + hits.collider.gameObject.name + "に当たった");
    //                if (unit != null)
    //                {
    //                    unitPos = unit.gameObject.transform.position;
    //                    hitsPos = hits.collider.gameObject.transform.position;

    //                    // 距離によって優先を決める
    //                    if (Vector2.Distance(unitPos, origin) >= Vector2.Distance(hitsPos, origin))
    //                    {
    //                        Debug.Log("先に当たった" + unit.gameObject.name + "より今当たった" +
    //                                hits.collider.gameObject.name + "のほうが優先度が高いよ");
    //                        unit = hits.collider.gameObject;
    //                    }
    //                    else
    //                    {
    //                        Debug.Log("当たったけどもともとある" + unit.gameObject.name +
    //                                "より優先度低いよ");
    //                    }
    //                }
    //                else
    //                {
    //                    unit = hits.collider.gameObject;
    //                    Debug.Log("最初に当たったオブジェクト" + unit.gameObject.name);
    //                    // 移動すべきobjに当たったらMoveに移行
    //                    //stateNo = (int)State.Move;
    //                    return 2;
    //                }
    //                break;
    //            }
    //        }
    //    }
    //    return _stateNo;
    //}
}
