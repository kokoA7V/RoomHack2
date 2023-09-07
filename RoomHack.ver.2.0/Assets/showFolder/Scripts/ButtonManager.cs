using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button buttonA;
    public Button buttonB;

    public GameObject[] buttonTrigA; // ButtonA���������Ƃ��ɕ\������{�^���Z�b�g
    public GameObject[] buttonTrigB; // ButtonB���������Ƃ��ɕ\������{�^���Z�b�g

    private bool isButtonAToggled = false;
    private bool isButtonBToggled = false;

    void Start()
    {
        buttonA.onClick.AddListener(ToggleButtonsSetA);
        buttonB.onClick.AddListener(ToggleButtonsSetB);
    }

    void ToggleButtonsSetA()
    {
        isButtonAToggled = !isButtonAToggled;

        // ButtonA���������Ƃ��ɂ�ButtonB�̃Z�b�g���\���ɂ���
        foreach (GameObject obj in buttonTrigB)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in buttonTrigA)
        {
            obj.SetActive(isButtonAToggled);
        }
    }

    void ToggleButtonsSetB()
    {
        isButtonBToggled = !isButtonBToggled;
        // ButtonB���������Ƃ��ɂ�ButtonA�̃Z�b�g���\���ɂ���
        foreach (GameObject obj in buttonTrigA)
        {
            obj.SetActive(false);
        }

        // ButtonB�̃Z�b�g��\������
        foreach (GameObject obj in buttonTrigB)
        {
            obj.SetActive(isButtonBToggled);
        }
    }
}




