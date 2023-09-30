using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    [SerializeField] Image im;

    [SerializeField] Sprite[] spr;

    int sprNo = 0;

    // Start is called before the first frame update
    public void ShowImage(int n) //引数n
    {
        sprNo = n;　//nに入る番号によって画像が切り替えれるように。
    }
    public void UnShowImage()
    {
        sprNo = 0; //非表示にする。0の番号には透明な画像を入れる予定。
    }

    private void Update()
    {
        im.sprite = spr[sprNo];

        if (sprNo == 0)
        {
            im.color = new Color(0, 0, 0, 0);
        }
        else
        {
            im.color = new Color(255, 255, 255, 255);
        }
    }
}
