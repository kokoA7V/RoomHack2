using UnityEngine;

public class BulletController : MonoBehaviour,IUnitDamage
{
    public int maxHP { get; set; }
    public int nowHP { get; set; }
    public int dmgLayer { get; set; } = 3;

    public int pow;

    void Start()
    {
        Destroy(gameObject, 4);
        maxHP = 1;
        nowHP = maxHP;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<IUnitDamage>(out var damage))
        {
            if(this.dmgLayer != damage.dmgLayer)
            {
                damage.HitDmg(1);
                this.HitDmg(1);
            }
        }
    }

    public void HitDmg(int dmg)
    {
        nowHP--;
        if (nowHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
