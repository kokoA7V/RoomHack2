using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneTextManager : MonoBehaviour
{
    [SerializeField] GameObject[] hackText = new GameObject[5];
    HackText[] SCT = new HackText[5];

    string[] dispText = new string[13];

    private void Start()
    {
        int index = -1;
        foreach (GameObject obj in hackText)
        {
            index++;
            SCT[index] = obj.GetComponent<HackText>();
        }

        dispText[0] = "ユーザーのデバイスステータスを確認中";
        dispText[1] = "batteryLevel: " + SystemInfo.batteryLevel * 100 + "%";
        dispText[2] = "deviceModel: " + SystemInfo.deviceModel;
        dispText[3] = "deviceType: " + SystemInfo.deviceType;
        dispText[4] = "operatingSystem: " + SystemInfo.operatingSystem;
        dispText[5] = "processorCount: " + SystemInfo.processorCount;
        dispText[6] = "processorFrequency: " + SystemInfo.processorFrequency;
        dispText[7] = "processorType: " + SystemInfo.processorType;
        dispText[8] = "supportsLocationService: " + SystemInfo.supportsLocationService;
        dispText[9] = "systemMemorySize: " + SystemInfo.systemMemorySize;
        dispText[10] = "起動申請を認証中";
        dispText[11] = "認証確認　RoomHack.exe Activate";
        dispText[12] = "...\n";

        SCT[0].inputText = dispText[0];
        SCT[1].inputText = dispText[12];
        SCT[2].inputText = dispText[1] + "\n";
        for (int i = 2; i < 11; i++)
        {
            SCT[2].inputText += dispText[i] + "\n";
        }
        SCT[3].inputText = dispText[12];
        SCT[4].inputText = dispText[11];

        SCT[0].textDelay = 5;
        SCT[1].textDelay = 60;
        SCT[2].textDelay = 5;
        SCT[3].textDelay = 60;
        SCT[4].textDelay = 7;

        SCT[1].afterDelay = 60;
        SCT[3].afterDelay = 60;
    }

    private void Update()
    {
        SCT[0].textStart = true;

        if (SCT[0].textEnd == true)
        {
            SCT[1].textStart = true;
        }

        if (SCT[1].textEnd == true)
        {
            SCT[2].textStart = true;
        }

        if (SCT[2].textEnd == true)
        {
            SCT[3].textStart = true;
        }

        if (SCT[3].textEnd == true)
        {
            SCT[4].textStart = true;
        }

        if (SCT[4].textEnd == true)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
