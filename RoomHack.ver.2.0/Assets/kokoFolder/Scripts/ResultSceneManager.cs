using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour
{
    [SerializeField] GameObject[] RObj;
    // 0 Result
    // 1 Time
    // 2 TimeValue
    // 3 Lost
    // 4 LostValue
    // 5 Reward
    // 6 RewardValue
    // 7 Button

    HackText[] HT = new HackText[3];

    Button AcceptButton;

    public static float resultTime;
    public static float resultLost;
    public static float resultReward;
    
    float time;

    bool skipStart = false;
    bool skipEnd = false;

    private void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            RObj[i].SetActive(false);
        }

        time = 0;

        // demo
        // resultTime = 34.56f;
        // resultLost = 2;
        // resultReward = 5000;

        HT[0] = RObj[2].GetComponent<HackText>();
        HT[1] = RObj[4].GetComponent<HackText>();
        HT[2] = RObj[6].GetComponent<HackText>();

        HT[0].inputText = resultTime.ToString("F");
        HT[1].inputText = resultLost.ToString();
        HT[2].inputText = resultReward.ToString();

        AcceptButton = RObj[7].GetComponent<Button>();
        AcceptButton.onClick.AddListener(AcceptOnClick);
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= 1)
        {
            RObj[0].SetActive(true);

            if (Input.anyKeyDown)
            {
                skipStart = true;
            }
        }

        if (time >= 2)
        {
            RObj[1].SetActive(true);
        }

        if (time >= 2.5)
        {
            RObj[2].SetActive(true);
            HT[0].textStart = true;
        }

        if (time >= 3.5)
        {
            RObj[3].SetActive(true);
        }

        if (time >= 4)
        {
            RObj[4].SetActive(true);
            HT[1].textStart = true;
        }

        if (time >= 5)
        {
            RObj[5].SetActive(true);
        }

        if (time >= 5.5)
        {
            RObj[6].SetActive(true);
            HT[2].textStart = true;
        }

        if (time >= 6.5)
        {
            RObj[7].SetActive(true);
        }

        if (skipStart && !skipEnd)
        {
            time = 10;

            HT[0].textEnd = true;
            HT[1].textEnd = true;
            HT[2].textEnd = true;

            skipEnd = true;
        }
    }

    void AcceptOnClick()
    {
        Load.SL = 2;
        SceneManager.LoadScene("LoadScene");
    }
}
