using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{
    public float flashSpd = 1.0f;

    Text textCom;
    // Start is called before the first frame update
    void Start()
    {
        textCom = GetComponent<Text>();
        StartCoroutine(FlashMove()); //コルチーン開始
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FlashMove()  //Textの点滅
    {
        //textのcolorの色を直接切り替える。
        while (true)
        {
            textCom.color = new Color(textCom.color.r, textCom.color.g, textCom.color.b, 0f);
            yield return new WaitForSeconds(flashSpd);
            textCom.color = new Color(textCom.color.r, textCom.color.g, textCom.color.b, 0.5f);
            yield return new WaitForSeconds(flashSpd);
        }
    }

    public void OnClick()
    {
        FadeManager.Instance.LoadScene("HomeScene", 1.0f);
    }
}
