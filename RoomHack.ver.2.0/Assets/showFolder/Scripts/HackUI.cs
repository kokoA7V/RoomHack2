using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackUI : MonoBehaviour
{
    public RectTransform HackUIPos;

    public Vector2 targetPosition = new Vector2(555f, -140f);

    void Start()
    {
        // RectTransform�̍��W��ݒ�
        HackUIPos.anchoredPosition = targetPosition;
    }
    void Update()
    {
        
    }
}
