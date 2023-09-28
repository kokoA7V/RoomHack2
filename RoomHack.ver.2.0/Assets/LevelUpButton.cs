using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpButton : MonoBehaviour
{
    [SerializeField] ButtonManager buttonmng;
    // Start is called before the first frame update
    public void OnClick()
    {
        //クリーン用
        if (buttonmng.Cleanbutton)
        {
            GameData.CleanerLv += 1;
            this.gameObject.SetActive(false);
            
            buttonmng.Cleanbutton = false;
        }

        //消化機器用
        if (buttonmng.firedeathbutton)
        {
            GameData.DigestionLv += 1;
            this.gameObject.SetActive(false);
            buttonmng.firedeathbutton = false;
        }

        //コンピューター用
        if (buttonmng.pcbutton)
        {
            GameData.ComputerLv += 1;
            this.gameObject.SetActive(false);
            buttonmng.pcbutton = false;
        }

        //エアコン用
        if (buttonmng.eaconbutton)
        {
            GameData.AriConditionerLv += 1;
            this.gameObject.SetActive(false);
            buttonmng.eaconbutton = false;
        }

        //警報機器用
        if (buttonmng.Keihoubutton)
        {
            GameData.AlarmLv += 1;
            this.gameObject.SetActive(false);
            buttonmng.Keihoubutton = false;
        }

        //タレット用
        if (buttonmng.Tarretbutton)
        {
            GameData.TurretLv += 1;
            this.gameObject.SetActive(false);
            buttonmng.Tarretbutton = false;
        }

        //敵用
        if (buttonmng.Enemybutton)
        {
            GameData.EnemyLv += 1;
            this.gameObject.SetActive(false);
            buttonmng.Enemybutton = false;
        }

        //ドア用
        if (buttonmng.Doorbutton)
        {
            GameData.DoorLv += 1;
            this.gameObject.SetActive(false);
            buttonmng.Doorbutton = false;
        }

        //カメラ用
        if(buttonmng.Camerabutton)
        {
            GameData.CameraLv += 1;
            this.gameObject.SetActive(false);
            buttonmng.Camerabutton = false;
        }
    }
}
