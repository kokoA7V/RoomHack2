using UnityEngine;
using UnityEngine.UI;

public class HackUI : MonoBehaviour
{
    public RectTransform HackUIPos;
    public GameObject typingobj;

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

    [SerializeField]
    private Vector2 targetPosition = new Vector2(541f, -260);

    public Image imageBG,imageIcon;

    public Text titleText, lvText, comentText;

    private bool hackedFlg = false;

    void Start()
    {
        // RectTransformの座標を設定
        HackUIPos.anchoredPosition = targetPosition;
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
        if (unitHack.hacked) unitHack.StatusDisp();
        else TypingStart();
    }

    private void TypingStart()
    {
        GameObject obj = Instantiate(typingobj);
        hackManager.nowTypingFlg = true;
        //TypingObjにHackManagerから貰ったデータを送る。
        TypingObj typing = obj.GetComponent<TypingObj>();
        typing.hit = hit;
        typing.hackManager = hackManager;
        typing.unitHack = unitHack;
        typing.randomFlg = _randomFlg;
        typing.word = new string[_word.Length];
        for (int i = 0; i < _word.Length; i++) typing.word[i] = _word[i];

        Destroy(gameObject);
    }
}
