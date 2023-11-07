using UnityEngine;
using UnityEngine.UI;

public class HackUI : MonoBehaviour
{
    [HideInInspector]
    public string[] _word;
    [HideInInspector]
    public bool _randomFlg;
    [HideInInspector]
    public HackManager hackManager;
    [HideInInspector]
    public IUnitHack unitHack;
    [HideInInspector]
    public RaycastHit2D hit;
    [HideInInspector]
    public StageTimeManager timeManager;

    public GameObject unHacked, hacked, typing;

    public Image imageIcon;

    public Text titleText, hackLvText, lvText, comentText;

    private bool hackedFlg = false;

    private void Update()
    {
        if (unitHack.hacked) hackedFlg = true;
        else if (!unitHack.hacked && hackedFlg) Destroy(gameObject);
        if (Input.GetKeyDown(KeyCode.Return)) PushButton();
    }

    public void PushButton()
    {
        if (unitHack.hacked) unitHack.StatusDisp();
        else TypingStart();
    }

    private void TypingStart()
    {
        typing.SetActive(true);
        unHacked.SetActive(false);
        hackManager.nowTypingFlg = true;
        //TypingObjにHackManagerから貰ったデータを送る。
        TypingObj typingObj = typing.GetComponentInChildren<TypingObj>();
        typingObj.hackUI = GetComponent<HackUI>();
        typingObj.hit = hit;
        typingObj.timeManager = timeManager;
        typingObj.hackManager = hackManager;
        typingObj.unitHack = unitHack;
        typingObj.randomFlg = _randomFlg;
        Debug.Log(_word.Length);
        typingObj.word = new string[_word.Length];
        for (int i = 0; i < _word.Length; i++) typingObj.word[i] = _word[i];
    }
}