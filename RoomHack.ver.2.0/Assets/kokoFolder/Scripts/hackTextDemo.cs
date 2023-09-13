using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class hackTextDemo : MonoBehaviour
{
    [SerializeField] string inputText = "hello";
    string outputText = "";
    string dispText;
    string timeText;

    [SerializeField] float textSpd = 10;
    float time = 0;
    int count = 0;

    [SerializeField] bool timeDisp = true;

    Text tx;

    DateTime dt;

    private void Start()
    {
        tx = GetComponent<Text>();

        dt = DateTime.Now;
        timeText = "[" + dt + "]";
        if (timeDisp)
        {
            dispText += timeText;
        }

        dispText += inputText;
    }

    private void Update()
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
        }
    }
}
