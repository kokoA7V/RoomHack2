using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitDamage
{
    public int maxHP{ get; set; }
    public int nowHP{ get; set; }
    public int dmgLayer { get; set; }
    public void HitDmg(int dmg);
    public void Die();
}
