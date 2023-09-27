using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] GameObject[] Obj = new GameObject[8];
    // 0 Title
    // 1 ID
    // 2 Pass
    // 3 Login
    // 4 Exit
    // 5 Title2
    // 6 IDText
    // 7 PassText

    float delay;

    private void Start()
    {
        Obj[0].SetActive(true);
        Obj[1].SetActive(false);
        Obj[2].SetActive(false);
        Obj[3].SetActive(false);
        Obj[4].SetActive(false);
        Obj[5].SetActive(false);

        delay = 0;

    }

    private void Update()
    {
        delay++;
        if (delay >= 180)
        {
            Obj[0].SetActive(false);
            Obj[1].SetActive(true);
            Obj[2].SetActive(true);
            Obj[3].SetActive(true);
            Obj[4].SetActive(true);
            Obj[5].SetActive(true);
        }
    }
}
