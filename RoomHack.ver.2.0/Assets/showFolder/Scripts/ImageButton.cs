using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageButton : MonoBehaviour,IPointerEnterHandler
{
    public ImageManager imagemanager;

    [SerializeField] int showImageNo;�@//�摜���\�������ԍ�

    public void OnPointerEnter(PointerEventData eventData) //UI�v�f�ɐG�ꂽ�Ƃ�
    {
        Debug.Log("a");
        imagemanager.ShowImage(showImageNo);
    }
}
