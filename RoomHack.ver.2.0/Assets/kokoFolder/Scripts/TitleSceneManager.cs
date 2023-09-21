using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] GameObject[] Obj = new GameObject[7];
    // 0 Title
    // 1 ID
    // 2 Pass
    // 3 Login
    // 4 Exit
    // 5 IDText
    // 6 PassText

    byte iconTrans;

    float time;

    bool[] mode = new bool[4]{ false, false, false, false};
    // mode[0] アイコン透過度上昇
    // mode[1] アイコン移動
    // mode[2] textboxとlogin,exit表示
    // mode[3] login押せばテキスト打ってシーン偏移

    float[] delay;
    // 

    private void Start()
    {
        Obj[1].SetActive(false);
        Obj[2].SetActive(false);
        Obj[3].SetActive(false);
        Obj[4].SetActive(false);

        iconTrans = 0;

        time = 0;

        StartCoroutine(TransUp());

        mode[0] = true;
    }

    private void Update()
    {
        if (mode[0])
        {
            Obj[0].GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, iconTrans);
            // Debug.Log(TitleIcon.GetComponent<SpriteRenderer>().color);

            if (iconTrans >= 255)
            {
                mode[1] = true;
            }
        }

        if (mode[1])
        {

        }
    }

    IEnumerator TransUp()
    {
        for (int i = 0; i < 255; i++)
        {
            yield return new WaitForSeconds(0.0005f);
            iconTrans++;
        }
    }
}
