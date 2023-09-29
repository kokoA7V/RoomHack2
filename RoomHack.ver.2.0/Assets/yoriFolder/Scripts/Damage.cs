using UnityEngine;

public class Damage : MonoBehaviour
{
    UnitCore uCore;
    private int nowHp;

    void Start()
    {
        uCore = this.gameObject.GetComponent<UnitCore>();
        nowHp = uCore.maxHP;
    }

    public void HitDmg(int dmg)
    {
        nowHp -= dmg;
        uCore.nowHP = nowHp;
        if (uCore.nowHP <= 0)
        {
            uCore.Die();
        }
    }
}
