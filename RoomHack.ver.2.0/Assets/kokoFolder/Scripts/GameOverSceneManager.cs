using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverSceneManager : MonoBehaviour
{
    [SerializeField] GameObject[] GOObj;
    // 0 noise
    // 1 warning
    // 2 gameover
    // 3 text1 lost
    // 4 text2 reboot
    // 5 reboot
    // 6 back

    HackText HT1;
    HackText HT2;

    Button RebootButton;
    Button BackButton;

    public static int GameOverNo = 0;
    // 0 謎
    // 1 味方死亡
    // 2 逆探知

    string[] GOTx = new string[3];

    float time;

    float trans;

    bool skipStart = false;
    bool skipEnd = false;

    private void Start()
    {
        for (int i = 1; i < 7; i++)
        {
            GOObj[i].SetActive(false);
        }

        GOTx[0] = "Setteimisu static GameOverSceneManager.GameOverNo Wo Kaetene";
        GOTx[1] = "Mate Unit Signal Lost";
        GOTx[2] = "Detects Suspicious Connection From Outside";

        time = 0;
        trans = 1;

        HT1 = GOObj[3].GetComponent<HackText>();
        HT2 = GOObj[4].GetComponent<HackText>();

        HT1.inputText = GOTx[GameOverNo] + "\nSelf Disconnection System Activate";
        HT2.inputText = "Reboot RoomHack.exe";

        RebootButton = GOObj[5].GetComponent<Button>();
        RebootButton.onClick.AddListener(RebootOnClick);

        BackButton = GOObj[6].GetComponent<Button>();
        BackButton.onClick.AddListener(BackOnClick);
    }

    private void Update()
    {
        time += Time.deltaTime;

        GOObj[0].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, trans);

        if (time >= 1 && time < 2.5)
        {
            trans -= Time.deltaTime / 2;
        }

        if (time >= 3)
        {
            GOObj[1].SetActive(true);
        }

        if (time >= 3.5)
        {
            GOObj[2].SetActive(true);
        }

        if (time >= 4)
        {
            GOObj[3].SetActive(true);
            HT1.textStart = true;
        }
        
        if (HT1.textEnd)
        {
            GOObj[4].SetActive(true);
            HT2.textStart = true;
        }

        if (HT2.textEnd)
        {
            GOObj[5].SetActive(true);
            GOObj[6].SetActive(true);
        }

        if (Input.anyKeyDown)
        {
            skipStart = true;
        }

        if (skipStart && !skipEnd)
        {
            time = 10;
            trans = 0.25f;

            HT1.textEnd = true;
            HT2.textEnd = true;

            skipEnd = true;
        }
    }

    void RebootOnClick()
    {
        SceneManager.LoadScene("LoadScene");
    }

    void BackOnClick()
    {
        Load.SL = 2;
        SceneManager.LoadScene("LoadScene");
    }
}
