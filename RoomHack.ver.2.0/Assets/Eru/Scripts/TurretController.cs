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
    private Sprite frameEnemySprite;

    [SerializeField]
    private float[] hackTime = new float[3];

    private float time;

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
    private bool hackedFlg = false;

    void Update()
    {
        if (time > 0) time -= Time.deltaTime;
        else if (hackedFlg && time <= 0)
        {
            hacked = false;
            hackedFlg = false;
            frameSR.sprite = frameEnemySprite;
            atkEnemyFlg = false;
        }

        GameObject obj = rayCircle.CircleChk();

        if (atkEnemyFlg && GameData.TurretLv == 1)
        {
            Debug.Log("タレット停止");
            return;
        }
        else if (atkEnemyFlg && GameData.TurretLv == 2)
        {
            //誤動作

        }
        else if (atkEnemyFlg && GameData.TurretLv == 3)
        {
            if (obj == null) return;

            if (obj.TryGetComponent<EnemyController>(out EnemyController ec))
            {
                ObjRotation(obj);
                if (!shotFlg) return;
                StartCoroutine(Shoting());
            }
        }

        if (obj == null) return;
        if (!hacked)
        {
            if (obj.TryGetComponent<MateController>(out MateController pc))
            {
                ObjRotation(obj);
                if (!shotFlg) return;
                StartCoroutine(Shoting());
            }
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
        yield return new WaitForSeconds(shotTime);

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector3 shootDirection = Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector3.up;
        rb.velocity = shootDirection * bulletSpeed;

        yield return new WaitForSeconds(shotTime);

        yield break;
    }

    public void StatusDisp()
    {
        if (!hacked) return;
        if (time <= 0) time = hackTime[GameData.TurretLv - 1];
        hackedFlg = true;
        atkEnemyFlg = true;
    }
}
