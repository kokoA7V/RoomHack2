using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Button buttonA;
    [SerializeField] Button buttonB;
    [SerializeField] Button OptionButton;

    //�}�l�[�V�X�e���֘A
    [SerializeField] Button CleanButton;
    [SerializeField] Button firedeathButton;
    [SerializeField] Button pcButton;
    [SerializeField] Button eaconButton;
    [SerializeField] Button KeihouButton;
    [SerializeField] Button TarretButton;
    [SerializeField] Button EnemyButton;
    [SerializeField] Button DoorButton;
    [SerializeField] Button CameraButton;

    //���x���A�b�v�{�^��
    [SerializeField] GameObject LevelUpButton;


    [SerializeField] GameObject[] buttonTrigA; // ButtonA���������Ƃ��ɕ\������{�^���Z�b�g
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

        //Unity���ɂ���OnClick�̏������v���O������

        buttonA.onClick.AddListener(ToggleButtonsSetA);
        buttonB.onClick.AddListener(ToggleButtonsSetB);
        OptionButton.onClick.AddListener(OptionButtonSet);

        //�}�l�[�V�X�e���֘A

        //�{�^�����ݒ肳��Ă��邩�̔��ʏ���
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

    //�}�l�[�V�X�e���֘A


    //�N���[���p
    void CleanButtonSet()
    {
        LevelUpButton.SetActive(true);
        Cleanbutton = true;
    }

    //�����@��p
    void FireDeathButtonSet()
    {
        LevelUpButton.SetActive(true);
        firedeathbutton = true;
    }

    //�R���s���[�^�[�p
    void PcButtonSet()
    {
        LevelUpButton.SetActive(true);
        pcbutton = true;
    }

    //�G�A�R���p
    void EaconButtonSet()
    {
        LevelUpButton.SetActive(true);
        eaconbutton = true;
    }

    //�x��@��p
    void KeihouButtonSet()
    {
        LevelUpButton.SetActive(true);
        Keihoubutton = true;
    }

    //�^���b�g�p
    void TarretButtonSet()
    {
        LevelUpButton.SetActive(true);
        Tarretbutton = true;
    }

    //�G�p
    void EnemyButtonSet()
    {
        LevelUpButton.SetActive(true);
        Enemybutton = true;
    }

    //�h�A�p
    void DoorButtonSet()
    {
        LevelUpButton.SetActive(true);
        Doorbutton = true;
    }

    //�J�����p
    void CameraButtonSet()
    {
        LevelUpButton.SetActive(true);
        Camerabutton = true;
    }



    void ToggleButtonsSet(int i) //���ꂼ��̃{�^���������ŊǗ�
    {
        imagemanager.UnShowImage(); //image��\��

        for (int j = 0; j < toggleNo; j++)
        {
            isButtonToggled[j] = false;
        }

        isButtonToggled[i] = true;

        // ButtonA���������Ƃ��ɂ�ButtonB�̃Z�b�g���\���ɂ���
        foreach (GameObject obj in buttonTrigB) obj.SetActive(false);

        // ButtonB���������Ƃ��ɂ�ButtonA�̃Z�b�g���\���ɂ���
        foreach (GameObject obj in buttonTrigA) obj.SetActive(false);

        OptionTrig.SetActive(false);
        LevelUpButton.SetActive(false);

        if (i == 0)
        {
            // ButtonA�Z�b�g��\������B
            foreach (GameObject obj in buttonTrigA) obj.SetActive(true);
        }
        else if(i == 1)
        {
            // ButtonB�̃Z�b�g��\������
            foreach (GameObject obj in buttonTrigB) obj.SetActive(true);
        }
        else if(i == 2)
        {
            OptionTrig.SetActive(true);
        }
    }
}




