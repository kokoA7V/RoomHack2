using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class MoneyManager : MonoBehaviour
{
    public int haveMoney = 2000; //持っているお金

    public Text haveText; //所持金を表示するテキスト
    public Text useText;　//消費するお金を表示するテキスト
    public Text NotbuyText;　//購入出来なかった場合のテキスト

    bool isNotText = false;

    float timecounter = 2;

    public SkillData Skillmng;
    void Start()
    {
        HaveMoneyText();
    }

    //ButtonManagerで呼び出す用のメソッド
    public bool BuySkill(int skill)
    {
        bool flg = false;
        //クリーンのスキル購入
        if (skill == 0)
        {
            if (haveMoney >= Skillmng.Cleanercost && GameData.CleanerLv < 3)
            {
                haveMoney -= Skillmng.Cleanercost;
                HaveMoneyText();
                flg= true;
            }
        }

        //消化機器のスキル購入
        else if (skill == 1)
        {
            if (haveMoney >= Skillmng.Digestioncost && GameData.DigestionLv < 3)
            {
                haveMoney -= Skillmng.Digestioncost;
                HaveMoneyText();
                flg= true;
            }
        }

        //コンピュータのスキル購入
        else if (skill == 2)
        {
            if (haveMoney >= Skillmng.Computercost && GameData.ComputerLv < 3)
            {
                haveMoney -= Skillmng.Computercost;
                HaveMoneyText();
                flg= true;
            }
        }

        //エアコンのスキル購入
        else if (skill == 3)
        {
            if (haveMoney >= Skillmng.AriConditioncost && GameData.AriConditionerLv < 3)
            {
                haveMoney -= Skillmng.AriConditioncost;
                HaveMoneyText();
                flg= true;
            }
        }

        //警報機器のスキル購入
        else if (skill == 4)
        {
            if (haveMoney >= Skillmng.Alarmcost && GameData.AlarmLv < 3)
            {
                haveMoney -= Skillmng.Alarmcost;
                HaveMoneyText();
                flg= true;
            }
        }

        //タレットのスキル購入
        else if (skill == 5)
        {
            if (haveMoney >= Skillmng.Turretcost && GameData.TurretLv < 3)
            {
                haveMoney -= Skillmng.Turretcost;
                HaveMoneyText();
                flg= true;
            }
        }

        //敵のスキル購入
        else if (skill == 6)
        {
            if (haveMoney >= Skillmng.Enemycost && GameData.EnemyLv < 3)
            {
                haveMoney -= Skillmng.Enemycost;
                HaveMoneyText();
                flg= true;
            }
        }

        //ドアのスキル購入
        else if (skill == 7)
        {
            if (haveMoney >= Skillmng.Doorcost && GameData.DoorLv < 3)
            {
                haveMoney -= Skillmng.Doorcost;
                HaveMoneyText();
                flg= true;
            }
        }

        //カメラのスキル購入
        else if (skill == 8)
        {
            if (haveMoney >= Skillmng.Cameracost && GameData.CameraLv < 3)
            {
                haveMoney -= Skillmng.Cameracost;
                HaveMoneyText();
                flg= true;
            }
        }
        HaveMoneyText();
        if (!flg)
        {
            NoMoney();
        }
        return flg;
    }

    private void NoMoney()
    {
        if (isNotText) return;
        isNotText = true;
        StartCoroutine(TextActive());
        NotbuyText.text = "購入出来ませんでした。";
    }

    void HaveMoneyText()
    {
        haveText.text = "所持: " + haveMoney.ToString();
    }

    IEnumerator TextActive()
    {
        Debug.Log("a");
        NotbuyText.enabled = true;
        yield return new WaitForSeconds(timecounter);
        NotbuyText.enabled = false;
        isNotText = false;

        yield break;
    }
}
