using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMesManager : MonoBehaviour
{
    [HideInInspector]
    public bool stopFlg = false;

    [HideInInspector]
    public bool clearFlg = false;

    [HideInInspector]
    public bool storyflag = false;　//ストーリー進行中のフラグ

    [HideInInspector]
    public bool storyclearflag = false; //ストーリークリアしたらtrueにする（テキスト表示用）

    [HideInInspector]
    public int i = 0;

    [HideInInspector]
    public bool textFlg = true;

    [SerializeField, Header("名前")]
    private Text nameText;

    [SerializeField, Header("本文")]
    private Text mineText;

    [SerializeField, Header("バックグラウンド")]
    private Image backGround;

    [SerializeField, Header("アイコン")]
    private Image icon;

    [SerializeField, Header("AIアイコン")]
    private Sprite aiSprite;

    [SerializeField, Header("Astraアイコン")]
    private Sprite astraSprite;

    [SerializeField, Header("バックグラウンド開閉時間")]
    private float bgTime = 1f;

    [SerializeField, Header("テキスト表示時間")]
    private float textTime = 0.1f;

    [SerializeField, Header("チュートリアル文"), Multiline]
    private string[] mineStr;

    [SerializeField]
    private Image endIcon;


    private readonly string aiName = "AI";
    private readonly string astraName = "Astra";


    private float timer = 0;

    private float scale = 0;

    private bool startFlg = false;

    private bool bgOpenFlg = false;




    void Start()
    {
        //アイコン設定
        //icon.sprite = aiSprite;

        ////テキスト
        if (storyflag || storyclearflag)
        {
            nameText.text = aiName;
            mineText.text = "";
        }
        textFlg = true;
        startFlg = false;

        //バックグラウンドを開く
        scale = 0;
        backGround.transform.localScale = new Vector3(1, scale, 1);
        timer = bgTime;
        bgOpenFlg = true;

        endIcon.enabled = false;
    }

    void Update()
    {
        if (clearFlg) return;
        storyclearflag = true;
        //バックグラウンド開閉
        if (storyclearflag && bgOpenFlg && scale < 1)
        {
            if (timer > 0) timer -= Time.deltaTime;
            else
            {
                timer = bgTime * 0.0001f;
                scale += 0.1f;
                backGround.transform.localScale = new Vector3(1, Mathf.Clamp01(scale), 1);
            }
        }
        else if (storyclearflag && !bgOpenFlg && scale > 0)
        {
            if (timer > 0) timer -= Time.deltaTime;
            else
            {
                timer = bgTime * 0.0001f;
                scale -= 0.1f;
                backGround.transform.localScale = new Vector3(1, Mathf.Clamp01(scale), 1);
            }
        }

        //Dialogue起動
        if (storyclearflag && scale >= 1)
        {
            startFlg = true;
            textFlg = false;
            Dialogue();
        }


        if (!bgOpenFlg && scale <= 0) clearFlg = true;

        if (Input.anyKeyDown && startFlg && !stopFlg)
        {
            if (!textFlg)
            {
                if (i >= mineStr.Length)
                {
                    //StartCoroutine(EndTutorial());
                    return;
                }
                textFlg = true;
                endIcon.enabled = true;
                mineText.text = mineStr[i];
                i++;
            }
            else
            {
                textFlg = false;
                endIcon.enabled = false;
                //if (i >= mineStr.Length) StartCoroutine(EndTutorial());
            }
        }
    }

    //private IEnumerator EndTutorial()
    //{
    //    yield return new WaitForSeconds(0.5f);

    //    bgOpenFlg = false;

    //    yield break;
    //}


    public void  Dialogue()
    {
        AudioPlay.instance.SEPlay(4);
        if (clearFlg || i >= mineStr.Length) return; // インデックスが範囲外の場合は処理を中断

        IconSetting();
        mineText.text = "";

        foreach (var word in mineStr[i])
        {
            if (textFlg) return;
            mineText.text += word;
        }

        i++; // テキストが表示される番号の加算処理
        textFlg = true;
        endIcon.enabled = true;
        return;
    }

    private void IconSetting()
    {
        //アイコン設定
        //以下の処理で上記のi++でアイコンやテキストを管理していると思う。

        if (i == 2 || i == 45) icon.sprite = astraSprite;
        else if (i == 44 || i == 48) icon.sprite = aiSprite;

        if (i == 2) nameText.text = "???";
        else if (i == 3) nameText.text = astraName;
        else if (i == 44) nameText.text = aiName;
        else if (i == 45) nameText.text = astraName;
        else if (i == 48) nameText.text = aiName;
    }
}
