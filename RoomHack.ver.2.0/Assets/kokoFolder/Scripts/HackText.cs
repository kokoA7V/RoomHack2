using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HackText : MonoBehaviour
{
    public bool textStart = false;
    public bool textEnd = false;

    public int textDelay = 10;

    public int beforeDelay = 0;
    public int afterDelay = 0;

    int bt = 0;
    int at = 0;

    int count = 0;
    int time = 0;

    public string inputText = "hello world";

    string outputText;

    Text tx;

    private void Start()
    {
        tx = GetComponent<Text>();
        tx.text = "";
    }

    private void Update()
    {
        if (textStart && !textEnd)
        {
            bt++;
            if (bt >= beforeDelay)
            {
                time++;
                if (time >= textDelay && inputText.Length > count)
                {
                    outputText += inputText[count];
                    time = 0;
                    count++;
                }
                else if (inputText.Length <= count)
                {
                    at++;
                    if (at >= afterDelay)
                    {
                        textEnd = true;
                    }
                }
            }

            tx.text = outputText;
        }
    }
}
