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
    public void ShowImage(int n)
    {
        sprNo = n;
    }
    public void UnShowImage()
    {
        sprNo = 0;
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
