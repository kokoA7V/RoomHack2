using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class hackTextDemo : MonoBehaviour
{
    [SerializeField] string hackText = "hello";

    string dispText;

    [SerializeField] float textSpd = 10;

    float time = 0;
    int count = 0;

    Text text;

    DateTime dt;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = dispText;
        dt = DateTime.Now;

        time++;
        if (time >= textSpd)
        {
            string timeText = "[" + dt + "]";
            timeText += hackText;
            dispText += timeText[count];
            time = 0;
            count++;
        }
    }
}
