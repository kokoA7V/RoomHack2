using UnityEngine;

[System.Serializable]
public class RayCircle
{
    public int numberOfRays = 12; // レイの本数
    public float detectionRange = 5f; // レイキャストの最大距離

    private GameObject obj;
    //private LineRenderer lineRenderer; // レイの可視化用

    //void Start()
    //{
    //    // LineRendererコンポーネントを取得または作成
    //    lineRenderer = GetComponent<LineRenderer>();
    //    if (lineRenderer == null)
    //    {
    //        lineRenderer = gameObject.AddComponent<LineRenderer>();
    //    }

    //    // LineRendererの設定
    //    lineRenderer.positionCount = numberOfRays * 2; // 頂点の数を設定（始点と終点の2倍）
    //    lineRenderer.startWidth = 0.05f; // 開始幅
    //    lineRenderer.endWidth = 0.05f; // 終了幅
    //}

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

            // レイキャストが何かにヒットした場合
            if (hit.collider != null && obj == null) obj = hit.collider.gameObject;
            else obj = null;

            //// レイの始点と終点を設定して可視化
            //lineRenderer.SetPosition(i * 2, transform.position); // 始点
            //lineRenderer.SetPosition(i * 2 + 1, (Vector3)transform.position + (Vector3)(direction * detectionRange)); // 終点
        }
        return obj;
    }
}

