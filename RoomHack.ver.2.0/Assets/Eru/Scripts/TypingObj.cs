using System;
using UnityEngine;
using UnityEngine.UI;

public class TypingObj : MonoBehaviour
{
    [Header("ワード")]
    public string[] word;

    [Header("ランダムワード")]
    public bool randomFlg = false;

    [SerializeField, Header("クリア文字の色")]
    private Color clearColor;

    [SerializeField, Header("ミス文字の色")]
    private Color missColor = Color.red;

    [SerializeField, Header("振動時間")]
    private float shakeDuration = 0.15f;

    [SerializeField, Header("振動する力")]
    private float shakeAmount = 5f;

    [SerializeField, Header("減衰率")]
    private float decreaseFactor = 1.0f;

    [SerializeField, Header("位置")]
    private Vector2 targetPosition = new Vector2(555f, -140f);

    [HideInInspector]
    public HackManager hackManager;

    [HideInInspector]
    public HackUI hackUI;

    [HideInInspector]
    public IUnitHack unitHack;

    [HideInInspector]
    public RaycastHit2D hit;

    [HideInInspector]
    public StageTimeManager timeManager;

    private Text text;

    private string clearColorCode, missColorCode;

    private int i, clearWord;

    private bool clearFlg = false;

    private Vector3 originalPosition;

    private float currentShakeDuration = 0f;

    void Start()
    {
        i = 0;
        clearWord = 0;
        clearFlg = false;
        text = gameObject.GetComponentInChildren<Text>();
        this.transform.position = targetPosition;
        if (randomFlg) ShuffleArray(word);
        originalPosition = targetPosition;
        clearColorCode = ColorUtility.ToHtmlStringRGB(clearColor);
        missColorCode = ColorUtility.ToHtmlStringRGB(missColor);
        text.text = "<color=#" + clearColorCode + "></color>" + word[clearWord].ToString();
    }

    void Update()
    {
        if (clearFlg) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //キャンセル
            hackManager.nowTypingFlg = false;
            hackManager.nowObj = null;
            Destroy(gameObject);
        }

        if (Input.anyKeyDown)
        {
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (code >= KeyCode.A && code <= KeyCode.Z && Input.GetKeyDown(code))
                {
                    if (code.ToString() == word[clearWord][i].ToString())
                    {
                        i++;

                        //色変更処理
                        string _text = "<color=#" + clearColorCode + ">";
                        for (int j = 0; j < i; j++) _text += word[clearWord][j].ToString();
                        _text += "</color>";
                        for (int j = i; j < word[clearWord].Length; j++) _text += word[clearWord][j].ToString();
                        text.text = _text;

                        //1ワードクリア処理
                        if (i == word[clearWord].Length)
                        {
                            i = 0;
                            if (clearWord == word.Length - 1)
                            {
                                //ゲームクリア処理
                                text.text = "GameClear";
                                clearFlg = true;
                                hackManager.nowTypingFlg = false;
                                unitHack.hacked = true;

                                if (hit.collider.gameObject.TryGetComponent<CameraController>(out CameraController cameraCon)) cameraCon.frameSR.sprite = cameraCon.frameSprite;
                                else if (hit.collider.gameObject.TryGetComponent<DoorController>(out DoorController doorCon))
                                {
                                    doorCon.leftFrameSR.sprite = doorCon.frameMateSprite;
                                    doorCon.rightFrameSR.sprite = doorCon.frameMateSprite;
                                }
                                else if (hit.collider.gameObject.TryGetComponent<TurretController>(out TurretController turretCon)) turretCon.frameSR.sprite = turretCon.frameSprite;
                                else if (hit.collider.gameObject.TryGetComponent<EnemyController>(out EnemyController enemyCon)) enemyCon.frameSR.sprite = enemyCon.frameSprite;
                                else if (hit.collider.gameObject.TryGetComponent<AlarmController>(out AlarmController alarmCon)) alarmCon.frameSR.sprite = alarmCon.frameSprite;
                                else if (hit.collider.gameObject.TryGetComponent<CleanerController>(out CleanerController cleanerCon)) cleanerCon.frameSR.sprite = cleanerCon.frameSprite;
                                else if (hit.collider.gameObject.TryGetComponent<DigestionController>(out DigestionController digestionCon)) digestionCon.frameSR.sprite = digestionCon.frameSprite;
                                else if (hit.collider.gameObject.TryGetComponent<ComputerController>(out ComputerController computerCon)) computerCon.frameSR.sprite = computerCon.frameSprite;
                                else if (hit.collider.gameObject.TryGetComponent<AirConditionerController>(out AirConditionerController ariConditionerCon)) ariConditionerCon.frameSR.sprite = ariConditionerCon.frameSprite;

                                //CoolHackUI生成
                                hackUI.hacked.SetActive(true);
                                hackManager.nowObj = null;
                                hackUI.typing.SetActive(false);
                            }
                            else
                            {
                                clearWord++;
                                text.text = "<color=#" + clearColorCode + "></color>" + word[clearWord].ToString();
                            }
                        }
                    }
                    else
                    {
                        //ミス処理
                        currentShakeDuration = shakeDuration;
                        timeManager.TypingMiss();
                    }
                }
            }
        }

        if (currentShakeDuration > 0 && !clearFlg)
        {
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * shakeAmount;
            text.transform.localPosition = originalPosition + randomOffset;

            string _text = "<color=#" + clearColorCode + ">";
            for (int j = 0; j < i; j++) _text += word[clearWord][j].ToString();
            _text = _text + "</color><color=#" + missColorCode + ">" + word[clearWord][i].ToString() + "</color>";
            for (int j = i + 1; j < word[clearWord].Length; j++) _text += word[clearWord][j].ToString();
            text.text = _text;

            currentShakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else if (!clearFlg)
        {
            currentShakeDuration = 0f;
            text.transform.localPosition = originalPosition;

            string _text = "<color=#" + clearColorCode + ">";
            for (int j = 0; j < i; j++) _text += word[clearWord][j].ToString();
            _text += "</color>";
            for (int j = i; j < word[clearWord].Length; j++) _text += word[clearWord][j].ToString();
            text.text = _text;
        }
    }

    void ShuffleArray(string[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            string value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }
}