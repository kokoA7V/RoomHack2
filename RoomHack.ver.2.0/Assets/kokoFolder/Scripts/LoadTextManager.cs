using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTextManager : MonoBehaviour
{
    [SerializeField] GameObject[] TextObj = new GameObject[3];

    [SerializeField] GameObject LoadObj;

    string loadSceneText;

    private void Start()
    {
        TextObj[0].GetComponent<HackText>().textStart = true;

        TextObj[2].GetComponent<Text>().text = "(0/2)";

        // LoadObj.GetComponent<Load>().async.progress

        if(Load.SL == 0)
        {
            loadSceneText = "Scene2";
        }
        else if(Load.SL == 1)
        {

        }

        TextObj[0].GetComponent<HackText>().inputText = loadSceneText + " Data Loading...";
    }

    private void Update()
    {
        if(LoadObj.GetComponent<Load>().async.progress >= 1)
        {
            TextObj[1].GetComponent<HackText>().textDelay = 0;

            TextObj[2].GetComponent<Text>().text = "(2/2)";
        }
        else if (LoadObj.GetComponent<Load>().async.progress >= 0.5f)
        {
            TextObj[0].GetComponent<HackText>().textDelay = 0;

            TextObj[1].GetComponent<HackText>().textStart = true;

            TextObj[2].GetComponent<Text>().text = "(1/2)";
        }
    }
}
