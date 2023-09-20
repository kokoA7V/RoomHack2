using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Button buttonA;
    [SerializeField] Button buttonB;
    [SerializeField] Button OptionButton;

    [SerializeField] GameObject[] buttonTrigA; // ButtonAを押したときに表示するボタンセット
    [SerializeField] GameObject[] buttonTrigB; // ButtonBを押したときに表示するボタンセット
    public  GameObject OptionTrig;

    [SerializeField] ImageManager imagemanager;

    [SerializeField] bool[] isButtonToggled;
    [SerializeField] bool OptionTri = false;

    const int toggleNo = 3;

    void Start()
    {
        isButtonToggled = new bool[toggleNo] { false, false, false };

        //Unity内にあるOnClickの処理をプログラムで

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

    private void Update()
    {
     
    }
}




