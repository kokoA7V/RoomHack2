using UnityEngine;

[System.Serializable]
public class RayCircle
{
    [Header("レイの本数")]
    public int numberOfRays = 12;

    [Header("レイの最大距離")]
    public float detectionRange = 5f;

    private GameObject obj;

    public GameObject CircleChk(Transform transform)
    {
        obj = null;

        for (int i = 0; i < numberOfRays; i++)
        {
            // レイの角度を計算
            float angle = i * 360f / numberOfRays;

            // 角度をラジアンに変換
            float radians = angle * Mathf.Deg2Rad;

            // レイの方向ベクトルを計算
            Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            // レイキャストを発射
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange);

            Debug.DrawRay(transform.position, direction * detectionRange, Color.red);

            // レイキャストが何かにヒットした場合
            if (hit.collider != null)
            {
                obj = hit.collider.gameObject;
                break;
            }
            else obj = null;

            //// レイの始点と終点を設定して可視化
            //lineRenderer.SetPosition(i * 2, transform.position); // 始点
            //lineRenderer.SetPosition(i * 2 + 1, (Vector3)transform.position + (Vector3)(direction * detectionRange)); // 終点
        }
        return obj;
    }
}

