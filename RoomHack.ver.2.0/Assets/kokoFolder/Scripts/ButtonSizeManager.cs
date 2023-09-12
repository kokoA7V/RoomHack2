using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSizeManager : MonoBehaviour
{
    [SerializeField]float size = 1;

    GameObject left;
    GameObject middle;
    GameObject right;

    private void Start()
    {
        left = transform.GetChild(0).gameObject;
        middle = transform.GetChild(1).gameObject;
        right = transform.GetChild(2).gameObject;
    }

    private void Update()
    {
        middle.transform.localScale = new Vector2(size, 1);
        middle.transform.localPosition = new Vector2(0.32f + size * 0.32f, 0);
        right.transform.localPosition = new Vector2(0.64f + size * 0.64f, 0);
    }
}
