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


    public Text useText;
    public GameObject OptionTrig;

    [SerializeField] ImageManager imagemanager;
    [SerializeField] SkillData skilldata;
    /*[SerializeField] */AudioPlay audioplay;

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

    [SerializeField] int CleanUseMoney;
    [SerializeField] int DigestionUseMoney;
    [SerializeField] int ComputorUseMoney;
    [SerializeField] int AriConditionerUseMoney;
    [SerializeField] int AlarmUseMoney;
    [SerializeField] int TurretUseMoney;
    [SerializeField] int EnemyUseMoney;
    [SerializeField] int DoorUseMoney;
    [SerializeField] int CameraUseMoney;

    [SerializeField, Header("タイトルのロゴ")]
    private GameObject titleLogo;

    const int toggleNo = 3;

    void Start()
    {
        isButtonToggled = new bool[toggleNo] { false, false, false };
        audioplay = GameObject.Find("AudioPlay").GetComponent<AudioPlay>();


        //Unity内にあるOnClickの処理をプログラムで

        buttonA.onClick.AddListener(StageButtonsSet);
        buttonB.onClick.AddListener(ShopButtonSet);
        OptionButton.onClick.AddListener(OptionButtonSet);

        //以下マネーシステム関連

        //Unityで設定するOnClickをプログラムで。

        CleanButton.onClick.AddListener(CleanButtonSet);
        firedeathButton.onClick.AddListener(DigestionButtonSet);
        pcButton.onClick.AddListener(ComputorButtonSet);
        eaconButton.onClick.AddListener(AriConditionerButtonSet);
        KeihouButton.onClick.AddListener(AlarmButtonSet);
        TarretButton.onClick.AddListener(TurretButtonSet);
        EnemyButton.onClick.AddListener(EnemyButtonSet);
        DoorButton.onClick.AddListener(DoorButtonSet);
        CameraButton.onClick.AddListener(CameraButtonSet);        
    }
    //マネーシステム関連


    //クリーン用のボタンを押した後の処理
    void CleanButtonSet()
    {
        FlgReset();
        Debug.Log("Clean");
        AudioPlay.instance.SEPlay(0);
        useText.text = "消費: " + skilldata.Cleanercost.ToString();
        LevelUpButton.SetActive(true);
        Cleanbutton = true;
    }

    //消化機器用のボタン
    void DigestionButtonSet()
    {
        FlgReset();
        AudioPlay.instance.SEPlay(0);
        useText.text = "消費: " + skilldata.Digestioncost.ToString();
        LevelUpButton.SetActive(true);
        Digestionbutton = true;
    }

    //コンピューター用のボタンを押した後の処理
    void ComputorButtonSet()
    {
        FlgReset();
        AudioPlay.instance.SEPlay(0);
        useText.text = "消費: " + skilldata.Computercost.ToString();
        LevelUpButton.SetActive(true);
        computorbutton = true;
    }

    //エアコン用のボタンを押した後の処理
    void AriConditionerButtonSet()
    {
        FlgReset();
        AudioPlay.instance.SEPlay(0);
        useText.text = "消費: " + skilldata.AriConditioncost.ToString();
        LevelUpButton.SetActive(true);
        AriConditionerbutton = true;
    }

    //警報機器用のボタンを押した後の処理
    void AlarmButtonSet()
    {
        FlgReset();
        AudioPlay.instance.SEPlay(0);
        useText.text = "消費: " + skilldata.Alarmcost.ToString();
        LevelUpButton.SetActive(true);
        Alarmbutton = true;
    }

    //タレット用のボタンを押した後の処理
    void TurretButtonSet()
    {
        FlgReset();
        AudioPlay.instance.SEPlay(0);
        useText.text = "消費: " + skilldata.Turretcost.ToString();
        LevelUpButton.SetActive(true);
        Turretbutton = true;
    }

    //敵用のボタンを押した後の処理
    void EnemyButtonSet()
    {
        FlgReset();
        Debug.Log("Enemy");
        AudioPlay.instance.SEPlay(0);
        useText.text = "消費: " + skilldata.Enemycost.ToString();
        LevelUpButton.SetActive(true);
        Enemybutton = true;
    }

    //ドア用のボタンを押した後の処理
    void DoorButtonSet()
    {
        FlgReset();
        AudioPlay.instance.SEPlay(0);
        useText.text = "消費: " + skilldata.Doorcost.ToString();
        LevelUpButton.SetActive(true);
        Doorbutton = true;
    }

    //カメラ用のボタンを押した後の処理
    void CameraButtonSet()
    {
        FlgReset();
        AudioPlay.instance.SEPlay(0);
        useText.text = "消費: " + skilldata.Cameracost.ToString();
        LevelUpButton.SetActive(true);
        Camerabutton = true;
    }

    void StageButtonsSet() //Stage用のボタン
    {
        ToggleButtonsSet(0);
        AudioPlay.instance.SEPlay(0);
    }

    void ShopButtonSet() //Shop用のボタン
    {
        ToggleButtonsSet(1);
        AudioPlay.instance.SEPlay(0);
    }

    void OptionButtonSet() //オプションボタン
    {
        ToggleButtonsSet(2);
        AudioPlay.instance.SEPlay(0);
    }

    private void FlgReset()
    {
        Cleanbutton = false;
        Digestionbutton = false;
        computorbutton = false;
        AriConditionerbutton = false;
        Alarmbutton = false;
        Turretbutton = false;
        Enemybutton = false;
        Doorbutton = false;
        Camerabutton = false;
    }


    //各それぞれのボタンの表示・非表示の設定。

    void ToggleButtonsSet(int i) //それぞれのボタンを引数で管理
    {
        //imagemanager.UnShowImage(); //image非表示

        titleLogo.SetActive(false);

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
        else if (i == 1)
        {
            // Shopのセットを表示する
            foreach (GameObject obj in ShopTrig) obj.SetActive(true);
        }
        else if (i == 2)
        {
            //Option画面を表示する。
            OptionTrig.SetActive(true);
        }
    }
}