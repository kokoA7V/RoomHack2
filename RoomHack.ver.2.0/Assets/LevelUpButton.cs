using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpButton : MonoBehaviour
{
    [SerializeField] ButtonManager buttonmng;
    // Start is called before the first frame update
    public void OnClick()
    {
        //�N���[���p
        if (buttonmng.Cleanbutton)
        {
            GameData.CleanerLv += 1;
            this.gameObject.SetActive(false);
            
            buttonmng.Cleanbutton = false;
        }

        //�����@��p
        if (buttonmng.firedeathbutton)
        {
            GameData.DigestionLv += 1;
            this.gameObject.SetActive(false);
            buttonmng.firedeathbutton = false;
        }

        //�R���s���[�^�[�p
        if (buttonmng.pcbutton)
        {
            GameData.ComputerLv += 1;
            this.gameObject.SetActive(false);
            buttonmng.pcbutton = false;
        }

        //�G�A�R���p
        if (buttonmng.eaconbutton)
        {
            GameData.AriConditionerLv += 1;
            this.gameObject.SetActive(false);
            buttonmng.eaconbutton = false;
        }

        //�x��@��p
        if (buttonmng.Keihoubutton)
        {
            GameData.AlarmLv += 1;
            this.gameObject.SetActive(false);
            buttonmng.Keihoubutton = false;
        }

        //�^���b�g�p
        if (buttonmng.Tarretbutton)
        {
            GameData.TurretLv += 1;
            this.gameObject.SetActive(false);
            buttonmng.Tarretbutton = false;
        }

        //�G�p
        if (buttonmng.Enemybutton)
        {
            GameData.EnemyLv += 1;
            this.gameObject.SetActive(false);
            buttonmng.Enemybutton = false;
        }

        //�h�A�p
        if (buttonmng.Doorbutton)
        {
            GameData.DoorLv += 1;
            this.gameObject.SetActive(false);
            buttonmng.Doorbutton = false;
        }

        //�J�����p
        if(buttonmng.Camerabutton)
        {
            GameData.CameraLv += 1;
            this.gameObject.SetActive(false);
            buttonmng.Camerabutton = false;
        }
    }
}
