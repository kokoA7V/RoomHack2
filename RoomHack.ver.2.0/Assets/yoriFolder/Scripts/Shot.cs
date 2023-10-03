using System.Collections;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed = 5f;

    [SerializeField]
    private float shotTime = 0.3f;

    public void UnitShot(int layer, int pow , int burst)
    {
        StartCoroutine(enumerator(layer, pow, burst));
    }

    IEnumerator enumerator(int layer, int pow, int burst)
    {
        for (int i = 0; i < burst; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            BulletController bc = bullet.GetComponent<BulletController>();
            bc.dmgLayer = layer;
            bc.pow = pow;

            Vector3 shootDirection = Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector3.up;
            rb.velocity = shootDirection * bulletSpeed;
            bullet.transform.up = shootDirection;

            yield return new WaitForSeconds(shotTime);
        }

        yield break;
    }
}
