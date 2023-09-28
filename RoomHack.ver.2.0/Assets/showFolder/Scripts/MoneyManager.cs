using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MoneyManager : MonoBehaviour
{
    public int StartMoney = 2000;
    public int haveMoney = 0;

    public Text haveText; //所持金を表示するテキスト
    public Text useText;　//消費するお金を表示するテキスト
    public Text notText;　//購入出来なかった場合のテキスト
  

    public SkillData Skillmng;
    void Start()
    {
        haveMoney = StartMoney;
        HaveMoneyText();
    }

    //ButtonManagerで呼び出す用のメソッド
    public void BuySkill(int skill)
    {
        //クリーンのスキル購入
        if(skill == 0)
        {
            if (haveMoney >= Skillmng.Cleanercost)
            {
                haveMoney -= Skillmng.Cleanercost;

            }
        }

        //消化機器のスキル購入
        if (skill == 1)
        {
            if(haveMoney >= Skillmng.Digestioncost)
            {
                haveMoney -= Skillmng.Digestioncost;
            }
        }

        //コンピュータのスキル購入
        if (skill == 2)
        {
            if(haveMoney >= Skillmng.Computercost)
            {
                haveMoney -= Skillmng.Computercost;
            }
        }

        //エアコンのスキル購入
        if(skill == 3)
        {
            if(haveMoney >= Skillmng.AriConditioncost)
            {
                haveMoney -= Skillmng.AriConditioncost;
            }
        }

        //警報機器のスキル購入
        if (skill == 4)
        {
            if(haveMoney >= Skillmng.Alarmcost)
            {
                haveMoney -= Skillmng.Alarmcost;
            }
        }

        //タレットのスキル購入
        if(skill == 5)
        {
            if(haveMoney >= Skillmng.Turretcost)
            {
                haveMoney -= Skillmng.Turretcost;
            }
        }

        //敵のスキル購入
        if (skill == 6)
        {
            if(haveMoney >= Skillmng.Enemycost)
            {
                haveMoney -= Skillmng.Enemycost;
            }
        }

        //ドアのスキル購入
        if (skill == 7)
        {
            if(haveMoney >= Skillmng.Doorcost)
            {
                haveMoney -= Skillmng.Doorcost;
            }
        }

        //カメラのスキル購入
        if(skill == 8)
        {
            if(haveMoney >= Skillmng.Cameracost)
            {
                haveMoney -= Skillmng.Cameracost;
            }
        }
        HaveMoneyText();
    }
    void HaveMoneyText()
    { 
        haveText.text = "所持: " + haveMoney.ToString();
    }
}
