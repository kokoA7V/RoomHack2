using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HaveMoneyText : MonoBehaviour
{
    public int money;

    [SerializeField]
    private Text HaveText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HaveText.text = money.ToString("C0");
    }
}
