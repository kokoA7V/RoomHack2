using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollSystem : MonoBehaviour
{ 
    public ScrollRect scrollRect;
    public float scrollSpd = 1.0f;

    Vector2 targetpos = new Vector2(540,540);
    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;

        if (mousePosition.x > targetpos.x && mousePosition.y > targetpos.y)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            scrollRect.verticalNormalizedPosition += scroll * scrollSpd;
            scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition);//スクロールの速度を制限
        }
    }
}
