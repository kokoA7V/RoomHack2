using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Button buttonA;
    [SerializeField] Button buttonB;
    [SerializeField] Button OptionButton;

    [SerializeField] GameObject[] buttonTrigA; // ButtonA���������Ƃ��ɕ\������{�^���Z�b�g
    [SerializeField] GameObject[] buttonTrigB; // ButtonB���������Ƃ��ɕ\������{�^���Z�b�g
    public  GameObject OptionTrig;

    [SerializeField] ImageManager imagemanager;

    [SerializeField] bool[] isButtonToggled;
    [SerializeField] bool OptionTri = false;

    const int toggleNo = 3;

    void Start()
    {
        isButtonToggled = new bool[toggleNo] { false, false, false };

        //Unity���ɂ���OnClick�̏������v���O������

        buttonA.onClick.AddListener(ToggleButtonsSetA);
        buttonB.onClick.AddListener(ToggleButtonsSetB);
        OptionButton.onClick.AddListener(OptionButtonSet);
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

    private void Update()
    {
     
    }
}




