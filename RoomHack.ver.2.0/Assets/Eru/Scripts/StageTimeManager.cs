using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageTimeManager : MonoBehaviour
{
    [SerializeField,Header("ステージの時間制限")]
    private int stageTime = 100;

    [SerializeField, Header("タイピングミスによる時間減少")]
    private int missTime = 5;

    [SerializeField, Header("デフォルトカラー")]
    private Color defaultColor = Color.white;

    [SerializeField, Header("ミスカラー")]
    private Color missColor = Color.red;

    [SerializeField, Header("振動時間")]
    private float shakeDuration = 0.15f;

    [SerializeField, Header("振動する力")]
    private float shakeAmount = 5f;

    [SerializeField, Header("減衰率")]
    private float decreaseFactor = 1.0f;

    private Vector3 originalPosition = new Vector3(125, -100, 0);

    private string defaultColorCode, missColorCode;

    private Text text;

    // GameManagerで取りたいのでpublic
    public float timer;

    private float currentShakeDuration = 0f;

    void Start()
    {
        timer = stageTime;
        text = GetComponentInChildren<Text>();
        text.color = defaultColor;
        originalPosition = text.transform.localPosition;
        defaultColorCode = ColorUtility.ToHtmlStringRGB(defaultColor);
        missColorCode = ColorUtility.ToHtmlStringRGB(missColor);
        text.text = "<color=#" + defaultColorCode + ">" + timer.ToString("F2") + "</color>";
    }

    void Update()
    {
        if (timer <= 0) TimeOver();
        else
        {
            timer -= Time.deltaTime;

            if (currentShakeDuration > 0)
            {
                Vector3 randomOffset = Random.insideUnitSphere * shakeAmount;
                text.transform.localPosition = originalPosition + randomOffset;

                text.text = "<color=#" + missColorCode + ">" + timer.ToString("F2") + "</color>";

                currentShakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                currentShakeDuration = 0f;
                text.transform.localPosition = originalPosition;

                if (timer <= stageTime / 4) text.text = "<color=#" + missColorCode + ">" + timer.ToString("F2") + "</color>";
                else text.text = "<color=#" + defaultColorCode + ">" + timer.ToString("F2") + "</color>";
            }
        }
    }

    public void TypingMiss()
    {
        timer -= missTime;
        currentShakeDuration = shakeDuration;
    }

    private void TimeOver()
    {
    //    text.text = "<color=#" + missColorCode + ">0</color>";
    //    GameOverSceneManager.GameOverNo = 2;
    //    SceneManager.LoadScene("GameOverScene");
    }
}
