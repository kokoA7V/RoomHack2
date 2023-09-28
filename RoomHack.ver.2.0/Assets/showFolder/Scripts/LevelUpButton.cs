using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    [SerializeField] ButtonManager buttonmng;
    [SerializeField] MoneyManager moneymanager;

    public Text Cleanleveltx;
    public GameObject Digeleveltx;
    public GameObject Computerleveltx;
    public GameObject Ariconleveltx;
    public GameObject Alarmleveltx;
    public GameObject Turretleveltx;
    public GameObject Enemyleveltx;
    public GameObject Doorleveltx;
    public GameObject Cameraleveltx;

    
    // Start is called before the first frame update
    public void OnClick()
    {
        //クリーン用
        if (buttonmng.Cleanbutton)　//クリーンのlevelが3以下なら処理
        {
            if (GameData.CleanerLv <= 4)
            {
                moneymanager.BuySkill(0);
                GameData.CleanerLv += 1;
                this.gameObject.SetActive(false);
                buttonmng.Cleanbutton = false;
            }
        }

        //消化機器用
        if (buttonmng.Digestionbutton)
        {
            if (GameData.DigestionLv <= 4) //Digeのlevelが3以下なら処理
            {
                moneymanager.BuySkill(1);
                GameData.DigestionLv += 1;
                this.gameObject.SetActive(false);
                buttonmng.Digestionbutton = false;
            }
        }

        //コンピューター用
        if (buttonmng.computorbutton)
        {
            if (GameData.ComputerLv <= 4)　//Computerのlevelが3以下なら処理
            {
                moneymanager.BuySkill(2);
                GameData.ComputerLv += 1;
                this.gameObject.SetActive(false);
                buttonmng.computorbutton = false;
            }
        }

        //エアコン用
        if (buttonmng.AriConditionerbutton) //エアコンのlevelが3以下なら処理
        {
            if (GameData.AriConditionerLv <= 4)
            {
                moneymanager.BuySkill(3);
                GameData.AriConditionerLv += 1;
                this.gameObject.SetActive(false);
                buttonmng.AriConditionerbutton = false;
            }
        }

        //警報機器用
        if (buttonmng.Alarmbutton)  //Alarmのlevelが3以下なら処理
        {
            {
                if (GameData.AlarmLv <= 4)
                {
                    moneymanager.BuySkill(4);
                    GameData.AlarmLv += 1;
                    this.gameObject.SetActive(false);
                    buttonmng.Digestionbutton = false;
                }
            }

            //タレット用
            if (buttonmng.Turretbutton) //Turretのlevelが3以下なら処理
            {
                if (GameData.TurretLv <= 4)
                {
                    moneymanager.BuySkill(5);
                    GameData.TurretLv += 1;
                    this.gameObject.SetActive(false);
                    buttonmng.Turretbutton = false;
                }
            }

            //敵用
            if (buttonmng.Enemybutton)
            {
                if (GameData.EnemyLv <= 4) //Enemyのlevelが3以下なら処理
                {
                    moneymanager.BuySkill(6);
                    GameData.EnemyLv += 1;
                    this.gameObject.SetActive(false);
                    buttonmng.Enemybutton = false;
                }
            }

            //ドア用
            if (buttonmng.Doorbutton)
            {
                if (GameData.EnemyLv <= 4) //Doorのlevelが3以下なら処理
                {
                    moneymanager.BuySkill(7);
                    GameData.DoorLv += 1;
                    this.gameObject.SetActive(false);
                    buttonmng.Doorbutton = false;
                }
            }

            //カメラ用
            if (buttonmng.Camerabutton)
            {
                if (GameData.CameraLv <= 4) //Cameraのlevelが3以下なら処理
                {
                    moneymanager.BuySkill(8);
                    GameData.CameraLv += 1;
                    this.gameObject.SetActive(false);
                    buttonmng.Camerabutton = false;
                }
            }
        }
    }
}
