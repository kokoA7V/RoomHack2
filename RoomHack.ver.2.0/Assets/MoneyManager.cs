using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MoneyManager : MonoBehaviour
{
    public int StartMoney = 2000;
    public Text moneyText;

    public int haveMoney;
    void Start()
    {
        haveMoney = StartMoney;
        HaveMoneyText();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public bool BuySkill(int skillcost)
    {
        if (haveMoney >= skillcost)
        {
            haveMoney -= skillcost;
            HaveMoneyText();
            return true;
        }
        else
        {
            return false;
        }
    }

    void HaveMoneyText()
    {
        moneyText.text = "Š: " + haveMoney.ToString();
    }
}
