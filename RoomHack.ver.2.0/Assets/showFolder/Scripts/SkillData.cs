using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData : MonoBehaviour
{
    //ドアのスキルコスト
    [SerializeField] public int Doorcost = 100;
    //カメラのスキルコスト
    [SerializeField] public int Cameracost = 200;
    //タレットのスキルコスト
    [SerializeField] public int Turretcost = 300;
    //敵のスキルコスト
    [SerializeField] public int Enemycost = 400;
    //警報機器のコスト
    [SerializeField] public int Alarmcost = 500;
    //クリーンのスキルコスト
    [SerializeField] public int Cleanercost = 600;
    //消化機器用のスキルコスト
    [SerializeField] public int Digestioncost = 700;
    //コンピュータのスキルコスト
    [SerializeField] public int Computercost = 800;
    //エアコンのスキルコスト
    [SerializeField] public int AriConditioncost = 900;
}
