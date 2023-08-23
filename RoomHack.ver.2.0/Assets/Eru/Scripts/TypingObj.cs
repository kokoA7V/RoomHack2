using System;
using UnityEngine;
using UnityEngine.UI;

public class TypingObj : MonoBehaviour
{
    [Header("ワード")]
    public string[] word;

    [Header("ランダムワード")]
    public bool randomFlg = false;

    [SerializeField,Header("UIテキスト")]
    private Text text;

    [SerializeField,Header("クリア文字の色")]
    private Color clearColor;

    [SerializeField, Header("ミス文字の色")]
    private Color missColor;

    [SerializeField,Header("振動時間")]
    private float shakeDuration = 0.1f;

    [SerializeField,Header("振動する力")]
    private float shakeAmount = 5f;

    [SerializeField,Header("減衰率")]
    private float decreaseFactor = 1.0f;

    private string clearColorCode, missColorCode;

    private int i, clearWord;

    private bool clearFlg = false;

    private Vector3 originalPosition;

    private float currentShakeDuration = 0f;

    void Start()
    {
        i = 0;
        clearWord = 0;
        clearFlg = false;
        if (randomFlg) ShuffleArray(word);
        originalPosition = text.transform.localPosition;
        clearColorCode = ColorUtility.ToHtmlStringRGB(clearColor);
        missColorCode = ColorUtility.ToHtmlStringRGB(missColor);
        text.text = "<color=#" + clearColorCode + "></color>" + word[clearWord].ToString();
    }

    void Update()
    {
        if (clearFlg) return;

        if (Input.anyKeyDown)
        {
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (code >= KeyCode.A && code <= KeyCode.Z && Input.GetKeyDown(code))
                {
                    if (code.ToString() == word[clearWord][i].ToString())
                    {
                        i++;

                        //色変更処理
                        string _text = "<color=#" + clearColorCode + ">";
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
                                text.text = "<color=#" + clearColorCode + "></color>" + word[clearWord].ToString();
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

        if (currentShakeDuration > 0 && !clearFlg)
        {
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * shakeAmount;
            text.transform.localPosition = originalPosition + randomOffset;

            string _text = "<color=#" + clearColorCode + ">";
            for (int j = 0; j < i; j++) _text = _text + word[clearWord][j].ToString();
            _text = _text + "</color><color=#" + missColorCode + ">" + word[clearWord][i].ToString() + "</color>";
            for (int j = i + 1; j < word[clearWord].Length; j++) _text = _text + word[clearWord][j].ToString();
            text.text = _text;

            currentShakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else if(!clearFlg)
        {
            currentShakeDuration = 0f;
            text.transform.localPosition = originalPosition;

            string _text = "<color=#" + clearColorCode + ">";
            for (int j = 0; j < i; j++) _text = _text + word[clearWord][j].ToString();
            _text = _text + "</color>";
            for (int j = i; j < word[clearWord].Length; j++) _text = _text + word[clearWord][j].ToString();
            text.text = _text;
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