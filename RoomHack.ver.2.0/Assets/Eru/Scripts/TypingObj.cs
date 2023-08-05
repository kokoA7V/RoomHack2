using System;
using UnityEngine;
using UnityEngine.UI;

public class TypingObj : MonoBehaviour
{
    public string[] word;

    [SerializeField]
    private Text text;

    [SerializeField]
    private bool randomFlg = false;

    private int i,clearWord;

    private bool clearFlg = false;

    void Start()
    {
        i = 0;
        clearWord = 0;
        clearFlg = false;
        if (randomFlg) ShuffleArray(word);
        text.text = word[clearWord].ToString();
    }

    void Update()
    {
        if (Input.anyKeyDown && !clearFlg)
        {
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    if (code.ToString() == word[clearWord][i].ToString())
                    {
                        i++;
                        if (i == word[clearWord].Length)
                        {
                            i = 0;
                            if (clearWord == word.Length - 1)
                            {
                                text.text = "GameClear";
                                clearFlg = true;
                            }
                            else
                            {
                                clearWord++;
                                text.text = word[clearWord].ToString();
                            }
                        }
                    }
                    else
                    {
                        //ƒ~ƒXˆ—
                        Debug.Log("Miss");
                    }
                }
            }
        }

        
    }

    void ShuffleArray(string[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            string value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }
}