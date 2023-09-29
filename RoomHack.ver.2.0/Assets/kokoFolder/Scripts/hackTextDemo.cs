using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class hackTextDemo : MonoBehaviour
{
    [SerializeField] string[] inputText = new string[] { "hello" };
    string outputText = "";
    string dispText;
    string timeText;

    [SerializeField] float textSpd = 10;
    [SerializeField] int lineNo = 30;
    float time = 0;
    int count = 0;

    [SerializeField] bool timeDisp = true;

    public bool textStart = false;
    public bool textEnd = false;

    Text tx;

    DateTime dt;

    private void Start()
    {
        tx = GetComponent<Text>();

        inputText[0] = "ユーザーのデバイスステータスを確認中…                                   ";
        inputText[1] = "batteryLevel: " + SystemInfo.batteryLevel * 100 + "%";
        inputText[2] = "deviceModel: " + SystemInfo.deviceModel;
        inputText[3] = "deviceType: " + SystemInfo.deviceType;
        inputText[4] = "operatingSystem: " + SystemInfo.operatingSystem;
        inputText[5] = "processorCount: " + SystemInfo.processorCount;
        inputText[6] = "processorFrequency: " + SystemInfo.processorFrequency;
        inputText[7] = "processorType: " + SystemInfo.processorType;
        inputText[8] = "supportsLocationService: " + SystemInfo.supportsLocationService;
        inputText[9] = "systemMemorySize: " + SystemInfo.systemMemorySize;
        inputText[10] = "起動申請を認証中…                                                         ";
        inputText[11] = "認証確認　RoomHack.exe Activate                              ";

        dt = DateTime.Now;
        timeText = "[" + dt + "]";
        if (timeDisp)
        {
            dispText += timeText;
        }

        foreach(string str in inputText)
        {
            dispText += str;
            dispText += "\n";
        }

        //dispText += inputText[0];
    }

    private void Update()
    {
        if (textStart)
        {
            tx.text = outputText;

            //Debug.Log(dispText.Length);
            //Debug.Log(outputText.Length);

            time++;
            if (time >= textSpd && dispText.Length > outputText.Length)
            {
                outputText += dispText[count];
                time = 0;
                count++;

                //if (count % lineNo == 0)
                //{
                //    dispText += "\n";
                //}
            }
            else if (dispText.Length <= outputText.Length)
            {
                textEnd = true;
            }
        }
    }
}
