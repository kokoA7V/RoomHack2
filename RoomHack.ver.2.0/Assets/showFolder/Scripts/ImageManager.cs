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
    public void ShowImage(int n) //����n
    {
        sprNo = n;�@//n�ɓ���ԍ��ɂ���ĉ摜���؂�ւ����悤�ɁB
    }
    public void UnShowImage()
    {
        sprNo = 0; //��\���ɂ���B0�̔ԍ��ɂ͓����ȉ摜������\��B
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
