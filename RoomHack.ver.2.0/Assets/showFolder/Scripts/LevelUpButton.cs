using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    [SerializeField] ButtonManager buttonmng;
    [SerializeField] MoneyManager moneymanager;
    [SerializeField] LevelManager levelmng;
    [SerializeField] SkillData skillmng;

    // Start is called before the first frame update
    public void OnClick()
    {
        //クリーン用
        if (buttonmng.Cleanbutton )　//クリーンのlevelが3以下なら処理
        {
            bool flg = moneymanager.BuySkill(0);
            if (GameData.CleanerLv < 3 && flg)
            {
                levelmng.CleanLevelcounter++;
                GameData.CleanerLv += 1;
                this.gameObject.SetActive(false);
                buttonmng.Cleanbutton = false;
            }
        }

        //消化機器用
        if (buttonmng.Digestionbutton)
        {
            bool flg = moneymanager.BuySkill(1);
            if (GameData.DigestionLv < 3 && flg) //Digeのlevelが3以下なら処理
            {
                levelmng.DigeLevelcounter++;
                GameData.DigestionLv += 1;
                this.gameObject.SetActive(false);
                buttonmng.Digestionbutton = false;
            }
          
        }


        //コンピューター用
        if (buttonmng.computorbutton)
        {
            bool flg = moneymanager.BuySkill(2);
            if (GameData.ComputerLv < 3 && flg)　//Computerのlevelが3以下なら処理
            {
                levelmng.ComputerLevelcounter++;
                GameData.ComputerLv += 1;
                this.gameObject.SetActive(false);
                buttonmng.computorbutton = false;
            }
        }

        //エアコン用
        if (buttonmng.AriConditionerbutton) //エアコンのlevelが3以下なら処理
        {
            bool flg = moneymanager.BuySkill(3);
            if (GameData.AriConditionerLv < 3 && flg)
            { 
                levelmng.AriconLevelcounter++;
                GameData.AriConditionerLv += 1;
                this.gameObject.SetActive(false);
                buttonmng.AriConditionerbutton = false;
            }
        }

        //警報機器用
        if (buttonmng.Alarmbutton)  //Alarmのlevelが3以下なら処理
        {
            bool flg = moneymanager.BuySkill(4);
            if (GameData.AlarmLv < 3 && flg)
            {
                levelmng.AlarmLevelcounter++;
                GameData.AlarmLv += 1;
                this.gameObject.SetActive(false);
                buttonmng.Digestionbutton = false;
            }
        }

        //タレット用
        if (buttonmng.Turretbutton) //Turretのlevelが3以下なら処理
        {
            bool flg = moneymanager.BuySkill(5);
            if (GameData.TurretLv < 3 && flg)
            {
              
                levelmng.TurretLevelcounter++;
                GameData.TurretLv += 1;
                this.gameObject.SetActive(false);
                buttonmng.Turretbutton = false;
            }
        }

        //敵用
        if (buttonmng.Enemybutton)
        {
            bool flg = moneymanager.BuySkill(6);
            if (GameData.EnemyLv < 3 && flg) //Enemyのlevelが3以下なら処理
            {
              
                levelmng.EnemyLevelcounter++;
                GameData.EnemyLv += 1;
                this.gameObject.SetActive(false);
                buttonmng.Enemybutton = false;
            }
        }

        //ドア用
        if (buttonmng.Doorbutton)
        {
            bool flg = moneymanager.BuySkill(7);
            if (GameData.DoorLv < 3 && flg) //Doorのlevelが3以下なら処理
            {
               
                levelmng.DoorLevelcounter++;
                GameData.DoorLv += 1;
                this.gameObject.SetActive(false);
                buttonmng.Doorbutton = false;
            }
        }

        //カメラ用
        if (buttonmng.Camerabutton)
        {
            bool flg = moneymanager.BuySkill(8);
            if (GameData.CameraLv < 3  && flg) //Cameraのlevelが3以下なら処理
            {
                levelmng.CameraLevelcounter++;
                GameData.CameraLv += 1;
                this.gameObject.SetActive(false);
                buttonmng.Camerabutton = false;
            }
        }
    }
}

