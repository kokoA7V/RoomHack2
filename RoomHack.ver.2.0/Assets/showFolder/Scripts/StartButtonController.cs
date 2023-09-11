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
        StartCoroutine(FlashMove()); //�R���`�[���J�n
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FlashMove()  //Text�̓_��
    {
        //text��color�̐F�𒼐ڐ؂�ւ���B
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
