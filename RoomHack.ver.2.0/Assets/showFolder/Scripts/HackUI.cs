using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackUI : MonoBehaviour
{
    public RectTransform HackUIPos;
    public GameObject typingobj;
    public string[] _word;
    public bool _randomFlg;

    public Vector2 targetPosition = new Vector2(541f, -260); //inspector�Ŏ��R�ɕύX�\

    void Start()
    {
        // RectTransform�̍��W��ݒ�
        HackUIPos.anchoredPosition = targetPosition;
    }
    void Update()
    {
        
    }

    public void TypingStart()
    {
        GameObject obj = Instantiate(typingobj);

        //TypingObj��HackManager���������f�[�^�𑗂�B
        TypingObj typing = obj.GetComponent<TypingObj>();
        typing.randomFlg = _randomFlg;
        typing.word = new string[_word.Length];
        for (int i = 0; i < _word.Length; i++) typing.word[i] = _word[i];

        Destroy(gameObject);
    }
}
