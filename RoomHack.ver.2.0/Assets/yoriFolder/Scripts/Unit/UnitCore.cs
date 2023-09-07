using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCore : MonoBehaviour, IUnitMove, IUnitShot, IUnitHack, IUnitDamage
{
    public int maxHP { get; set; } = 3;
    public int nowHP { get; set; }
    public int dmgLayer { get; set; } = 2;

    public float moveSpd { get; set; } = 0.1f;


    public bool hacked { get; set; } = false;
    
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

    public void StatusDisp()
    {

    }

    public void Move(float moveSpd,GameObject unit)
    {
        GetComponent<Move>().UnitMove(moveSpd,unit);
    }
}
