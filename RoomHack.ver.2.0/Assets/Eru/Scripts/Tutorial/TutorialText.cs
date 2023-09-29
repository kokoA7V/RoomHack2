using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    [SerializeField, Header("名前")]
    private Text nameText;

    [SerializeField,Header("本文")]
    private Text mineText;

    [SerializeField, Header("バックグラウンド")]
    private Image backGround;

    [SerializeField,Header("アイコン")]
    private Image icon;

    [SerializeField, Header("AIアイコン")]
    private Sprite aiSprite;

    [SerializeField, Header("Eruアイコン")]
    private Sprite eruSprite;

    [SerializeField, Header("バックグラウンド開閉時間")]
    private float bgTime = 1f;

    [SerializeField, Header("テキスト表示時間")]
    private float textTime = 0.1f;

    [SerializeField, Header("チュートリアル文"), Multiline]
    private string[] mineStr;

    private string aiName = "AI";
    private string eruName = "Eru";

    private int i = 0;

    private float timer = 0;

    private float scale = 0;

    private bool startFlg = false;  

    private bool textFlg = true;

    private bool bgOpenFlg = false;

    void Start()
    {
        //アイコン設定
        icon.sprite = aiSprite;

        //テキスト設定
        nameText.text = aiName;
        mineText.text = "";
        textFlg = true;
        startFlg = false;

        //バックグラウンドを開く
        scale = 0;
        backGround.transform.localScale = new Vector3(1, scale, 1);
        timer = bgTime;
        bgOpenFlg = true;
    }

    void Update()
    {

        //バックグラウンド開閉
        if (bgOpenFlg && scale < 1)
        {
            if (timer > 0) timer -= Time.deltaTime;
            else
            {
                timer = bgTime *= 0.0001f;
                scale += 0.1f;
                backGround.transform.localScale = new Vector3(1, Mathf.Clamp01(scale), 1);
            }
        }
        else if (!bgOpenFlg && scale > 0)
        {
            if (timer > 0) timer -= Time.deltaTime;
            else
            {
                timer = bgTime *= 0.0001f;
                scale -= 0.1f;
                backGround.transform.localScale = new Vector3(1, Mathf.Clamp01(scale), 1);
            }
        }

        //最初の一回だけ起動
        if(!startFlg && bgOpenFlg && scale >= 1)
        {
            startFlg = true;
            textFlg = false;
            StartCoroutine(Dialogue());
        }

        if (!bgOpenFlg && scale <= 0) Debug.Log("チュートリアルクリア");

        if (Input.anyKeyDown && startFlg)
        {
            if (!textFlg)
            {
                textFlg = true;
                mineText.text = mineStr[i];
                i++;
                if (i >= mineStr.Length - 1) bgOpenFlg = false;
            }
            else
            {
                textFlg = false;
                StartCoroutine(Dialogue());
            }
        }
    }
    
    private IEnumerator Dialogue()
    {
        IconSetting();
        mineText.text = "";
        foreach (var word in mineStr[i])
        {
            if (textFlg) yield break;
            mineText.text += word;
            yield return new WaitForSeconds(textTime);
        }
        i++;
        if (i >= mineStr.Length - 1) bgOpenFlg = false;
        textFlg = true;
        yield break;
    }

    private void IconSetting()
    {
        //アイコン設定
        if (i == 2 || i == 45) icon.sprite = eruSprite;
        else if (i == 44) icon.sprite = aiSprite;

        if (i == 2) nameText.text = "???";
        else if (i == 3) nameText.text = eruName;
        else if (i == 44) nameText.text = aiName;
        else if (i == 45) nameText.text = eruName;
    }
}
