using UnityEngine;
using UnityEngine.UI;

public class TutorialHackUI : MonoBehaviour
{
    [HideInInspector]
    public string[] _word;
    [HideInInspector]
    public bool _randomFlg;
    [HideInInspector]
    public TutorialHackManager tutorialHackManager;
    [HideInInspector]
    public IUnitHack unitHack;
    [HideInInspector]
    public RaycastHit2D hit;
    [HideInInspector]
    public TutorialTimeManager timeManager;
    [HideInInspector]
    public TutorialManager tutorialManager;

    [SerializeField]
    private Image button1,button2;

    public GameObject unHacked, hacked, typing;

    public Image imageIcon;

    public Text titleText, hackLvText, lvText, comentText;

    private bool hackedFlg = false;

    void Start()
    {
        tutorialManager.hackButton1 = button1;
        tutorialManager.hackButton2 = button2;
    }

    private void Update()
    {
        if (unitHack.hacked) hackedFlg = true;
        else if (!unitHack.hacked && hackedFlg) Destroy(gameObject);
        if (Input.GetKeyDown(KeyCode.Return)) PushButton();
    }

    public void PushButton()
    {
        if (!tutorialManager.buttonFlg) return;
        if (unitHack.hacked)
        {
            if (tutorialManager.j == 10|| tutorialManager.j == 15 || tutorialManager.j == 17 || tutorialManager.j == 19) tutorialManager.questFlg = true;
            unitHack.StatusDisp();
            Destroy(gameObject);
        }
        else
        {
            if (tutorialManager.j == 7) tutorialManager.questFlg = true;
            TypingStart();
        }
    }

    private void TypingStart()
    {
        typing.SetActive(true);
        unHacked.SetActive(false);
        tutorialHackManager.nowTypingFlg = true;
        //TypingObjにHackManagerから貰ったデータを送る。
        TutorialTyping typingObj = typing.GetComponentInChildren<TutorialTyping>();
        typingObj.tutorialHackUI = GetComponent<TutorialHackUI>();
        typingObj.hit = hit;
        typingObj.timeManager = timeManager;
        typingObj.tutorialHackManager = tutorialHackManager;
        typingObj.tutorialManager = tutorialManager;
        typingObj.unitHack = unitHack;
        typingObj.randomFlg = _randomFlg;
        typingObj.word = new string[_word.Length];
        for (int i = 0; i < _word.Length; i++) typingObj.word[i] = _word[i];
    }
}
