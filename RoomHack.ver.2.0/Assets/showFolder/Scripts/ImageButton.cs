using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageButton : MonoBehaviour,IPointerEnterHandler
{
    public ImageManager imagemanager;

    [SerializeField] int showImageNo;@//‰æ‘œ‚ª•\¦‚³‚ê‚é”Ô†

    public void OnPointerEnter(PointerEventData eventData) //UI—v‘f‚ÉG‚ê‚½‚Æ‚«
    {
        Debug.Log("a");
        imagemanager.ShowImage(showImageNo);
    }
}
