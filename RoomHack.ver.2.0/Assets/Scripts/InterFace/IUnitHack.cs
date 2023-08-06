using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitHack 
{ 
    public bool hacked { get; set; }
    void StatusDisp();
    enum PuzzleGimmick
    {
        atomicHearts,
        dbd,
        osu,
        valt,
    }
}