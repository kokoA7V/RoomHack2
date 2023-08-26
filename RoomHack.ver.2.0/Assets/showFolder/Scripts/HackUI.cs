using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackUI : MonoBehaviour
{
    public RectTransform HackUIPos;
    public GameObject typingobj;

    [HideInInspector]
    public string[] _word;
    [HideInInspector]
    public bool _randomFlg;
    [HideInInspector]
    public HackManager hackManager;
    [HideInInspector]
    public IUnitHack unitHack;

    public Vector2 targetPosition = new Vector2(541f, -260); //inspectorで自由に変更可能

    void Start()
    {
        // RectTransformの座標を設定
        HackUIPos.anchoredPosition = targetPosition;
    }
    void Update()
    {
        
    }

    public void TypingStart()
    {
        GameObject obj = Instantiate(typingobj);
        hackManager.nowTypingFlg = true;
        //TypingObjにHackManagerから貰ったデータを送る。
        TypingObj typing = obj.GetComponent<TypingObj>();
        typing.hackManager = hackManager;
        typing.unitHack = unitHack;
        typing.randomFlg = _randomFlg;
        typing.word = new string[_word.Length];
        for (int i = 0; i < _word.Length; i++) typing.word[i] = _word[i];

        Destroy(gameObject);
    }
}
