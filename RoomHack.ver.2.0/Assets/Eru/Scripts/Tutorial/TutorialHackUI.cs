using UnityEngine;
using UnityEngine.UI;

public class TutorialHackUI : MonoBehaviour
{
    public RectTransform HackUIPos;
    public GameObject typingobj;

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
    private Image button;

    [SerializeField]
    private Vector2 targetPosition = new Vector2(541f, -260);

    public Image imageBG, imageIcon;

    public Text titleText, lvText, comentText;

    private bool hackedFlg = false;

    void Start()
    {
        // RectTransformの座標を設定
        HackUIPos.anchoredPosition = targetPosition;
        tutorialManager.hackButton = button;
        if (unitHack.hacked) hackedFlg = true;
        else hackedFlg = false;
    }

    private void Update()
    {
        if (!unitHack.hacked && hackedFlg) Destroy(gameObject);
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
        GameObject obj = Instantiate(typingobj);
        tutorialHackManager.nowTypingFlg = true;
        //TypingObjにHackManagerから貰ったデータを送る。
        TutorialTyping typing = obj.GetComponent<TutorialTyping>();
        typing.hit = hit;
        typing.timeManager = timeManager;
        typing.tutorialHackManager = tutorialHackManager;
        typing.tutorialManager = tutorialManager;
        typing.unitHack = unitHack;
        typing.randomFlg = _randomFlg;
        typing.word = new string[_word.Length];
        for (int i = 0; i < _word.Length; i++) typing.word[i] = _word[i];

        Destroy(gameObject);
    }
}
