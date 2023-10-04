using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyManager : MonoBehaviour
{
    public Text haveText; //所持金を表示するテキスト
    public Text useText;　//消費するお金を表示するテキスト
    public Text NotbuyText;　//購入出来なかった場合のテキスト

    public bool isSe = true;
    bool isNotText = false;

    float timecounter = 1;

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
            if (GameData.Money >= Skillmng.Cleanercost && GameData.CleanerLv < 3)
            {
                GameData.Money -= Skillmng.Cleanercost;
                HaveMoneyText();
                flg= true;
                isSe = true;
            }
        }

        //消化機器のスキル購入
        else if (skill == 1)
        {
            if (GameData.Money >= Skillmng.Digestioncost && GameData.DigestionLv < 3)
            {
                GameData.Money -= Skillmng.Digestioncost;
                HaveMoneyText();
                flg= true;
                isSe = true;
            }
        }

        //コンピュータのスキル購入
        else if (skill == 2)
        {
            if (GameData.Money >= Skillmng.Computercost && GameData.ComputerLv < 3)
            {
                GameData.Money -= Skillmng.Computercost;
                HaveMoneyText();
                flg= true;
                isSe = true;
            }
        }

        //エアコンのスキル購入
        else if (skill == 3)
        {
            if (GameData.Money >= Skillmng.AriConditioncost && GameData.AriConditionerLv < 3)
            {
                GameData.Money -= Skillmng.AriConditioncost;
                HaveMoneyText();
                flg= true;
                isSe = true;
            }
        }

        //警報機器のスキル購入
        else if (skill == 4)
        {
            if (GameData.Money >= Skillmng.Alarmcost && GameData.AlarmLv < 3)
            {
                GameData.Money -= Skillmng.Alarmcost;
                HaveMoneyText();
                flg= true;
                isSe = true;
            }
        }

        //タレットのスキル購入
        else if (skill == 5)
        {
            if (GameData.Money >= Skillmng.Turretcost && GameData.TurretLv < 3)
            {
                GameData.Money -= Skillmng.Turretcost;
                HaveMoneyText();
                flg= true;
                isSe = true;
            }
        }

        //敵のスキル購入
        else if (skill == 6)
        {
            if (GameData.Money >= Skillmng.Enemycost && GameData.EnemyLv < 3)
            {
                GameData.Money -= Skillmng.Enemycost;
                HaveMoneyText();
                flg= true;
                isSe = true;
            }
        }

        //ドアのスキル購入
        else if (skill == 7)
        {
            if (GameData.Money >= Skillmng.Doorcost && GameData.DoorLv < 3)
            {
                GameData.Money -= Skillmng.Doorcost;
                HaveMoneyText();
                flg= true;
                isSe = true;
            }
        }

        //カメラのスキル購入
        else if (skill == 8)
        {
            if (GameData.Money >= Skillmng.Cameracost && GameData.CameraLv < 3)
            {
                GameData.Money -= Skillmng.Cameracost;
                HaveMoneyText();
                flg= true;
                isSe = true;
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
        isSe = false;
        StartCoroutine(TextActive());
        NotbuyText.text = "購入出来ませんでした。";
      
    }

    void HaveMoneyText()
    {
        haveText.text = "所持: " + GameData.Money.ToString();
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
