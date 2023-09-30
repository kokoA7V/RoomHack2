using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageButton : MonoBehaviour,IPointerEnterHandler
{
    public ImageManager imagemanager;

    [SerializeField] int showImageNo;　//画像が表示される番号

    public void OnPointerEnter(PointerEventData eventData) //UI要素に触れたとき
    {
        Debug.Log("a");
        imagemanager.ShowImage(showImageNo);
    }
}
