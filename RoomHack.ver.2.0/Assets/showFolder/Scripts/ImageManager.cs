using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{

    [SerializeField] Image[] spr;

    int sprNo = 0;

    // Start is called before the first frame update
    public void ShowImage(int n) //引数n
    {
        spr[n].gameObject.SetActive(true);
    }
    public void UnShowImage(int n)
    {
        spr[n].gameObject.SetActive(false);
    }

    private void Update()
    {
        //im.sprite = spr[sprNo];

        //if (sprNo == 0)
        //{
        //    spr[sprNo].gameObject.SetActive(false);
        //    //im.color = new Color(0, 0, 0, 0);
        //}
        //else
        //{
        //    spr[sprNo].gameObject.SetActive(true);
        //    //im.color = new Color(255, 255, 255, 255);
        //}
    }
}
