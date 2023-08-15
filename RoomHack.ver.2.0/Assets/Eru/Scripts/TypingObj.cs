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

    [SerializeField]
    private Color color;

    private string colorCode;

    private int i, clearWord;

    private bool clearFlg = false;

    public float shakeDuration = 0.1f;
    public float shakeAmount = 5f;
    public float decreaseFactor = 1.0f;

    private Vector3 originalPosition;
    private float currentShakeDuration = 0f;

    void Start()
    {
        i = 0;
        clearWord = 0;
        clearFlg = false;
        if (randomFlg) ShuffleArray(word);
        originalPosition = text.transform.localPosition;
        colorCode = ColorUtility.ToHtmlStringRGB(color);
        text.text = "<color=#" + colorCode + "></color>" + word[clearWord].ToString();
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

                        //色変更処理
                        string _text = "<color=#" + colorCode + ">";
                        for (int j = 0; j < i; j++) _text = _text + word[clearWord][j].ToString();
                        _text = _text + "</color>";
                        for(int j = i;j< word[clearWord].Length; j++) _text = _text + word[clearWord][j].ToString();
                        text.text = _text;

                        //1ワードクリア処理
                        if (i == word[clearWord].Length)
                        {
                            i = 0;
                            if (clearWord == word.Length - 1)
                            {
                                //ゲームクリア処理
                                text.text = "GameClear";
                                clearFlg = true;
                            }
                            else
                            {
                                clearWord++;
                                text.text = "<color=#" + colorCode + "></color>" + word[clearWord].ToString();
                            }
                        }
                    }
                    else
                    {
                        //ミス処理
                        currentShakeDuration = shakeDuration;
                    }
                }
            }
        }

        if (currentShakeDuration > 0)
        {
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * shakeAmount;
            text.transform.localPosition = originalPosition + randomOffset;
            text.color = Color.red;
            currentShakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            currentShakeDuration = 0f;
            text.transform.localPosition = originalPosition;
            text.color = Color.white;
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