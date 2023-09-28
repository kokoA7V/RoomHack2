using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Button buttonA;
    [SerializeField] Button buttonB;
    [SerializeField] Button OptionButton;

    //マネーシステム関連
    [SerializeField] Button CleanButton;
    [SerializeField] Button firedeathButton;
    [SerializeField] Button pcButton;
    [SerializeField] Button eaconButton;
    [SerializeField] Button KeihouButton;
    [SerializeField] Button TarretButton;
    [SerializeField] Button EnemyButton;
    [SerializeField] Button DoorButton;
    [SerializeField] Button CameraButton;

    //レベルアップボタン
    [SerializeField] GameObject LevelUpButton;


    [SerializeField] GameObject[] StageTrig; //Stageを押したときに表示されるUIを格納する場所
    [SerializeField] GameObject[] ShopTrig; //Shopを押したときに表示されるUIを格納する場所

    public  GameObject OptionTrig;

    [SerializeField] ImageManager imagemanager;

    [SerializeField] LevelUpButton levelup;

    [SerializeField] bool[] isButtonToggled;
    [SerializeField] bool OptionTri = false;
    bool alreadybutton = false;
    
    public bool Cleanbutton = false;
    public bool Digestionbutton = false;
    public bool computorbutton = false;
    public bool AriConditionerbutton = false;
    public bool Alarmbutton = false;
    public bool Turretbutton = false;
    public bool Enemybutton = false;
    public bool Doorbutton = false;
    public bool Camerabutton = false;


    const int toggleNo = 3;

    void Start()
    {
        isButtonToggled = new bool[toggleNo] { false, false, false };

        //Unity内にあるOnClickの処理をプログラムで

        buttonA.onClick.AddListener(StageButtonsSet);
        buttonB.onClick.AddListener(ShopButtonSet);
        OptionButton.onClick.AddListener(OptionButtonSet);

        //以下マネーシステム関連

        //ボタンが設定されているかの判別処理
        if (!alreadybutton)
        {
            CleanButton.onClick.AddListener(CleanButtonSet);
            firedeathButton.onClick.AddListener(DigestionButtonSet);
            pcButton.onClick.AddListener(ComputorButtonSet);
            eaconButton.onClick.AddListener(AriConditionerButtonSet);
            KeihouButton.onClick.AddListener(AlarmButtonSet);
            TarretButton.onClick.AddListener(TurretButtonSet);
            EnemyButton.onClick.AddListener(EnemyButtonSet);
            DoorButton.onClick.AddListener(DoorButtonSet);
            CameraButton.onClick.AddListener(CameraButtonSet);
            alreadybutton = true; 
        }
    }
    //マネーシステム関連


    //クリーン用のボタンを押した後の処理
    void CleanButtonSet()
    {
        LevelUpButton.SetActive(true);
        Cleanbutton = true;
    }

    //消化機器用のボタン
    void DigestionButtonSet()
    {
         LevelUpButton.SetActive(true);
         Digestionbutton = true;
    }

    //コンピューター用のボタンを押した後の処理
    void ComputorButtonSet()
    {
            LevelUpButton.SetActive(true);
            computorbutton = true;
    }

    //エアコン用のボタンを押した後の処理
    void AriConditionerButtonSet()
    {
            LevelUpButton.SetActive(true);
            AriConditionerbutton = true;
    }

    //警報機器用のボタンを押した後の処理
    void AlarmButtonSet()
    {
            LevelUpButton.SetActive(true);
            Alarmbutton = true;
    }

    //タレット用のボタンを押した後の処理
    void TurretButtonSet()
    {
            LevelUpButton.SetActive(true);
            Turretbutton = true;
    }

    //敵用のボタンを押した後の処理
    void EnemyButtonSet()
    {
            LevelUpButton.SetActive(true);
            Enemybutton = true;
    }

    //ドア用のボタンを押した後の処理
    void DoorButtonSet()
    {
            LevelUpButton.SetActive(true);
            Doorbutton = true;
    }

    //カメラ用のボタンを押した後の処理
    void CameraButtonSet()
    {
            LevelUpButton.SetActive(true);
            Camerabutton = true;
    }

    void StageButtonsSet() //Stage用のボタン
    {
        ToggleButtonsSet(0);
    }

    void ShopButtonSet() //Shop用のボタン
    {
        ToggleButtonsSet(1);
    }

    void OptionButtonSet() //オプションボタン
    {
        ToggleButtonsSet(2);
    }
    //各それぞれのボタンの表示・非表示の設定。

    void ToggleButtonsSet(int i) //それぞれのボタンを引数で管理
    {
        imagemanager.UnShowImage(); //image非表示

        for (int j = 0; j < toggleNo; j++)
        {
            isButtonToggled[j] = false;
        }

        isButtonToggled[i] = true;

        // ButtonAを押したときにはButtonBのセットを非表示にする
        foreach (GameObject obj in ShopTrig) obj.SetActive(false);

        // ButtonBを押したときにはButtonAのセットを非表示にする
        foreach (GameObject obj in StageTrig) obj.SetActive(false);

        OptionTrig.SetActive(false);
        LevelUpButton.SetActive(false);

        if (i == 0)
        {
            // Stageのセットを表示する。
            foreach (GameObject obj in StageTrig) obj.SetActive(true);
        }
        else if(i == 1)
        {
            // Shopのセットを表示する
            foreach (GameObject obj in ShopTrig) obj.SetActive(true);
        }
        else if(i == 2)
        {
            //Option画面を表示する。
            OptionTrig.SetActive(true);
        }
    }
}




