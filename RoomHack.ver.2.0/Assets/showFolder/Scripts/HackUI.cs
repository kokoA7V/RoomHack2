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
        //TypingObj��HackManager���������f�[�^�𑗂�B
        typingobj.randomFlg = _randomFlg;
        for(int i = 0; i < _word.Length; i++)
        {
            typingobj.word[i] = _word[i];
        }
    }
}
