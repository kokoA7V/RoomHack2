using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackUI : MonoBehaviour
{
    public RectTransform HackUIPos;
    public TypingObj typingobj;
    public string[] _word;
    public bool _randomFlg;

    public Vector2 targetPosition = new Vector2(555f, -140f); //inspectorで自由に変更可能

    void Start()
    {
        // RectTransformの座標を設定
        HackUIPos.anchoredPosition = targetPosition;
        typingobj = gameObject.GetComponent<TypingObj>();
    }
    void Update()
    {
        typingobj.word = _word;
        typingobj.randomFlg = _randomFlg;
    }
}
