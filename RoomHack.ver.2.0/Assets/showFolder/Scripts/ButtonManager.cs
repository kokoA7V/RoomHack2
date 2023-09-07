using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button buttonA;
    public Button buttonB;

    public GameObject[] buttonTrigA; // ButtonAを押したときに表示するボタンセット
    public GameObject[] buttonTrigB; // ButtonBを押したときに表示するボタンセット

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

        // ButtonAを押したときにはButtonBのセットを非表示にする
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
        // ButtonBを押したときにはButtonAのセットを非表示にする
        foreach (GameObject obj in buttonTrigA)
        {
            obj.SetActive(false);
        }

        // ButtonBのセットを表示する
        foreach (GameObject obj in buttonTrigB)
        {
            obj.SetActive(isButtonBToggled);
        }
    }
}




