using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSystem : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollSpd = 1.0f;

    void Update()
    {
        //MouseWhell�̒l��scroll�ɑ��
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        scrollRect.verticalNormalizedPosition += scroll * scrollSpd; 
        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition);//�X�N���[���̑��x�𐧌�
    }
}
