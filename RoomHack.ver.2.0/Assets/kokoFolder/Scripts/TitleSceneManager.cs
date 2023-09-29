using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    Button loginButton;
    Button exitButton;

    [SerializeField] GameObject[] hackText = new GameObject[7];
    [SerializeField] GameObject[] titleObj = new GameObject[6];
    // 0 title
    // 1 id
    // 2 pass
    // 3 login
    // 4 exit
    // 5 subtitle

    HackText[] SCT = new HackText[7];
    // 5 ID
    // 6 Pass

    string[] dispText = new string[15];

    float textDelayTotal = 0.5f;

    int count;
    float time;
    float delay = 0.3f;
    Vector3 move;
    Vector3 move2;

    private void Start()
    {
        loginButton = titleObj[3].GetComponent<Button>();
        loginButton.onClick.AddListener(() => SCT[5].textStart = true);

        exitButton = titleObj[4].GetComponent<Button>();
        exitButton.onClick.AddListener(GameExit);

        int index = -1;
        foreach (GameObject obj in hackText)
        {
            index++;
            SCT[index] = obj.GetComponent<HackText>();
        }

        dispText[0] = "Checking user device status";
        dispText[1] = "batteryLevel: " + SystemInfo.batteryLevel * 100 + "%";
        dispText[2] = "deviceModel: " + SystemInfo.deviceModel;
        dispText[3] = "deviceType: " + SystemInfo.deviceType;
        dispText[4] = "operatingSystem: " + SystemInfo.operatingSystem;
        dispText[5] = "processorCount: " + SystemInfo.processorCount;
        dispText[6] = "processorFrequency: " + SystemInfo.processorFrequency;
        dispText[7] = "processorType: " + SystemInfo.processorType;
        dispText[8] = "supportsLocationService: " + SystemInfo.supportsLocationService;
        dispText[9] = "systemMemorySize: " + SystemInfo.systemMemorySize;
        dispText[10] = "Activation application is being verified";
        dispText[11] = "Authentication Confirmation RoomHack.exe Activate";
        dispText[12] = "...\n";
        // id
        dispText[13] = SystemInfo.deviceName;
        // pass
        dispText[14] = "********";

        SCT[0].inputText = dispText[0];
        SCT[1].inputText = dispText[12];
        SCT[2].inputText = dispText[1] + "\n";
        for (int i = 2; i < 11; i++)
        {
            SCT[2].inputText += dispText[i] + "\n";
        }
        SCT[3].inputText = dispText[12];
        SCT[4].inputText = dispText[11];

        SCT[5].inputText = dispText[13];
        SCT[6].inputText = dispText[14];

        SCT[0].textDelay = 5 * textDelayTotal;
        SCT[1].textDelay = 60 * textDelayTotal;
        SCT[2].textDelay = 5 * textDelayTotal;
        SCT[3].textDelay = 60 * textDelayTotal;
        SCT[4].textDelay = 7 * textDelayTotal;

        SCT[1].afterDelay = 60;
        SCT[3].afterDelay = 60;


        titleObj[1].SetActive(false);
        titleObj[2].SetActive(false);
        titleObj[3].SetActive(false);
        titleObj[4].SetActive(false);
        titleObj[5].SetActive(false);

        count = 0;
        time = 0;
        move = new Vector3(0, 50, 0);
        move2 = new Vector3(0, 0.46f, 0);

        if (Load.SL == 1)
        {
            for (int i = 0; i < 5; i++)
            {
                SCT[i].textEnd = true;
            }

            for (int i = 0; i < 5; i++)
            {
                SCT[i].GetComponent<RectTransform>().anchoredPosition3D += move * 25;
            }
            titleObj[0].transform.position += move2 * 25;
            titleObj[1].transform.position += move2 * 25;
            titleObj[2].transform.position += move2 * 25;
            titleObj[3].GetComponent<RectTransform>().anchoredPosition3D += move * 25;
            titleObj[4].GetComponent<RectTransform>().anchoredPosition3D += move * 25;
            titleObj[5].transform.position += move2 * 25;

            count = 25;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < 5; i++)
            {
                SCT[i].textDelay = 0;
            }
            delay = 0.1f;
        }

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
            //SceneManager.LoadScene("TitleScene");

            time += Time.deltaTime;
            if(time >= delay)
            {
                if (count < 25)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        SCT[i].GetComponent<RectTransform>().anchoredPosition3D += move;
                    }
                    titleObj[0].transform.position += move2;
                    titleObj[1].transform.position += move2;
                    titleObj[2].transform.position += move2;
                    titleObj[3].GetComponent<RectTransform>().anchoredPosition3D += move;
                    titleObj[4].GetComponent<RectTransform>().anchoredPosition3D += move;
                    titleObj[5].transform.position += move2;
                }

                time = 0;
                count++;
            }

            if (count >= 28)
            {
                titleObj[5].SetActive(true);
            }

            if (count >= 29)
            {
                titleObj[1].SetActive(true);
                titleObj[2].SetActive(true);
            }

            if (count >= 30)
            {
                titleObj[3].SetActive(true);
                titleObj[4].SetActive(true);
            }


            if (SCT[5].textEnd)
            {
                SCT[6].textStart = true;
            }

            if(SCT[6].textEnd)
            {
                Load.SL = 2;
                SceneManager.LoadScene("LoadScene");
            }

        }
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
#endif
    }
}
