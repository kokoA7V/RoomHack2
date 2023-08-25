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

    public Vector2 targetPosition = new Vector2(555f, -140f); //inspector�Ŏ��R�ɕύX�\

    void Start()
    {
        // RectTransform�̍��W��ݒ�
        HackUIPos.anchoredPosition = targetPosition;
        typingobj = gameObject.GetComponent<TypingObj>();
    }
    void Update()
    {
        typingobj.word = _word;
        typingobj.randomFlg = _randomFlg;
    }
}
