using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] GameObject TitleIcon;

    byte iconTrans;

    private void Start()
    {
        iconTrans = 0;

        StartCoroutine(TransUp());
    }

    private void Update()
    {
        TitleIcon.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, iconTrans);
        Debug.Log(TitleIcon.GetComponent<SpriteRenderer>().color);
    }

    IEnumerator TransUp()
    {
        for (int i = 0; i < 255; i++)
        {
            yield return new WaitForSeconds(0.001f);
            iconTrans++;
        }
    }
}
