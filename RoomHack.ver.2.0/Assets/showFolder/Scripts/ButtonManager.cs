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


    [SerializeField] GameObject[] buttonTrigA; // ButtonAを押したときに表示するボタンセット
    [SerializeField] GameObject[] buttonTrigB; 
    public  GameObject OptionTrig;

    [SerializeField] ImageManager imagemanager;
    [SerializeField] LevelUpButton levelup;

    [SerializeField] bool[] isButtonToggled;
    [SerializeField] bool OptionTri = false;
    bool alreadybutton = false;
    
    public bool Cleanbutton = false;
    public bool firedeathbutton = false;
    public bool pcbutton = false;
    public bool eaconbutton = false;
    public bool Keihoubutton = false;
    public bool Tarretbutton = false;
    public bool Enemybutton = false;
    public bool Doorbutton = false;
    public bool Camerabutton = false;


    const int toggleNo = 3;

    void Start()
    {
        isButtonToggled = new bool[toggleNo] { false, false, false };

        //Unity内にあるOnClickの処理をプログラムで

        buttonA.onClick.AddListener(ToggleButtonsSetA);
        buttonB.onClick.AddListener(ToggleButtonsSetB);
        OptionButton.onClick.AddListener(OptionButtonSet);

        //マネーシステム関連

        //ボタンが設定されているかの判別処理
        if (!alreadybutton)
        {
            CleanButton.onClick.AddListener(CleanButtonSet);
            firedeathButton.onClick.AddListener(FireDeathButtonSet);
            pcButton.onClick.AddListener(PcButtonSet);
            eaconButton.onClick.AddListener(EaconButtonSet);
            KeihouButton.onClick.AddListener(KeihouButtonSet);
            TarretButton.onClick.AddListener(TarretButtonSet);
            EnemyButton.onClick.AddListener(EnemyButtonSet);
            DoorButton.onClick.AddListener(DoorButtonSet);
            CameraButton.onClick.AddListener(CameraButtonSet);
            alreadybutton = true; 
        }
    }
    void ToggleButtonsSetA() //ButtonA
    {
        ToggleButtonsSet(0);
    }

    void ToggleButtonsSetB() //ButtonB
    {
        ToggleButtonsSet(1);
    }

    void OptionButtonSet()
    {
        ToggleButtonsSet(2);
    }

    //マネーシステム関連


    //クリーン用
    void CleanButtonSet()
    {
        LevelUpButton.SetActive(true);
        Cleanbutton = true;
    }

    //消化機器用
    void FireDeathButtonSet()
    {
        LevelUpButton.SetActive(true);
        firedeathbutton = true;
    }

    //コンピューター用
    void PcButtonSet()
    {
        LevelUpButton.SetActive(true);
        pcbutton = true;
    }

    //エアコン用
    void EaconButtonSet()
    {
        LevelUpButton.SetActive(true);
        eaconbutton = true;
    }

    //警報機器用
    void KeihouButtonSet()
    {
        LevelUpButton.SetActive(true);
        Keihoubutton = true;
    }

    //タレット用
    void TarretButtonSet()
    {
        LevelUpButton.SetActive(true);
        Tarretbutton = true;
    }

    //敵用
    void EnemyButtonSet()
    {
        LevelUpButton.SetActive(true);
        Enemybutton = true;
    }

    //ドア用
    void DoorButtonSet()
    {
        LevelUpButton.SetActive(true);
        Doorbutton = true;
    }

    //カメラ用
    void CameraButtonSet()
    {
        LevelUpButton.SetActive(true);
        Camerabutton = true;
    }



    void ToggleButtonsSet(int i) //それぞれのボタンを引数で管理
    {
        imagemanager.UnShowImage(); //image非表示

        for (int j = 0; j < toggleNo; j++)
        {
            isButtonToggled[j] = false;
        }

        isButtonToggled[i] = true;

        // ButtonAを押したときにはButtonBのセットを非表示にする
        foreach (GameObject obj in buttonTrigB) obj.SetActive(false);

        // ButtonBを押したときにはButtonAのセットを非表示にする
        foreach (GameObject obj in buttonTrigA) obj.SetActive(false);

        OptionTrig.SetActive(false);
        LevelUpButton.SetActive(false);

        if (i == 0)
        {
            // ButtonAセットを表示する。
            foreach (GameObject obj in buttonTrigA) obj.SetActive(true);
        }
        else if(i == 1)
        {
            // ButtonBのセットを表示する
            foreach (GameObject obj in buttonTrigB) obj.SetActive(true);
        }
        else if(i == 2)
        {
            OptionTrig.SetActive(true);
        }
    }
}




