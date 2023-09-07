using System.Collections;
using UnityEngine;

public class TurretController : MonoBehaviour, IUnitHack
{
    public string[] word;

    public bool randomFlg;

    public bool hacked { get; set; } = false;

    public Sprite icon;

    [Multiline]
    public string titleStr;
    [Multiline]
    public string[] lvStr = new string[2];
    [Multiline]
    public string comentStr;

    public SpriteRenderer frameSR;

    public Sprite frameSprite;

    [SerializeField]
    private GameObject top;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed = 3f;

    [SerializeField]
    private float shotTime = 1f;

    [SerializeField,Header("レイの設定")]
    private RayCircle rayCircle = new RayCircle();


    private bool atkEnemyFlg = false;
    private bool shotFlg = false;

    void Update()
    {
        GameObject obj = rayCircle.CircleChk(transform);

        if (obj == null) return;
        ObjRotation(obj);
        if (!shotFlg) return;

        if (!hacked)
        {
            //if (obj.TryGetComponent<PlayerController>(out PlayerController pc)) StartCoroutine(Shoting());
        }
        else if(atkEnemyFlg)
        {
            //if (obj.TryGetComponent<EnemyController>(out EnemyController ec)) StartCoroutine(Shoting());
        }
    }

    private void ObjRotation(GameObject obj)
    {
        // 敵の位置から自分の位置を引いて、敵を向く方向ベクトルを計算
        Vector3 direction = obj.transform.position - top.transform.position;

        // ベクトルを角度に変換して敵を向く
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(top.transform.rotation, targetRotation, 5f);

        // 目標の角度に対する許容誤差内になると向いたこととみなす
        float currentAngle = transform.eulerAngles.z;
        if (Mathf.Abs(angle - currentAngle) < 3f || Mathf.Abs(angle - currentAngle) > 357f) shotFlg = true;
        else shotFlg = false;
    }

    private IEnumerator Shoting()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector3 shootDirection = Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector3.up;
        rb.velocity = shootDirection * bulletSpeed;

        yield return new WaitForSeconds(shotTime);

        yield break;
    }

    public void StatusDisp()
    {
        atkEnemyFlg = true;
    }
}
