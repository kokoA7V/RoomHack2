using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour,IUnitDamage
{
    public int maxHP { get; set; }
    public int nowHP { get; set; }
    public int dmgLayer { get; set; } = 0;
    // Start is called before the first frame update

    private void Start()
    {
        maxHP = 999;
    }
    public void HitDmg(int dmg)
    {
        maxHP -= dmg;
        maxHP += dmg;
    }
    public void Die()
    {

    }

}
