using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCore : MonoBehaviour, IUnitMove, IUnitShot, IUnitDamage,IUnitHack
{
    private UnitStatusDisp uStatusDisp = new UnitStatusDisp();

    public int maxHP { get; set; } = 3;
    public int nowHP { get; set; }
    public int dmgLayer { get; set; }

    public float moveSpd { get; set; } = 1f;

    public bool hacked { get; set; }
    
    public void HitDmg(int dmg)
    {
        GetComponent<Damage>().HitDmg(dmg);
    }
    public void Die() {
        GetComponent<UnitDie>().Die();
    }
    public void Shot(int layer, int pow,int burst)
    {
        GetComponent<Shot>().UnitShot(layer, pow, burst);
    }

    public void Move(float moveSpd,Vector3 unit)
    {
        GetComponent<Move>().UnitMove(moveSpd,unit);
    }

    public void StatusDisp(bool hacked, float time, float hackTime, bool hackedFlg)
    {
        uStatusDisp.StatusDisp( hacked, time,  hackTime,  hackedFlg);
    }
}
