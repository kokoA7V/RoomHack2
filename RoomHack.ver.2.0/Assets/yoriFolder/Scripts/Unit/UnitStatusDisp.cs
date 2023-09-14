using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatusDisp 
{
    public void StatusDisp(bool hacked, float time, float hackTime, bool hackedFlg)
    {
        if (!hacked) return;
        if (time <= 0) time = hackTime;
        hackedFlg = true;
        Debug.Log("ハッキング完了");
    }
}
