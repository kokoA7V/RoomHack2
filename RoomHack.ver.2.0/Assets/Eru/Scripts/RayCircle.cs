using UnityEngine;

[System.Serializable]
public class RayCircle
{
    [SerializeField, Header("レイの本数")]
    private int numberOfRays = 12;

    [SerializeField, Header("レイの最大距離")]
    private float detectionRange = 5f;

    [SerializeField, Header("対象のレイヤー")]
    private LayerMask layerMask;

    private enum CHK_TYPE
    {
        FULL = 360,
        HALF = 180,
        QUARTER = 90,
    }
    [SerializeField, Header("検知範囲")]
    private CHK_TYPE chkType = CHK_TYPE.FULL;

    private enum GAME_TYPE
    {
        DOWN = 45,
        SIDE = 90,
    }
    [SerializeField, Header("ゲームタイプ"), Tooltip("見下ろし or 横")]
    private GAME_TYPE gameType = GAME_TYPE.DOWN;

    [SerializeField,Header("レイの可視化")]
    private bool rayFlg = false;

    private GameObject obj;

    public GameObject CircleChk(Transform transform)
    {
        obj = null;
        
        int q;
        if (chkType == CHK_TYPE.QUARTER) q = (int)gameType;
        else q = 0;

        for (int i = 0; i < numberOfRays; i++)
        {
            // レイの角度を計算
            float angle = (i * (int)chkType / numberOfRays) + q + transform.rotation.eulerAngles.z;

            // 角度をラジアンに変換
            float radians = angle * Mathf.Deg2Rad;

            // レイの方向ベクトルを計算
            Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            // レイキャストを発射
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, layerMask);

            if(rayFlg) Debug.DrawRay(transform.position, direction * detectionRange, Color.red);

            // レイキャストが何かにヒットした場合
            if (hit.collider != null)
            {
                obj = hit.collider.gameObject;
                break;
            }
            else obj = null;
        }
        return obj;
    }
}

