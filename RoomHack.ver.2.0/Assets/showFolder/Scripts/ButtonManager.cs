using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Button buttonA;
    [SerializeField] Button buttonB;

    [SerializeField] GameObject[] buttonTrigA; // ButtonA���������Ƃ��ɕ\������{�^���Z�b�g
    [SerializeField] GameObject[] buttonTrigB; // ButtonB���������Ƃ��ɕ\������{�^���Z�b�g

    [SerializeField] ImageManager imagemanager;

    [SerializeField] bool[] isButtonToggled;

    const int toggleNo = 3;

    void Start()
    {
        isButtonToggled = new bool[toggleNo] { false, false, false };

        buttonA.onClick.AddListener(ToggleButtonsSetA);
        buttonB.onClick.AddListener(ToggleButtonsSetB);
    }

    void ToggleButtonsSetA() //ButtonA
    {
        ToggleButtonsSet(0);
    }

    void ToggleButtonsSetB() //ButtonB
    {
        ToggleButtonsSet(1);
    }

    void ToggleButtonsSet(int i)
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
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            // ButtonA���������Ƃ��ɂ�ButtonB�̃Z�b�g���\���ɂ���
            foreach (GameObject obj in buttonTrigB) obj.SetActive(false);

            // ButtonB���������Ƃ��ɂ�ButtonA�̃Z�b�g���\���ɂ���
            foreach (GameObject obj in buttonTrigA) obj.SetActive(false);
        }
    }
}




